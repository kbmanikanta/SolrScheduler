using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrScheduler.GUI.Extensions;

namespace SolrScheduler.Objects.Tests.Gui.Extensions
{
    [TestClass]
    public class ScrollViewerExtensionTests
    {
        [TestMethod]
        public void GetAlwaysScrollToEndReturnsProperBooleanValueWhenTrue()
        {
            ScrollViewer viewer = new ScrollViewer();
            viewer.SetValue(ScrollViewerExtensions.AlwaysScrollToEndProperty, true);

            Assert.IsTrue(ScrollViewerExtensions.GetAlwaysScrollToEnd(viewer));
        }

        [TestMethod]
        public void GetAlwaysScrollToEndReturnsProperBooleanValueWhenFalse()
        {
            ScrollViewer viewer = new ScrollViewer();
            Assert.IsFalse(ScrollViewerExtensions.GetAlwaysScrollToEnd(viewer));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAlwaysScrollToEndThrowsExceptionWithNull()
        {
            ScrollViewerExtensions.GetAlwaysScrollToEnd(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetAlwaysScrollToEndThrowsExceptionWithNull()
        {
            ScrollViewerExtensions.SetAlwaysScrollToEnd(null, true);
        }

        [TestMethod]
        public void SetAlwaysScrollToEndSetsPropertlyToTrue()
        {
            ScrollViewer viewer = new ScrollViewer();
            Assert.IsFalse(ScrollViewerExtensions.GetAlwaysScrollToEnd(viewer));

            ScrollViewerExtensions.SetAlwaysScrollToEnd(viewer, true);

            Assert.IsTrue(ScrollViewerExtensions.GetAlwaysScrollToEnd(viewer));
        }
    }
}
