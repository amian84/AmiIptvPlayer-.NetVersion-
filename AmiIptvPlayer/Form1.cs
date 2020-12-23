
using Mpv.NET.Player;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
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
                Text = text;
                Number = number;
            }

            public string Text { get; set; }
            public int Number { get; set; }
        }

        
        private PrgInfo currentPrg = null;
        private Dictionary<string, List<ChannelListItem>> lstListsChannels = new Dictionary<string, List<ChannelListItem>>();
        private List<ChannelListItem> lstChannels = new List<ChannelListItem>();
        
        private JArray fillFilmResults = null;
        private JObject filmInfo = null;
        private ChannelInfo currentChannel;
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
        private static string ALL_GROUP = "All";
        private static string EMPTY_GROUP = "Without group";
        public Form1()
        {
            InitializeComponent();
            chList.View = View.Details;
            logoEPG.WaitOnLoad = false;
            logoChannel.WaitOnLoad = false;
            _instance = this; 

        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

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
            lbVersion.Text = ApplicationDeployment.IsNetworkDeployed
               ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
               : Assembly.GetExecutingAssembly().GetName().Version.ToString();

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            chList.FullRowSelect = true;
            if (!File.Exists(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiIptvConf.json"))
            {
                MessageBox.Show("Please check your configuration and save again to use new way to store the configuration.", "Possible wrong settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AmiConfiguration amiConf = AmiConfiguration.Get();
                amiConf.DEF_LANG = config.AppSettings.Settings["audio"].Value;
                amiConf.DEF_SUB = config.AppSettings.Settings["sub"].Value;
                amiConf.URL_IPTV = config.AppSettings.Settings["Url"].Value;
                amiConf.URL_EPG = config.AppSettings.Settings["Epg"].Value;
                using (StreamWriter file = File.CreateText(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiIptvConf.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, amiConf);
                }
            }
            else
            {
                using (StreamReader r = new StreamReader(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiIptvConf.json"))
                {
                    string json = r.ReadToEnd();
                    AmiConfiguration item = JsonConvert.DeserializeObject<AmiConfiguration>(json);
                    AmiConfiguration.SetInstance(item);
                }
            }
            EPG_DB epg = EPG_DB.Get();
            epg.epgEventFinish += FinishLoadEpg;
            DefaultEpgLabels();
            logoEPG.Image = Image.FromFile("./resources/images/info.png");
            Channels channels = Channels.Get();
            channels.SetUrl(AmiConfiguration.Get().URL_IPTV);
            if (File.Exists(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\channelCache.json"))
            {
                channels = Channels.LoadFromJSON();
                fillChannelList();
            }
            else
            {
                ChannelInfo ch = new ChannelInfo();
                ch.Title = "Please load iptv list";
                ListViewItem i = new ListViewItem("0");
                i.SubItems.Add("Please load iptv list");
                chList.Items.Add(i);
                lstListsChannels[ALL_GROUP].Add(new ChannelListItem(ch.Title, ch.ChNumber));
                lstChannels.Add(new ChannelListItem(ch.Title, ch.ChNumber));
            }

            cmbGroups.Items.Clear();
            
            foreach(string group in lstListsChannels.Keys)
            {
                cmbGroups.Items.Add(group);

            }
            cmbGroups.SelectedIndex = 0;

            DateTime creationCacheChannel = File.GetLastWriteTimeUtc(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\channelCache.json");
            if (File.Exists(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\channelCache.json")
                && creationCacheChannel.Day < DateTime.Now.Day - 1)
            {
                RefreshChList(false);
            }


            if (File.Exists(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiiptvepgCache.json"))
            {
                epg = EPG_DB.LoadFromJSON();
            }

            DateTime creation = File.GetLastWriteTimeUtc(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiiptvepgCache.json");
            if (File.Exists(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiiptvepgCache.json")
                && creation.Day < DateTime.Now.Day - 1)
            {
                //DownloadEPGFile(epg, config.AppSettings.Settings["Epg"].Value);
                
            }
            else
            {
                if (File.Exists(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiiptvepgCache.json"))
                {
                    epg = EPG_DB.LoadFromJSON();
                }
                else if (File.Exists(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiiptvepg.xml"))
                {
                    lbProcessingEPG.Text = "Loading...";
                    epg.ParseDB();
                }
                else
                {
                    DownloadEPGFile(epg, "http://bit.ly/AVappEPG");
                    AmiConfiguration amiConf = AmiConfiguration.Get();
                    amiConf.URL_EPG = "http://bit.ly/AVappEPG";
                    using (StreamWriter file = File.CreateText(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiIptvConf.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, amiConf);
                    }
                }

            }
           
        }

        private void FinishLoadEpg(EPG_DB epg, EPGEventArgs e)
        {
            if (e.Error)
            {
                MessageBox.Show("Error processing EPG, please check your url", "EPG ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lbProcessingEPG.Invoke((System.Threading.ThreadStart)delegate
                {
                    lbProcessingEPG.Text = "Error";
                });
            }
            else
            {
                lbProcessingEPG.Invoke((System.Threading.ThreadStart)delegate
                {
                    lbProcessingEPG.Text = "Loaded";
                });
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
                        if (File.Exists(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiiptvepg.xml"))
                        {
                            File.Delete(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiiptvepg.xml");
                        }
                        File.Move(tempFile, System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiiptvepg.xml");

                        epgDB.Refresh = false;

                        loadingPanel.Invoke((System.Threading.ThreadStart)delegate {
                            loadingPanel.Visible = false;
                            loadingPanel.Size = new Size(20, 20);
                        });
                        lbProcessingEPG.Invoke((System.Threading.ThreadStart)delegate
                        {
                            lbProcessingEPG.Text = "Loading...";
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
                if (channel == null)
                {
                    MessageBox.Show(item.SubItems[1].Text);
                }
                else
                {
                    playerForm.Stop();
                    playerForm.SetIsChannel(channel.ChannelType == ChType.CHANNEL);
                    playerForm.SetIsPaused(false);
                    Thread.Sleep(500);
                    currLang = -1;
                    currSub = -1;
                    playerForm.SetMedia(channel.URL, 0, currLang, currSub);
                    try
                    {
                        string chName = channel.TVGName.Length < 100 ? channel.TVGName : channel.TVGName.Substring(0, 99);
                        Task<string> stats = Utils.GetAsync("http://amian.es:5085/stats?ctype=connected&app=net&chn=" + chName);
                    } catch (Exception ex)
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
                    currentChannel = channel;
                    currentChType = channel.ChannelType;
                    SetEPG(channel);
                    
                }
            }
        }

        private void SetEPG(ChannelInfo channel)
        {
            if (channel.ChannelType == ChType.CHANNEL)
            {
                VisibleEPGLabes(true);
                
                EPG_DB epg = EPG_DB.Get();
                if (epg.Loaded)
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
            label6.Visible = visible;
            lbStartTime.Visible = visible;
            lbEndTime.Visible = visible;
            label8.Visible = visible;
            btnFixId.Visible = !visible;
            label9.Visible = !visible;
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
            return currentChannel;
        }

        
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Preferences pref = new Preferences();
            pref.ShowDialog();
            Channels channels = Channels.Get();
            if (channels.NeedRefresh())
            {
                loadingPanel.Visible = true;
                
                loadingPanel.Size = this.Size;
                loadingPanel.BringToFront();
                new System.Threading.Thread(delegate ()
                {
                    channels.RefreshList();
                    fillChannelList();
                    cmbGroups.Invoke((System.Threading.ThreadStart)delegate
                    {
                        cmbGroups.Items.Clear();
                        foreach (string group in lstListsChannels.Keys)
                        {
                            cmbGroups.Items.Add(group);
                        }
                    }); 
                    loadingPanel.Invoke((System.Threading.ThreadStart)delegate {
                        loadingPanel.Visible = false;
                        loadingPanel.Size = new Size(20, 20);
                    });
                    
                }).Start();
                
            }
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
                lstChannels.Add(new ChannelListItem(channel.Title, channel.ChNumber));
                lstListsChannels[ALL_GROUP].Add(new ChannelListItem(channel.Title, channel.ChNumber));
                string group = channel.TVGGroup;
                if (string.IsNullOrEmpty(group))
                {
                    woGroup.Add(new ChannelListItem(channel.Title, channel.ChNumber));
                }
                else
                {
                    if (!lstListsChannels.ContainsKey(group))
                    {
                        lstListsChannels[group] = new List<ChannelListItem>();
                    }
                    lstListsChannels[group].Add(new ChannelListItem(channel.Title, channel.ChNumber));
                }
            }
            if (woGroup.Count > 0)
            {
                lstListsChannels[EMPTY_GROUP] = woGroup;
            }
            chList.Invoke((System.Threading.ThreadStart)delegate {
                chList.Items.Clear();
                chList.Items.AddRange(lstListsChannels[ALL_GROUP].Select(c =>  new ListViewItem(new string[] { c.Number.ToString(), c.Text })).ToArray());
            });
        }


        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1_FormClosing(this, null);
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
                    txtLoadCh.Text = "Laoding channels... please wait a moment";
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
                        listCh.Add(new ListViewItem(new string[] { item.Number.ToString(), item.Text }));
                    }
                }
                chList.Invoke((System.Threading.ThreadStart)delegate
                {
                    chList.BeginUpdate();
                    if (listCh.Count < 1)
                    {
                        ChannelInfo ch = new ChannelInfo();
                        listCh.Add(new ListViewItem(new string[] { "0", "Not found" }));
                    }
                    chList.Items.AddRange(listCh.ToArray());
                    chList.EndUpdate();
                });
                txtLoadCh.Invoke((System.Threading.ThreadStart)delegate
                {
                    txtLoadCh.Text = "Laoding channels... please wait a moment";
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
                    txtLoadCh.Text = "Laoding channels... please wait a moment";
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
                    txtLoadCh.Text = "Laoding channels... please wait a moment";
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
                loadingPanel.Size = this.Size;
                loadingPanel.Visible = true;
                loadingPanel.BringToFront();
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

       
        private void exit()
        {
            playerForm.ExitApp(true);
            playerForm.Stop();
            EPG_DB epg = EPG_DB.Get();
            epg.epgEventFinish -= FinishLoadEpg;
            
            System.Windows.Forms.Application.Exit();
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
                if (currentChannel!= null && (currentChType == ChType.MOVIE || currentChType == ChType.SHOW))
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
            tt.SetToolTip(this.logoEPG, "Click for more details");
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
            List<SearchIdent> listSearch = Utils.TransformJArrayToSearchIdent(fillFilmResults);
            FixIdent fixident = new FixIdent();
            fixident.SetSearch(listSearch);
            fixident.SetSearchText(Utils.LastSearch);
            fixident.ShowDialog();
        }

        

        private void btnURLInfo_Click(object sender, EventArgs e)
        {
            if (currentChannel != null)
            {
                URLInfo uRLInfo = new URLInfo();
                uRLInfo.setURL(currentChannel.URL);
                uRLInfo.StartPosition = FormStartPosition.CenterParent;
                uRLInfo.ShowDialog();
            }
        }
        public void UnDockPlayer()
        {

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
            }
            playerForm.Stop();
            
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
                playerForm.SetMedia(currentChannel.URL, Convert.ToInt32(currPos), currLang, currSub);

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
            isLoaded = playerForm.IsMediaLoaded();
            currPos = playerForm.GetPosition();
            isDocked = true;
            playerForm.SetDockedEvent(true);
            if (playerForm.IsMediaLoaded())
            {
                currLang = playerForm.GetLangValue();
                currSub = playerForm.GetSubValue();
            }
            playerForm.Stop();
            
        }

        private void cmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = ((ComboBox)sender).SelectedItem.ToString();
            new System.Threading.Thread(delegate ()
            {
                txtLoadCh.Invoke((System.Threading.ThreadStart)delegate
                {
                    txtLoadCh.Text = "Laoding channels... please wait a moment";
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
                    chList.Items.AddRange(lstListsChannels[selected].Select(c => new ListViewItem(new string[] { c.Number.ToString(), c.Text })).ToArray());
                });
                selectedList = selected;
                txtLoadCh.Invoke((System.Threading.ThreadStart)delegate
                {
                    txtLoadCh.Text = "Laoding channels... please wait a moment";
                    txtLoadCh.Visible = false;
                });
            }).Start();
        }
    }
}
