
using Newtonsoft.Json;
using PlaylistsNET.Content;
using PlaylistsNET.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AmiIptvPlayer
{
    public struct GrpInfo
    {
        public string Title { get; set; }
        public bool Show { get; set; }
        public override bool Equals(Object obj)
        {
            if (obj.GetType() != typeof(GrpInfo))
            {
                return false;
            }
            else
            {
                return this.Title == ((GrpInfo)obj).Title;
            }
        }

        public override int GetHashCode()
        {
            return 434131217 + EqualityComparer<string>.Default.GetHashCode(Title);
        }
    }

    public class Channels
    {
        private static Channels instance;
        private string url;
        private bool needRefresh = false;
        private Dictionary<int, ChannelInfo> channelsInfo = new Dictionary<int, ChannelInfo>();
        private Dictionary<GrpInfo, List<ChannelInfo>> groupsInfo = new Dictionary<GrpInfo, List<ChannelInfo>>();
        public static Channels Get()
        {
            if (instance == null)
            {
                instance = new Channels();
                
                instance.SetUrl(AmiConfiguration.Get().URL_IPTV);
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
                channel.ChNumber = channelNumber;
                GrpInfo groupInfo = new GrpInfo();
                groupInfo.Title = channel.TVGGroup;
                if (channel.ChannelType == ChType.UNKNOWN)
                {
                    channel.CalculateType();
                }
                groupInfo.Show = channel.ChannelType == ChType.SHOW;
                if (!groupsInfo.ContainsKey(groupInfo))
                {
                    groupsInfo[groupInfo] = new List<ChannelInfo>();
                }
                groupsInfo[groupInfo].Add(channel);
                channelNumber++;
            }
            Task<string> stats = Utils.GetAsync("http://amian.es:5085/stats?ctype=connected&app=net&chn=CONNECT");
           
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
                groupsInfo.Clear();
                int channelNumber = 0;
                foreach (M3uPlaylistEntry entry in m3uList.PlaylistEntries)
                {
                    if (entry.CustomProperties.Count > 0)
                    {
                        ChannelInfo channelInfo = new ChannelInfo(entry);
                        channelsInfo.Add(channelNumber, channelInfo);
                        channelInfo.ChNumber = channelNumber;
                        GrpInfo groupInfo = new GrpInfo();
                        groupInfo.Title = channelInfo.TVGGroup;
                        groupInfo.Show = channelInfo.ChannelType == ChType.SHOW;
                        if (!groupsInfo.ContainsKey(groupInfo))
                        {
                            groupsInfo[groupInfo] = new List<ChannelInfo>();
                        }
                        groupsInfo[groupInfo].Add(channelInfo);
                        channelNumber++;
                    }
                }
                if (File.Exists(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\channelCache.json"))
                {
                    File.Delete(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\channelCache.json");
                }
                using (StreamWriter file = File.CreateText(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\channelCache.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, channelsInfo.Values);
                }
                needRefresh = true;
                Task<string> stats = Utils.GetAsync("http://amian.es:5085/stats?ctype=connected&app=net&chn=CONNECT");

            } catch (Exception ex) {
                Console.WriteLine("Some error occur");
                needRefresh = false;
            }
            
            
            
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
