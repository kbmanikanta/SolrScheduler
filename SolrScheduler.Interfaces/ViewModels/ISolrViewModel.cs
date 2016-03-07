using System.ComponentModel;

namespace SolrScheduler.Interfaces.ViewModels {

    /// <summary>
    /// Interface ISolrViewModel
    /// </summary>
    public interface ISolrViewModel : INotifyPropertyChanged {

        /// <summary>
        /// Registers the one way binding listeners.
        /// </summary>
        void RegisterOneWayBindingListeners();
    }
}