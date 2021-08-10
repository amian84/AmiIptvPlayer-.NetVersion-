
using AmiIptvPlayer.Tools;
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
using System.Windows.Forms;

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
        private UrlObject url;
        private bool needRefresh = false;
        private Dictionary<int, ChannelInfo> channelsInfo = new Dictionary<int, ChannelInfo>();
        private Dictionary<GrpInfo, List<ChannelInfo>> groupsInfo = new Dictionary<GrpInfo, List<ChannelInfo>>();
        public static Channels Get()
        {
            if (instance == null)
            {
                instance = new Channels();
                if (UrlLists.Get().Lists.Count>0)
                    instance.SetUrl(UrlLists.Get().Lists[UrlLists.Get().Selected]);
                else
                    instance.SetUrl(new UrlObject() { Name = "NONE", URL = "http://no_url" });
            }
            return instance;
        }

        public List<GrpInfo> GetGroups()
        {
            return new List<GrpInfo>(groupsInfo.Keys);
        }

        public List<ChannelInfo> GetChannelsByGroup(GrpInfo group)
        {
            return groupsInfo[group];
        }

        public static Channels LoadFromJSON()
        {
            if (instance == null)
            {
                instance = new Channels();
            }
            UrlLists urls = UrlLists.Get();
            using (StreamReader r = new StreamReader(Utils.CONF_PATH + "\\lists\\" + urls.Lists[urls.Selected].Name + "_cache.json"))
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
            SeenResumeChannels src = SeenResumeChannels.Get();
            foreach (ChannelInfo channel in channels)
            {
                channelsInfo.Add(channelNumber, channel);
                channel.seen = src.IsSeen(channel.Title);
                channel.currentPostion = src.GetCurrentPosition(channel.Title);
                channel.totalDuration = src.GetCurrentTotalDuration(channel.Title);
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
            Task<string> stats = Utils.GetAsync("http://amiansito.ddns.net:5087/stats?ctype=connected&app=net&chn=CONNECT");
           
        }

        public Dictionary<int, ChannelInfo> GetChannelsDic(){
            return channelsInfo;
        }

        public void RefreshList()
        {
            RefreshList(UrlLists.Get().Lists[UrlLists.Get().Selected]);
        }

        public void SetUrl(UrlObject url)
        {
            this.url = url;
        }

        public void RefreshList(UrlObject _url)
        {
            if (_url!=null && _url!= url)
            {
                url = _url;
            }
            string contents;
            try
            {
                using (var wc = new WebClient())
                {
                    contents = wc.DownloadString(_url.URL);
                }

                var parser = PlaylistParserFactory.GetPlaylistParser(".m3u");
                IBasePlaylist playlist = parser.GetFromString(contents);
                M3uPlaylist m3uList = (M3uPlaylist)playlist;
                channelsInfo.Clear();
                groupsInfo.Clear();
                int channelNumber = 0;
                SeenResumeChannels src = SeenResumeChannels.Get();
                foreach (M3uPlaylistEntry entry in m3uList.PlaylistEntries)
                {
                    if (entry.CustomProperties.Count > 0)
                    {
                        ChannelInfo channelInfo = new ChannelInfo(entry);
                        channelInfo.seen = src.IsSeen(channelInfo.Title);
                        channelInfo.currentPostion = src.GetCurrentPosition(channelInfo.Title);
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
                if (!Directory.Exists(Utils.CONF_PATH + "\\lists\\"))
                {
                    Directory.CreateDirectory(Utils.CONF_PATH + "\\lists\\");
                }
                if (File.Exists(Utils.CONF_PATH +  "\\lists\\" + _url.Name + "_cache.json"))
                {
                    File.Delete(Utils.CONF_PATH + "\\lists\\" + _url.Name + "_cache.json");
                }
                using (StreamWriter file = File.CreateText(Utils.CONF_PATH + "\\lists\\" + _url.Name + "_cache.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, channelsInfo.Values);
                }
                needRefresh = true;
                Task<string> stats = Utils.GetAsync("http://amiansito.ddns.net:5087/stats?ctype=connected&app=net&chn=CONNECT");

            } catch (Exception ex) {
                Logger.Current.Error("Some error occur downloading the list: " + ex.Message.ToString());
                MessageBox.Show(
                        "Error: " + ex.Message + ". URL=" + url.URL,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                needRefresh = false;
            }
            
            
            
        }
        public List<ChannelInfo> GetAllShowChannelsByGroupName(string name)
        {
            foreach(var grp in GetGroups())
            {
                if (grp.Show && grp.Title == name)
                {
                    return groupsInfo[grp];
                }
            }
            return new List<ChannelInfo>();
        }
        public void SetNeedRefresh(bool value)
        {
            this.needRefresh = value;
        }

        public bool NeedRefresh()
        {
            return this.needRefresh;
        }

        public void IsShowAndGetMoreEpisodes(ChannelInfo channelInfo)
        {
            if (channelInfo.ChannelType == ChType.SHOW)
            {
                var channels = GetAllShowChannelsByGroupName(channelInfo.TVGGroup);
                channels.Sort((x, y) => x.ChNumber.CompareTo(y.ChNumber));
                var isLast = channels.Find((x) => x.ChNumber > channelInfo.ChNumber) == null;
                channelInfo.hasNextEpisode = !isLast;
            }else
                channelInfo.hasNextEpisode = false;
        }
    }
}
