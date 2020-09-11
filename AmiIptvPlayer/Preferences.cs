using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AmiIptvPlayer
{
    public partial class Preferences : Form
    {
        public Configuration config = null;
        public Preferences()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            InitializeComponent();
        }

        private void Preferences_Load(object sender, EventArgs e)
        {
            txtURL.Text = config.AppSettings.Settings["Url"].Value;
            txtEPG.Text = config.AppSettings.Settings["Epg"].Value;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Ok();
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void Ok()
        {
            
            string lastList = config.AppSettings.Settings["Url"].Value;
            if (lastList != txtURL.Text)
            {
                config.AppSettings.Settings["Url"].Value = txtURL.Text;
                Channels channels = Channels.Get();
                channels.SetUrl(txtURL.Text);
                channels.SetNeedRefresh(true);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            
            this.Close();
            this.Dispose();
        }

        private void txtURL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Ok();
            }
        }

        private void txtEPG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Ok();
            }
        }
    }
}
