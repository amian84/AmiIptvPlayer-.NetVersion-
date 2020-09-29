using PlaylistsNET.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            string audioConf = config.AppSettings.Settings["audio"].Value;
            audio.SelectedItem = Utils.audios[audioConf];
            string subConf = config.AppSettings.Settings["sub"].Value;
            sub.SelectedItem = Utils.subs[subConf];

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
                ConfigurationManager.RefreshSection("appSettings");
            }

            string lastEPG = config.AppSettings.Settings["Epg"].Value;
            if (lastEPG != txtEPG.Text)
            {
                config.AppSettings.Settings["Epg"].Value = txtEPG.Text;
                EPG_DB epgDB = EPG_DB.Get();
                epgDB.Refresh = true;
                ConfigurationManager.RefreshSection("appSettings");
            }
            config.AppSettings.Settings["audio"].Value = Utils.GetAudioConfName(audio.SelectedItem.ToString());
            config.AppSettings.Settings["sub"].Value = Utils.GetSubConfName(sub.SelectedItem.ToString());
            config.Save(ConfigurationSaveMode.Modified);
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
