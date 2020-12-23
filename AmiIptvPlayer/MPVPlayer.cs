using Mpv.NET.Player;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Threading;

using System.Windows.Forms;

namespace AmiIptvPlayer
{
    public partial class MPVPlayer : Form
    {
        private enum TrackType
        {
            AUDIO,
            SUB,
            UNKNOWN
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

        private MpvPlayer player;
        private Dictionary<TrackType, List<TrackInfo>> tracksParser;
        private bool isFullScreen = false;
        private bool isPaused = true;
        private bool lastFSDocked = true;
        private bool positioncchangedevent = false;
        private bool isChannel = true;
        private bool exitApp = false;
        private bool docked = true;
        private Tuple<int, int> originalPositionWin;
        private Rectangle originalSizePanel;
        private Rectangle originalSizeWin;
        private Form1 principalForm;
        private bool dockedEvent = false;
        private bool SetPositionOnLoad = false;
        private int positionOnLoad = 0;
        private int currLang = -1;
        private int currSub = -1;
        private string currURL = "";


        public MPVPlayer()
        {
            InitializeComponent();
            player = new MpvPlayer(panelvideo.Handle);
            originalSizePanel = panelvideo.Bounds;
            originalSizeWin = this.Bounds;
            originalPositionWin = new Tuple<int, int>(this.Top, this.Left);
        }

        public void SetDockedEvent (bool value)
        {
            dockedEvent = value;
           
        }

        public void SetDockIcon(bool value)
        {
            string icon = "./resources/images/float.png";
            if (!value)
            {
                icon = "./resources/images/dock.png";
            }
            btnDock.BackgroundImage = Image.FromFile(icon);
            btnDock.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

            player.Stop();
            isPaused = true;
            if (isFullScreen)
            {
                GoFullscreen(false);
            }
        }

        private void setProperty(string prop, string value)
        {
            player.API.SetPropertyString(prop, value);
        }


        private void MPVPlayer_Load(object sender, EventArgs e)
        {
            player.MediaUnloaded += StopPlayEvent;
            player.MediaLoaded += MediaLoaded;
            player.Volume = 100;
            trVolumen.Value = Convert.ToInt32(player.Volume / 2);
            btnMuteUnmute.BackgroundImage = Image.FromFile("./resources/images/unmute.png");
            btnMuteUnmute.BackgroundImageLayout = ImageLayout.Stretch;

            btnPlayPause.BackgroundImage = Image.FromFile("./resources/images/play.png");
            btnPlayPause.BackgroundImageLayout = ImageLayout.Stretch;

            btnStop.BackgroundImage = Image.FromFile("./resources/images/stop.png");
            btnStop.BackgroundImageLayout = ImageLayout.Stretch;

            

            player.API.SetPropertyString("deinterlace", "yes");
        }

        public void SetPrincipalForm(Form1 form1)
        {
            principalForm = form1;
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
                    if (SetPositionOnLoad)
                        seekBar.Value = positionOnLoad;
                });
                if (!positioncchangedevent)
                {
                    player.PositionChanged += PositionChanged;
                    positioncchangedevent = true;

                }

            }
            else
            {
                cmbLangs.Invoke((System.Threading.ThreadStart)delegate
                {
                    cmbLangs.Enabled = false;
                    cmbLangs.Items.Clear();
                });
                cmbSubs.Invoke((System.Threading.ThreadStart)delegate
                {
                    cmbSubs.Enabled = false;
                    cmbSubs.Items.Clear();

                });
                seekBar.Invoke((System.Threading.ThreadStart)delegate {
                    seekBar.Enabled = false;
                    seekBar.Value = 0;
                });
            }
            player.Resume();
            if (SetPositionOnLoad)
            {
                player.SeekAsync(positionOnLoad);
                SetPositionOnLoad = false;
            }
            btnPlayPause.BackgroundImage = Image.FromFile("./resources/images/pause.png");
            btnPlayPause.BackgroundImageLayout = ImageLayout.Stretch;
            if (currLang > -1)
            {
                cmbLangs.Invoke((System.Threading.ThreadStart)delegate
                {
                    cmbLangs.SelectedIndex = currLang;
                });
                currLang = -1;
            }
            if (currSub > -1)
            {
                cmbSubs.Invoke((System.Threading.ThreadStart)delegate
                {
                    cmbSubs.SelectedIndex = currSub;
                });
                currSub = -1;
            }
        }

        public void UnloadPlayerEvents()
        {
            player.MediaLoaded -= MediaLoaded;
            player.MediaUnloaded -= StopPlayEvent;
            
        }

        private void StopPlayEvent(object sender, EventArgs e)
        {
            //currentChannel = null;
            if (positioncchangedevent)
            {
                player.PositionChanged -= PositionChanged;
                positioncchangedevent = false;

            }
            btnPlayPause.BackgroundImage = Image.FromFile("./resources/images/play.png");
            btnPlayPause.BackgroundImageLayout = ImageLayout.Stretch;
            
            if (!exitApp)
            {
                lbDuration.Invoke((System.Threading.ThreadStart)delegate
                {
                    lbDuration.Text = "Video Time: --/--";
                });
                if (dockedEvent)
                {
                    if (InvokeRequired)
                    {
                        Invoke((MethodInvoker)delegate { principalForm.ShowAgainPlayer(); });

                    }

                }
            }
            

            
            

        }

        private void cmbSubs_SelectedIndexChanged(object sender, EventArgs e)
        {
            long id = ((ComboboxItem)cmbSubs.SelectedItem).Value;
            foreach (TrackInfo tkinfo in tracksParser[TrackType.SUB])
            {
                if (tkinfo.ID == id)
                {
                    player.API.SetPropertyLong("sid", tkinfo.ID);
                    break;
                }
            }
        }

        private void panelvideo_Click_1(object sender, EventArgs e)
        {
            if (docked && !isFullScreen)
            {
                pnPrincipal.RowStyles[1].Height = 50;
                pnPrincipal.RowStyles[2].Height = 50;
            }
            else{
                if (pnPrincipal.RowStyles[1].Height == 50)
                {
                    pnPrincipal.RowStyles[1].Height = 0;
                    pnPrincipal.RowStyles[2].Height = 0;
                }
                else
                {
                    pnPrincipal.RowStyles[1].Height = 50;
                    pnPrincipal.RowStyles[2].Height = 50;
                }
            }
        }

        private void btnMuteUnmute_Click(object sender, EventArgs e)
        {
            if (player.Volume != 0)
            {
                player.Volume = 0;
                trVolumen.Enabled = false;
                btnMuteUnmute.BackgroundImage = Image.FromFile("./resources/images/mute.png");
                btnMuteUnmute.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                player.Volume = trVolumen.Value * 2;
                trVolumen.Enabled = true;
                btnMuteUnmute.BackgroundImage = Image.FromFile("./resources/images/unmute.png");
                btnMuteUnmute.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void trVolumen_MouseUp(object sender, MouseEventArgs e)
        {
            player.Volume = trVolumen.Value * 2;
        }

        private void cmbLangs_SelectedIndexChanged(object sender, EventArgs e)
        {
            long id = ((ComboboxItem)cmbLangs.SelectedItem).Value;
            foreach (TrackInfo tkinfo in tracksParser[TrackType.AUDIO])
            {
                if (tkinfo.ID == id)
                {
                    player.API.SetPropertyLong("aid", tkinfo.ID);
                    break;
                }
            }
        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (player.IsMediaLoaded)
            {
                if (isPaused)
                {
                    btnPlayPause.BackgroundImage = Image.FromFile("./resources/images/pause.png");
                    btnPlayPause.BackgroundImageLayout = ImageLayout.Stretch;
                    player.Resume();
                    isPaused = false;
                }
                else
                {
                    btnPlayPause.BackgroundImage = Image.FromFile("./resources/images/play.png");
                    btnPlayPause.BackgroundImageLayout = ImageLayout.Stretch;
                    player.Pause();
                    isPaused = true;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(currURL))
                    SetMedia(currURL, 0, GetLangValue(), GetSubValue());
            }
        }

        private void LoadAllTracks()
        {
            long tracks = player.API.GetPropertyLong("track-list/count");
            tracksParser = new Dictionary<TrackType, List<TrackInfo>>();
            tracksParser[TrackType.SUB] = new List<TrackInfo>();
            tracksParser[TrackType.AUDIO] = new List<TrackInfo>();
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
                    }
                    catch (Exception ex)
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
        }

        private void CleanCmbLangSub()
        {
            cmbLangs.Invoke((System.Threading.ThreadStart)delegate
            {
                if (tracksParser[TrackType.AUDIO].Count > 0)
                {
                    cmbLangs.Enabled = true;
                }
                else
                {
                    cmbLangs.Enabled = false;
                }
                cmbLangs.Items.Clear();
            });
            ComboboxItem noneSub = new ComboboxItem();
            ComboboxItem subCandidate = new ComboboxItem();
            cmbSubs.Invoke((System.Threading.ThreadStart)delegate
            {
                if (tracksParser[TrackType.SUB].Count > 0)
                {
                    cmbSubs.Enabled = true;
                }
                else
                {
                    cmbSubs.Enabled = false;
                }



                cmbSubs.Items.Clear();

                noneSub.Text = "None";
                noneSub.Value = -1;
                cmbSubs.Items.Add(noneSub);
            });
        }

        private void FillAndSetLangSub(List<TrackInfo> trackList, ComboBox cmb, string valueToSet, int typeLS)
        {
            string propertyPlayer = "aid";
            if (typeLS != 0)
            {
                propertyPlayer = "sid";
            }
            ComboboxItem candidateCMB = null;
            ComboboxItem noneValue = null;
            foreach(ComboboxItem item in cmb.Items)
            {
                if (item.Value == -1)
                {
                    noneValue = item;
                }
            }
            TrackInfo candidateTR = new TrackInfo(TrackType.UNKNOWN);
            bool found = false;
            foreach (TrackInfo tkinfo in trackList)
            {

                cmb.Invoke((System.Threading.ThreadStart)delegate
                {
                    if (!cmb.Enabled)
                    {
                        cmb.Enabled = true;
                    }
                    ComboboxItem item = new ComboboxItem();
                    item.Text = tkinfo.Title + " (" + tkinfo.ID + ")";
                    item.Value = tkinfo.ID;
                    cmb.Items.Add(item);
                    if (tkinfo.Lang == valueToSet && !found)
                    {
                        candidateCMB = item;
                        candidateTR = tkinfo;
                        found = true;
                    }
                });
            }

            if (valueToSet != "none" && found)
            {
                player.API.SetPropertyLong(propertyPlayer, candidateTR.ID);
                cmb.Invoke((System.Threading.ThreadStart)delegate
                {
                    cmb.SelectedItem = candidateCMB;
                });
                
            }
            else
            {
                if (typeLS == 1)
                {
                    player.API.SetPropertyString(propertyPlayer, "no");
                    cmb.Invoke((System.Threading.ThreadStart)delegate
                    {
                        cmb.SelectedItem = noneValue;
                    });
                }
                
            }

        }

        public int GetLangValue()
        {
            return cmbLangs.SelectedIndex;
        }

        public int GetSubValue()
        {
            return cmbSubs.SelectedIndex;
        }

        private void ParseTracksAndSetDefaults()
        {
            LoadAllTracks();
            CleanCmbLangSub();

            
            string audioConf = AmiConfiguration.Get().DEF_LANG;
            string subConf = AmiConfiguration.Get().DEF_SUB;

            FillAndSetLangSub(tracksParser[TrackType.AUDIO], cmbLangs, audioConf,0);
            FillAndSetLangSub(tracksParser[TrackType.SUB], cmbSubs, subConf,1);

        }

        private void seekBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (positioncchangedevent)
            {
                player.PositionChanged -= PositionChanged;
                positioncchangedevent = false;
            }
        }

        private void seekBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (player.IsMediaLoaded)
            {
                player.SeekAsync(seekBar.Value);
            }

            //player.Position.Seconds = seekBar.Value;
            if (!positioncchangedevent)
            {
                player.PositionChanged += PositionChanged;
                positioncchangedevent = true;
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

        public void SetIsChannel(bool isChannel)
        {
            this.isChannel = isChannel;
        }

        public void SetIsPaused(bool isPaused)
        {
            this.isPaused = isPaused;
        }

        public void Stop()
        {
            if (player.IsPlaying)
                player.Stop();
        }

        public void SetMedia(string url, int position, int lang, int sub)
        {
            currURL = url;
            player.Load(url);
            if (!isChannel)
            {
                positionOnLoad = position;
                SetPositionOnLoad = true;
                currLang = lang;
                currSub = sub;
            }
            
        }

        private void panelvideo_DoubleClick(object sender, EventArgs e)
        {
            if (player.IsMediaLoaded)
            {
                if (docked)
                {
                    principalForm.DockFullScreen(isFullScreen);
                }
                else
                {
                    GoFullscreen(!isFullScreen);
                    isFullScreen = !isFullScreen;
                }
                
            }
        }

        public void SetFullScreenAttr(bool value)
        {
            isFullScreen = value;
        }

        public void GoFullscreen(bool fullscreen)
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
                if (docked)
                {
                    this.TopLevel = false;
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.AutoScroll = true;
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                }
                
                panelvideo.Bounds = originalSizePanel;
                this.Bounds = originalSizeWin;
                this.Top = originalPositionWin.Item1;
                this.Left = originalPositionWin.Item2;
            }
        }

        public void SetDocked(bool value)
        {
            docked = value;
        }

        private void btnDock_Click(object sender, EventArgs e)
        {
            if (docked)
            {
                principalForm.UnDockPlayer();
            }
            else
            {
                principalForm.DockPlayer();
            }
        }

        public bool IsMediaLoaded()
        {
            return player.IsMediaLoaded;
        }

        public void ExitApp(bool v)
        {
            exitApp = v;
        }

        public double GetPosition()
        {
            if (player.IsMediaLoaded)
                return player.Position.TotalSeconds;
            return 0;
        }


        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
    }
}
