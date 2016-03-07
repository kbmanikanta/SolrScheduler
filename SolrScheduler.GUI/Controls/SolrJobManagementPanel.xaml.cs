using System.Windows;
using System.Windows.Controls;
using SolrScheduler.GUI.ViewModels;
using SolrScheduler.Objects.Solr;

namespace SolrScheduler.GUI.Controls {
    /// <summary>
    ///     Interaction logic for GlobalActions.xaml
    /// </summary>
    public partial class SolrJobManagementPanel : UserControl {

        #region Member Variables

        /// <summary>
        /// The view model
        /// </summary>
        public readonly SolrJobManagementPanelViewModel ViewModel = new SolrJobManagementPanelViewModel(SolrOperationManager.Instance);

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SolrJobManagementPanel"/> class.
        /// </summary>
        public SolrJobManagementPanel() {
            InitializeComponent();

            DataContext = ViewModel;
        }

        #endregion

        #region Click Events

        /// <summary>
        /// Handles the Click event of the StartAllJobs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void StartAllJobs_Click(object sender, RoutedEventArgs e) {
            SolrOperationManager.Instance.OperationRunner.StartAllOperations();
            gridJobList.Items.Refresh();
        }

        /// <summary>
        /// Handles the Click event of the StopAllJobs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void StopAllJobs_Click(object sender, RoutedEventArgs e) {
            SolrOperationManager.Instance.OperationRunner.StopAllOperations();
            gridJobList.Items.Refresh();
        }

        /// <summary>
        /// Handles the Click event of the ReloadAllJobs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ReloadAllJobs_Click(object sender, RoutedEventArgs e) {
            SolrOperationManager.Instance.Reload();
            SolrOperationManager.Instance.SolrLog.AppendReload();

            gridJobList.Items.Refresh();
        }

        /// <summary>
        /// Handles the Click event of the StopJob control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void StopJob_Click(object sender, RoutedEventArgs e) {
            Button stopJobButton = (Button) sender;
            string operationName = stopJobButton.CommandParameter.ToString();

            SolrOperationManager.Instance.OperationRunner.StopOperation(operationName);
            gridJobList.Items.Refresh();
        }

        /// <summary>
        /// Handles the Click event of the StartJob control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void StartJob_Click(object sender, RoutedEventArgs e) {
            Button startJobButton = (Button) sender;
            string operationName = startJobButton.CommandParameter.ToString();

            SolrOperationManager.Instance.OperationRunner.StartOperation(operationName);
            gridJobList.Items.Refresh();
        }

        #endregion
    }
}