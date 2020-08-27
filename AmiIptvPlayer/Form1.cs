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
           
            //vlcControl.MediaPlayer = _mp;

            chList.View = View.Details;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListViewItem i = new ListViewItem("1");
            i.SubItems.Add("La 1HD");
            chList.Items.Add(i);
            chList.FullRowSelect = true;
            axWindowsMediaPlayer1.URL = "http://test";
            axWindowsMediaPlayer1.Ctlcontrols.play();

            //chList.Items.Add(new ListViewItem(new string[] { "2", "La 2HD" }));
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (chList.SelectedItems.Count > 0)
            {
                ListViewItem item = chList.SelectedItems[0];
                MessageBox.Show(item.SubItems[1].ToString());
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Preferences pref = new Preferences();
            pref.ShowDialog();

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

    }
}
