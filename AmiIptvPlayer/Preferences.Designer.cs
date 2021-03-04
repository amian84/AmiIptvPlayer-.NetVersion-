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
            this.lbSettingsURL = new System.Windows.Forms.Label();
            this.lbSettingsEPG = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
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
            this.SuspendLayout();
            // 
            // lbSettingsURL
            // 
            this.lbSettingsURL.AutoSize = true;
            this.lbSettingsURL.Location = new System.Drawing.Point(12, 22);
            this.lbSettingsURL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSettingsURL.Name = "lbSettingsURL";
            this.lbSettingsURL.Size = new System.Drawing.Size(44, 13);
            this.lbSettingsURL.TabIndex = 0;
            this.lbSettingsURL.Text = "Url Iptv:";
            // 
            // lbSettingsEPG
            // 
            this.lbSettingsEPG.AutoSize = true;
            this.lbSettingsEPG.Location = new System.Drawing.Point(11, 54);
            this.lbSettingsEPG.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSettingsEPG.Name = "lbSettingsEPG";
            this.lbSettingsEPG.Size = new System.Drawing.Size(48, 13);
            this.lbSettingsEPG.TabIndex = 1;
            this.lbSettingsEPG.Text = "Url EPG:";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(121, 19);
            this.txtURL.Margin = new System.Windows.Forms.Padding(2);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(233, 20);
            this.txtURL.TabIndex = 2;
            this.txtURL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtURL_KeyPress);
            // 
            // txtEPG
            // 
            this.txtEPG.Location = new System.Drawing.Point(121, 51);
            this.txtEPG.Margin = new System.Windows.Forms.Padding(2);
            this.txtEPG.Name = "txtEPG";
            this.txtEPG.Size = new System.Drawing.Size(233, 20);
            this.txtEPG.TabIndex = 3;
            this.txtEPG.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEPG_KeyPress);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(201, 263);
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
            this.btnCancel.Location = new System.Drawing.Point(287, 263);
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
            this.lbSettingsDefAud.Location = new System.Drawing.Point(11, 81);
            this.lbSettingsDefAud.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSettingsDefAud.Name = "lbSettingsDefAud";
            this.lbSettingsDefAud.Size = new System.Drawing.Size(101, 13);
            this.lbSettingsDefAud.TabIndex = 6;
            this.lbSettingsDefAud.Text = "Default Audio Lang:";
            // 
            // audio
            // 
            this.audio.FormattingEnabled = true;
            this.audio.Location = new System.Drawing.Point(214, 78);
            this.audio.Margin = new System.Windows.Forms.Padding(2);
            this.audio.Name = "audio";
            this.audio.Size = new System.Drawing.Size(140, 21);
            this.audio.TabIndex = 7;
            // 
            // lbSettingsDefSub
            // 
            this.lbSettingsDefSub.AutoSize = true;
            this.lbSettingsDefSub.Location = new System.Drawing.Point(11, 110);
            this.lbSettingsDefSub.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSettingsDefSub.Name = "lbSettingsDefSub";
            this.lbSettingsDefSub.Size = new System.Drawing.Size(93, 13);
            this.lbSettingsDefSub.TabIndex = 8;
            this.lbSettingsDefSub.Text = "Default Sub Lang:";
            // 
            // sub
            // 
            this.sub.FormattingEnabled = true;
            this.sub.Location = new System.Drawing.Point(214, 107);
            this.sub.Margin = new System.Windows.Forms.Padding(2);
            this.sub.Name = "sub";
            this.sub.Size = new System.Drawing.Size(140, 21);
            this.sub.TabIndex = 9;
            // 
            // lbSettingsUILang
            // 
            this.lbSettingsUILang.AutoSize = true;
            this.lbSettingsUILang.Location = new System.Drawing.Point(11, 141);
            this.lbSettingsUILang.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSettingsUILang.Name = "lbSettingsUILang";
            this.lbSettingsUILang.Size = new System.Drawing.Size(72, 13);
            this.lbSettingsUILang.TabIndex = 10;
            this.lbSettingsUILang.Text = "UI Language:";
            // 
            // cmbUI
            // 
            this.cmbUI.FormattingEnabled = true;
            this.cmbUI.Location = new System.Drawing.Point(214, 138);
            this.cmbUI.Margin = new System.Windows.Forms.Padding(2);
            this.cmbUI.Name = "cmbUI";
            this.cmbUI.Size = new System.Drawing.Size(140, 21);
            this.cmbUI.TabIndex = 11;
            this.cmbUI.SelectedIndexChanged += new System.EventHandler(this.cmbUI_SelectedIndexChanged);
            // 
            // lbRequestEmail
            // 
            this.lbRequestEmail.AutoSize = true;
            this.lbRequestEmail.Location = new System.Drawing.Point(12, 173);
            this.lbRequestEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbRequestEmail.Name = "lbRequestEmail";
            this.lbRequestEmail.Size = new System.Drawing.Size(78, 13);
            this.lbRequestEmail.TabIndex = 12;
            this.lbRequestEmail.Text = "Request Email:";
            // 
            // txtRequestEmail
            // 
            this.txtRequestEmail.Location = new System.Drawing.Point(214, 170);
            this.txtRequestEmail.Margin = new System.Windows.Forms.Padding(2);
            this.txtRequestEmail.Name = "txtRequestEmail";
            this.txtRequestEmail.Size = new System.Drawing.Size(140, 20);
            this.txtRequestEmail.TabIndex = 13;
            // 
            // txtParentalControl
            // 
            this.txtParentalControl.Location = new System.Drawing.Point(213, 200);
            this.txtParentalControl.Margin = new System.Windows.Forms.Padding(2);
            this.txtParentalControl.Name = "txtParentalControl";
            this.txtParentalControl.PasswordChar = '*';
            this.txtParentalControl.Size = new System.Drawing.Size(140, 20);
            this.txtParentalControl.TabIndex = 15;
            // 
            // lbParentalControl
            // 
            this.lbParentalControl.AutoSize = true;
            this.lbParentalControl.Location = new System.Drawing.Point(11, 203);
            this.lbParentalControl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbParentalControl.Name = "lbParentalControl";
            this.lbParentalControl.Size = new System.Drawing.Size(78, 13);
            this.lbParentalControl.TabIndex = 14;
            this.lbParentalControl.Text = "Request Email:";
            // 
            // Preferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 313);
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
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.lbSettingsEPG);
            this.Controls.Add(this.lbSettingsURL);
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

        private System.Windows.Forms.Label lbSettingsURL;
        private System.Windows.Forms.Label lbSettingsEPG;
        private System.Windows.Forms.TextBox txtURL;
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
    }
}