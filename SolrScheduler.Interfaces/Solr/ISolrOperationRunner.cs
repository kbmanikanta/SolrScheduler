using System;
using System.Collections.Generic;

namespace SolrScheduler.Interfaces.Solr {
    /// <summary>
    /// Interface ISolrOperationRunner
    /// </summary>
    public interface ISolrOperationRunner {

        #region Properties

        /// <summary>
        /// Gets the solr operations.
        /// </summary>
        /// <value>The solr operations.</value>
        IList<ISolrOperation> SolrOperations { get; }
        
        /// <summary>
        /// Occurs when [on job action].
        /// </summary>
        event Action<string> OnJobAction;

        #endregion

        #region Methods

        /// <summary>
        /// Starts the operationModel.
        /// </summary>
        /// <param name="operationName">The operationModel identifier.</param>
        void StartOperation(string operationName);

        /// <summary>
        /// Starts all operations.
        /// </summary>
        void StartAllOperations();

        /// <summary>
        /// Stops the operationModel.
        /// </summary>
        /// <param name="operationName">The operationModel identifier.</param>
        void StopOperation(string operationName);

        /// <summary>
        /// Stops all operations.
        /// </summary>
        void StopAllOperations();

        /// <summary>
        /// Reloads all operations.
        /// </summary>
        void ReloadAllOperations();

        #endregion
    }
}