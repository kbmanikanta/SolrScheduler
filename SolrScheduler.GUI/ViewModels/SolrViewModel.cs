using System.ComponentModel;
using System.Runtime.CompilerServices;
using SolrScheduler.GUI.Annotations;
using SolrScheduler.Interfaces.Solr;
using SolrScheduler.Interfaces.ViewModels;
using SolrScheduler.Objects.Solr;

namespace SolrScheduler.GUI.ViewModels {
    /// <summary>
    ///     Class SolrViewModel.
    /// </summary>
    public abstract class SolrViewModel : ISolrViewModel {
        #region Protected Member Variables

        /// <summary>
        /// The operation manager
        /// </summary>
        protected ISolrOperationManager OperationManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SolrViewModel"/> class.
        /// </summary>
        protected SolrViewModel(ISolrOperationManager operationManager)
        {
            //Set the operation manager to either the singleton or the one passed in.
            //This is to facilitate a testing seam around a static entity.
            OperationManager = operationManager ?? SolrOperationManager.Instance;

            // ReSharper disable once VirtualMemberCallInContructor
            RegisterOneWayBindingListeners();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Registers the one way binding listeners.
        /// </summary>
        public abstract void RegisterOneWayBindingListeners();

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}