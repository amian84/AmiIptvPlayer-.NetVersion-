
using Mpv.NET.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        private bool isMKV = false;
        private bool positioncchangedevent = false;
        private PrgInfo currentPrg = null;
        private List<ChannelInfo> lstChannels = new List<ChannelInfo>();
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
            chList.FullRowSelect = true;
            player.MediaUnloaded += StopPlayEvent;
            player.MediaLoaded += MediaLoaded;
            player.Volume = 100;
            EPG_DB epg = EPG_DB.Get();
            epg.epgEventFinish += FinishLoadEpg;
            DefaultEpgLabels();
            if (File.Exists(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\channelCache.json"))
            {
                Channels channels = Channels.LoadFromJSON();
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
            DateTime creation = File.GetCreationTime(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiiptvepgCache.json");
            if (creation.Day < DateTime.Now.Day - 1)
            {
                DownloadEPGFile(epg);
            }
            else
            {
                if (File.Exists(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiiptvepgCache.json"))
                {
                    epg = EPG_DB.LoadFromJSON();
                }
                else if (File.Exists(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiiptvepg.xml"))
                {

                    epg.ParseDB();
                }

            }
            player.API.SetPropertyString("deinterlace", "yes");

        }

        private void FinishLoadEpg(EPG_DB epg, EPGEventArgs e)
        {
            if (e.Error)
            {
                MessageBox.Show("Error processing EPG, please check your url", "EPG ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void DownloadEPGFile(EPG_DB epgDB)
        {
            new System.Threading.Thread(delegate ()
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                try
                {
                    using (var client = new WebClient())
                    {

                        client.DownloadFile(config.AppSettings.Settings["Epg"].Value, System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiiptvepg.xml");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error: " + ex.Message + ". URL=" + config.AppSettings.Settings["Epg"].Value,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }

                epgDB.Refresh = false;

                loadingPanel.Invoke((System.Threading.ThreadStart)delegate {
                    loadingPanel.Visible = false;
                    loadingPanel.Size = new Size(20, 20);
                });
                epgDB.ParseDB();

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
                    isMKV = channel.URL.Contains(".mkv");

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
                    EPG_DB epg = EPG_DB.Get();
                    if (epg.Loaded)
                    {
                        PrgInfo prg = epg.GetCurrentProgramm(channel);
                        if (prg != null)
                        {
                            lbTitleEPG.Text = prg.Title;
                            string description = prg.Description;
                            if (description.Length > 200 || description.Split('\n').Length > 3)
                            {
                                description = description.Substring(0, 190) + "...\n (Click to show more description)";
                            }
                            lbDescription.Text = description;
                            //epgSTR += "\nRatings: " + prg.Rating;
                            lbStars.Text = prg.Stars;
                            /*epgSTR += "\nCategories: ";
                            if (prg.Categories.Count > 0)
                            {
                                foreach (string category in prg.Categories)
                                {
                                    epgSTR += category + ",";
                                }
                                if (epgSTR.EndsWith(","))
                                {
                                    epgSTR.Remove(epgSTR.Length - 1);
                                }
                            }
                            */
                            lbStartTime.Text = prg.StartTime.ToShortTimeString();
                            lbEndTime.Text = prg.StopTime.ToShortTimeString();
                            logoEPG.LoadCompleted -= logoEPGLoaded;

                            logoEPG.Image = Image.FromFile("./resources/images/nochannel.png");
                            if (!string.IsNullOrEmpty(prg.Logo))
                            {
                                logoEPG.LoadAsync(prg.Logo);
                                logoEPG.LoadCompleted += logoEPGLoaded;
                            }
                            currentPrg = prg;

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
            }
        }


        private void DefaultEpgLabels()
        {
            lbTitleEPG.Text = "-";
            lbDescription.Text = "-";
            lbEndTime.Text = "-";
            lbStars.Text = "-";
            lbStartTime.Text = "-";
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
                lbDuration.Text = "Video Time: --/--";
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
            
            if (isMKV && player.Duration.TotalSeconds > 0)
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
                DownloadEPGFile(epgDB);
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
                channel.ChNumber = chNumber;
                lstChannels.Add(channel);
                /*ListViewItem i = new ListViewItem(chNumber.ToString());
                i.SubItems.Add(channel.Title);
                listChannels.Add(i);*/
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
            chList.Items.Clear(); // clear list items before adding 
                                  // filter the items match with search key and add result to list view 
            chList.Items.AddRange(lstChannels.Where(i => string.IsNullOrEmpty(txtFilter.Text) || i.Title.ToLower().Contains(txtFilter.Text.ToLower()))
                .Select(c => new ListViewItem(new string[] { c.ChNumber.ToString(), c.Title })).ToArray());
        }

        
        private void btnClear_Click_1(object sender, EventArgs e)
        {
            chList.Items.Clear();
            txtFilter.Clear();
            chList.Items.AddRange(lstChannels.Select(c => new ListViewItem(new string[] { c.ChNumber.ToString(), c.Title })).ToArray());
        }

        private void refreshListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Channels channels = Channels.Get();
            loadingPanel.Visible = true;
            loadingPanel.BringToFront();
            new System.Threading.Thread(delegate ()
            {
                channels.RefreshList();
                fillChannelList();
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
            Form form = new Form();
            
            form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            form.TopMost = true;
            

            PictureBox pb = new PictureBox();
            pb.ImageLocation = ((PictureBox)sender).ImageLocation;
            pb.Size = ((PictureBox)sender).Image.Size;
            pb.Width += 20;
            pb.Height += 40;
            form.Size = pb.Size;
            //pb.SizeMode = PictureBoxSizeMode.Zoom;

            form.Controls.Add(pb);
            form.ShowDialog();

        }

        private void lbDescription_Click(object sender, EventArgs e)
        {
            LongDescription lDescriptionForm = new LongDescription();
            string desc = "Description not available.";
            if (currentPrg != null)
            {
                desc = currentPrg.Description;
            }
            lDescriptionForm.SetTextDes(desc);
            lDescriptionForm.ShowDialog();
        }
    }
}
