using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrScheduler.Interfaces.Utility;
using SolrScheduler.Objects.Utility;

namespace SolrScheduler.Objects.Tests.Utility {

    [TestClass]
    public class DirectoryInfoWrapperTests {

        [TestMethod]
        [DeploymentItem("test.json")]
        [DeploymentItem("test2.json")]
        public void DirectoryInfoReturnsCorrectFileCount() {
            IDirectoryInfoWrapper directoryWrapper = new DirectoryInfoWrapper(new System.IO.DirectoryInfo("./"));
            Assert.AreEqual(2, directoryWrapper.GetFileContentsFromDirectory().Count());
        }
    }
}