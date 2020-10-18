
using Mpv.NET.Player;
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
        private enum TrackType
        {
            AUDIO,
            SUB
        }
        private struct TrackInfo
        {
            public TrackInfo(TrackType ttype)
            {
                TType = ttype;
                Title = "";
                Lang = "";
                ID = -1;
            }
            public long ID { get; set; }
            public TrackType TType { get; }
            public string Title { get; set; }
            public string Lang { get; set; }
            public override string ToString() => $"({TType}: {Title}, {Lang})";
        }

        private bool exitApp = false;
        private MpvPlayer player;
        private Rectangle originalSizePanel;
        private Rectangle originalSizeWin;
        private Tuple<int, int> originalPositionWin;
        private bool isFullScreen = false;
        private bool isPaused = true;
        private bool isChannel = true;
        private bool positioncchangedevent = false;
        private PrgInfo currentPrg = null;
        private List<ChannelInfo> lstChannels = new List<ChannelInfo>();
        private Configuration config;
        private JArray fillFilmResults = null;
        private ChType currentCh;
        public Form1()
        {
            InitializeComponent();
            originalSizePanel = panelvideo.Bounds;
            originalSizeWin = this.Bounds;
            originalPositionWin = new Tuple<int, int>(this.Top, this.Left);
            player = new MpvPlayer(panelvideo.Handle);

            chList.View = View.Details;
            logoEPG.WaitOnLoad = false;
            logoChannel.WaitOnLoad = false;
             

        }
        private void setProperty(string prop, string value)
        {
            player.API.SetPropertyString(prop, value);
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

            lbVersion.Text = ApplicationDeployment.IsNetworkDeployed
               ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
               : Assembly.GetExecutingAssembly().GetName().Version.ToString();

            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            chList.FullRowSelect = true;
            player.MediaUnloaded += StopPlayEvent;
            player.MediaLoaded += MediaLoaded;
            player.Volume = 100;
            EPG_DB epg = EPG_DB.Get();
            epg.epgEventFinish += FinishLoadEpg;
            DefaultEpgLabels();
            logoEPG.Image = Image.FromFile("./resources/images/info.png");
            Channels channels = Channels.Get();
            channels.SetUrl(config.AppSettings.Settings["Url"].Value);
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
                lstChannels.Add(ch);
            }

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
                DownloadEPGFile(epg, config.AppSettings.Settings["Epg"].Value);
                
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

                    config.AppSettings.Settings["Epg"].Value = "http://bit.ly/AVappEPG";
                    ConfigurationManager.RefreshSection("appSettings");
                    config.Save(ConfigurationSaveMode.Modified);
                }

            }
            player.API.SetPropertyString("deinterlace", "yes");

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
                    player.Stop();
                    isChannel = channel.ChannelType == ChType.CHANNEL;

                    isPaused = false;
                    Thread.Sleep(500);
                    player.Load(channel.URL);
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
                    SetEPG(channel);
                    currentCh = channel.ChannelType;
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
                if (filmMatch!= null)
                {
                    FillMoviesEPG(filmMatch, channel.ChannelType);
                }
                var hola = 1;
            }
        }

        private void FillMoviesEPG(JObject filmMatch, ChType channelType)
        {
            string title = "";
            string description = "";
            string stars = "";
            string year = "";
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

        private void StopPlayEvent(object sender, EventArgs e)
        {
            if (positioncchangedevent)
            {
                player.PositionChanged -= PositionChanged;
                positioncchangedevent = false;

            }
            
            if (exitApp)
            {
                player.MediaLoaded -= MediaLoaded;
                player.MediaUnloaded -= StopPlayEvent;
                Thread.Sleep(500);
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                lbDuration.Invoke((System.Threading.ThreadStart)delegate
                {
                    lbDuration.Text = "Video Time: --/--";
                });
                
            }

        }

        private void PositionChanged(object sender, EventArgs e)
        {
            if (seekBar.Enabled)
            {
                seekBar.Invoke((System.Threading.ThreadStart)delegate
                {

                    seekBar.Value = Convert.ToInt32(player.Position.TotalSeconds);
                });
                string durationText = (player.Duration.Hours < 10 ? "0" + player.Duration.Hours.ToString() : player.Duration.Hours.ToString())
                    + ":" + (player.Duration.Minutes < 10 ? "0" + player.Duration.Minutes.ToString() : player.Duration.Minutes.ToString())
                    + ":" + (player.Duration.Seconds < 10 ? "0" + player.Duration.Seconds.ToString() : player.Duration.Seconds.ToString());
                string positionText = (player.Position.Hours < 10 ? "0" + player.Position.Hours.ToString() : player.Position.Hours.ToString())
                    + ":" + (player.Position.Minutes < 10 ? "0" + player.Position.Minutes.ToString() : player.Position.Minutes.ToString())
                    + ":" + (player.Position.Seconds < 10 ? "0" + player.Position.Seconds.ToString() : player.Position.Seconds.ToString());
                lbDuration.Invoke((System.Threading.ThreadStart)delegate
                {
                    lbDuration.Text = "Video Time: " + positionText + " / " + durationText;
                });
                
            }
            
            
        }

        private void ParseTracksAndSetDefaults()
        {
            long tracks = player.API.GetPropertyLong("track-list/count");
            Dictionary<TrackType, List<TrackInfo>> tracksParser = new Dictionary<TrackType, List<TrackInfo>>();
            for (long i = 0; i < tracks; i++)
            {
                var id = player.API.GetPropertyLong("track-list/" + i + "/id");
                var ttype = player.API.GetPropertyString("track-list/" + i + "/type");
                if (ttype != "video")
                {
                    var lang = "";
                    try
                    {
                        lang = player.API.GetPropertyString("track-list/" + i + "/lang");
                    } catch (Exception ex)
                    {
                        lang = "spa";
                    }
                    var title = "";
                    TrackType TKInfoType = TrackType.AUDIO;
                    if (ttype == "sub")
                    {
                        TKInfoType = TrackType.SUB;
                    }
                    if (!tracksParser.ContainsKey(TKInfoType))
                    {
                        tracksParser[TKInfoType] = new List<TrackInfo>();
                    }
                    try
                    {
                        title = player.API.GetPropertyString("track-list/" + i + "/title");
                    }
                    catch (Exception ex)
                    {
                        if (TKInfoType == TrackType.AUDIO)
                        {
                            if (Utils.audios.ContainsKey(lang))
                            {
                                title = Utils.audios[lang];
                            }
                            else
                            {
                                title = lang;
                            }
                         
                        }
                        else
                        {
                            if (Utils.subs.ContainsKey(lang))
                            {
                                title = Utils.subs[lang];
                            }
                            else
                            {
                                title = lang;
                            }
                        }
                    }


                    TrackInfo TKInfo = new TrackInfo(TKInfoType);
                    TKInfo.Title = title;
                    TKInfo.Lang = lang;
                    TKInfo.ID = id;
                    tracksParser[TKInfoType].Add(TKInfo);
                }
            }
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string audioConf = config.AppSettings.Settings["audio"].Value;
            string subConf = config.AppSettings.Settings["sub"].Value;
            foreach (TrackInfo tkinfo in tracksParser[TrackType.AUDIO])
            {
                if (tkinfo.Lang == audioConf)
                {
                    player.API.SetPropertyLong("aid", tkinfo.ID);
                    
                    break;
                }
            }
            if (subConf != "none")
            {
                foreach (TrackInfo tkinfo in tracksParser[TrackType.SUB])
                {
                    if (tkinfo.Lang == subConf)
                    {
                        player.API.SetPropertyLong("sid", tkinfo.ID);
                        break;
                    }
                }
            }
            else
            {
                player.API.SetPropertyString("sid", "no");
            }
            
        }


        private void MediaLoaded(object sender, EventArgs e)
        {
            
            if (!isChannel && player.Duration.TotalSeconds > 0)
            {
                ParseTracksAndSetDefaults();
                seekBar.Invoke((System.Threading.ThreadStart)delegate {
                    seekBar.Enabled = true;
                    seekBar.Value = 0;
                    seekBar.Maximum = Convert.ToInt32(player.Duration.TotalSeconds);
                });
                if (!positioncchangedevent)
                {
                    player.PositionChanged += PositionChanged;
                    positioncchangedevent = true;

                }
                
            }
            else
            {
                seekBar.Invoke((System.Threading.ThreadStart)delegate {
                    seekBar.Enabled = false;
                });
            }
            player.Resume();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Preferences pref = new Preferences();
            pref.ShowDialog();
            Channels channels = Channels.Get();
            if (channels.NeedRefresh())
            {
                loadingPanel.Visible = true;
                loadingPanel.Size = new Size(799, 516);
                loadingPanel.BringToFront();
                new System.Threading.Thread(delegate ()
                {
                    channels.RefreshList();
                    fillChannelList();
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
                loadingPanel.Size = new Size(799, 516);
                loadingPanel.BringToFront();
                DownloadEPGFile(epgDB, config.AppSettings.Settings["Epg"].Value);
            }
        }
        
        private void fillChannelList()
        {
            Channels channels = Channels.Get();
            
            List<ListViewItem> listChannels = new List<ListViewItem>();
            lstChannels.Clear();
            foreach (var elem in channels.GetChannelsDic())
            {
                int chNumber = elem.Key;
                ChannelInfo channel = elem.Value;
                lstChannels.Add(channel);
            }
            chList.Invoke((System.Threading.ThreadStart)delegate {
                chList.Items.Clear();
                
                chList.Items.AddRange(lstChannels.Select(c =>  new ListViewItem(new string[] { c.ChNumber.ToString(), c.Title })).ToArray());
                
            });
        }


        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exit();
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
                
                ListViewItem[] list = lstChannels.Where(i => string.IsNullOrEmpty(txtFilter.Text) || i.Title.ToLower().Contains(txtFilter.Text.ToLower()))
               .Select(c => new ListViewItem(new string[] { c.ChNumber.ToString(), c.Title })).ToArray();
                chList.Invoke((System.Threading.ThreadStart)delegate
                {
                    chList.BeginUpdate();
                    chList.Items.AddRange(list);
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
                ListViewItem[] list = lstChannels.Select(c => new ListViewItem(new string[] { c.ChNumber.ToString(), c.Title })).ToArray();
               
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
                loadingPanel.Visible = true;
                loadingPanel.BringToFront();
            }
            new System.Threading.Thread(delegate ()
            {
                channels.RefreshList();
                if (channels.NeedRefresh())
                {
                    fillChannelList();
                }
                loadingPanel.Invoke((System.Threading.ThreadStart)delegate {
                    loadingPanel.Visible = false;
                });

            }).Start();
        }

        private void panelvideo_DoubleClick(object sender, EventArgs e)
        {
            if (player.IsMediaLoaded)
            {
                GoFullscreen(!isFullScreen);
                isFullScreen = !isFullScreen;
            }
            
        }
        private void exit()
        {
            exitApp = true;
            player.Stop();
            EPG_DB epg = EPG_DB.Get();
            epg.epgEventFinish -= FinishLoadEpg;
            Thread.Sleep(500);
            
            
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit();
        }

        private void GoFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                Screen screen = Screen.FromControl(this);
                originalPositionWin = new Tuple<int, int>(this.Top, this.Left);
                this.Bounds = screen.Bounds;
                panelvideo.Bounds = this.Bounds;
                panelvideo.Top = 0;
                panelvideo.Left = 0;
                panelvideo.BringToFront();
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                panelvideo.Bounds = originalSizePanel;
                this.Bounds = originalSizeWin;
                this.Top = originalPositionWin.Item1;
                this.Left = originalPositionWin.Item2;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (player.IsMediaLoaded)
            {
                if (isPaused)
                {
                    player.Resume(); 
                    isPaused = false;
                }
                else
                {
                    player.Pause();
                    isPaused = true;
                }
            }
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            player.Stop();
            isPaused = true;
        }
        
        private void seekBar_MouseDown(object sender, MouseEventArgs e)
        {
            player.PositionChanged -= PositionChanged;
            positioncchangedevent = false;
        }

        private void seekBar_MouseUp(object sender, MouseEventArgs e)
        {
            player.SeekAsync(seekBar.Value);
            //player.Position.Seconds = seekBar.Value;
            if (!positioncchangedevent)
            {
                player.PositionChanged += PositionChanged;
                positioncchangedevent = true;
            }
        }

        private void logoEPG_Click(object sender, EventArgs e)
        {   
            if (currentPrg != null)
            {
                LongDescription lDescriptionForm = new LongDescription();
                lDescriptionForm.SetData(currentPrg);
                lDescriptionForm.ShowDialog();
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
            
            DownloadEPGFile(EPG_DB.Get(), config.AppSettings.Settings["Epg"].Value);
        }

        private void btnFixId_Click(object sender, EventArgs e)
        {
            List<SearchIdent> listSearch = new List<SearchIdent>();
            foreach(JObject obj in fillFilmResults)
            {
                string title = "";
                string year = "";
                if (currentCh == ChType.MOVIE)
                {
                    title = obj["title"].ToString();
                    year = obj["release_date"].ToString().Split('-')[0];
                }
                else
                {
                    title = obj["name"].ToString();
                    year = obj["first_air_date"].ToString().Split('-')[0];                   
                }
                SearchIdent se = new SearchIdent();
                se.Title = title;
                se.Year = year;
                listSearch.Add(se);
            }
            FixIdent fixident = new FixIdent();
            
            fixident.SetSearch(listSearch);
            fixident.SetSearchText(Utils.LastSearch);
            fixident.ShowDialog();
        }
    }
}
