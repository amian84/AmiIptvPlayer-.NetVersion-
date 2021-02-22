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
using System.Net;
using System.Net.Mail;
using System.IO;

namespace AmiIptvPlayer
{
    public partial class RequestVOD : Form
    {
        private SearchIdent SEToSend = null;
        private List<SearchIdent> searchInfo = new List<SearchIdent>();
        public RequestVOD()
        {
            InitializeComponent();
        }

        private void RequestVOD_Load(object sender, EventArgs e)
        {
            LoadI18N();
            logoPRG.WaitOnLoad = false;
            foundList.View = View.Details;
            foundList.FullRowSelect = true;
            logoPRG.Image = Image.FromFile("./resources/images/nochannel.png");
        }

        private void LoadI18N()
        {
            this.Text = Strings.RequestNewVOD;
            btnExit.Text = Strings.ExitBtn;
            btnSearch.Text = Strings.Search;
            grpSearch.Text = Strings.Search;
            btnSend.Text = Strings.Send;
            lbTitle.Text = Strings.lbTitleTitle + ":";
            lbTitleSeach.Text = Strings.lbTitleTitle + ":";
            lbType.Text = Strings.Type + ":";
            lbYear.Text = Strings.lbYearsTitle;
            lbSendAdition.Text = Strings.SendExtra + ":";
            chSenAdition.Checked = true;
            cmbTypes.Items.Add(Strings.Movie);
            cmbTypes.Items.Add(Strings.Show);
            cmbTypes.SelectedText = Strings.Movie;
            cmbTypes.SelectedItem = Strings.Movie;
            foundList.Columns[0].Text = Strings.FITitle;
            foundList.Columns[1].Text = Strings.FIOrigTitle;
            foundList.Columns[3].Text = Strings.lbCountryTitle;
            foundList.Columns[2].Text = Strings.lbDescriptionTitle;
            foundList.Columns[4].Text = Strings.FIYear;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string textToSearch = txtSearch.Text;
            var result = Utils.GetFilmInfo(textToSearch, null, Strings.Culture.Name);
            JArray fillFilmResults = result["movies"];
            JArray fillShowResults = result["shows"];
            List<SearchIdent> listSearch = Utils.TransformJArrayToSearchIdent(fillFilmResults, ChType.MOVIE);
            
            listSearch.AddRange(Utils.TransformJArrayToSearchIdent(fillShowResults, ChType.SHOW));
            SetSearch(listSearch);
        }
        public void SetSearch(List<SearchIdent> searchs)
        {
            searchInfo = searchs;
            foreach (SearchIdent se in searchs)
            {
                var origTitle = "";
                var country = "";
                var overview = "";
                if (se.ChType == ChType.SHOW)
                {
                    origTitle = se.SearchData["original_name"]?.ToString();
                    country = se.SearchData["original_language"]?.ToString();
                    overview = se.SearchData["overview"]?.ToString();
                }
                else
                {
                    origTitle = se.SearchData["original_title"]?.ToString();
                    country = se.SearchData["original_language"]?.ToString();
                    overview = se.SearchData["overview"]?.ToString();
                }
                ListViewItem i = new ListViewItem(se.Title);
                i.SubItems.Add(origTitle);
                i.SubItems.Add(overview);
                i.SubItems.Add(country);
                i.SubItems.Add(se.Year);
                
                foundList.BeginUpdate();
                foundList.Items.Add(i);
                foundList.EndUpdate();
            }

        }

        

        private void logoEPGLoaded(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                logoPRG.Image = Image.FromFile("./resources/images/nochannel.png");
            }

        }


        private void foundList_SelectedIndexChanged(object sender, EventArgs e)
        {
            logoPRG.LoadCompleted -= logoEPGLoaded;
            if (foundList.SelectedItems.Count > 0)
            {
                SearchIdent selected = searchInfo[foundList.SelectedItems[0].Index];
                if (selected.SearchData != null)
                {
                    var logoUrl = Utils.LogoUrl(selected.SearchData["poster_path"].ToString());
                    logoPRG.LoadAsync(logoUrl);
                    logoPRG.LoadCompleted += logoEPGLoaded;
                }
            }
        }

        private void foundList_DoubleClick(object sender, EventArgs e)
        {
            if (foundList.SelectedItems.Count > 0)
            {
                SearchIdent selected = searchInfo[foundList.SelectedItems[0].Index];
                txtTitle.Text= selected.Title;
                txtYear.Text = selected.Year;
                cmbTypes.Text = selected.ChType == ChType.MOVIE ? Strings.Movie : Strings.Show;
                chSenAdition.Checked = false;
                SEToSend = selected;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                MessageBox.Show(Strings.FillReqVOD, Strings.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!String.IsNullOrEmpty(AmiConfiguration.Get().REQ_EMAIL))
            {
                var overview = "";
                var title = txtTitle.Text;
                var year = txtYear.Text;
                var stype = cmbTypes.SelectedItem.ToString();
                var originalTitle = "";
                var logo = "https://i.ibb.co/vP1QYdy/No-Cover-Image-01.png";
                if (!chSenAdition.Checked && SEToSend != null)
                {
                    overview = SEToSend.SearchData["overview"]?.ToString();
                    if (SEToSend.SearchData["poster_path"]!=null)
                        logo = Utils.LogoUrl(SEToSend.SearchData["poster_path"].ToString());
                    if (stype == Strings.Movie)
                    {
                        originalTitle = SEToSend.SearchData["original_title"]?.ToString();
                    }
                    else
                    {
                        originalTitle = SEToSend.SearchData["original_name"]?.ToString();
                    }
                }
                string template = File.ReadAllText("./resources/emailtemplate.html");
                template = template.Replace("$$EMAILHEADER$$", Strings.EmailHeader);
                template = template.Replace("$$EMAILTEXT$$", Strings.EmailText);
                template = template.Replace("$$NAMETITLE$$", Strings.FITitle);
                template = template.Replace("$$ONAMETITLE$$", Strings.FIOrigTitle);
                template = template.Replace("$$OVERVIEWTITLE$$", Strings.lbDescriptionTitle);
                template = template.Replace("$$TYPETITLE$$", Strings.Type);
                template = template.Replace("$$YEARTITLE$$", Strings.FIYear);
                template = template.Replace("$$POSTERTITLE$$", Strings.PosterEmail);
                template = template.Replace("$$THANKS$$", Strings.ThanksEmail);
                template = template.Replace("$$USERTITLE$$", Strings.User);
                template = template.Replace("$$NAME$$", title);
                template = template.Replace("$$ONAME$$", originalTitle);
                template = template.Replace("$$OVERVIEW$$", overview);
                template = template.Replace("$$TYPE$$", stype);
                template = template.Replace("$$YEAR$$", year);
                template = template.Replace("$$URL$$", logo);
                template = template.Replace("$$USER$$",IPTVData.Get().USER);
                SendEmail(template);
                
            }
            else
            {
                MessageBox.Show(Strings.FillReqEmail, Strings.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void SendEmail(string htmlString)
        {
            try
            {
                
                
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("amiiptvplayer@gmail.com");
                message.To.Add(new MailAddress(AmiConfiguration.Get().REQ_EMAIL));
                message.Subject = Strings.SubjectEmail;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("amiiptvplayer@gmail.com", "H$2j3&!xPQoXJy");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                this.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(Strings.ERROR + ": " + ex.Message, Strings.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chSenAdition_CheckedChanged(object sender, EventArgs e)
        {
            if (!chSenAdition.Checked)
            {
                if (foundList.Items.Count < 1)
                {
                    chSenAdition.Checked = true;
                    return;
                }
                txtTitle.Enabled = false;
                txtYear.Enabled = false;
                cmbTypes.Enabled = false;
            }
            else
            {
                txtTitle.Enabled = true;
                txtYear.Enabled = true;
                cmbTypes.Enabled = true;
                foundList.Items.Clear();
                SEToSend = null;
            }
        }
    }
}
