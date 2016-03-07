using SolrScheduler.Interfaces.Solr;
using SolrScheduler.Objects.Solr;

namespace SolrScheduler.GUI.ViewModels {
    /// <summary>
    /// Class SolrJobLogViewModel.
    /// </summary>
    public class SolrJobLogViewModel : SolrViewModel {
        /// <summary>
        /// Gets the log.
        /// </summary>
        /// <value>The log.</value>
        public string Log => string.Join("\r\n", OperationManager.SolrLog.Values);

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SolrJobLogViewModel" /> class.
        /// </summary>
        public SolrJobLogViewModel() : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolrJobLogViewModel" /> class.
        /// </summary>
        /// <param name="operationManager">The operation manager.</param>
        public SolrJobLogViewModel(ISolrOperationManager operationManager = null) : base(operationManager) { }

        #endregion
        /// <summary>
        /// Registers the one way binding listeners.
        /// </summary>
        public override void RegisterOneWayBindingListeners() {
            OperationManager.SolrLog.OnLogAction += OnPropertyChanged;
        }
    }
}