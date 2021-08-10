using AmiIptvPlayer.i18n;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace AmiIptvPlayer
{
    public partial class ManageLists : Form
    {
        private bool EditMode = false;
        private string EditNameList = "";
        private bool Save = false;
        public ManageLists()
        {
            InitializeComponent();
        }

        private void ManageLists_Load(object sender, EventArgs e)
        {
            ReloadLang();
            iptvList.View = View.Details;
            iptvList.FullRowSelect = true;
            FillList();
        }
        private void FillList()
        {
            iptvList.Items.Clear();
            foreach (var url in UrlLists.Get().Lists)
            {
                ListViewItem i = new ListViewItem(url.Name);
                i.SubItems.Add(url.URL);

                iptvList.BeginUpdate();
                iptvList.Items.Add(i);
                iptvList.EndUpdate();
            }
        }
        private void ReloadLang()
        {
            lbIptvLists.Text = Strings.lbIptvLists;
            btnClose.Text = Strings.Close;
            btnDelete.Text = Strings.Delete;
            btnEdit.Text = Strings.Edit;
            lbName.Text = Strings.lbName + ":";
            lbUrl.Text = Strings.lbSettingsURL + ":";
            btnAdd.Text = Strings.Add;
            btnCancel.Text = Strings.CANCEL;
            iptvList.Columns[0].Text = Strings.lbName;
            iptvList.Columns[1].Text = Strings.lbSettingsURL;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UrlLists urls = UrlLists.Get();
            if (string.IsNullOrEmpty(txtUrl.Text) || string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show(Strings.FillUrlError, Strings.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               
               
                try
                {
                    HttpUtility.ParseQueryString(txtUrl.Text);
                    if (!EditMode)
                    {
                        urls.Add(txtName.Text, txtUrl.Text);
                        ListViewItem i = new ListViewItem(txtName.Text);
                        i.SubItems.Add(txtUrl.Text);
                        iptvList.BeginUpdate();
                        iptvList.Items.Add(i);
                        iptvList.EndUpdate();
                    }
                    else
                    {
                        urls.Remove(EditNameList);
                        urls.Add(txtName.Text, txtUrl.Text);
                        EditMode = false;
                        EditNameList = "";
                        btnAdd.Text = Strings.Add;
                        FillList();
                    }
                    txtName.Text = "";
                    txtUrl.Text = "";
                    Save = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Strings.FillUrlError + ":" + ex.Message, Strings.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (Save)
            {
                using (StreamWriter listJson = new StreamWriter(Utils.CONF_PATH + "amiIptvConf_lists.json"))
                {
                    UrlLists urlList = UrlLists.Get();
                    urlList.Refresh = true;
                    listJson.Write(JsonConvert.SerializeObject(urlList));
                }
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtUrl.Text = "";
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (iptvList.SelectedItems.Count < 1)
            {
                MessageBox.Show(Strings.ERROR_SELECT_IPTV_LIST, Strings.WARN, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                foreach (ListViewItem item in iptvList.SelectedItems)
                {
                    UrlLists.Get().Remove(item.Text);
                    FillList();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (iptvList.SelectedItems.Count < 1)
            {
                MessageBox.Show(Strings.ERROR_SELECT_IPTV_LIST, Strings.WARN, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                btnAdd.Text = Strings.SAVE;
                string name = "";
                string url = "";
                foreach (ListViewItem item in iptvList.SelectedItems)
                {
                    name = item.Text;
                    url = item.SubItems[1].Text;
                }
                EditMode = true;
                EditNameList = name;
                txtName.Text = name;
                txtUrl.Text = url;
            }
        }
    }
}
