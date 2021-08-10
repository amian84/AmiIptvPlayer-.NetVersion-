namespace AmiIptvPlayer
{
    partial class Preferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Preferences));
            this.lbSettingsEPG = new System.Windows.Forms.Label();
            this.txtEPG = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbSettingsDefAud = new System.Windows.Forms.Label();
            this.audio = new System.Windows.Forms.ComboBox();
            this.lbSettingsDefSub = new System.Windows.Forms.Label();
            this.sub = new System.Windows.Forms.ComboBox();
            this.lbSettingsUILang = new System.Windows.Forms.Label();
            this.cmbUI = new System.Windows.Forms.ComboBox();
            this.lbRequestEmail = new System.Windows.Forms.Label();
            this.txtRequestEmail = new System.Windows.Forms.TextBox();
            this.txtParentalControl = new System.Windows.Forms.TextBox();
            this.lbParentalControl = new System.Windows.Forms.Label();
            this.lbLog = new System.Windows.Forms.Label();
            this.chLog = new System.Windows.Forms.CheckBox();
            this.chAutoPlayEpisodes = new System.Windows.Forms.CheckBox();
            this.lbAutoPlayEpisodes = new System.Windows.Forms.Label();
            this.lbDonate = new System.Windows.Forms.Label();
            this.chDonate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbSettingsEPG
            // 
            this.lbSettingsEPG.AutoSize = true;
            this.lbSettingsEPG.Location = new System.Drawing.Point(11, 17);
            this.lbSettingsEPG.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSettingsEPG.Name = "lbSettingsEPG";
            this.lbSettingsEPG.Size = new System.Drawing.Size(48, 13);
            this.lbSettingsEPG.TabIndex = 1;
            this.lbSettingsEPG.Text = "Url EPG:";
            // 
            // txtEPG
            // 
            this.txtEPG.Location = new System.Drawing.Point(121, 14);
            this.txtEPG.Margin = new System.Windows.Forms.Padding(2);
            this.txtEPG.Name = "txtEPG";
            this.txtEPG.Size = new System.Drawing.Size(233, 20);
            this.txtEPG.TabIndex = 3;
            this.txtEPG.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEPG_KeyPress);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(201, 310);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(66, 30);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Save";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(287, 310);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCancel.Size = new System.Drawing.Size(66, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbSettingsDefAud
            // 
            this.lbSettingsDefAud.AutoSize = true;
            this.lbSettingsDefAud.Location = new System.Drawing.Point(11, 44);
            this.lbSettingsDefAud.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSettingsDefAud.Name = "lbSettingsDefAud";
            this.lbSettingsDefAud.Size = new System.Drawing.Size(101, 13);
            this.lbSettingsDefAud.TabIndex = 6;
            this.lbSettingsDefAud.Text = "Default Audio Lang:";
            // 
            // audio
            // 
            this.audio.FormattingEnabled = true;
            this.audio.Location = new System.Drawing.Point(214, 41);
            this.audio.Margin = new System.Windows.Forms.Padding(2);
            this.audio.Name = "audio";
            this.audio.Size = new System.Drawing.Size(140, 21);
            this.audio.TabIndex = 7;
            // 
            // lbSettingsDefSub
            // 
            this.lbSettingsDefSub.AutoSize = true;
            this.lbSettingsDefSub.Location = new System.Drawing.Point(11, 73);
            this.lbSettingsDefSub.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSettingsDefSub.Name = "lbSettingsDefSub";
            this.lbSettingsDefSub.Size = new System.Drawing.Size(93, 13);
            this.lbSettingsDefSub.TabIndex = 8;
            this.lbSettingsDefSub.Text = "Default Sub Lang:";
            // 
            // sub
            // 
            this.sub.FormattingEnabled = true;
            this.sub.Location = new System.Drawing.Point(214, 70);
            this.sub.Margin = new System.Windows.Forms.Padding(2);
            this.sub.Name = "sub";
            this.sub.Size = new System.Drawing.Size(140, 21);
            this.sub.TabIndex = 9;
            // 
            // lbSettingsUILang
            // 
            this.lbSettingsUILang.AutoSize = true;
            this.lbSettingsUILang.Location = new System.Drawing.Point(11, 104);
            this.lbSettingsUILang.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSettingsUILang.Name = "lbSettingsUILang";
            this.lbSettingsUILang.Size = new System.Drawing.Size(72, 13);
            this.lbSettingsUILang.TabIndex = 10;
            this.lbSettingsUILang.Text = "UI Language:";
            // 
            // cmbUI
            // 
            this.cmbUI.FormattingEnabled = true;
            this.cmbUI.Location = new System.Drawing.Point(214, 101);
            this.cmbUI.Margin = new System.Windows.Forms.Padding(2);
            this.cmbUI.Name = "cmbUI";
            this.cmbUI.Size = new System.Drawing.Size(140, 21);
            this.cmbUI.TabIndex = 11;
            this.cmbUI.SelectedIndexChanged += new System.EventHandler(this.cmbUI_SelectedIndexChanged);
            // 
            // lbRequestEmail
            // 
            this.lbRequestEmail.AutoSize = true;
            this.lbRequestEmail.Location = new System.Drawing.Point(12, 136);
            this.lbRequestEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbRequestEmail.Name = "lbRequestEmail";
            this.lbRequestEmail.Size = new System.Drawing.Size(78, 13);
            this.lbRequestEmail.TabIndex = 12;
            this.lbRequestEmail.Text = "Request Email:";
            // 
            // txtRequestEmail
            // 
            this.txtRequestEmail.Location = new System.Drawing.Point(214, 133);
            this.txtRequestEmail.Margin = new System.Windows.Forms.Padding(2);
            this.txtRequestEmail.Name = "txtRequestEmail";
            this.txtRequestEmail.Size = new System.Drawing.Size(140, 20);
            this.txtRequestEmail.TabIndex = 13;
            // 
            // txtParentalControl
            // 
            this.txtParentalControl.Location = new System.Drawing.Point(213, 163);
            this.txtParentalControl.Margin = new System.Windows.Forms.Padding(2);
            this.txtParentalControl.Name = "txtParentalControl";
            this.txtParentalControl.PasswordChar = '*';
            this.txtParentalControl.Size = new System.Drawing.Size(140, 20);
            this.txtParentalControl.TabIndex = 15;
            // 
            // lbParentalControl
            // 
            this.lbParentalControl.AutoSize = true;
            this.lbParentalControl.Location = new System.Drawing.Point(11, 166);
            this.lbParentalControl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbParentalControl.Name = "lbParentalControl";
            this.lbParentalControl.Size = new System.Drawing.Size(78, 13);
            this.lbParentalControl.TabIndex = 14;
            this.lbParentalControl.Text = "Request Email:";
            // 
            // lbLog
            // 
            this.lbLog.AutoSize = true;
            this.lbLog.Location = new System.Drawing.Point(11, 196);
            this.lbLog.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(78, 13);
            this.lbLog.TabIndex = 16;
            this.lbLog.Text = "Request Email:";
            // 
            // chLog
            // 
            this.chLog.AutoSize = true;
            this.chLog.Location = new System.Drawing.Point(334, 196);
            this.chLog.Name = "chLog";
            this.chLog.Size = new System.Drawing.Size(15, 14);
            this.chLog.TabIndex = 17;
            this.chLog.UseVisualStyleBackColor = true;
            // 
            // chAutoPlayEpisodes
            // 
            this.chAutoPlayEpisodes.AutoSize = true;
            this.chAutoPlayEpisodes.Location = new System.Drawing.Point(334, 220);
            this.chAutoPlayEpisodes.Name = "chAutoPlayEpisodes";
            this.chAutoPlayEpisodes.Size = new System.Drawing.Size(15, 14);
            this.chAutoPlayEpisodes.TabIndex = 19;
            this.chAutoPlayEpisodes.UseVisualStyleBackColor = true;
            // 
            // lbAutoPlayEpisodes
            // 
            this.lbAutoPlayEpisodes.AutoSize = true;
            this.lbAutoPlayEpisodes.Location = new System.Drawing.Point(11, 220);
            this.lbAutoPlayEpisodes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbAutoPlayEpisodes.Name = "lbAutoPlayEpisodes";
            this.lbAutoPlayEpisodes.Size = new System.Drawing.Size(64, 13);
            this.lbAutoPlayEpisodes.TabIndex = 18;
            this.lbAutoPlayEpisodes.Text = "AUTOPLAY";
            // 
            // lbDonate
            // 
            this.lbDonate.AutoSize = true;
            this.lbDonate.Location = new System.Drawing.Point(12, 247);
            this.lbDonate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDonate.Name = "lbDonate";
            this.lbDonate.Size = new System.Drawing.Size(52, 13);
            this.lbDonate.TabIndex = 20;
            this.lbDonate.Text = "DONATE";
            // 
            // chDonate
            // 
            this.chDonate.AutoSize = true;
            this.chDonate.Location = new System.Drawing.Point(334, 246);
            this.chDonate.Name = "chDonate";
            this.chDonate.Size = new System.Drawing.Size(15, 14);
            this.chDonate.TabIndex = 21;
            this.chDonate.UseVisualStyleBackColor = true;
            // 
            // Preferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 360);
            this.Controls.Add(this.chDonate);
            this.Controls.Add(this.lbDonate);
            this.Controls.Add(this.chAutoPlayEpisodes);
            this.Controls.Add(this.lbAutoPlayEpisodes);
            this.Controls.Add(this.chLog);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.txtParentalControl);
            this.Controls.Add(this.lbParentalControl);
            this.Controls.Add(this.txtRequestEmail);
            this.Controls.Add(this.lbRequestEmail);
            this.Controls.Add(this.cmbUI);
            this.Controls.Add(this.lbSettingsUILang);
            this.Controls.Add(this.sub);
            this.Controls.Add(this.lbSettingsDefSub);
            this.Controls.Add(this.audio);
            this.Controls.Add(this.lbSettingsDefAud);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtEPG);
            this.Controls.Add(this.lbSettingsEPG);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Preferences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AmiIPTVPlayer: Preferences";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Preferences_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbSettingsEPG;
        private System.Windows.Forms.TextBox txtEPG;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbSettingsDefAud;
        private System.Windows.Forms.ComboBox audio;
        private System.Windows.Forms.Label lbSettingsDefSub;
        private System.Windows.Forms.ComboBox sub;
        private System.Windows.Forms.Label lbSettingsUILang;
        private System.Windows.Forms.ComboBox cmbUI;
        private System.Windows.Forms.Label lbRequestEmail;
        private System.Windows.Forms.TextBox txtRequestEmail;
        private System.Windows.Forms.TextBox txtParentalControl;
        private System.Windows.Forms.Label lbParentalControl;
        private System.Windows.Forms.Label lbLog;
        private System.Windows.Forms.CheckBox chLog;
        private System.Windows.Forms.CheckBox chAutoPlayEpisodes;
        private System.Windows.Forms.Label lbAutoPlayEpisodes;
        private System.Windows.Forms.Label lbDonate;
        private System.Windows.Forms.CheckBox chDonate;
    }
}