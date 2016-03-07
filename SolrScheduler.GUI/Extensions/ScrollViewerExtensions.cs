using System;
using System.Windows;
using System.Windows.Controls;

namespace SolrScheduler.GUI.Extensions {

    /// <summary>
    /// Class ScrollViewerExtensions.
    /// </summary>
    public class ScrollViewerExtensions {

        #region Member Variables

        /// <summary>
        /// The always scroll to end property
        /// </summary>
        public static readonly DependencyProperty AlwaysScrollToEndProperty = DependencyProperty.RegisterAttached("AlwaysScrollToEnd",
            typeof (bool),
            typeof (ScrollViewerExtensions),
            new PropertyMetadata(false, AlwaysScrollToEndChanged));

        /// <summary>
        /// The auto scroll flag
        /// </summary>
        private static bool _autoScroll;

        #endregion

        #region Methods

        /// <summary>
        /// Gets the always scroll to end.
        /// </summary>
        /// <param name="scroll">The scroll.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static bool GetAlwaysScrollToEnd(ScrollViewer scroll) {
            if (scroll == null)
                throw new ArgumentNullException(nameof(scroll));
            return (bool) scroll.GetValue(AlwaysScrollToEndProperty);
        }

        /// <summary>
        /// Sets the always scroll to end.
        /// </summary>
        /// <param name="scroll">The scroll.</param>
        /// <param name="alwaysScrollToEnd">if set to <c>true</c> [always scroll to end].</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static void SetAlwaysScrollToEnd(ScrollViewer scroll, bool alwaysScrollToEnd) {
            if (scroll == null)
                throw new ArgumentNullException(nameof(scroll));
            scroll.SetValue(AlwaysScrollToEndProperty, alwaysScrollToEnd);
        }

        /// <summary>
        /// Scrolls the changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ScrollChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.InvalidOperationException">The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances.</exception>
        private static void ScrollChanged(object sender, ScrollChangedEventArgs e) {
            var scroll = sender as ScrollViewer;
            if (scroll == null)
                throw new InvalidOperationException("The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances.");

            // User scroll event : set or unset autoscroll mode
            if (e.ExtentHeightChange == 0)
                _autoScroll = scroll.VerticalOffset == scroll.ScrollableHeight;

            // Content scroll event : autoscroll eventually
            if (_autoScroll && e.ExtentHeightChange != 0)
                scroll.ScrollToVerticalOffset(scroll.ExtentHeight);
        }

        /// <summary>
        /// Alwayses the scroll to end changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.InvalidOperationException">The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances.</exception>
        private static void AlwaysScrollToEndChanged(object sender, DependencyPropertyChangedEventArgs e) {
            var scroll = sender as ScrollViewer;
            if (scroll == null)
                throw new InvalidOperationException("The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances.");

            var alwaysScrollToEnd = (e.NewValue != null) && (bool) e.NewValue;
            if (alwaysScrollToEnd) {
                scroll.ScrollToEnd();
                scroll.ScrollChanged += ScrollChanged;
            }
            else
                scroll.ScrollChanged -= ScrollChanged;
        }

        #endregion
    }
}