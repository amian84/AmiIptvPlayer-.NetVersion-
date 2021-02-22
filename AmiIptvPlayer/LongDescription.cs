using AmiIptvPlayer.i18n;
using Newtonsoft.Json.Linq;
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
            this.Text = Strings.LongDescriptionTitle;
            lbCategoryTitle.Text = Strings.lbCategoryTitle;
            lbCountryTitle.Text = Strings.lbCountryTitle + ":";
            lbDescriptionTitle.Text = Strings.lbDescriptionTitle + ":";
            lbEndTimeTitle.Text = Strings.lbEndTimeTitle;
            lbStarTimeTitle.Text = Strings.lbStartTimeTitle;
            lbStarsTitle.Text = Strings.lbStarsTitle;
            lbTitleTitle.Text = Strings.lbTitleTitle + ":";
            lbRatingsTitle.Text = Strings.lbRatingsTitle;
            lbReleaseDateTitle.Text = Strings.lbReleaseDateTitle;


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
            VisibleChannel(true);
        }

        private void VisibleChannel(bool visible)
        {
            lbReleaseDateTitle.Visible = visible;
            lbEndTimeTitle.Visible = visible;
            lbEndTime.Visible = visible;
            lbStartTime.Visible = visible;
            lbStarTimeTitle.Visible = visible;
            lbReleaseDate.Visible = !visible;
            lbReleaseDateTitle.Visible = !visible;
            lbStarsTitle.Visible = !visible;
            lbStars.Visible = !visible;
            lbCategoryTitle.Visible = visible;
            lbCountryTitle.Visible = visible;
            lbRatingsTitle.Visible = visible;
            lbCategories.Visible = visible;
            lbCountry.Visible = visible;
            lbRatings.Visible = visible;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void FillMovieData()
        {
            VisibleChannel(false);
            JObject filmInfo = Form1.Get().GetFilmInfo();
            
            string title = "";
            string description = "";
            string stars = "";
            string year = "";
            if (Form1.Get().GetCurrentChannel().ChannelType == ChType.MOVIE)
            {
                title = filmInfo["title"].ToString();
                year = filmInfo["release_date"].ToString().Split('-')[0];
            }
            else
            {
                title = filmInfo["name"].ToString();
                year = filmInfo["first_air_date"].ToString().Split('-')[0];
            }
            logoPRG.LoadCompleted -= logoEPGLoaded;
            description = filmInfo["overview"].ToString();
            stars = filmInfo["vote_average"].ToString();
            txtDescription.Text = description;
            lbCountry.Text = "-";
            lbTitleEPG.Text = title;
            lbStars.Text = stars;
            lbReleaseDate.Text = year;
            string poster_path = filmInfo["poster_path"].ToString();
            logoPRG.Image = Image.FromFile("./resources/images/nochannel.png");
            if (!string.IsNullOrEmpty(poster_path))
            {
                logoPRG.LoadAsync(Utils.PosterBasePath + poster_path);
                logoPRG.LoadCompleted += logoEPGLoaded;
            }
            
            /*List<string> genre_ids = new List<string>();
            foreach (JValue genre in (JArray)filmInfo["genre_ids"])
            {
                genre_ids.Add(genre.ToString());
            }
            if (genre_ids.Count > 0)
            {
                lbRatings.Text = string.Join(",", genre_ids.ToArray<string>());
            }*/
            
        }
    }
}
