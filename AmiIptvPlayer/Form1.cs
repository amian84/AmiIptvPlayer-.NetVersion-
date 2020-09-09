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


namespace AmiIptvPlayer
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            
            InitializeComponent();
           
            chList.View = View.Details;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListViewItem i = new ListViewItem("0");
            i.SubItems.Add("Please load iptv list");
            chList.Items.Add(i);
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
            foreach (var elem in channels.GetChannelsDic())
            {
                int chNumber = elem.Key;
                ChannelInfo channel = elem.Value;
                ListViewItem i = new ListViewItem(chNumber.ToString());
                i.SubItems.Add(channel.Title);
                listChannels.Add(i);
            }
            chList.Invoke((System.Threading.ThreadStart)delegate {
                chList.Items.Clear();
                chList.Items.AddRange(listChannels.ToArray());
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
