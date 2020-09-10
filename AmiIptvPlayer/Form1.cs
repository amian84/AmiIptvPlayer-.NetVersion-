using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace AmiIptvPlayer
{
    public partial class Form1 : Form
    {
        private List<ChannelInfo> lstChannels =  new List<ChannelInfo>();
        public Form1()
        {
            InitializeComponent();

            chList.View = View.Details;
            lbEPG.Text = "EPG: Not implemented yet, will try develop that feature ASAP";
            lbEPG.Visible = true;
            lbEPG.BringToFront();
            
            if (File.Exists("channelCache.json"))
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
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chList.FullRowSelect = true;
            axWindowsMediaPlayer1.URL = "http://test";
            
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            Channels channels = Channels.Get();
            if (chList.SelectedItems.Count > 0)
            {
                ListViewItem item = chList.SelectedItems[0];
                ChannelInfo channel = channels.GetChannel(int.Parse(item.SubItems[0].Text));
                if (channel ==  null)
                {
                    MessageBox.Show(item.SubItems[1].Text);
                }
                else
                {
                    axWindowsMediaPlayer1.URL = channel.URL;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    try
                    {
                        logoChannel.Load(channel.TVGLogo);
                    } catch (Exception ex)
                    {
                        logoChannel.Image = Image.FromFile("./resources/images/nochannel.png");
                    }
                    string title = channel.Title;
                    if (title.Length > 20)
                    {
                        title = title.Substring(0, 20) + "...";
                    }
                    lbChName.Text = title;
                }
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Preferences pref = new Preferences();
            pref.ShowDialog();
            Channels channels = Channels.Get();
            if (channels.NeedRefresh())
            {
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
            System.Windows.Forms.Application.Exit();
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
    }
}
