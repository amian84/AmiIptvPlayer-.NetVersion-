using AmiIptvPlayer.i18n;
using Newtonsoft.Json;
using PlaylistsNET.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace AmiIptvPlayer
{
    public partial class Preferences : Form
    {

        
        public Configuration config = null;
        public Form1 PrincipalForm { get; set; }
        
        public Preferences()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            InitializeComponent();
            
        }

        public void ReloadLang()
        {
            AmiConfiguration amiconf = AmiConfiguration.Get();
            cmbUI.Items.Clear();
            if (string.IsNullOrEmpty(amiconf.UI_LANG))
            {
                amiconf.UI_LANG = "SYSTEM";

            }
            cmbUI.Items.Add(Strings.LangSYS);
            cmbUI.Items.Add(Strings.LangSP);
            cmbUI.Items.Add(Strings.LangEN);
            cmbUI.SelectedIndex = amiconf.availableLangs[amiconf.UI_LANG];

            audio.Items.Clear();
            foreach(string item in Utils.audios.Values)
            {
                audio.Items.Add(item);
            }

            sub.Items.Clear();
            foreach (string item in Utils.subs.Values)
            {
                sub.Items.Add(item);
            }

            lbSettingsDefAud.Text = Strings.lbSettingsDefAud;
            lbSettingsDefSub.Text = Strings.lbSettingsDefSub;
            lbSettingsEPG.Text = Strings.lbSettingsEPG;
            lbSettingsUILang.Text = Strings.lbSettingsUILang;
            lbSettingsURL.Text = Strings.lbSettingsURL;
            this.Text = Strings.PreferencesTitle;
            btnCancel.Text = Strings.CANCEL;
            btnOk.Text = Strings.SAVE;
        }

        private void Preferences_Load(object sender, EventArgs e)
        {
            ReloadLang();
            AmiConfiguration amiconf = AmiConfiguration.Get();
            txtURL.Text = amiconf.URL_IPTV;
            txtEPG.Text = amiconf.URL_EPG;
            string audioConf = amiconf.DEF_LANG;
            audio.SelectedItem = Utils.audios[audioConf];
            string subConf = amiconf.DEF_SUB;
            sub.SelectedItem = Utils.subs[subConf];
            
        }

        

        private void btnOk_Click(object sender, EventArgs e)
        {
            Ok();
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Channels channels = Channels.Get();
            channels.SetNeedRefresh(false);
            this.Close();
            this.Dispose();
        }

        private void Ok()
        {
            AmiConfiguration amiconf = AmiConfiguration.Get();
            
            if (amiconf.URL_IPTV != txtURL.Text)
            {
                amiconf.URL_IPTV = txtURL.Text;
                Channels channels = Channels.Get();
                channels.SetUrl(txtURL.Text);
                channels.SetNeedRefresh(true);
            }
            else
            {
                Channels channels = Channels.Get();
                channels.SetNeedRefresh(false);
            }

            if (amiconf.URL_EPG != txtEPG.Text)
            {
                amiconf.URL_EPG = txtEPG.Text;
                EPG_DB epgDB = EPG_DB.Get();
                epgDB.Refresh = true;

            }
            amiconf.DEF_LANG = Utils.GetAudioConfName(audio.SelectedItem.ToString());
            amiconf.DEF_SUB = Utils.GetSubConfName(sub.SelectedItem.ToString());

            using (StreamWriter file = File.CreateText(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiIptvConf.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, amiconf);
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

        private void cmbUI_SelectedIndexChanged(object sender, EventArgs e)
        {
            AmiConfiguration amiConfg =  AmiConfiguration.Get();
            foreach (var item in amiConfg.availableLangs)
            {
                if (item.Value == cmbUI.SelectedIndex && amiConfg.UI_LANG != item.Key)
                {
                    amiConfg.UI_LANG = item.Key;
                    if (item.Key == "SYSTEM")
                    {
                        Strings.Culture = CultureInfo.InstalledUICulture;
                    }
                    else
                    {
                        Strings.Culture = new CultureInfo(AmiConfiguration.Get().UI_LANG);
                    }
                    this.Preferences_Load(null, null);
                    PrincipalForm.RepaintLabels();
                }
            }
        }
    }
}
