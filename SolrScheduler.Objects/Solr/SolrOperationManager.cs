using System;
using SolrScheduler.Interfaces.Log;
using SolrScheduler.Interfaces.Solr;
using SolrScheduler.Objects.Log;

namespace SolrScheduler.Objects.Solr {
    /// <summary>
    /// Class SolrOperationManager.
    /// </summary>
    public sealed class SolrOperationManager : ISolrOperationManager {
        #region Member Variables

        /// <summary>
        /// The solr log
        /// </summary>
        public ISolrLog SolrLog { get; }

        /// <summary>
        /// The operation runner
        /// </summary>
        public ISolrOperationRunner OperationRunner { get; }
        
        #endregion

        #region Static Members / Singleton

        /// <summary>
        /// The lazyInstance
        /// </summary>
        private static readonly Lazy<SolrOperationManager> lazyInstance = new Lazy<SolrOperationManager>();

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static ISolrOperationManager Instance => lazyInstance.Value;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SolrOperationManager" /> class.
        /// </summary>
        public SolrOperationManager() : this(new SolrLog()) {}
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SolrOperationManager" /> class.
        /// </summary>
        /// <param name="operationLogger">The operation logger.</param>
        /// <param name="operationRunner">The operation runner.</param>
        public SolrOperationManager(ISolrLog operationLogger = null, ISolrOperationRunner operationRunner = null) {
            SolrLog = operationLogger ?? new SolrLog();

            if (OperationRunner == null)
                OperationRunner = operationRunner ?? new SolrOperationRunner(null, null, SolrLog);

            Reload();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reloads the specified operation logger.
        /// </summary>
        public void Reload() {
            OperationRunner?.ReloadAllOperations();
        }

        #endregion
    }
}