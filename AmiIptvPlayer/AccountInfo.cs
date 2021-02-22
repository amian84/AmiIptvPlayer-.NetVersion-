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
    public partial class AccountInfo : Form
    {
        public AccountInfo()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AccountInfo_Load(object sender, EventArgs e)
        {
            ReloadLang();
            PrintValues();
        }

        private void ReloadLang()
        {
            lbActiveConsText.Text = Strings.ActiveCons;
            lbMaxConText.Text = Strings.MaxCons;
            lbExpText.Text = Strings.ExpirationDate;
            lbHeader.Text = Strings.AccountInfoHead;
            this.Text = Strings.AccountInfoTitle;
            lbServerText.Text = Strings.Server;
            lbUserText.Text = Strings.User + ":";
            btnClose.Text = Strings.Close;
            btnRefresh.Text = Strings.Refresh;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Utils.GetAccountInfo();
            PrintValues();

        }
        private void PrintValues()
        {
            IPTVData data = IPTVData.Get();
            lbServer.Text = data.HOST;
            lbUser.Text = data.USER;
            lbMaxCon.Text = data.MAX_CONECTIONS;
            lbActiveCons.Text = data.ACTIVE_CONECTIONS;
            lbExp.Text = data.EXPIRE_DATE.ToString();
        }
    }
}
