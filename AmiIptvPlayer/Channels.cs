
using Newtonsoft.Json;
using PlaylistsNET.Content;
using PlaylistsNET.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AmiIptvPlayer
{
    public class Channels
    {
        private static Channels instance;
        private string url;
        private bool needRefresh = false;
        private Dictionary<int, ChannelInfo> channelsInfo = new Dictionary<int, ChannelInfo>();
        public static Channels Get()
        {
            if (instance == null)
            {
                instance = new Channels();
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                instance.SetUrl(config.AppSettings.Settings["Url"].Value);
            }
            return instance;
        }

        public static Channels LoadFromJSON()
        {
            if (instance == null)
            {
                instance = new Channels();
            }
            using (StreamReader r = new StreamReader(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\channelCache.json"))
            {
                string json = r.ReadToEnd();
                List<ChannelInfo> items = JsonConvert.DeserializeObject<List<ChannelInfo>>(json);
                instance.FillFromListChannelInfo(items);
            }
            return instance;
        }

        public ChannelInfo GetChannel(int chNumber)
        {
            if (channelsInfo.ContainsKey(chNumber))
            {
                return channelsInfo[chNumber];
            }
            return null;
        }

        public void FillFromListChannelInfo(List<ChannelInfo> channels)
        {
            channelsInfo.Clear();
            int channelNumber = 0;
            foreach (ChannelInfo channel in channels)
            {
                channelsInfo.Add(channelNumber, channel);
                channelNumber++;
            }
            Task<string> stats = Utils.GetAsync("http://amian.es:5084/stats?ctype=connected&app=net");
           
        }

        public Dictionary<int, ChannelInfo> GetChannelsDic(){
            return channelsInfo;
        }

        public void RefreshList()
        {
            RefreshList("");
        }

        public void SetUrl(string url)
        {
            this.url = url;
        }

        public void RefreshList(string _url)
        {
            if (!string.IsNullOrEmpty(_url))
            {
                url = _url;
            }
            string contents;
            try
            {
                using (var wc = new WebClient())
                {
                    contents = wc.DownloadString(url);
                }

                var parser = PlaylistParserFactory.GetPlaylistParser(".m3u");
                IBasePlaylist playlist = parser.GetFromString(contents);
                M3uPlaylist m3uList = (M3uPlaylist)playlist;
                channelsInfo.Clear();
                int channelNumber = 0;
                foreach (M3uPlaylistEntry entry in m3uList.PlaylistEntries)
                {
                    ChannelInfo channelInfo = new ChannelInfo(entry);
                    channelsInfo.Add(channelNumber, channelInfo);
                    channelNumber++;

                }
                using (StreamWriter file = File.CreateText(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\channelCache.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, channelsInfo.Values);
                }
                Task<string> stats = Utils.GetAsync("http://amian.es:5084/stats?ctype=connected&app=net");

            } catch (Exception ex) {
                Console.WriteLine("Some error occur");
            }
            
            needRefresh = false;
            
        }
        public void SetNeedRefresh(bool value)
        {
            this.needRefresh = value;
        }

        public bool NeedRefresh()
        {
            return this.needRefresh;
        }


    }
}
