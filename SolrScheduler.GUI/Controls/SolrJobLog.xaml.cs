using System.Windows.Controls;
using SolrScheduler.GUI.ViewModels;
using SolrScheduler.Objects.Solr;

namespace SolrScheduler.GUI.Controls {
    /// <summary>
    ///     Interaction logic for SolrJobLog.xaml
    /// </summary>
    public partial class SolrJobLog : UserControl {
        #region Member Variables

        /// <summary>
        /// The view model
        /// </summary>
        public readonly SolrJobLogViewModel ViewModel = new SolrJobLogViewModel(SolrOperationManager.Instance);

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SolrJobLog"/> class.
        /// </summary>
        public SolrJobLog() {
            InitializeComponent();
            DataContext = ViewModel;
        }

        #endregion
    }
}