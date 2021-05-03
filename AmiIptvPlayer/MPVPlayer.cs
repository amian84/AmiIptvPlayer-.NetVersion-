using AmiIptvPlayer.i18n;
using AmiIptvPlayer.Tools;
using Mpv.NET.API;
using Mpv.NET.Player;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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
                Default = false;
            }
            public long ID { get; set; }
            public TrackType TType { get; }
            public string Title { get; set; }
            public string Lang { get; set; }
            public override string ToString() => $"({TType}: {Title}, {Lang})";
            public bool Default { get; set; }
        }
        private Task NumbersTaks;
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
            player.API.LogMessage += LogMessage;
            
            originalSizePanel = panelvideo.Bounds;
            originalSizeWin = this.Bounds;
            originalPositionWin = new Tuple<int, int>(this.Top, this.Left);
            ReloadLang();
        }

        private void LogMessage(object sender, MpvLogMessageEventArgs e)
        {
            Utils.WriteMPVLog($"[{e.Message.Prefix}] {e.Message.Text}", player.LogLevel);
        }

        public void ReloadLang()
        {
            lbDuration.Text = Strings.Duration + "--/--";
            lbVol.Text = Strings.Volume;
            lbLang.Text = Strings.Language;
            lbSub.Text = Strings.Subs;
            pnPrincipal.RowStyles[0].Height = 0;
            pnPrincipal.RowStyles[1].Height = 0;
            if (AmiConfiguration.Get().ENABLE_LOG)
            {
                player.LogLevel = MpvLogLevel.Debug;
            }
            else
            {
                player.LogLevel = MpvLogLevel.Info;
            }
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
            //if (isFullScreen)
            //{
            //    GoFullscreen(false);
            //}
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
            player.API.SetPropertyString("hwdec", "auto");
        }

        public void SetPrincipalForm(Form1 form1)
        {
            principalForm = form1;
        }

        private void MediaLoaded(object sender, EventArgs e)
        {
            if (AmiConfiguration.Get().ENABLE_LOG)
            {
                string msg = "[MediaLoaded-MPV] MPV load streaming with next values: ";
                msg += $"SetPositionOnLoad => {SetPositionOnLoad} ";
                msg += $"currLang => {currLang} ";
                msg += $"isChannel=> {isChannel} ";
                msg += $"positioncchangedevent=> {positioncchangedevent} ";
                msg += $"player.Duration.TotalSeconds=> {player.Duration.TotalSeconds} ";
                Logger.Current.Debug(msg);
            }
            ParseTracksAndSetDefaults();
            
            if (!isChannel && player.Duration.TotalSeconds > 0)
            {
                
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
            
            /*else
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
            }*/
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
            panelvideo.Invoke((System.Threading.ThreadStart)delegate
            {
                panelvideo.Focus();
                
            });
            
        }

        public void UnloadPlayerEvents()
        {
            player.MediaLoaded -= MediaLoaded;
            player.MediaUnloaded -= StopPlayEvent;
            player.API.LogMessage -= LogMessage;

        }

        private void StopPlayEvent(object sender, EventArgs e)
        {
            if (AmiConfiguration.Get().ENABLE_LOG)
            {
                Logger.Current.Debug($"[STOPPLAYEVENT] MPV stopped the streaming with values: exitApp => {exitApp.ToString()} and dockedEvent {dockedEvent.ToString()}");
            }
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
                    lbDuration.Text = Strings.Duration + "--/--";
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
                pnPrincipal.RowStyles[3].Height = 50;
                pnPrincipal.RowStyles[4].Height = 50;
            }
            else{
                if (pnPrincipal.RowStyles[3].Height == 50)
                {
                    pnPrincipal.RowStyles[3].Height = 0;
                    pnPrincipal.RowStyles[4].Height = 0;
                }
                else
                {
                    pnPrincipal.RowStyles[3].Height = 50;
                    pnPrincipal.RowStyles[4].Height = 50;
                }
            }
            ((Panel)sender).Focus();
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
                    player.RestartAsync();
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
                        lang = "unk";
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
                    var defaultbool = false;
                    try 
                    {
                        defaultbool = player.API.GetPropertyString("track-list/" + i + "/default")=="yes";
                    }catch(Exception ex)
                    {
                        
                    }


                    TrackInfo TKInfo = new TrackInfo(TKInfoType);
                    TKInfo.Title = title;
                    TKInfo.Lang = lang;
                    TKInfo.ID = id;
                    TKInfo.Default = defaultbool;
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

        public void SetFocusOnVideoPanel()
        {
            panelvideo.Focus();
        }

        private void FillAndSetLangSub(List<TrackInfo> trackList, ComboBox cmb, string valueToSet, int typeLS)
        {
            string propertyPlayer = "aid";
            if (typeLS != 0)
            {
                propertyPlayer = "sid";
            }
            ComboboxItem candidateCMB = null;
            ComboboxItem defaultCMB = null;
            ComboboxItem noneValue = null;
            foreach(ComboboxItem item in cmb.Items)
            {
                if (item.Value == -1)
                {
                    noneValue = item;
                }
            }
            TrackInfo candidateTR = new TrackInfo(TrackType.UNKNOWN);
            TrackInfo defaultTR = new TrackInfo(TrackType.UNKNOWN);

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
                    if (tkinfo.Default)
                    {
                        defaultCMB = item;
                        defaultTR = tkinfo;
                    }
                });
            }
            if (defaultCMB == null)
            {
                cmb.Invoke((System.Threading.ThreadStart)delegate
                {
                    var iter = cmb.Items.GetEnumerator();
                    iter.MoveNext();
                    defaultCMB = iter.Current as ComboboxItem;
                });
                
                defaultTR = trackList.Find((x) => x.ID == 1);
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
                else
                {
                    if (defaultCMB != null)
                    {
                        player.API.SetPropertyLong(propertyPlayer, defaultTR.ID);
                        cmb.Invoke((System.Threading.ThreadStart)delegate
                        {
                            cmb.SelectedItem = defaultCMB;
                        });
                    }
                    
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
                    lbDuration.Text = Strings.Duration + positionText + " / " + durationText;
                });

            }
            if (player.Position.TotalSeconds>0 && player.Position.TotalSeconds >= player.Duration.TotalSeconds - 300)
            {
                if (!principalForm.GetCurrentChannel().seen)
                {
                    SeenResumeChannels.Get().UpdateOrSetSeen(principalForm.GetCurrentChannel().Title, true, player.Duration.TotalSeconds, DateTime.Now);
                    principalForm.GetCurrentChannel().seen = true;
                    principalForm.RefreshListView();
                }
            }
            if (player.Position.TotalSeconds >= 150)
            {
                if (principalForm.GetCurrentChannel().currentPostion == null)
                {
                    SeenResumeChannels.Get().UpdateOrSetResume(principalForm.GetCurrentChannel().Title, player.Position.TotalSeconds, player.Duration.TotalSeconds, DateTime.Now);
                    principalForm.GetCurrentChannel().currentPostion = player.Position.TotalSeconds;
                    principalForm.RefreshListView();
                }
                principalForm.GetCurrentChannel().currentPostion = player.Position.TotalSeconds;
            }

           /* if (player.Position.TotalSeconds > 0 && player.Position.TotalSeconds >= player.Duration.TotalSeconds - 30)
            {
                pnPrincipal.Invoke((System.Threading.ThreadStart)delegate
                {
                    pnPrincipal.RowStyles[0].Height = 50;
                });
            }*/

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
            }
            currLang = lang;
            currSub = sub;
            
            
        }

        private void panelvideo_DoubleClick(object sender, EventArgs e)
        {
            /*if (player.IsMediaLoaded)
            {*/
            if (docked)
            {
                principalForm.DockFullScreen(isFullScreen);
            }
            else
            {
                GoFullscreen(!isFullScreen);
                isFullScreen = !isFullScreen;
            }
                
            //}
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


        

        private void MPVPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!exitApp)
            {
                this.btnDock_Click(null, null);
                e.Cancel = true;
                return;
            }
        }

        private void panelvideo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                principalForm.NextChannel();
            }
            if (e.KeyCode == Keys.Down)
            {
                principalForm.PrevChannel();
            }

            
            if (isNumric(e.KeyCode) )
            {

                if (pnPrincipal.RowStyles[1].Height == 0)
                {
                    pnPrincipal.RowStyles[1].Height = 50;
                    NumbersTaks = Task.Run(async () => {
                        await Task.Delay(3000);
                        pnPrincipal.Invoke((System.Threading.ThreadStart)delegate
                        {
                            pnPrincipal.RowStyles[1].Height = 0;
                        });
                        this.Invoke(new Action(() => principalForm.ChannelToNumber(int.Parse(txtNumbers.Text))));
                        txtNumbers.Invoke((System.Threading.ThreadStart)delegate
                        {
                            txtNumbers.Text = "";
                        });
                        
                        
                    });
                }
                txtNumbers.Text += KeyToNumber(e.KeyCode);
            }

            ((Panel)sender).Focus();
            
            
        }

        private string KeyToNumber(Keys key)
        {
            string returnValue = "";
            switch (key)
            {
                case Keys.D0:
                case Keys.NumPad0:
                    returnValue = "0";
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    returnValue = "1";
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    returnValue = "2";
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    returnValue = "3";
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    returnValue = "4";
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    returnValue = "5";
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    returnValue = "6";
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    returnValue = "7";
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    returnValue = "8";
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    returnValue = "9";
                    break;
                default:
                    returnValue = "*";
                    break;

            }
            return returnValue;
        }

        private bool isNumric(Keys key)
        {
            return (key >= Keys.D0 && key <= Keys.D9) ||
                   (key >= Keys.NumPad0 && key <= Keys.NumPad9);
        }

        private void cmbLangs_DropDownClosed(object sender, EventArgs e)
        {
            
        }
    }
}
