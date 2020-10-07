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
    public partial class LongDescription : Form
    {
        public LongDescription()
        {
            InitializeComponent();
        }

        private void LongDescription_Load(object sender, EventArgs e)
        {
            logoPRG.WaitOnLoad = false;
            this.ActiveControl = lbTitleEPG;

        }
        public void SetTextDes(string text)
        {
            txtDescription.Text = text;
        }

        private void logoEPGLoaded(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                logoPRG.Image = Image.FromFile("./resources/images/nochannel.png");
            }
            
        }

        public void SetData(PrgInfo prg)
        {
            txtDescription.Text = prg.Description;
            lbCountry.Text = prg.Country;
            lbTitleEPG.Text = prg.Title;
            lbStars.Text = prg.Stars;
            lbStartTime.Text = prg.StartTime.ToShortTimeString();
            lbEndTime.Text = prg.StopTime.ToShortTimeString();
            logoPRG.LoadCompleted -= logoEPGLoaded;
            lbRatings.Text = prg.Rating.Replace("Ratings:", "");
            foreach(string cat in prg.Categories)
            {
                lbCategories.Text += cat + ",";
            }
            if (lbCategories.Text.EndsWith(","))
            {
                lbCategories.Text = lbCategories.Text.Substring(0, lbCategories.Text.Length - 1);
            }
            logoPRG.Image = Image.FromFile("./resources/images/nochannel.png");
            if (!string.IsNullOrEmpty(prg.Logo))
            {
                logoPRG.LoadAsync(prg.Logo);
                logoPRG.LoadCompleted += logoEPGLoaded;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
