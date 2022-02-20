
using AmiIptvPlayer.i18n;
using AmiIptvPlayer.Tools;
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
using System.Windows.Forms;

namespace AmiIptvPlayer
{
    public struct ChannelListItem
    {
        public ChannelListItem(string text, int number)
        {
            Seen = false;
            Resume = false;
            Text = text;
            Number = number;

        }
        public bool Seen { get; set; }
        public bool Resume { get; set; }
        public string Text { get; set; }
        public int Number { get; set; }
    }

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
        private Dictionary<string, List<ChannelListItem>> lstListsChannels = new Dictionary<string, List<ChannelListItem>>();
        private static Channels instance;
        private UrlObject url;
        private bool needRefresh = false;
        private Dictionary<int, ChannelInfo> channelsInfo = new Dictionary<int, ChannelInfo>();
        private Dictionary<GrpInfo, List<ChannelInfo>> groupsInfo = new Dictionary<GrpInfo, List<ChannelInfo>>();

        #region events
        public delegate void FillGroups(List<string> groups);
        public event FillGroups OnFillGroups;
        public delegate void StartLoadAndProcessChannels();
        public event StartLoadAndProcessChannels OnStartLoadAndProcessChannels;
        public delegate void EndLoadAndProcessChannels();
        public event EndLoadAndProcessChannels OnEndLoadAndProcessChannels;

        public delegate void RefreshChannelsList(Dictionary<string, List<ChannelListItem>> lstChannels);
        public event RefreshChannelsList OnRefreshChannelsList;
        #endregion



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
            if (url.Name != "NONE")
                this.SetNeedRefresh(true);
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

        public void LoadChannels()
        {
            UrlLists urls = UrlLists.Get();
            
            bool withoutUrls = false;
            bool refreshChList = false;
            if (urls.Lists.Count > 0)
                SetUrl(urls.Lists[urls.Selected]);
            else
            {
                SetUrl(new UrlObject() { Name = "NONE", URL = "http://no_url" });
                withoutUrls = true;
            }
            if (!withoutUrls && File.Exists(Utils.CONF_PATH + "\\lists\\" + urls.Lists[urls.Selected].Name + "_cache.json"))
            {
                LoadFromJSON();
                FillChList();
                DateTime creationCacheChannel = File.GetLastWriteTimeUtc(Utils.CONF_PATH + "\\lists\\" + urls.Lists[urls.Selected].Name + "_cache.json");
                if (File.Exists(Utils.CONF_PATH + "\\lists\\" + urls.Lists[urls.Selected].Name + "_cache.json")
                    && creationCacheChannel.Day < DateTime.Now.Day - 1)
                {
                    refreshChList = true;
                }
            }
            else
            {
                
                var x = new ChannelListItem(Strings.DEFAULT_MSG_NO_LIST, 0);
                lstListsChannels[Strings.ALLGROUP].Add(x);
                if (urls.Lists.Count > 0 && !File.Exists(Utils.CONF_PATH + "\\lists\\" + urls.Lists[urls.Selected].Name + "_cache.json"))
                {
                    refreshChList = true;
                }
            }

            OnFillGroups(lstListsChannels.Keys.ToList());

            if (refreshChList)
            {
                try
                {
                    OnStartLoadAndProcessChannels();
                    RefreshList();
                    FillChList();
                    OnEndLoadAndProcessChannels();
                }catch (Exception ex)
                {
                    Logger.Current.Error($"ERROR refreshing channels: {ex.ToString()}");
                    OnEndLoadAndProcessChannels();
                }
            }
            OnRefreshChannelsList(lstListsChannels);

        }

        public void FillChList()
        {
            List<ChannelListItem> woGroup = new List<ChannelListItem>();
            lstListsChannels.Clear();
            lstListsChannels[Strings.ALLGROUP] = new List<ChannelListItem>();
            lstListsChannels[Strings.ALLGROUP].Clear();

            foreach (var elem in channelsInfo)
            {
                int chNumber = elem.Key;
                ChannelInfo channel = elem.Value;
                ChannelListItem chanItem = new ChannelListItem(channel.Title, channel.ChNumber);
                chanItem.Seen = channel.seen;
                chanItem.Resume = channel.currentPostion != null;
                lstListsChannels[Strings.ALLGROUP].Add(chanItem);
                string group = channel.TVGGroup;
                if (string.IsNullOrEmpty(group))
                {
                    woGroup.Add(chanItem);
                }
                else
                {
                    if (!lstListsChannels.ContainsKey(group))
                    {
                        lstListsChannels[group] = new List<ChannelListItem>();
                    }
                    lstListsChannels[group].Add(chanItem);
                }
            }
            if (woGroup.Count > 0)
            {
                lstListsChannels[Strings.WOGROUP] = woGroup;
            }
        }



    }

}
