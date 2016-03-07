using System.ServiceProcess;
using SolrScheduler.Objects.Solr;

namespace SolrScheduler.WindowsService
{
    public partial class SolrSchedulerService : ServiceBase
    {
        public SolrSchedulerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            SolrOperationManager.Instance.OperationRunner.StartAllOperations();
        }

        protected override void OnStop()
        {
            SolrOperationManager.Instance.OperationRunner.StopAllOperations();
        }
    }
}
