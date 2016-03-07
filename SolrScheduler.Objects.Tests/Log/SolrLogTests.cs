using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrScheduler.Interfaces;
using SolrScheduler.Interfaces.Log;
using SolrScheduler.Objects.Log;
using SolrScheduler.Objects.Models;

namespace SolrScheduler.Objects.Tests.Log {
    [TestClass]
    public class SolrLogTests {
        [TestMethod]
        public void GetFileNameReturnsCorrectString() {
            const string DATE_FORMATTING = "yyy-MM-dd";
            const string OPERATION_NAME = "mytestjob";
            string EXPECTED_FILENAME = $"{OPERATION_NAME}-{DateTime.Now.ToString(DATE_FORMATTING)}.txt";

            ISolrLog log = new SolrLog(new DirectoryInfo("/"));
            Assert.AreEqual(EXPECTED_FILENAME, log.GetFileName(OPERATION_NAME));
        }

        [TestMethod]
        public void WriteLogOutputsFile() {
            DirectoryInfo thisDirectory = new DirectoryInfo("./");
            string operationName = $"mytestjob-{DateTime.Now.Ticks}";

            ISolrLog log = new SolrLog(thisDirectory);

            log.WriteLog(operationName, "test");

            Assert.IsTrue(File.Exists(thisDirectory.FullName + log.GetFileName(operationName)));
            File.Delete(thisDirectory.FullName + log.GetFileName(operationName));
        }

        [TestMethod]
        public void ValueReturnsTheProperCollectionString() {
            DirectoryInfo thisDirectory = new DirectoryInfo("./");
            string operationName = $"mytestjob-{DateTime.Now.Ticks}";

            ISolrLog log = new SolrLog(thisDirectory, false);

            Assert.IsTrue(string.IsNullOrEmpty(log.Value));
            log.AppendSolrAction(DateTime.Now, DateTime.Now, operationName, new SolrResponseObject(null, null));
            Assert.IsFalse(string.IsNullOrEmpty(log.Value));
        }

        [TestMethod]
        public void ValuesReturnsTheProperCollectionStrings() {
            DirectoryInfo thisDirectory = new DirectoryInfo("./");
            string operationName = $"mytestjob-{DateTime.Now.Ticks}";

            ISolrLog log = new SolrLog(thisDirectory, false);

            Assert.IsTrue(string.IsNullOrEmpty(log.Value));
            log.AppendSolrAction(DateTime.Now, DateTime.Now, operationName, new SolrResponseObject(null, null));
            log.AppendSolrAction(DateTime.Now, DateTime.Now, operationName, new SolrResponseObject(null, null));
            Assert.AreEqual(2, log.Values.Count());
        }

        [TestMethod]
        public void AppendActionCallsReferralFunction() {
            DirectoryInfo thisDirectory = new DirectoryInfo("./");
            ISolrLog log = new SolrLog(thisDirectory, false);

            int i = 0;
            ((SolrLog) log).OnLogAction += (x) => i++;

            Assert.AreEqual(0, i);
            log.AppendSolrAction(DateTime.Now, DateTime.Now, "", new SolrResponseObject(null, null));
            Assert.AreEqual(1, i);
        }

        [TestMethod]
        public void AppendCancellationCallsReferralFunction() {
            DirectoryInfo thisDirectory = new DirectoryInfo("./");
            ISolrLog log = new SolrLog(thisDirectory, false);

            int i = 0;
            ((SolrLog) log).OnLogAction += (x) => i++;

            Assert.AreEqual(0, i);
            log.AppendJobAction("", Interfaces.Enumerations.SolrLogAction.Stop);
            Assert.AreEqual(1, i);
        }
    }
}