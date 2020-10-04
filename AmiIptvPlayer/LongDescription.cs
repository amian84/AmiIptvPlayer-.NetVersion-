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

        }
        public void SetTextDes(string text)
        {
            txtDescription.Text = text;
        }
    }
}
