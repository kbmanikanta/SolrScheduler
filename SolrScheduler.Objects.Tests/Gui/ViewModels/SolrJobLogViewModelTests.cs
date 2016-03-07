using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolrScheduler.GUI.ViewModels;
using SolrScheduler.Interfaces.Solr;
using SolrScheduler.Objects.Log;
using SolrScheduler.Objects.Models;
using SolrScheduler.Objects.Solr;

namespace SolrScheduler.Objects.Tests.Gui.ViewModels
{
    [TestClass]
    public class SolrJobLogViewModelTests
    {
        [TestMethod]
        public void ViewModelLogOutputsCorrectValues()
        {
            SolrOperationManager operationManager = new SolrOperationManager(new SolrLog(null, false), new Mock<ISolrOperationRunner>().Object);
            SolrJobLogViewModel viewModel = new SolrJobLogViewModel(operationManager);

            Assert.AreEqual(string.Empty, viewModel.Log);

            operationManager.SolrLog.AppendSolrAction(DateTime.Now, DateTime.Now, "Test", new SolrResponseObject(new SolrResponseHeader(), new SolrResponseStatus()));

            Assert.AreNotEqual(string.Empty, viewModel.Log);
        }
    }
}
