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
    public class SolrOperationManagerTests {
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
        public void OperationManagerLoadsRunnerAndJobs() {
            ISolrOperationRunner runner = new SolrOperationRunner(_stubDirectory.Object, _stubRestClient.Object, _log.Object);
            ISolrOperationManager manager = new SolrOperationManager(_log.Object, runner);

            Assert.IsTrue(manager.OperationRunner.SolrOperations.Count(x => x.IsRunning) > 0);
        }
    }
}