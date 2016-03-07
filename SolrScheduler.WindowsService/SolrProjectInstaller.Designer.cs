namespace SolrScheduler.WindowsService
{
    partial class SolrProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SolrProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SolrServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // SolrProcessInstaller
            // 
            this.SolrProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.SolrProcessInstaller.Password = null;
            this.SolrProcessInstaller.Username = null;
            // 
            // SolrServiceInstaller
            // 
            this.SolrServiceInstaller.DelayedAutoStart = true;
            this.SolrServiceInstaller.ServiceName = "Solr Scheduler";
            this.SolrServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // SolrProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SolrProcessInstaller,
            this.SolrServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SolrProcessInstaller;
        private System.ServiceProcess.ServiceInstaller SolrServiceInstaller;
    }
}