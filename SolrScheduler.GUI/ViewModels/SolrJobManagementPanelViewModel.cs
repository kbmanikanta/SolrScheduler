using System.Collections.Generic;
using SolrScheduler.Interfaces.Solr;

namespace SolrScheduler.GUI.ViewModels {
    /// <summary>
    /// Class GlobalActionsViewModel.
    /// </summary>
    public class SolrJobManagementPanelViewModel : SolrViewModel {
        /// <summary>
        /// Gets or sets the solr operations.
        /// </summary>
        /// <value>The solr operations.</value>
        public IList<ISolrOperation> SolrOperations => OperationManager.OperationRunner.SolrOperations;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SolrJobManagementPanelViewModel" /> class.
        /// </summary>
        public SolrJobManagementPanelViewModel() : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolrJobManagementPanelViewModel" /> class.
        /// </summary>
        /// <param name="operationManager">The operation manager.</param>
        public SolrJobManagementPanelViewModel(ISolrOperationManager operationManager = null) : base(operationManager) { }

        #endregion

        /// <summary>
        /// Registers the one way binding listeners.
        /// </summary>
        public override void RegisterOneWayBindingListeners() {
            OperationManager.OperationRunner.OnJobAction += OnPropertyChanged;
        }
    }
}