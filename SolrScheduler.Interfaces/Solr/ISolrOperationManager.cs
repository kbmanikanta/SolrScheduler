using SolrScheduler.Interfaces.Log;

namespace SolrScheduler.Interfaces.Solr {
    /// <summary>
    /// Interface ISolrOperationManager
    /// </summary>
    public interface ISolrOperationManager {
        /// <summary>
        /// The solr log
        /// </summary>
        ISolrLog SolrLog { get; }

        /// <summary>
        /// The operation runner
        /// </summary>
        ISolrOperationRunner OperationRunner { get; }

        /// <summary>
        /// Reloads this instance.
        /// </summary>
        void Reload();
    }
}