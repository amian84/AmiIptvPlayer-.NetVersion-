using AmiIptvPlayer.i18n;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmiIptvPlayer
{
    public partial class Donation : Form
    {
        public Donation()
        {
            InitializeComponent();
        }

        private void Donation_Load(object sender, EventArgs e)
        {
            this.Text = Strings.Donation;
            label1.Text = Strings.DonationText;
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("https://github.com/amian84/AmiIptvPlayer-.NetVersion-");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel2.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.paypal.me/amian84");
            
        }
    }
}
