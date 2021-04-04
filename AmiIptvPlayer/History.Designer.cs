namespace AmiIptvPlayer
{
    partial class History
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(History));
            this.historyList = new System.Windows.Forms.ListView();
            this.title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.year = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbHisotry = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMoreInfo = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // historyList
            // 
            this.historyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.title,
            this.year});
            this.historyList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.historyList.HideSelection = false;
            this.historyList.Location = new System.Drawing.Point(12, 53);
            this.historyList.Name = "historyList";
            this.historyList.Size = new System.Drawing.Size(390, 149);
            this.historyList.TabIndex = 7;
            this.historyList.UseCompatibleStateImageBehavior = false;
            this.historyList.DoubleClick += new System.EventHandler(this.historyList_DoubleClick);
            // 
            // title
            // 
            this.title.Text = "Title";
            this.title.Width = 305;
            // 
            // year
            // 
            this.year.Text = "Year";
            this.year.Width = 75;
            // 
            // lbHisotry
            // 
            this.lbHisotry.AutoSize = true;
            this.lbHisotry.Location = new System.Drawing.Point(12, 27);
            this.lbHisotry.Name = "lbHisotry";
            this.lbHisotry.Size = new System.Drawing.Size(42, 13);
            this.lbHisotry.TabIndex = 8;
            this.lbHisotry.Text = "History:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(327, 220);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMoreInfo
            // 
            this.btnMoreInfo.Location = new System.Drawing.Point(93, 220);
            this.btnMoreInfo.Name = "btnMoreInfo";
            this.btnMoreInfo.Size = new System.Drawing.Size(128, 23);
            this.btnMoreInfo.TabIndex = 10;
            this.btnMoreInfo.Text = "button1";
            this.btnMoreInfo.UseVisualStyleBackColor = true;
            this.btnMoreInfo.Click += new System.EventHandler(this.btnMoreInfo_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(12, 220);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "button2";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 258);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnMoreInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbHisotry);
            this.Controls.Add(this.historyList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "History";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AmiIptvPlayer: History";
            this.Load += new System.EventHandler(this.History_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView historyList;
        private System.Windows.Forms.ColumnHeader title;
        private System.Windows.Forms.ColumnHeader year;
        private System.Windows.Forms.Label lbHisotry;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMoreInfo;
        private System.Windows.Forms.Button btnDelete;
    }
}