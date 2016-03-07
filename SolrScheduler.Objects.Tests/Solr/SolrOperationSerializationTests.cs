using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SolrScheduler.Interfaces.Solr;
using SolrScheduler.Objects.Solr;

namespace SolrScheduler.Objects.Tests.Solr {
    [TestClass]
    public class SolrOperationSerializationTests {

        [TestMethod]
        [ExpectedException(typeof (JsonSerializationException))]
        public void SolrOperationModelRequiredFieldsThrowsExceptionsWhenNotInFile() {
            string json = "{}";
            ISolrOperationModel operationModel = JsonConvert.DeserializeObject<SolrOperationModel>(json);
        }
    }
}