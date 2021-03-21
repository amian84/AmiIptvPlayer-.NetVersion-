using AmiIptvPlayer.i18n;
using System;
using System.Deployment.Application;
using System.Reflection;
using System.Windows.Forms;

namespace AmiIptvPlayer
{
    public partial class AboutUs : Form
    {
        public AboutUs()
        {
            InitializeComponent();
        }

        private void AboutUs_Load(object sender, EventArgs e)
        {
            
            lbVersion.Text = ApplicationDeployment.IsNetworkDeployed
               ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
               : Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text = Strings.AboutUsTitle;
            label1.Text = Strings.AboutUs;
            label3.Text = Strings.lbI18N + ":";
            label4.Text = "Beltrán Rodríguez: " + Strings.LangCAT + ", " + Strings.LangFR + "\n" +
                "Amian: " + Strings.LangEN + ", " + Strings.LangSP;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("http://github.com");
        }

        
    }
}
