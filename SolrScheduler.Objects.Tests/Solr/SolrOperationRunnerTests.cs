using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using SolrScheduler.Interfaces.Log;
using SolrScheduler.Interfaces.Solr;
using SolrScheduler.Interfaces.Utility;
using SolrScheduler.Objects.Models;
using SolrScheduler.Objects.Solr;

namespace SolrScheduler.Objects.Tests.Solr {
    [TestClass]
    public class SolrOperationRunnerTests {
        private readonly Mock<ISolrLog> _log = new Mock<ISolrLog>();
        private readonly Mock<IRestResponse> _stubEmptyResponse = new Mock<IRestResponse>();
        private readonly Mock<IRestClient> _stubRestClient = new Mock<IRestClient>();
        private readonly Mock<IDirectoryInfoWrapper> _stubDirectory = new Mock<IDirectoryInfoWrapper>();

        [TestInitialize]
        public void Initialize() {
            _stubEmptyResponse.Setup(x => x.Content).Returns(
                JsonConvert.SerializeObject(new SolrResponseObject(
                    new SolrResponseHeader(),
                    new SolrResponseStatus()))
                );
            _stubRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(_stubEmptyResponse.Object);
            _stubDirectory.Setup(x => x.GetFileContentsFromDirectory()).Returns(new[] {
                "{\"name\": \"TestJob1\",\"server\": \"testserver\",\"core\": \"integration_core_dataviewLayout\",\"importType\": \"full-import\",\"enabled\": true,\"clean\": true,\"commit\": true,\"optimize\" :  true,\"recurrenceStart\": 635877792000000000,\"recurrenceInterval\": 0}",
                "{\"name\": \"TestJob2\",\"server\": \"testserver\",\"core\": \"integration_core_dataviewLayout\",\"importType\": \"full-import\",\"enabled\": true,\"clean\": true,\"commit\": true,\"optimize\" :  true,\"recurrenceStart\": 635877792000000000,\"recurrenceInterval\": 0}"
            });
        }

        [TestMethod]
        public void SolrOperationRunnerStartsASingularJobSuccessfully() {
            ISolrOperationRunner runner = new SolrOperationRunner(_stubDirectory.Object, _stubRestClient.Object, _log.Object);
            runner.StartOperation("TestJob1");

            Assert.AreEqual(1, runner.SolrOperations.Count(x => x.IsRunning));
        }

        [TestMethod]
        public void SolrOperationRunnerStopsASingularJobSuccessfully() {
            ISolrOperationRunner runner = new SolrOperationRunner(_stubDirectory.Object, _stubRestClient.Object, _log.Object);
            runner.StartOperation("TestJob1");
            runner.StopOperation("TestJob1");

            Assert.IsFalse(runner.SolrOperations.Any(x => x.IsRunning));
        }

        [TestMethod]
        public void SolrOperationRunnerStartsAllJobsSuccessfully() {
            ISolrOperationRunner runner = new SolrOperationRunner(_stubDirectory.Object, _stubRestClient.Object, _log.Object);
            runner.StartAllOperations();

            Assert.AreEqual(2, runner.SolrOperations.Count(x => x.IsRunning));
        }

        [TestMethod]
        public void SolrOperationRunnerCancelsAllJobsSuccessfully() {
            ISolrOperationRunner runner = new SolrOperationRunner(_stubDirectory.Object, _stubRestClient.Object, _log.Object);
            runner.StartAllOperations();
            runner.StopAllOperations();
            Assert.IsFalse(runner.SolrOperations.Any(x => x.IsRunning));
        }

        [TestMethod]
        public void SolrOperationRunnerReloadsAllJobsSuccessfully() {
            ISolrOperationRunner runner = new SolrOperationRunner(_stubDirectory.Object, _stubRestClient.Object, _log.Object);
            runner.StartAllOperations();

            //Get the reference
            ISolrOperation earlierOperation = runner.SolrOperations[0];

            //Reload
            runner.ReloadAllOperations();

            Assert.AreNotEqual(earlierOperation, runner.SolrOperations[0]);
        }
    }
}