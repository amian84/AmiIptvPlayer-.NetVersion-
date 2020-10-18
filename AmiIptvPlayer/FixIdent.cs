using Json.Net;
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

    
    public partial class FixIdent : Form
    {

        public FixIdent()
        {
            InitializeComponent();
            foundList.View = View.Details;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetSearchText(string text)
        {
            txtSearch.Text = text;
        }
        public void SetSearch(List<SearchIdent> searchs)
        {
            foreach(SearchIdent se in searchs)
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
        }
    }
}
