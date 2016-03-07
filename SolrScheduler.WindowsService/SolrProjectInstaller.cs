using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Threading.Tasks;

namespace SolrScheduler.WindowsService
{
    [RunInstaller(true)]
    public partial class SolrProjectInstaller : System.Configuration.Install.Installer
    {
        public SolrProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
