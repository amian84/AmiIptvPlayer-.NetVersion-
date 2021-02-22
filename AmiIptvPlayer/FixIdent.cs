using AmiIptvPlayer.i18n;
using Json.Net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmiIptvPlayer
{

    
    public partial class FixIdent : Form
    {
        private Form1 formParent;
        private List<SearchIdent> searchInfo = new List<SearchIdent>();
        public FixIdent()
        {
            InitializeComponent();
            foundList.View = View.Details;
            formParent = Form1.Get();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private JObject GetSearchData(string name, string year)
        {
            foreach(SearchIdent se in searchInfo)
            {
                if (se.Title == name && se.Year == year)
                {
                    return se.SearchData;
                }
            }
            return null;
        }

        public void SetSearchText(string text)
        {
            txtSearch.Text = text;
        }
        public void SetSearch(List<SearchIdent> searchs)
        {
            searchInfo = searchs;
            foreach (SearchIdent se in searchs)
            {
                ListViewItem i = new ListViewItem(se.Title);
                i.SubItems.Add(se.Year);
                foundList.BeginUpdate();
                foundList.Items.Add(i);
                foundList.EndUpdate();
            }
            
        }

        private void FixIdent_Load(object sender, EventArgs e)
        {
            foundList.FullRowSelect = true;
            foundList.Columns[1].Text = Strings.FIYear;
            foundList.Columns[0].Text = Strings.FITitle;
            btnExit.Text = Strings.ExitBtn;
            btnSearch.Text = Strings.SearchAgainBtn;
            lbSearch.Text = Strings.SearchLb;
            this.Text = Strings.FixIdentTitle;
        }

        private void foundList_DoubleClick(object sender, EventArgs e)
        {
            if (foundList.SelectedItems.Count > 0)
            {
                ListViewItem item = foundList.SelectedItems[0];
                JObject sdata = GetSearchData(item.SubItems[0].Text, item.SubItems[1].Text);
                if (sdata != null && formParent != null)
                {
                    formParent.DrawMovieInfo(sdata);
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (formParent!=null && formParent.GetCurrentChannel() != null)
            {
                string textToSearch = txtSearch.Text;
                dynamic result = Utils.GetFilmInfo(formParent.GetCurrentChannel().ChannelType, textToSearch, null, "es");
                JArray fillFilmResults = result["results"];
                List<SearchIdent> listSearch = Utils.TransformJArrayToSearchIdent(fillFilmResults, formParent.GetCurrentChannel().ChannelType);
                SetSearch(listSearch);
            }
            
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnSearch_Click(btnSearch, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Strings.Culture = new CultureInfo("en-US");
            this.FixIdent_Load(null, null);
        }
    }
}
