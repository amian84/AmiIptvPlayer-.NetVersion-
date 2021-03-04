namespace AmiIptvPlayer
{
    partial class ParentalControlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParentalControlForm));
            this.treeList = new System.Windows.Forms.TreeView();
            this.treeBlock = new System.Windows.Forms.TreeView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lbChannelList = new System.Windows.Forms.Label();
            this.lbBlockList = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeList
            // 
            this.treeList.Location = new System.Drawing.Point(12, 45);
            this.treeList.Name = "treeList";
            this.treeList.Size = new System.Drawing.Size(275, 419);
            this.treeList.TabIndex = 0;
            // 
            // treeBlock
            // 
            this.treeBlock.Location = new System.Drawing.Point(407, 45);
            this.treeBlock.Name = "treeBlock";
            this.treeBlock.Size = new System.Drawing.Size(275, 419);
            this.treeBlock.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(322, 106);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(51, 51);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(322, 183);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(51, 51);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(308, 395);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 28);
            this.btnSave.TabIndex = 4;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(308, 436);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 28);
            this.btnExit.TabIndex = 5;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lbChannelList
            // 
            this.lbChannelList.AutoSize = true;
            this.lbChannelList.Location = new System.Drawing.Point(13, 26);
            this.lbChannelList.Name = "lbChannelList";
            this.lbChannelList.Size = new System.Drawing.Size(35, 13);
            this.lbChannelList.TabIndex = 6;
            this.lbChannelList.Text = "label1";
            // 
            // lbBlockList
            // 
            this.lbBlockList.AutoSize = true;
            this.lbBlockList.Location = new System.Drawing.Point(404, 29);
            this.lbBlockList.Name = "lbBlockList";
            this.lbBlockList.Size = new System.Drawing.Size(35, 13);
            this.lbBlockList.TabIndex = 7;
            this.lbBlockList.Text = "label1";
            // 
            // ParentalControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 506);
            this.Controls.Add(this.lbBlockList);
            this.Controls.Add(this.lbChannelList);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.treeBlock);
            this.Controls.Add(this.treeList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ParentalControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Parental Control";
            this.Load += new System.EventHandler(this.ParentalControlForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeList;
        private System.Windows.Forms.TreeView treeBlock;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lbChannelList;
        private System.Windows.Forms.Label lbBlockList;
    }
}