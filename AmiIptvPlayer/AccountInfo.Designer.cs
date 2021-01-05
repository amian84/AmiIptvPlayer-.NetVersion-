namespace AmiIptvPlayer
{
    partial class AccountInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountInfo));
            this.lbHeader = new System.Windows.Forms.Label();
            this.lbServerText = new System.Windows.Forms.Label();
            this.lbUserText = new System.Windows.Forms.Label();
            this.lbExpText = new System.Windows.Forms.Label();
            this.lbMaxConText = new System.Windows.Forms.Label();
            this.lbActiveConsText = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lbServer = new System.Windows.Forms.Label();
            this.lbUser = new System.Windows.Forms.Label();
            this.lbExp = new System.Windows.Forms.Label();
            this.lbMaxCon = new System.Windows.Forms.Label();
            this.lbActiveCons = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbHeader
            // 
            this.lbHeader.AutoSize = true;
            this.lbHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.Location = new System.Drawing.Point(12, 19);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(208, 20);
            this.lbHeader.TabIndex = 0;
            this.lbHeader.Text = "This is your account info:";
            // 
            // lbServerText
            // 
            this.lbServerText.AutoSize = true;
            this.lbServerText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbServerText.Location = new System.Drawing.Point(22, 51);
            this.lbServerText.Name = "lbServerText";
            this.lbServerText.Size = new System.Drawing.Size(59, 20);
            this.lbServerText.TabIndex = 1;
            this.lbServerText.Text = "Server:";
            // 
            // lbUserText
            // 
            this.lbUserText.AutoSize = true;
            this.lbUserText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUserText.Location = new System.Drawing.Point(22, 75);
            this.lbUserText.Name = "lbUserText";
            this.lbUserText.Size = new System.Drawing.Size(47, 20);
            this.lbUserText.TabIndex = 2;
            this.lbUserText.Text = "User:";
            // 
            // lbExpText
            // 
            this.lbExpText.AutoSize = true;
            this.lbExpText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExpText.Location = new System.Drawing.Point(22, 102);
            this.lbExpText.Name = "lbExpText";
            this.lbExpText.Size = new System.Drawing.Size(122, 20);
            this.lbExpText.TabIndex = 3;
            this.lbExpText.Text = "Expiration Date:";
            // 
            // lbMaxConText
            // 
            this.lbMaxConText.AutoSize = true;
            this.lbMaxConText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMaxConText.Location = new System.Drawing.Point(22, 127);
            this.lbMaxConText.Name = "lbMaxConText";
            this.lbMaxConText.Size = new System.Drawing.Size(135, 20);
            this.lbMaxConText.TabIndex = 4;
            this.lbMaxConText.Text = "Max Connections:";
            // 
            // lbActiveConsText
            // 
            this.lbActiveConsText.AutoSize = true;
            this.lbActiveConsText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbActiveConsText.Location = new System.Drawing.Point(22, 152);
            this.lbActiveConsText.Name = "lbActiveConsText";
            this.lbActiveConsText.Size = new System.Drawing.Size(149, 20);
            this.lbActiveConsText.TabIndex = 5;
            this.lbActiveConsText.Text = "Active Connections:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(422, 198);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(327, 198);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lbServer
            // 
            this.lbServer.AutoSize = true;
            this.lbServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbServer.Location = new System.Drawing.Point(329, 51);
            this.lbServer.Name = "lbServer";
            this.lbServer.Size = new System.Drawing.Size(14, 20);
            this.lbServer.TabIndex = 8;
            this.lbServer.Text = "-";
            // 
            // lbUser
            // 
            this.lbUser.AutoSize = true;
            this.lbUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUser.Location = new System.Drawing.Point(329, 75);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(14, 20);
            this.lbUser.TabIndex = 9;
            this.lbUser.Text = "-";
            // 
            // lbExp
            // 
            this.lbExp.AutoSize = true;
            this.lbExp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExp.Location = new System.Drawing.Point(329, 102);
            this.lbExp.Name = "lbExp";
            this.lbExp.Size = new System.Drawing.Size(14, 20);
            this.lbExp.TabIndex = 10;
            this.lbExp.Text = "-";
            // 
            // lbMaxCon
            // 
            this.lbMaxCon.AutoSize = true;
            this.lbMaxCon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMaxCon.Location = new System.Drawing.Point(329, 127);
            this.lbMaxCon.Name = "lbMaxCon";
            this.lbMaxCon.Size = new System.Drawing.Size(14, 20);
            this.lbMaxCon.TabIndex = 11;
            this.lbMaxCon.Text = "-";
            // 
            // lbActiveCons
            // 
            this.lbActiveCons.AutoSize = true;
            this.lbActiveCons.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbActiveCons.Location = new System.Drawing.Point(329, 152);
            this.lbActiveCons.Name = "lbActiveCons";
            this.lbActiveCons.Size = new System.Drawing.Size(14, 20);
            this.lbActiveCons.TabIndex = 12;
            this.lbActiveCons.Text = "-";
            // 
            // AccountInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 233);
            this.Controls.Add(this.lbActiveCons);
            this.Controls.Add(this.lbMaxCon);
            this.Controls.Add(this.lbExp);
            this.Controls.Add(this.lbUser);
            this.Controls.Add(this.lbServer);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbActiveConsText);
            this.Controls.Add(this.lbMaxConText);
            this.Controls.Add(this.lbExpText);
            this.Controls.Add(this.lbUserText);
            this.Controls.Add(this.lbServerText);
            this.Controls.Add(this.lbHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AccountInfo";
            this.Load += new System.EventHandler(this.AccountInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbHeader;
        private System.Windows.Forms.Label lbServerText;
        private System.Windows.Forms.Label lbUserText;
        private System.Windows.Forms.Label lbExpText;
        private System.Windows.Forms.Label lbMaxConText;
        private System.Windows.Forms.Label lbActiveConsText;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lbServer;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Label lbExp;
        private System.Windows.Forms.Label lbMaxCon;
        private System.Windows.Forms.Label lbActiveCons;
    }
}