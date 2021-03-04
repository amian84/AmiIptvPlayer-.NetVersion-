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
using System.Windows.Forms;

namespace AmiIptvPlayer
{
    public partial class ParentalControlForm : Form
    {
        public ParentalControlForm()
        {
            InitializeComponent();
        }

        private void ParentalControlForm_Load(object sender, EventArgs e)
        {
            I18N();
            FillChannelList();
        }

        private void FillChannelList()
        {
            Channels channels = Channels.Get();
            List<GrpInfo> groups = channels.GetGroups();
            var groupsNodeWhite = new List<TreeNode>();
            var groupsNodeBlack = new List<TreeNode>();
            foreach ( var group in groups)
            {
                var grpNode = new TreeNode(group.Title);
                grpNode.Tag = group;
                var grpNodeBlack = new TreeNode(group.Title);
                grpNodeBlack.Tag = group;
                foreach (var ch in channels.GetChannelsByGroup(group))
                {
                    var chNode = new TreeNode(ch.Title);
                    chNode.Tag = ch;
                    if (ParentalControl.Get().IsChBlock(ch))
                    {
                        grpNodeBlack.Nodes.Add(chNode);
                    }
                    else
                    {
                        grpNode.Nodes.Add(chNode);
                    }
                }
                if (grpNode.Nodes.Count>0)
                    groupsNodeWhite.Add(grpNode);
                if (grpNodeBlack.Nodes.Count > 0)
                    groupsNodeBlack.Add(grpNodeBlack);
            }
            treeList.Nodes.AddRange(groupsNodeWhite.ToArray());
            treeBlock.Nodes.AddRange(groupsNodeBlack.ToArray());

        }

        private void I18N()
        {
            btnExit.Text = Strings.ExitBtn;
            btnSave.Text = Strings.SAVE;
            lbBlockList.Text = Strings.BlockList + ":";
            lbChannelList.Text = Strings.ChannelList + ":";
            btnAdd.BackgroundImage = Image.FromFile("./resources/images/add.png");
            btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
            btnRemove.BackgroundImage = Image.FromFile("./resources/images/remove.png");
            btnRemove.BackgroundImageLayout = ImageLayout.Stretch;
            this.Text = Strings.ParentalControl;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private TreeNode GetNodeByTag(List<TreeNode> nodes, object tag)
        {
            foreach(TreeNode node in nodes)
            {
                if (node.Tag == tag)
                {
                    return node;
                }
            }
            return null;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (treeList.SelectedNode !=null)
            {
                var node = treeList.SelectedNode;
                MoveSourceDestNodes(node, treeList, treeBlock);
            }
        }

        private void MoveSourceDestNodes(TreeNode node, TreeView source, TreeView dest)
        {
            if (node.Parent != null)
            {
                TreeNode parent = GetNodeByTag(dest.Nodes.OfType<TreeNode>().ToList(), node.Parent.Tag);
                if (parent == null)
                {
                    parent = new TreeNode(node.Parent.Text);
                    parent.Tag = node.Parent.Tag;
                    source.Nodes.Remove(node);
                    parent.Nodes.Add(node);
                    dest.Nodes.Add(parent);
                }
                else
                {
                    source.Nodes.Remove(node);
                    parent.Nodes.Add(node);
                }
            }
            else
            {
                source.Nodes.Remove(node);
                dest.Nodes.Add(node);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (treeBlock.SelectedNode != null)
            {
                var node = treeBlock.SelectedNode;
                MoveSourceDestNodes(node, treeBlock, treeList);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(owner: this, Strings.SaveBlockList, "AmiIptvPlayer", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ParentalControl.Get().Clear();
                foreach (TreeNode groupNode in treeBlock.Nodes)
                {
                    foreach (TreeNode chNodel in groupNode.Nodes)
                    {

                        ParentalControl.Get().AddBlockList((ChannelInfo)chNodel.Tag);
                    }
                }
                if (File.Exists(Utils.CONF_PATH + "amiIptvParentalControl.json"))
                {
                    File.Delete(Utils.CONF_PATH + "amiIptvParentalControl.json");
                }
                using (StreamWriter file = File.CreateText(Utils.CONF_PATH + "amiIptvParentalControl.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, ParentalControl.Get().GetBlockList());
                }
                this.Close();
            }
        }
    }
}
