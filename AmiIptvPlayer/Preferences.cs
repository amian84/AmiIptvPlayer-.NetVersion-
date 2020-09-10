using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public Preferences()
        {
            InitializeComponent();
        }

        private void Preferences_Load(object sender, EventArgs e)
        {
            txtURL.Text = System.Configuration.ConfigurationSettings.AppSettings.Get("Url");
            txtEPG.Text = System.Configuration.ConfigurationSettings.AppSettings.Get("Epg");
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
            string lastList = System.Configuration.ConfigurationSettings.AppSettings.Get("Url");
            if (lastList != txtURL.Text)
            {
                System.Configuration.ConfigurationSettings.AppSettings.Set("Url", txtURL.Text);
                Channels channels = Channels.Get();
                channels.SetUrl(txtURL.Text);
                channels.SetNeedRefresh(true);
            }
            System.Configuration.ConfigurationSettings.AppSettings.Set("Epg", txtEPG.Text);
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
