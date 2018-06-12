using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using SolrScheduler.Interfaces.Enumerations;
using SolrScheduler.Interfaces.Log;
using SolrScheduler.Interfaces.Models;
using SolrScheduler.Interfaces.Solr;
using SolrScheduler.Objects.Models;

namespace SolrScheduler.Objects.Solr {
    public class SolrOperation : ISolrOperation {

        #region Constants

        /// <summary>
        /// The sleep time for a busy response
        /// </summary>
        private const int RESPONSE_BUSY_SLEEP_TIME_IN_MS = 500;
        
        /// <summary>
        /// The sleep time for being outside the recurrence interval range
        /// </summary>
        private const int OUTSIDE_INTERVAL_SLEEP_TIME_IN_MS = 100;

        #endregion

        #region Member Variables

        /// <summary>
        /// The cancellation token
        /// </summary>
        private readonly CancellationTokenSource CancellationToken = new CancellationTokenSource();

        /// <summary>
        /// The solr log
        /// </summary>
        private readonly ISolrLog _solrLog;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the operation model.
        /// </summary>
        /// <value>The operation model.</value>
        public ISolrOperationModel OperationModel { get; }

        /// <summary>
        /// Gets or sets the rest client.
        /// </summary>
        /// <value>The rest client.</value>
        public IRestClient RestClient { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value><c>true</c> if this instance is running; otherwise, <c>false</c>.</value>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Inverse of IsRunning - primarily used to disable button in WPF grid
        /// </summary>
        /// <value><c>true</c> if this instance is stopped; otherwise, <c>true</c>.</value>
        public bool IsStopped
        {
            get
            {
                return !IsRunning;
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SolrOperation"/> class.
        /// </summary>
        /// <param name="operationModel">The operation model.</param>
        /// <param name="restClient">The rest client.</param>
        /// <param name="solrLog">The solr log.</param>
        public SolrOperation(ISolrOperationModel operationModel, IRestClient restClient, ISolrLog solrLog) {
            OperationModel = operationModel;
            RestClient = restClient;
            _solrLog = solrLog;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start() {
            if ((OperationModel == null) || (RestClient == null))
                return;

            if (IsRunning)
                return;

            if (!OperationModel.Enabled)
                return;

            IsRunning = true;
            Task.Factory.StartNew(RunTask);
            _solrLog.AppendJobAction(OperationModel.Name, SolrLogAction.Start);
        }

        private void RunTask() {
            ISolrResponseObject response = null;
            
            long lastResponseTime = DateTime.Now.Ticks;
            while (IsRunning) {
                //If the previous response was "busy", sleep for a teensy bit before retrying
                if ((response != null) && (response.Status == "busy"))
                    Thread.Sleep(RESPONSE_BUSY_SLEEP_TIME_IN_MS);

                //If we're not yet inside the recurrence interval, sleep for a teensy bit
                if (((DateTime.Now.Ticks - lastResponseTime) / TimeSpan.TicksPerMillisecond) < OperationModel.RecurrenceInterval) {
                    Thread.Sleep(OUTSIDE_INTERVAL_SLEEP_TIME_IN_MS);

                    //Set the response to null so we don't resleep
                    response = null;

                    //Restart the loop
                    continue;
                }

                //Execute and get the response
                response = GetSolrResponse(RestClient, OperationModel, out lastResponseTime);

                //If we don't recur, then exit after getting one response
                if (OperationModel.RecurrenceInterval == -1)
                    IsRunning = false;
            }

            _solrLog.AppendJobAction(OperationModel.Name, SolrLogAction.Stop);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop() {
            IsRunning = false;
        }

        /// <summary>
        /// Gets the solr response.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="solrOperationModel">The solr operationModel.</param>
        /// <param name="responseTimeInTicks">The response time in ticks.</param>
        /// <returns>SolrScheduler.Objects.Models.SolrResponseObject.</returns>
        private ISolrResponseObject GetSolrResponse(IRestClient client, ISolrOperationModel solrOperationModel, out long responseTimeInTicks) {
            //Create request
            IRestRequest request = CreateRequestFromSolrOperation(solrOperationModel);

            //Start the timer and execute the request
            DateTime startResponseTime = DateTime.Now;

            IRestResponse response = client.Execute(request);

            //Get the time it took for the response to resolve
            DateTime stopResponseTime = DateTime.Now;

            responseTimeInTicks = stopResponseTime.Ticks;

            //Return the deserialized response
            ISolrResponseObject solrResponse = JsonConvert.DeserializeObject<SolrResponseObject>(response.Content);

            //Log the action
            _solrLog.AppendSolrAction(startResponseTime, stopResponseTime, solrOperationModel.Name, solrResponse);

            return solrResponse;
        }

        /// <summary>
        /// Creates the request from solr operationModel.
        /// </summary>
        /// <param name="solrOperationModel">The solr operationModel.</param>
        /// <returns>RestSharp.IRestRequest.</returns>
        private IRestRequest CreateRequestFromSolrOperation(ISolrOperationModel solrOperationModel) {
            RestRequest request = new RestRequest("/dataimport", Method.GET);
            request.AddQueryParameter("wt", "json");
            request.AddQueryParameter("command", solrOperationModel.ImportType);

            if (solrOperationModel.Clean)
                request.AddQueryParameter("clean", "true");

            if (solrOperationModel.Commit)
                request.AddQueryParameter("commit", "true");

            if (solrOperationModel.Optimize)
                request.AddQueryParameter("optimize", "true");

            return request;
        }
    }
}