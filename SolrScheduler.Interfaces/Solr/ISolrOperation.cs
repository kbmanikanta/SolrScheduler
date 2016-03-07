using RestSharp;

namespace SolrScheduler.Interfaces.Solr {
    /// <summary>
    /// Interface ISolrOperation
    /// </summary>
    public interface ISolrOperation {

        /// <summary>
        /// Gets the operation model.
        /// </summary>
        /// <value>The operation model.</value>
        ISolrOperationModel OperationModel { get; }

        /// <summary>
        /// Gets the rest client.
        /// </summary>
        /// <value>The rest client.</value>
        IRestClient RestClient { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value><c>true</c> if this instance is running; otherwise, <c>false</c>.</value>
        bool IsRunning { get; }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();
    }
}