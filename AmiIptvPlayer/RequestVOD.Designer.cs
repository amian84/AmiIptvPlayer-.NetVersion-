namespace AmiIptvPlayer
{
    partial class RequestVOD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestVOD));
            this.btnExit = new System.Windows.Forms.Button();
            this.lbTitleSeach = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.cmbTypes = new System.Windows.Forms.ComboBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbYear = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.logoPRG = new System.Windows.Forms.PictureBox();
            this.foundList = new System.Windows.Forms.ListView();
            this.title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.origtitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.overview = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.country = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.year = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbSendAdition = new System.Windows.Forms.Label();
            this.chSenAdition = new System.Windows.Forms.CheckBox();
            this.grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPRG)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(210, 150);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(118, 23);
            this.btnExit.TabIndex = 15;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lbTitleSeach
            // 
            this.lbTitleSeach.AutoSize = true;
            this.lbTitleSeach.Location = new System.Drawing.Point(16, 151);
            this.lbTitleSeach.Name = "lbTitleSeach";
            this.lbTitleSeach.Size = new System.Drawing.Size(30, 13);
            this.lbTitleSeach.TabIndex = 14;
            this.lbTitleSeach.Text = "Title:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(66, 148);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(195, 20);
            this.txtSearch.TabIndex = 13;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(285, 146);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(119, 23);
            this.btnSearch.TabIndex = 12;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(92, 28);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(236, 20);
            this.txtTitle.TabIndex = 16;
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(270, 57);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(58, 20);
            this.txtYear.TabIndex = 17;
            // 
            // cmbTypes
            // 
            this.cmbTypes.FormattingEnabled = true;
            this.cmbTypes.Location = new System.Drawing.Point(207, 83);
            this.cmbTypes.Name = "cmbTypes";
            this.cmbTypes.Size = new System.Drawing.Size(121, 21);
            this.cmbTypes.TabIndex = 18;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Location = new System.Drawing.Point(12, 31);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(30, 13);
            this.lbTitle.TabIndex = 19;
            this.lbTitle.Text = "Title:";
            // 
            // lbYear
            // 
            this.lbYear.AutoSize = true;
            this.lbYear.Location = new System.Drawing.Point(12, 57);
            this.lbYear.Name = "lbYear";
            this.lbYear.Size = new System.Drawing.Size(32, 13);
            this.lbYear.TabIndex = 20;
            this.lbYear.Text = "Year:";
            // 
            // lbType
            // 
            this.lbType.AutoSize = true;
            this.lbType.Location = new System.Drawing.Point(14, 83);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(34, 13);
            this.lbType.TabIndex = 21;
            this.lbType.Text = "Type:";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(334, 150);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(119, 23);
            this.btnSend.TabIndex = 22;
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.logoPRG);
            this.grpSearch.Controls.Add(this.foundList);
            this.grpSearch.Controls.Add(this.txtSearch);
            this.grpSearch.Controls.Add(this.lbTitleSeach);
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Location = new System.Drawing.Point(26, 179);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(668, 191);
            this.grpSearch.TabIndex = 23;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Search:";
            // 
            // logoPRG
            // 
            this.logoPRG.Location = new System.Drawing.Point(565, 18);
            this.logoPRG.Margin = new System.Windows.Forms.Padding(2);
            this.logoPRG.Name = "logoPRG";
            this.logoPRG.Size = new System.Drawing.Size(86, 109);
            this.logoPRG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPRG.TabIndex = 24;
            this.logoPRG.TabStop = false;
            // 
            // foundList
            // 
            this.foundList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.title,
            this.origtitle,
            this.overview,
            this.country,
            this.year});
            this.foundList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.foundList.HideSelection = false;
            this.foundList.Location = new System.Drawing.Point(19, 25);
            this.foundList.Name = "foundList";
            this.foundList.Size = new System.Drawing.Size(541, 102);
            this.foundList.TabIndex = 6;
            this.foundList.UseCompatibleStateImageBehavior = false;
            this.foundList.SelectedIndexChanged += new System.EventHandler(this.foundList_SelectedIndexChanged);
            this.foundList.DoubleClick += new System.EventHandler(this.foundList_DoubleClick);
            // 
            // title
            // 
            this.title.Text = "Title";
            this.title.Width = 150;
            // 
            // origtitle
            // 
            this.origtitle.Width = 150;
            // 
            // overview
            // 
            this.overview.Width = 400;
            // 
            // year
            // 
            this.year.Text = "Year";
            // 
            // lbSendAdition
            // 
            this.lbSendAdition.AutoSize = true;
            this.lbSendAdition.Location = new System.Drawing.Point(14, 113);
            this.lbSendAdition.Name = "lbSendAdition";
            this.lbSendAdition.Size = new System.Drawing.Size(51, 13);
            this.lbSendAdition.TabIndex = 24;
            this.lbSendAdition.Text = "lbLoaded";
            // 
            // chSenAdition
            // 
            this.chSenAdition.AutoSize = true;
            this.chSenAdition.Location = new System.Drawing.Point(311, 113);
            this.chSenAdition.Name = "chSenAdition";
            this.chSenAdition.Size = new System.Drawing.Size(15, 14);
            this.chSenAdition.TabIndex = 25;
            this.chSenAdition.UseVisualStyleBackColor = true;
            this.chSenAdition.CheckedChanged += new System.EventHandler(this.chSenAdition_CheckedChanged);
            // 
            // RequestVOD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 383);
            this.Controls.Add(this.chSenAdition);
            this.Controls.Add(this.lbSendAdition);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lbType);
            this.Controls.Add(this.lbYear);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.cmbTypes);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RequestVOD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RequestVOD";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.RequestVOD_Load);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPRG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lbTitleSeach;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.ComboBox cmbTypes;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbYear;
        private System.Windows.Forms.Label lbType;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.ListView foundList;
        private System.Windows.Forms.PictureBox logoPRG;
        private System.Windows.Forms.ColumnHeader title;
        private System.Windows.Forms.ColumnHeader year;
        private System.Windows.Forms.ColumnHeader origtitle;
        private System.Windows.Forms.ColumnHeader country;
        private System.Windows.Forms.ColumnHeader overview;
        private System.Windows.Forms.Label lbSendAdition;
        private System.Windows.Forms.CheckBox chSenAdition;
    }
}