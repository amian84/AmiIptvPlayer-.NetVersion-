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
    public partial class AskPass : Form
    {
        public AskPass()
        {
            InitializeComponent();
        }

        private void AskPass_Load(object sender, EventArgs e)
        {
            this.Text = Strings.ASK_PASS;
            lbAsk.Text = Strings.lbAskPass;
            btnCancel.Text = Strings.CANCEL;
            btnOK.Text = "Ok";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Utils.Base64Encode(txtPass.Text) == AmiConfiguration.Get().PARENTAL_PASS)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                lbAsk.Text = Strings.lbAskPassError + "\n" + Strings.lbAskPass;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnOK_Click(null, null);
            }
        }
    }
}
