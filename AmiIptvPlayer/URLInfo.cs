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
    public partial class URLInfo : Form
    {
        public URLInfo()
        {
            InitializeComponent();
        }

        private void URLInfo_Load(object sender, EventArgs e)
        {

        }
        public void setURL(string url)
        {
            txtURL.Text = url;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
