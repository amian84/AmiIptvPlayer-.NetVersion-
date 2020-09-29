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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.txtEPG = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.audio = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sub = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Url Iptv:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Url EPG:";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(182, 30);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(347, 26);
            this.txtURL.TabIndex = 2;
            this.txtURL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtURL_KeyPress);
            // 
            // txtEPG
            // 
            this.txtEPG.Location = new System.Drawing.Point(182, 78);
            this.txtEPG.Name = "txtEPG";
            this.txtEPG.Size = new System.Drawing.Size(347, 26);
            this.txtEPG.TabIndex = 3;
            this.txtEPG.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEPG_KeyPress);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(302, 234);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(99, 46);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Save";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(430, 234);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCancel.Size = new System.Drawing.Size(99, 46);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Default Audio Lang:";
            // 
            // audio
            // 
            this.audio.FormattingEnabled = true;
            this.audio.Items.AddRange(new object[] {
            "Spanish",
            "English"});
            this.audio.Location = new System.Drawing.Point(321, 120);
            this.audio.Name = "audio";
            this.audio.Size = new System.Drawing.Size(208, 28);
            this.audio.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Default Sub Lang:";
            // 
            // sub
            // 
            this.sub.FormattingEnabled = true;
            this.sub.Items.AddRange(new object[] {
            "None",
            "Spanish",
            "English"});
            this.sub.Location = new System.Drawing.Point(321, 164);
            this.sub.Name = "sub";
            this.sub.Size = new System.Drawing.Size(208, 28);
            this.sub.TabIndex = 9;
            // 
            // Preferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 305);
            this.Controls.Add(this.sub);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.audio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtEPG);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.TextBox txtEPG;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox audio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox sub;
    }
}