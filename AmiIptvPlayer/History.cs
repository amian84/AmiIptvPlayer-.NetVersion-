using AmiIptvPlayer.i18n;
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
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
        }
        SeenResumeChannels seen;
        private void FillList()
        {
            historyList.Items.Clear();
            foreach (var saw in seen.channelsSeenResume["seen"])
            {
                ListViewItem i = new ListViewItem(saw.title);
                i.SubItems.Add(saw.date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern));
                
                historyList.BeginUpdate();
                historyList.Items.Add(i);
                historyList.EndUpdate();
            }
        }
        private void History_Load(object sender, EventArgs e)
        {
            ReloadLang();
            historyList.View = View.Details;
            historyList.FullRowSelect = true;
            seen = SeenResumeChannels.Get();
            FillList();
        }

        private void ReloadLang()
        {
            lbHisotry.Text = Strings.lbHistory;
            btnClose.Text = Strings.Close;
            btnDelete.Text = Strings.Delete;
            btnMoreInfo.Text = Strings.MORE_DETAILS;
            historyList.Columns[0].Text = Strings.FITitle;
            historyList.Columns[1].Text = Strings.Date;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (historyList.SelectedItems.Count < 1)
            {
                MessageBox.Show(Strings.ERROR_DELETE_HIS, Strings.WARN, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                foreach(ListViewItem item in historyList.SelectedItems)
                {
                    seen.RemoveSeen(item.Text);
                    FillList();
                }
            }
        }

        private void btnMoreInfo_Click(object sender, EventArgs e)
        {

            if (historyList.SelectedItems.Count != 1)
            {
                MessageBox.Show(Strings.ERROR_INFO_HIS, Strings.WARN, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                ListViewItem item = historyList.SelectedItems[0];
                LongDescription lDescriptionForm = new LongDescription();
                string year = Utils.YearFromFilmName(item.Text);
                if (year != null)
                {
                    dynamic result = Utils.GetFilmInfo(ChType.MOVIE, item.Text.Replace(year, ""), year, "es"); ;
                    JObject filmMatch = null;
                    if (result["results"].Count > 0)
                    {
                        filmMatch = result["results"][0];
                        lDescriptionForm.FillMovieData(filmMatch, ChType.MOVIE);
                        lDescriptionForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(Strings.NOT_FOUND, Strings.WARN, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(Strings.NOT_INFO_HISTORY_SHOW, Strings.WARN, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

            }
            
        }

        private void historyList_DoubleClick(object sender, EventArgs e)
        {
            btnMoreInfo_Click(null, null);
        }
    }
}
