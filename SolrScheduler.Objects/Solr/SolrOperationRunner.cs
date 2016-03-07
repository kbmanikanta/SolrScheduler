using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using SolrScheduler.Interfaces.Log;
using SolrScheduler.Interfaces.Solr;
using SolrScheduler.Interfaces.Utility;
using SolrScheduler.Objects.Utility;

namespace SolrScheduler.Objects.Solr {
    public class SolrOperationRunner : ISolrOperationRunner {

        #region Private Variables

        private readonly IDirectoryInfoWrapper _directoryInfoWrapper;

        /// <summary>
        /// The rest client
        /// </summary>
        private readonly IRestClient _restClient;

        /// <summary>
        /// The solr log
        /// </summary>
        private readonly ISolrLog _log;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the solr operations.
        /// </summary>
        /// <value>The solr operations.</value>
        public IList<ISolrOperation> SolrOperations { get; } = new List<ISolrOperation>();

        /// <summary>
        /// Occurs when [on job action].
        /// </summary>
        public event Action<string> OnJobAction;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SolrOperationRunner" /> class.
        /// </summary>
        /// <param name="directoryInfoWrapper">The directory information wrapper.</param>
        /// <param name="restClient">The rest client.</param>
        /// <param name="log">The log.</param>
        public SolrOperationRunner(IDirectoryInfoWrapper directoryInfoWrapper = null, IRestClient restClient = null, ISolrLog log = null) {
            _directoryInfoWrapper = directoryInfoWrapper ?? new DirectoryInfoWrapper();
            _restClient = restClient;
            _log = log;

            LoadDeserializedOperationObjects(_restClient, _log);
        }

        #endregion

        /// <summary>
        /// Starts the operationModel.
        /// </summary>
        /// <param name="operationName">The operationModel identifier.</param>
        public void StartOperation(string operationName) {
            ISolrOperation operation = SolrOperations.FirstOrDefault(x => x.OperationModel.Name == operationName);
            operation?.Start();
            OnJobAction?.Invoke(null);
        }

        /// <summary>
        /// Starts all operations.
        /// </summary>
        public void StartAllOperations() {
            foreach (ISolrOperation operation in SolrOperations.Where(x => !x.IsRunning))
                operation.Start();
            OnJobAction?.Invoke(null);
        }

        /// <summary>
        /// Stops the operation.
        /// </summary>
        /// <param name="operationName">Name of the operation.</param>
        public void StopOperation(string operationName) {
            ISolrOperation operation = SolrOperations.FirstOrDefault(x => x.OperationModel.Name == operationName);
            operation?.Stop();
            OnJobAction?.Invoke(null);
        }

        /// <summary>
        /// Stops all operations.
        /// </summary>
        public void StopAllOperations() {
            foreach (ISolrOperation operation in SolrOperations)
                operation.Stop();
            OnJobAction?.Invoke(null);
        }

        /// <summary>
        /// Reloads this instance.
        /// </summary>
        public void ReloadAllOperations() {
            StopAllOperations();
            SolrOperations.Clear();
            LoadDeserializedOperationObjects(_restClient, _log);
            StartAllOperations();
        }

        #region Private Methods

        /// <summary>
        /// Loads the deserialized operationModel objects from the flat files.
        /// </summary>
        private void LoadDeserializedOperationObjects(IRestClient restClient = null, ISolrLog log = null) {
            IEnumerable<string> jobFiles = _directoryInfoWrapper.GetFileContentsFromDirectory();

            foreach (string jobFileText in jobFiles) {
                ISolrOperationModel operationModel = JsonConvert.DeserializeObject<SolrOperationModel>(jobFileText);
                IRestClient operationRestClient = restClient ?? GetSolrRestClient(operationModel);

                SolrOperations.Add(new SolrOperation(operationModel, operationRestClient, log));
            }
        }

        /// <summary>
        /// Gets the solr rest client.
        /// </summary>
        /// <param name="solrOperationModel">The solr operationModel.</param>
        /// <returns>RestSharp.IRestClient.</returns>
        private IRestClient GetSolrRestClient(ISolrOperationModel solrOperationModel) {
            return new RestClient($"http://{solrOperationModel.Server}/{solrOperationModel.Core}/");
        }

        #endregion
    }
}