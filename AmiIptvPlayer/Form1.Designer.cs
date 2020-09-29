namespace AmiIptvPlayer
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chList = new System.Windows.Forms.ListView();
            this.chNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.loadingPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.logoChannel = new System.Windows.Forms.PictureBox();
            this.lbChName = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panelvideo = new System.Windows.Forms.Panel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.seekBar = new System.Windows.Forms.TrackBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbEPG = new System.Windows.Forms.Label();
            this.lbDuration = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.loadingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seekBar)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1433, 33);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.refreshListToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(185, 30);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // refreshListToolStripMenuItem
            // 
            this.refreshListToolStripMenuItem.Name = "refreshListToolStripMenuItem";
            this.refreshListToolStripMenuItem.Size = new System.Drawing.Size(185, 30);
            this.refreshListToolStripMenuItem.Text = "Refresh List";
            this.refreshListToolStripMenuItem.Click += new System.EventHandler(this.refreshListToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(185, 30);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(61, 29);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(169, 30);
            this.aboutToolStripMenuItem.Text = "About us";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // chList
            // 
            this.chList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chNumber,
            this.chName});
            this.chList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.chList.HideSelection = false;
            this.chList.Location = new System.Drawing.Point(12, 231);
            this.chList.Name = "chList";
            this.chList.Size = new System.Drawing.Size(572, 588);
            this.chList.TabIndex = 5;
            this.chList.UseCompatibleStateImageBehavior = false;
            this.chList.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // chNumber
            // 
            this.chNumber.Text = "Nº";
            // 
            // chName
            // 
            this.chName.Text = "Channel Name";
            this.chName.Width = 340;
            // 
            // loadingPanel
            // 
            this.loadingPanel.Controls.Add(this.label1);
            this.loadingPanel.Controls.Add(this.pgBar);
            this.loadingPanel.Location = new System.Drawing.Point(427, 193);
            this.loadingPanel.Name = "loadingPanel";
            this.loadingPanel.Size = new System.Drawing.Size(25, 12);
            this.loadingPanel.TabIndex = 9;
            this.loadingPanel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(277, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 46);
            this.label1.TabIndex = 1;
            this.label1.Text = "Loading...";
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(102, 239);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(555, 84);
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            this.pgBar.UseWaitCursor = true;
            // 
            // logoChannel
            // 
            this.logoChannel.Location = new System.Drawing.Point(25, 49);
            this.logoChannel.Name = "logoChannel";
            this.logoChannel.Size = new System.Drawing.Size(115, 105);
            this.logoChannel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoChannel.TabIndex = 10;
            this.logoChannel.TabStop = false;
            // 
            // lbChName
            // 
            this.lbChName.AutoSize = true;
            this.lbChName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbChName.Location = new System.Drawing.Point(177, 69);
            this.lbChName.Name = "lbChName";
            this.lbChName.Size = new System.Drawing.Size(0, 37);
            this.lbChName.TabIndex = 11;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(25, 193);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(207, 26);
            this.txtFilter.TabIndex = 17;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(251, 187);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 38);
            this.btnFilter.TabIndex = 18;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(346, 187);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 38);
            this.btnClear.TabIndex = 21;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.TabIndex = 22;
            this.label2.Text = "Filter list:";
            // 
            // panelvideo
            // 
            this.panelvideo.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelvideo.ForeColor = System.Drawing.Color.Black;
            this.panelvideo.Location = new System.Drawing.Point(607, 82);
            this.panelvideo.Name = "panelvideo";
            this.panelvideo.Size = new System.Drawing.Size(767, 387);
            this.panelvideo.TabIndex = 23;
            this.panelvideo.DoubleClick += new System.EventHandler(this.panelvideo_DoubleClick);
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Location = new System.Drawing.Point(604, 481);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(110, 35);
            this.btnPlayPause.TabIndex = 27;
            this.btnPlayPause.Text = "Play/Pause";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            this.btnPlayPause.Click += new System.EventHandler(this.btnPlayPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(738, 481);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(110, 35);
            this.btnStop.TabIndex = 27;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // seekBar
            // 
            this.seekBar.LargeChange = 0;
            this.seekBar.Location = new System.Drawing.Point(607, 522);
            this.seekBar.Name = "seekBar";
            this.seekBar.Size = new System.Drawing.Size(764, 69);
            this.seekBar.TabIndex = 28;
            this.seekBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.seekBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.seekBar_MouseDown);
            this.seekBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.seekBar_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbEPG);
            this.panel1.Location = new System.Drawing.Point(604, 574);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(770, 211);
            this.panel1.TabIndex = 25;
            // 
            // lbEPG
            // 
            this.lbEPG.AutoSize = true;
            this.lbEPG.Location = new System.Drawing.Point(14, 13);
            this.lbEPG.Name = "lbEPG";
            this.lbEPG.Size = new System.Drawing.Size(0, 20);
            this.lbEPG.TabIndex = 1;
            // 
            // lbDuration
            // 
            this.lbDuration.AutoSize = true;
            this.lbDuration.Location = new System.Drawing.Point(892, 488);
            this.lbDuration.Name = "lbDuration";
            this.lbDuration.Size = new System.Drawing.Size(120, 20);
            this.lbDuration.TabIndex = 29;
            this.lbDuration.Text = "Video Time: --/--";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1433, 846);
            this.Controls.Add(this.panelvideo);
            this.Controls.Add(this.lbDuration);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.seekBar);
            this.Controls.Add(this.btnPlayPause);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.lbChName);
            this.Controls.Add(this.logoChannel);
            this.Controls.Add(this.chList);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.loadingPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "AmiIptvPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.loadingPanel.ResumeLayout(false);
            this.loadingPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seekBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ListView chList;
        private System.Windows.Forms.ColumnHeader chNumber;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.Panel loadingPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.PictureBox logoChannel;
        private System.Windows.Forms.Label lbChName;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem refreshListToolStripMenuItem;
        private System.Windows.Forms.Panel panelvideo;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbEPG;
        private System.Windows.Forms.TrackBar seekBar;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Label lbDuration;
    }
}

