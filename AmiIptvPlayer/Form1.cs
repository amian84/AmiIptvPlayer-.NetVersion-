
using AmiIptvPlayer.i18n;
using AmiIptvPlayer.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace AmiIptvPlayer
{

   
    public partial class Form1 : Form
    {
        private static Form1 _instance;
        private enum TrackType
        {
            AUDIO,
            SUB
        }

        private struct ChannelListItem
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

        
        private PrgInfo currentPrg = null;
        private Dictionary<string, List<ChannelListItem>> lstListsChannels = new Dictionary<string, List<ChannelListItem>>();
        private List<ChannelListItem> lstChannels = new List<ChannelListItem>();
        
        private JArray fillFilmResults = null;
        private JObject filmInfo = null;
        private ChannelInfo chnl;
        private ChType currentChType;
        private MPVPlayer playerForm;
        private bool isLoaded = false;
        private string selectedList;
        private double currPos = 0;
        private bool isDocked = true;
        private Point newPositionUnDocked = new Point(0, 0);
        private int currLang = -1;
        private int currSub = -1;
        private int dockFullScreen = -1;
        private string ALL_GROUP = Strings.ALLGROUP;
        private string EMPTY_GROUP = Strings.WOGROUP;
        
        public Form1()
        {
            InitializeComponent();
            chList.View = View.Details;
            logoEPG.WaitOnLoad = false;
            logoChannel.WaitOnLoad = false;
            _instance = this; 

        }
        public void RepaintLabels()
        {
            parentalControlToolStripMenuItem.Text = Strings.ParentalControl;
            FileToolStripMenuItem.Text = Strings.FileTool;
            settingsToolStripMenuItem.Text = Strings.Settings;
            refreshEPGToolStripMenuItem.Text = Strings.RefreshEPGTool;
            refreshListToolStripMenuItem.Text = Strings.RefreshListTool;
            quitToolStripMenuItem.Text = Strings.QUIT;
            helpToolStripMenuItem.Text = Strings.HELP;
            aboutToolStripMenuItem.Text = Strings.AboutUsTitle;
            toolsToolStripMenuItem.Text = Strings.Tools;
            requestNewVODToolStripMenuItem.Text = Strings.RequestNewVOD;
            btnClear.Text = Strings.CLEAR;
            btnFilter.Text = Strings.FILTER;
            btnFixId.Text = Strings.FixIdentTitle;
            btnURLInfo.Text = Strings.URLinfo;
            lbGroups.Text = Strings.GROUPS;
            lbFilterList.Text = Strings.FilterList;
            lbDescriptionTitle.Text = Strings.lbDescriptionTitle + ":";
            lbTitleTitle.Text = Strings.lbTitleTitle + ":";
            lbYearsTitle.Text = Strings.lbYearsTitle;
            lbVersionText.Text = Strings.Version;
            lbEPGLoaded.Text = Strings.EPGloaded;
            lbEndTimeTitle.Text = Strings.lbEndTimeTitle;
            lbStarsTitle.Text = Strings.lbStarsTitle;
            lbStartTimeTitle.Text = Strings.lbStartTimeTitle;
            lbList.Text = Strings.lbIptvLists + ":";
            chName.Text = Strings.lbTitleTitle + ":";
            donationToolStripMenuItem.Text = Strings.Donation;
            accountInfoToolStripMenuItem.Text = Strings.AccountInfoTitle;
            manageListToolStripMenuItem.Text = Strings.ManageLists;
            playerForm.ReloadLang();
            ALL_GROUP = Strings.ALLGROUP;
            RepaintEPGLoadedLabel();
            EMPTY_GROUP = Strings.WOGROUP;

            HistoryMenuItem1.Text = Strings.lbHistory;
            if (AmiConfiguration.Get().REMOVE_DONATE)
            {
                donateButton.Visible = false;
            }
            else
            {
                donateButton.Visible = true;
            }
        }

        private void RepaintEPGLoadedLabel()
        {
            lbProcessingEPG.Text = Strings.LOADING;
            if (EPG_DB.Get().Loaded == EPG_STATUS.Loaded)
            {
                lbProcessingEPG.Text = Strings.LOADED;
            }
            if (EPG_DB.Get().Loaded == EPG_STATUS.Error)
            {
                lbProcessingEPG.Text = Strings.ERROR;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IPTVConfiguration cfg = new IPTVConfiguration();
            cfg.MoveConf();
            playerForm = new MPVPlayer();
            playerForm.TopLevel = false;
            playerForm.FormBorderStyle = FormBorderStyle.None;
            playerForm.AutoScroll = true;
            playerForm.SetPrincipalForm(this);
            panel2.Controls.Add(playerForm);
            playerForm.Show();
            playerForm.SetDockIcon(true);
            lstListsChannels[ALL_GROUP] = new List<ChannelListItem>();
            selectedList = ALL_GROUP;
#if _PORTABLE
            lbVersion.Text = Utils.PORTABLE_VERSION;
            lbPortables.Visible = true;
#else
            lbVersion.Text = ApplicationDeployment.IsNetworkDeployed
               ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
          : Assembly.GetExecutingAssembly().GetName().Version.ToString();
#endif

            chList.FullRowSelect = true;
            ImageList imageList = new ImageList();
            var x = imageList.Images;
            
            imageList.Images.Add(Image.FromFile("./resources/images/seen2.png"));
            imageList.Images.Add(Image.FromFile("./resources/images/resume.png"));
            chList.SmallImageList= imageList;
            cfg.LoadAmiSettings();

            Logger logger = Logger.Current;
            logger.SetBasePath(Utils.CONF_PATH);
            RepaintLabels();
            cmbLists.SelectedIndexChanged -= new System.EventHandler(this.cmbLists_SelectedIndexChanged);
            FillIPTVLists();
            Utils.GetAccountInfo();
            cfg.LoadChannelSeen();
            cfg.LoadParentalControl();
            RefreshListView();
            LoadChannels();
            LoadEPG();
            cmbLists.SelectedIndexChanged += new System.EventHandler(this.cmbLists_SelectedIndexChanged);


        }

        private void FillIPTVLists()
        {
            UrlLists urls = UrlLists.Get();
            if (urls.Lists.Count > 0)
            {
                if (cmbLists.Items != null)
                    cmbLists.Items.Clear();

                foreach (var item in urls.Lists)
                {
                    cmbLists.Items.Add(item.Name);

                }
                if (urls.Selected >= urls.Lists.Count)
                {
                    urls.Selected = urls.Lists.Count-1;
                }
                cmbLists.SelectedIndex = urls.Selected;
            }
            
        }

        
        public void HideAutoPlay()
        {
            if (playerForm != null)
                playerForm.HideNextButton();
        }

       
        private void LoadEPG()
        {
            EPG_DB epg = EPG_DB.Get();
            epg.epgEventFinish += FinishLoadEpg;
            DefaultEpgLabels();
            logoEPG.Image = Image.FromFile("./resources/images/info.png");

            if (File.Exists(Utils.CONF_PATH + "amiiptvepgCache.json"))
            {
                epg = EPG_DB.LoadFromJSON();
            }

            DateTime creation = File.GetLastWriteTimeUtc(Utils.CONF_PATH + "amiiptvepgCache.json");
            if (File.Exists(Utils.CONF_PATH + "amiiptvepgCache.json")
                && creation.Day < DateTime.Now.Day - 1)
            {
                DownloadEPGFile(epg, AmiConfiguration.Get().URL_EPG);
            }
            else
            {
                if (File.Exists(Utils.CONF_PATH + "amiiptvepgCache.json"))
                {
                    epg = EPG_DB.LoadFromJSON();
                }
                else if (File.Exists(Utils.CONF_PATH + "amiiptvepg.xml"))
                {
                    lbProcessingEPG.Text = Strings.LOADING;
                    epg.ParseDB();
                }
                else
                {
                    DownloadEPGFile(epg, "http://bit.ly/AVappEPG");
                    AmiConfiguration amiConf = AmiConfiguration.Get();
                    amiConf.URL_EPG = "http://bit.ly/AVappEPG";
                    using (StreamWriter file = File.CreateText(Utils.CONF_PATH + "amiIptvConf.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, amiConf);
                    }
                }

            }
        }

        private void LoadChannels()
        {
            UrlLists urls = UrlLists.Get();
            Channels channels = Channels.Get();
            bool withoutUrls = false;
            bool refreshList = false;
            if (urls.Lists.Count>0)
                channels.SetUrl(urls.Lists[urls.Selected]);
            else
            {
                channels.SetUrl(new UrlObject() { Name = "NONE", URL = "http://no_url" });
                withoutUrls = true;
            }
            if (!withoutUrls && File.Exists(Utils.CONF_PATH + "\\lists\\" + urls.Lists[urls.Selected].Name + "_cache.json"))
            {
                channels = Channels.LoadFromJSON();
                fillChannelList();
                DateTime creationCacheChannel = File.GetLastWriteTimeUtc(Utils.CONF_PATH + "\\lists\\" + urls.Lists[urls.Selected].Name + "_cache.json");
                if (File.Exists(Utils.CONF_PATH + "\\lists\\" + urls.Lists[urls.Selected].Name + "_cache.json")
                    && creationCacheChannel.Day < DateTime.Now.Day - 1)
                {
                    refreshList = true;
                }
            }
            else
            {
                ChannelInfo ch = new ChannelInfo();
                ch.Title = Strings.DEFAULT_MSG_NO_LIST;
                ListViewItem i = new ListViewItem("0");
                i.SubItems.Add(Strings.DEFAULT_MSG_NO_LIST);
                chList.Invoke((System.Threading.ThreadStart)delegate
                {
                    chList.Items.Add(i);
                });
                var x = new ChannelListItem(ch.Title, ch.ChNumber);
                x.Seen = ch.seen;
                x.Resume = ch.currentPostion!=null;
                lstListsChannels[ALL_GROUP].Add(x);
                lstChannels.Add(x);
                if (urls.Lists.Count > 0 && !File.Exists(Utils.CONF_PATH + "\\lists\\" + urls.Lists[urls.Selected].Name + "_cache.json"))
                {
                    refreshList = true;
                }
            }

            FillGroups();

            if (refreshList){
                RefreshChList(true);
            }
        }

        private void FillGroups()
        {
            cmbGroups.Invoke((System.Threading.ThreadStart)delegate
            {
                cmbGroups.SelectedIndexChanged -= new System.EventHandler(this.cmbGroups_SelectedIndexChanged);
                cmbGroups.Items.Clear();
                foreach (string group in lstListsChannels.Keys)
                {
                    cmbGroups.Items.Add(group);

                }
                cmbGroups.SelectedIndex = 0;
                cmbGroups.SelectedIndexChanged += new System.EventHandler(this.cmbGroups_SelectedIndexChanged);
            });
        }

        private void FinishLoadEpg(EPG_DB epg, EPGEventArgs e)
        {
            if (e.Error)
            {
                lbProcessingEPG.Invoke((System.Threading.ThreadStart)delegate
                {
                    lbProcessingEPG.Text = Strings.ERROR;
                });
                Logger.Current.Error("Error processing EPG: " + e.Error.ToString());
            }
            else
            {
                lbProcessingEPG.Invoke((System.Threading.ThreadStart)delegate
                {
                    lbProcessingEPG.Text = Strings.LOADED;
                });
                Logger.Current.Info("EPG Loaded correct");
            }
            
        }

        private void DownloadEPGFile(EPG_DB epgDB, string url)
        {
            new System.Threading.Thread(delegate ()
            {
                
                try
                {
                    using (var client = new WebClient())
                    {

                        string tempFile = Path.GetTempFileName();
                        client.DownloadFile(url, tempFile);
                        if (File.Exists(Utils.CONF_PATH + "amiiptvepg.xml"))
                        {
                            File.Delete(Utils.CONF_PATH + "amiiptvepg.xml");
                        }
                        File.Move(tempFile, Utils.CONF_PATH + "amiiptvepg.xml");

                        epgDB.Refresh = false;

                        loadingPanel.Invoke((System.Threading.ThreadStart)delegate {
                            loadingPanel.Visible = false;
                            loadingPanel.Size = new Size(20, 20);
                        });
                        lbProcessingEPG.Invoke((System.Threading.ThreadStart)delegate
                        {
                            lbProcessingEPG.Text = Strings.LOADING;
                        });
                        epgDB.ParseDB();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error: " + ex.Message + ". URL=" + url,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    Logger.Current.Error($"Error: {ex.Message} .URL={url}");
                }

            }).Start();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            Channels channels = Channels.Get();
            if (chList.SelectedItems.Count > 0)
            {
                ListViewItem item = chList.SelectedItems[0];
                ChannelInfo channel = channels.GetChannel(int.Parse(item.SubItems[0].Text));
                ChangeChannelTo(channel, item.SubItems[0].Text);
            }
        }

        private void ChangeChannelTo(ChannelInfo channel, string number)
        {
            if (channel == null)
            {
                MessageBox.Show(Strings.NOT_FOUND_CH + number);
            }
            else
            {
                if (ParentalControl.Get().IsChBlock(channel))
                {
                    using (var askForm = new AskPass())
                    {
                        var result = askForm.ShowDialog();
                        if (result == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                }
                Logger.Current.Info($"[ChangeChannelTo] Change channel to {channel.TVGName}");
                playerForm.Stop();
                playerForm.SetIsChannel(channel.ChannelType == ChType.CHANNEL);
                playerForm.SetIsPaused(false);
                Thread.Sleep(500);
                currLang = -1;
                currSub = -1;
                int currPostion = 0;
                if (channel.currentPostion!=null && !channel.seen)
                {
                    
                    if (MessageBox.Show(owner:this, Strings.Resume, "AmiIptvPlayer", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        currPostion = (int)channel.currentPostion;
                    else
                    {
                        channel.currentPostion = null;
                        SeenResumeChannels.Get().RemoveResume(channel.Title);
                        RefreshListView();
                    }
                    
                }
                playerForm.SetMedia(channel.URL, currPostion, currLang, currSub);
                try
                {
                    string chName = channel.TVGName.Length < 100 ? channel.TVGName : channel.TVGName.Substring(0, 99);
                    Task<string> stats = Utils.GetAsync("http://amiansito.ddns.net:5087/stats?ctype=connected&app=net&chn=" + chName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR SENDING STATS");
                }

                logoChannel.LoadCompleted -= logoLoaded;

                logoChannel.Image = Image.FromFile("./resources/images/nochannel.png");
                if (!string.IsNullOrEmpty(channel.TVGLogo))
                {
                    logoChannel.LoadAsync(channel.TVGLogo);
                    logoChannel.LoadCompleted += logoLoaded;
                }

                string title = channel.Title;
                if (title.Length > 20)
                {
                    title = title.Substring(0, 20) + "...";
                }
                lbChName.Text = title;
                chnl = channel;
                currentChType = channel.ChannelType;
                SetEPG(channel);

            }
            playerForm.SetFocusOnVideoPanel();
        }

        private void SetEPG(ChannelInfo channel)
        {
            if (channel.ChannelType == ChType.CHANNEL)
            {
                VisibleEPGLabes(true);
                
                EPG_DB epg = EPG_DB.Get();
                if (epg.Loaded == EPG_STATUS.Loaded)
                {
                    PrgInfo prg = epg.GetCurrentProgramm(channel);
                    if (prg != null)
                    {
                        FillChannelInfo(prg);
                    }
                    else
                    {
                        DefaultEpgLabels();
                    }
                }
                else
                {
                    DefaultEpgLabels();
                }
            }
            else
            {
                DefaultEpgLabels();
                VisibleEPGLabes(false);
                dynamic result = Utils.GetFilmInfo(channel, "es");
                fillFilmResults = result["results"];
                JObject filmMatch = null;
                if (result["results"].Count > 0)
                {
                    filmMatch = result["results"][0];
                }
                filmInfo = filmMatch;
                DrawMovieInfo();
            }
        }

        public JObject GetFilmInfo()
        {
            return filmInfo;
        }

        public void DrawMovieInfo(JObject info = null)
        {
            if (info != null)
            {
                filmInfo = info;
            }
            if (filmInfo != null)
            {
                FillMoviesEPG(filmInfo, currentChType);
            }
        }

        private void FillMoviesEPG(JObject filmMatch, ChType channelType)
        {
            string title;
            string description;
            string stars;
            string year;
            if (channelType == ChType.MOVIE) {
                title = filmMatch["title"].ToString();
                description = filmMatch["overview"].ToString();
                stars = filmMatch["vote_average"].ToString();
                year = filmMatch["release_date"].ToString().Split('-')[0];
            }
            else
            {
                title = filmMatch["name"].ToString();
                description = filmMatch["overview"].ToString();
                stars = filmMatch["vote_average"].ToString();
                year = filmMatch["first_air_date"].ToString().Split('-')[0];
            }
            lbTitleEPG.Text = title;
            if (description.Length > 200 || description.Split('\n').Length > 3)
            {
                description = description.Substring(0, 190) + " ...";
            }
            lbDescription.Text = description;
            lbStars.Text = stars;
            lbYear.Text = year;
        }

        private void VisibleEPGLabes(bool visible)
        {
            lbStartTimeTitle.Visible = visible;
            lbStartTime.Visible = visible;
            lbEndTime.Visible = visible;
            lbEndTimeTitle.Visible = visible;
            btnFixId.Visible = !visible;
            lbYearsTitle.Visible = !visible;
            lbYear.Visible = !visible;
        }

        private void FillChannelInfo(PrgInfo prg)
        {
            lbTitleEPG.Text = prg.Title;
            string description = prg.Description;
            if (description.Length > 200 || description.Split('\n').Length > 3)
            {
                description = description.Substring(0, 190) + " ...";
            }
            lbDescription.Text = description;
            lbStars.Text = prg.Stars;
            lbStartTime.Text = prg.StartTime.ToShortTimeString();
            lbEndTime.Text = prg.StopTime.ToShortTimeString();
            
            currentPrg = prg;
        }

        private void DefaultEpgLabels()
        {
            lbTitleEPG.Text = "-";
            lbDescription.Text = "-";
            lbEndTime.Text = "-";
            lbStars.Text = "-";
            lbStartTime.Text = "-";
            lbYear.Text = "-";
            currentPrg = null;
        }

        private void logoLoaded(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error!=null)
            {
                logoChannel.Image = Image.FromFile("./resources/images/nochannel.png");
            }
        }

        private void logoEPGLoaded(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                logoEPG.Image = Image.FromFile("./resources/images/nochannel.png");
            }
        }

        public ChannelInfo GetCurrentChannel()
        {
            return chnl;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Preferences pref = new Preferences();
            pref.PrincipalForm = this;
            
            pref.ShowDialog();
            
            EPG_DB epgDB = EPG_DB.Get();
            if (epgDB.Refresh)
            {
                loadingPanel.Visible = true;
                loadingPanel.Size = this.Size;
                loadingPanel.BringToFront();
                DownloadEPGFile(epgDB, AmiConfiguration.Get().URL_EPG);
            }
        }
        
        private void fillChannelList()
        {
            FillChList();
            chList.Invoke((System.Threading.ThreadStart)delegate {
                chList.Items.Clear();
                chList.Items.AddRange(lstListsChannels[ALL_GROUP].Select(c => {
                    var x = new ListViewItem(new string[] { c.Number.ToString(), c.Text });
                    if (c.Seen)
                        x.ImageIndex = 0;
                    else if (c.Resume)
                        x.ImageIndex = 1;
                    return x;
                }).ToArray());

            });
        }

        private void FillChList()
        {
            Channels channels = Channels.Get();
            List<ChannelListItem> woGroup = new List<ChannelListItem>();
            lstListsChannels.Clear();
            lstListsChannels[ALL_GROUP] = new List<ChannelListItem>();
            lstListsChannels[ALL_GROUP].Clear();
            lstChannels.Clear();
            foreach (var elem in channels.GetChannelsDic())
            {
                int chNumber = elem.Key;
                ChannelInfo channel = elem.Value;
                ChannelListItem chanItem = new ChannelListItem(channel.Title, channel.ChNumber);
                chanItem.Seen = channel.seen;
                chanItem.Resume = channel.currentPostion != null;
                lstChannels.Add(chanItem);
                lstListsChannels[ALL_GROUP].Add(chanItem);
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
                lstListsChannels[EMPTY_GROUP] = woGroup;
            }
        }

        public void RefreshListView()
        {
            FillChList();
            chList.Invoke((System.Threading.ThreadStart)delegate
            {
                txtLoadCh.Invoke((System.Threading.ThreadStart)delegate
                {
                    txtLoadCh.Text = Strings.LOADING_CHANNELS;
                    txtLoadCh.BringToFront();
                    txtLoadCh.Visible = true;
                });
                foreach (ListViewItem item in chList.Items)
                {
                    item.ImageIndex = -1;
                    var channel = Channels.Get().GetChannel(int.Parse(item.SubItems[0].Text));
                    if (channel.seen)
                    {
                        item.ImageIndex = 0;
                    }
                    else if (channel.currentPostion != null)
                    {
                        item.ImageIndex = 1;
                    }
                    
                }
                txtLoadCh.Invoke((System.Threading.ThreadStart)delegate
                {
                    txtLoadCh.Text = Strings.LOADING_CHANNELS;
                    txtLoadCh.Visible = false;
                });
            });
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUs ab = new AboutUs();
            ab.ShowDialog();

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            new System.Threading.Thread(delegate ()
            {
                txtLoadCh.Invoke((System.Threading.ThreadStart)delegate
                {
                    txtLoadCh.Text = Strings.LOADING_CHANNELS;
                    txtLoadCh.BringToFront();
                    txtLoadCh.Visible = true;
                });

                chList.Invoke((System.Threading.ThreadStart)delegate
                {
                    chList.BeginUpdate();
                    chList.Items.Clear(); // clear list items before adding 
                    chList.EndUpdate();
                });
                List<ListViewItem> listCh = new List<ListViewItem>() ;
                foreach (ChannelListItem item in lstListsChannels[selectedList])
                {
                    if (item.Text.ToLower().Contains(txtFilter.Text.ToLower()))
                    {
                        var chItem = new ListViewItem(new string[] { item.Number.ToString(), item.Text });
                        if (item.Seen)
                            chItem.ImageIndex = 0;
                        else if (item.Resume)
                            chItem.ImageIndex = 1;
                        listCh.Add(chItem);
                    }
                }
                chList.Invoke((System.Threading.ThreadStart)delegate
                {
                    chList.BeginUpdate();
                    if (listCh.Count < 1)
                    {
                        ChannelInfo ch = new ChannelInfo();
                        listCh.Add(new ListViewItem(new string[] { "0", Strings.NOT_FOUND }));
                    }
                    chList.Items.AddRange(listCh.ToArray());
                    chList.EndUpdate();
                });
                txtLoadCh.Invoke((System.Threading.ThreadStart)delegate
                {
                    txtLoadCh.Text = Strings.LOADING_CHANNELS;
                    txtLoadCh.Visible = false;
                });
            }).Start();
        }

        
        private void btnClear_Click_1(object sender, EventArgs e)
        {
            new System.Threading.Thread(delegate ()
            {
                txtLoadCh.Invoke((System.Threading.ThreadStart)delegate
                {
                    txtLoadCh.Text = Strings.LOADING_CHANNELS;
                    txtLoadCh.BringToFront();
                    txtLoadCh.Visible = true;
                });
                 chList.Invoke((System.Threading.ThreadStart)delegate
                {
                    chList.BeginUpdate();
                    chList.Items.Clear(); // clear list items before adding 
                    chList.EndUpdate();
                });
                ListViewItem[] list = lstListsChannels[selectedList].Select(c => new ListViewItem(new string[] { c.Number.ToString(), c.Text })).ToArray();
               
                chList.Invoke((System.Threading.ThreadStart)delegate
                {
                    chList.BeginUpdate();
                    chList.Items.AddRange(list);
                    chList.EndUpdate();
                });
                txtFilter.Invoke((System.Threading.ThreadStart)delegate
                {
                    txtFilter.Clear();
                });
                txtLoadCh.Invoke((System.Threading.ThreadStart)delegate
                {
                    txtLoadCh.Text = Strings.LOADING_CHANNELS;
                    txtLoadCh.Visible = false;
                });
            }).Start();
        }

        private void refreshListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshChList(true);
        }

        private void RefreshChList(bool showLoading)
        {
            Channels channels = Channels.Get();
            if (showLoading)
            {
                loadingPanel.Invoke((System.Threading.ThreadStart)delegate {
                    loadingPanel.Size = this.Size;
                    loadingPanel.Visible = true;
                    loadingPanel.BringToFront();
                });
                
            }
            new System.Threading.Thread(delegate ()
            {
                channels.RefreshList();
                if (channels.NeedRefresh())
                {
                    fillChannelList();
                    cmbGroups.Invoke((System.Threading.ThreadStart)delegate
                    {
                        cmbGroups.Items.Clear();
                        foreach (string group in lstListsChannels.Keys)
                        {
                            cmbGroups.Items.Add(group);
                        }
                    });
                }
                loadingPanel.Invoke((System.Threading.ThreadStart)delegate {
                    loadingPanel.Visible = false;
                });

            }).Start();
        }

        private void SaveSeen()
        {
            if (File.Exists(Utils.CONF_PATH + "amiIptvChannelSeen.json"))
            {
                File.Delete(Utils.CONF_PATH + "amiIptvChannelSeen.json");
            }
               
            SeenResumeChannels channels = SeenResumeChannels.Get();
            foreach (var ch in Channels.Get().GetChannelsDic().Values)
            {
                if (ch.ChannelType == ChType.MOVIE|| ch.ChannelType == ChType.SHOW )
                {
                    if (ch.seen)
                    {
                        if (!SeenResumeChannels.Get().IsSeen(ch.Title)){
                            SeenResumeChannels.Get().UpdateOrSetSeen(ch.Title, true, ch.totalDuration == null ? -1 : (double)ch.totalDuration, DateTime.Now);
                        }
                        
                    }
                    else
                    {
                        if (ch.currentPostion != null)
                        {
                            if (!SeenResumeChannels.Get().IsResume(ch.Title))
                            {
                                SeenResumeChannels.Get().UpdateOrSetResume(ch.Title, (double)ch.currentPostion, ch.totalDuration == null ? -1 : (double)ch.totalDuration, DateTime.Now);
                            }
                        }
                    }
                }
            }
            using (StreamWriter file = File.CreateText(Utils.CONF_PATH + "amiIptvChannelSeen.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, channels);
            }
         
        }
       
        private void exit()
        {
            Logger.Current.Info($"[Exit] Exit APP");
            SaveSeen();
            playerForm.ExitApp(true);
            playerForm.Stop();
            EPG_DB epg = EPG_DB.Get();
            epg.epgEventFinish -= FinishLoadEpg;
            epg.Stop();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit();
            
        }

        
        private void logoEPG_Click(object sender, EventArgs e)
        {   
            if (currentPrg != null)
            {
                LongDescription lDescriptionForm = new LongDescription();
                lDescriptionForm.SetData(currentPrg);
                lDescriptionForm.ShowDialog();
            }
            else
            {
                if (chnl!= null && (currentChType == ChType.MOVIE || currentChType == ChType.SHOW))
                {
                    if (filmInfo != null)
                    {
                        LongDescription lDescriptionForm = new LongDescription();
                        lDescriptionForm.FillMovieData();
                        lDescriptionForm.ShowDialog();
                    }
                    
                }
            }
        }
        
        private void logoEPG_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.logoEPG, Strings.MORE_DETAILS);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnFilter_Click(btnFilter, null);
            }
        }

        private void refreshEPGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            DownloadEPGFile(EPG_DB.Get(), AmiConfiguration.Get().URL_EPG);
        }
        public static Form1 Get()
        {
            if (_instance == null)
            {
                _instance = new Form1();
            }
            return _instance;
        }
        private void btnFixId_Click(object sender, EventArgs e)
        {
            List<SearchIdent> listSearch = Utils.TransformJArrayToSearchIdent(fillFilmResults, GetCurrentChannel().ChannelType);
            FixIdent fixident = new FixIdent();
            fixident.SetSearch(listSearch);
            fixident.SetSearchText(Utils.LastSearch);
            fixident.ShowDialog();
        }

        

        private void btnURLInfo_Click(object sender, EventArgs e)
        {
            if (chnl != null)
            {
                URLInfo uRLInfo = new URLInfo();
                uRLInfo.setURL(chnl.URL);
                uRLInfo.StartPosition = FormStartPosition.CenterParent;
                uRLInfo.ShowDialog();
            }
        }
        public void UnDockPlayer()
        {
            Logger.Current.Info($"[UnDockPlayer] Undock player");
            Rectangle oldBound = playerForm.Bounds;
            Rectangle formBound = this.Bounds;
            newPositionUnDocked = new Point();
            newPositionUnDocked = formBound.Location;
            newPositionUnDocked.X = newPositionUnDocked.X + (formBound.Width / 2) - (oldBound.Width / 2);
            newPositionUnDocked.Y = newPositionUnDocked.Y + (formBound.Height / 2) - (oldBound.Height / 2);
            isLoaded = playerForm.IsMediaLoaded();
            currPos = playerForm.GetPosition();
            isDocked = false;
            playerForm.SetDockedEvent(true);
            if (playerForm.IsMediaLoaded())
            {
                currLang = playerForm.GetLangValue();
                currSub = playerForm.GetSubValue();
                playerForm.Stop();
            }
            else
            {
                ShowAgainPlayer();
            }
            
        }

        public void DockFullScreen(bool isFullScreen)
        {
            dockFullScreen = isFullScreen ? 1 : 0;
            if (isFullScreen)
                DockPlayer();
            else
                UnDockPlayer();
        }

        public void ShowAgainPlayer()
        {
            playerForm.ExitApp(true);
            playerForm .Disposed += PlayerForm_Disposed;
            playerForm.Dispose();
            
            
        }

        private void PlayerForm_Disposed(object sender, EventArgs e)
        {
            playerForm.UnloadPlayerEvents();
            playerForm.Close();
            playerForm = null;
            playerForm = new MPVPlayer();
            if (isDocked)
            {
                playerForm.TopLevel = false;
                playerForm.SetPrincipalForm(this);
                playerForm.FormBorderStyle = FormBorderStyle.None;
                playerForm.AutoScroll = true;
                panel2.Controls.Add(playerForm);
            }
            playerForm.SetDocked(isDocked);
            playerForm.SetPrincipalForm(this);
            playerForm.SetIsChannel(currentChType == ChType.CHANNEL);

            if (isLoaded)
            {
                playerForm.SetMedia(chnl.URL, Convert.ToInt32(currPos), currLang, currSub);

            }
            playerForm.Show();
            if (!isDocked)
                playerForm.Location = newPositionUnDocked;
            playerForm.SetDockedEvent(false);
            playerForm.SetDockIcon(isDocked);
            if (dockFullScreen > -1)
            {
                playerForm.GoFullscreen(dockFullScreen == 0 ? true : false);
                playerForm.SetFullScreenAttr(dockFullScreen == 0 ? true : false);
                playerForm.SetDocked(true);
                dockFullScreen = -1;
            }
        }

        public void DockPlayer()
        {
            Logger.Current.Info($"[DockPlayer] Dock player");
            isLoaded = playerForm.IsMediaLoaded();
            currPos = playerForm.GetPosition();
            isDocked = true;
            playerForm.SetDockedEvent(true);
            if (playerForm.IsMediaLoaded())
            {
                currLang = playerForm.GetLangValue();
                currSub = playerForm.GetSubValue();
                playerForm.Stop();
            }
            else
            {
                ShowAgainPlayer();
            }
            
        }

        private void cmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = ((ComboBox)sender).SelectedItem.ToString();
            new System.Threading.Thread(delegate ()
            {
                txtLoadCh.Invoke((System.Threading.ThreadStart)delegate
                {
                    txtLoadCh.Text = Strings.LOADING_CHANNELS;
                    txtLoadCh.BringToFront();
                    txtLoadCh.Visible = true;
                });

                chList.Invoke((System.Threading.ThreadStart)delegate
                {
                    chList.Items.Clear();
                    if (string.IsNullOrEmpty(selected))
                    {
                        selected = ALL_GROUP;
                    }
                    chList.Items.AddRange(lstListsChannels[selected].Select(c =>
                    {
                        var x = new ListViewItem(new string[] { c.Number.ToString(), c.Text });
                        if (c.Seen)
                            x.ImageIndex = 0;
                        else if (c.Resume)
                            x.ImageIndex = 1;
                        return x;
                    }
                    ).ToArray());
                });
                selectedList = selected;
                txtLoadCh.Invoke((System.Threading.ThreadStart)delegate
                {
                    txtLoadCh.Text = Strings.LOADING_CHANNELS;
                    txtLoadCh.Visible = false;
                });
            }).Start();
        }

        private void accountInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountInfo acc = new AccountInfo();
            acc.ShowDialog();
        }

        public void NextChannel()
        {
            ChannelInfo channel = Channels.Get().GetChannel(chnl.ChNumber + 1);
            ChangeChannelTo(channel, (chnl.ChNumber + 1).ToString());
        }

        public void ChannelToNumber(int number)
        {
            ChannelInfo channel = Channels.Get().GetChannel(number);
            ChangeChannelTo(channel, (number).ToString());
        }

        public void PrevChannel()
        {
            ChannelInfo channel = Channels.Get().GetChannel(chnl.ChNumber - 1);
            ChangeChannelTo(channel, (chnl.ChNumber - 1).ToString());
        }

        private void chList_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void chList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var channel = Channels.Get().GetChannel(int.Parse(chList.SelectedItems[0].SubItems[0].Text));
                if (channel.ChannelType == ChType.MOVIE || channel.ChannelType == ChType.SHOW)
                {
                    if (SeenResumeChannels.Get().IsSeen(chList.SelectedItems[0].SubItems[1].Text))
                    {
                        menuItemChnSeen.Text = Strings.UnSeen;
                    }
                    else
                    {
                        menuItemChnSeen.Text = Strings.Seen;
                    }
                    if (SeenResumeChannels.Get().IsResume(chList.SelectedItems[0].SubItems[1].Text))
                    {
                        menuItemChnResum.Text = Strings.UnResume;
                        menuItemChnResum.Visible = true;
                    }
                    else
                    {
                        menuItemChnResum.Visible = false;
                    }
                    contextMenuChannel.Show(Cursor.Position);
                }
            }
        }

        private void menuItemChnResum_Click(object sender, EventArgs e)
        {
            var channel = Channels.Get().GetChannel(int.Parse(chList.SelectedItems[0].SubItems[0].Text));
            channel.currentPostion = null;
            SeenResumeChannels.Get().RemoveResume(channel.Title);
            RefreshListView();
        }

        private void menuItemChnSeen_Click(object sender, EventArgs e)
        {
            var channel = Channels.Get().GetChannel(int.Parse(chList.SelectedItems[0].SubItems[0].Text));
            if (SeenResumeChannels.Get().IsSeen(chList.SelectedItems[0].SubItems[1].Text))
            {
                channel.seen = false;
                SeenResumeChannels.Get().RemoveSeen(channel.Title);
            }
            else
            {
                channel.seen = true;
                SeenResumeChannels.Get().UpdateOrSetSeen(channel.Title, true, channel.totalDuration==null?-1:(double)channel.totalDuration, DateTime.Now);
            }
            RefreshListView();
        }

        private void requestNewVODToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RequestVOD rvod = new RequestVOD();
            rvod.ShowDialog();
        }

        private void parentalControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(AmiConfiguration.Get().PARENTAL_PASS))
            {
                MessageBox.Show(Strings.FillParentalPass, Strings.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                using (var askForm = new AskPass())
                {
                    var result = askForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        ParentalControlForm pcontrol = new ParentalControlForm();
                        pcontrol.ShowDialog();
                    }
                }
                
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            History his = new History();
            his.ShowDialog();
        }

        private void donationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Donation don = new Donation();
            don.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            donationToolStripMenuItem_Click(sender, e);
        }

        private void manageListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageLists mgList = new ManageLists();
            mgList.ShowDialog();
            if (UrlLists.Get().Refresh)
            {
                cmbLists.SelectedIndexChanged -= new System.EventHandler(this.cmbLists_SelectedIndexChanged);
                FillIPTVLists();
                UrlLists.Get().Refresh = false;
                cmbLists.SelectedIndexChanged += new System.EventHandler(this.cmbLists_SelectedIndexChanged);
            }
        }

        private void cmbLists_SelectedIndexChanged(object sender, EventArgs e)
        {

            int selected = ((ComboBox)sender).SelectedIndex;
            UrlLists.Get().Selected = selected;
            loadingPanel.Visible = true;

            loadingPanel.Size = this.Size;
            loadingPanel.BringToFront();
            new System.Threading.Thread(delegate ()
            {
                LoadChannels();
                loadingPanel.Invoke((System.Threading.ThreadStart)delegate {
                    loadingPanel.Visible = false;
                    loadingPanel.Size = new Size(20, 20);
                });

            }).Start();
            
        }
    }
}
