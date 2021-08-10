using AmiIptvPlayer.i18n;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmiIptvPlayer
{
    public class IPTVConfiguration
    {

        public void MoveConf()
        {
            if (!Directory.Exists(Utils.CONF_PATH))
            {
                Directory.CreateDirectory(Utils.CONF_PATH);
                Utils.MoveFile(Utils.CONF_PATH_OLD + "amiIptvChannelSeen.json", Utils.CONF_PATH + "amiIptvChannelSeen.json");
                Utils.MoveFile(Utils.CONF_PATH_OLD + "channelCache.json", Utils.CONF_PATH + "channelCache.json");
                Utils.MoveFile(Utils.CONF_PATH_OLD + "amiIptvConf.json", Utils.CONF_PATH + "amiIptvConf.json");
                Utils.MoveFile(Utils.CONF_PATH_OLD + "amiiptvepg.xml", Utils.CONF_PATH + "amiiptvepg.xml");
                Utils.MoveFile(Utils.CONF_PATH_OLD + "amiiptvepgCache.json", Utils.CONF_PATH + "amiiptvepgCache.json");

            }
        }

        public void LoadAmiSettings()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (!File.Exists(Utils.CONF_PATH + "amiIptvConf.json"))
            {
                MessageBox.Show("Please check your configuration and save again to use new way to store the configuration.", "Possible wrong settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AmiConfiguration amiConf = AmiConfiguration.Get();
                amiConf.DEF_LANG = config.AppSettings.Settings["audio"].Value;
                amiConf.DEF_SUB = config.AppSettings.Settings["sub"].Value;
                amiConf.URL_IPTV = "NEW_VERSION";
                amiConf.URL_EPG = config.AppSettings.Settings["Epg"].Value;
                amiConf.ENABLE_LOG = false;
                amiConf.AUTOPLAY_EPISODES = false;
                using (StreamWriter file = File.CreateText(Utils.CONF_PATH + "amiIptvConf.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, amiConf);
                }
            }
            else
            {
                bool saveAgain = false;
                using (StreamReader r = new StreamReader(Utils.CONF_PATH + "amiIptvConf.json"))
                {
                    string json = r.ReadToEnd();
                    AmiConfiguration item = JsonConvert.DeserializeObject<AmiConfiguration>(json);
                    AmiConfiguration.SetInstance(item);

                    if (string.IsNullOrEmpty(item.UI_LANG) || item.UI_LANG == "SYSTEM")
                    {
                        Strings.Culture = CultureInfo.InstalledUICulture;
                    }
                    else
                    {
                        Strings.Culture = new CultureInfo(item.UI_LANG);
                    }
                    if (item.URL_IPTV != "NEW_VERSION")
                    {
                        //Old version we need move the url to the new version
                        using (StreamWriter listJson = new StreamWriter(Utils.CONF_PATH + "amiIptvConf_lists.json"))
                        {
                            UrlLists urlList = UrlLists.Get();
                            UrlObject url = new UrlObject();
                            url.URL = item.URL_IPTV;
                            url.Name = Strings.Main;
                            url.LogoList = "";
                            urlList.Lists = new List<UrlObject>() { url };
                            urlList.Selected = 0;
                            listJson.Write(JsonConvert.SerializeObject(urlList));
                        }
                        item.URL_IPTV = "NEW_VERSION";
                        saveAgain = true;

                    }
                    else
                    {
                        if (File.Exists(Utils.CONF_PATH + "amiIptvConf_lists.json"))
                        {
                            using (StreamReader listJson = new StreamReader(Utils.CONF_PATH + "amiIptvConf_lists.json"))
                            {
                                string jsonStr = listJson.ReadToEnd();
                                UrlLists desUrls = JsonConvert.DeserializeObject<UrlLists>(jsonStr);
                                UrlLists.SetInstance(desUrls);
                            }
                        }
                        else
                        {
                            UrlLists urlLists = UrlLists.Get();
                            urlLists.Lists = new List<UrlObject>();
                            urlLists.Selected = 0;
                        }

                    }

                }
                if (saveAgain)
                {
                    using (StreamWriter file = File.CreateText(Utils.CONF_PATH + "amiIptvConf.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, AmiConfiguration.Get());
                    }
                }
            }
        }

        public void LoadChannelSeen()
        {
            if (File.Exists(Utils.CONF_PATH + "amiIptvChannelSeen.json"))
            {
                using (StreamReader r = new StreamReader(Utils.CONF_PATH + "amiIptvChannelSeen.json"))
                {
                    string json = r.ReadToEnd();
                    SeenResumeChannels items = JsonConvert.DeserializeObject<SeenResumeChannels>(json);
                    SeenResumeChannels.Get().Set(items);
                }
            }
        }

        public void LoadParentalControl()
        {
            if (File.Exists(Utils.CONF_PATH + "amiIptvParentalControl.json"))
            {
                using (StreamReader r = new StreamReader(Utils.CONF_PATH + "amiIptvParentalControl.json"))
                {
                    string json = r.ReadToEnd();
                    List<ChannelInfo> blockChannels = JsonConvert.DeserializeObject<List<ChannelInfo>>(json);
                    foreach (var ch in blockChannels)
                    {
                        ParentalControl.Get().AddBlockList(ch);
                    }
                }
            }
        }


    }
}
