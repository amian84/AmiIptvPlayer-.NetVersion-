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
            this.refreshEPGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.logoEPG = new System.Windows.Forms.PictureBox();
            this.lbEndTime = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbStartTime = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbStars = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbDescription = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbTitleEPG = new System.Windows.Forms.Label();
            this.lbDuration = new System.Windows.Forms.Label();
            this.lbVersionText = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbProcessingEPG = new System.Windows.Forms.Label();
            this.btnFixId = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.loadingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seekBar)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoEPG)).BeginInit();
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(936, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.refreshListToolStripMenuItem,
            this.refreshEPGToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // refreshListToolStripMenuItem
            // 
            this.refreshListToolStripMenuItem.Name = "refreshListToolStripMenuItem";
            this.refreshListToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.refreshListToolStripMenuItem.Text = "Refresh List";
            this.refreshListToolStripMenuItem.Click += new System.EventHandler(this.refreshListToolStripMenuItem_Click);
            // 
            // refreshEPGToolStripMenuItem
            // 
            this.refreshEPGToolStripMenuItem.Name = "refreshEPGToolStripMenuItem";
            this.refreshEPGToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.refreshEPGToolStripMenuItem.Text = "Refresh EPG";
            this.refreshEPGToolStripMenuItem.Click += new System.EventHandler(this.refreshEPGToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
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
            this.chList.Location = new System.Drawing.Point(8, 150);
            this.chList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chList.Name = "chList";
            this.chList.Size = new System.Drawing.Size(383, 475);
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
            this.loadingPanel.Location = new System.Drawing.Point(285, 125);
            this.loadingPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.loadingPanel.Name = "loadingPanel";
            this.loadingPanel.Size = new System.Drawing.Size(17, 8);
            this.loadingPanel.TabIndex = 9;
            this.loadingPanel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(185, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Loading...";
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(68, 154);
            this.pgBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(370, 55);
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            this.pgBar.UseWaitCursor = true;
            // 
            // logoChannel
            // 
            this.logoChannel.Location = new System.Drawing.Point(17, 32);
            this.logoChannel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.logoChannel.Name = "logoChannel";
            this.logoChannel.Size = new System.Drawing.Size(77, 68);
            this.logoChannel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoChannel.TabIndex = 10;
            this.logoChannel.TabStop = false;
            // 
            // lbChName
            // 
            this.lbChName.AutoSize = true;
            this.lbChName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbChName.Location = new System.Drawing.Point(118, 45);
            this.lbChName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbChName.Name = "lbChName";
            this.lbChName.Size = new System.Drawing.Size(0, 26);
            this.lbChName.TabIndex = 11;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(17, 125);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(139, 20);
            this.txtFilter.TabIndex = 17;
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(166, 122);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(50, 24);
            this.btnFilter.TabIndex = 18;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(231, 122);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(50, 24);
            this.btnClear.TabIndex = 21;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 107);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Filter list:";
            // 
            // panelvideo
            // 
            this.panelvideo.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelvideo.ForeColor = System.Drawing.Color.Black;
            this.panelvideo.Location = new System.Drawing.Point(405, 54);
            this.panelvideo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelvideo.Name = "panelvideo";
            this.panelvideo.Size = new System.Drawing.Size(511, 252);
            this.panelvideo.TabIndex = 23;
            this.panelvideo.DoubleClick += new System.EventHandler(this.panelvideo_DoubleClick);
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Location = new System.Drawing.Point(403, 314);
            this.btnPlayPause.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(74, 23);
            this.btnPlayPause.TabIndex = 27;
            this.btnPlayPause.Text = "Play/Pause";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            this.btnPlayPause.Click += new System.EventHandler(this.btnPlayPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(492, 314);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(74, 23);
            this.btnStop.TabIndex = 27;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // seekBar
            // 
            this.seekBar.LargeChange = 0;
            this.seekBar.Location = new System.Drawing.Point(405, 340);
            this.seekBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.seekBar.Name = "seekBar";
            this.seekBar.Size = new System.Drawing.Size(509, 45);
            this.seekBar.TabIndex = 28;
            this.seekBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.seekBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.seekBar_MouseDown);
            this.seekBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.seekBar_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFixId);
            this.panel1.Controls.Add(this.logoEPG);
            this.panel1.Controls.Add(this.lbEndTime);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lbStartTime);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lbStars);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lbDescription);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbTitleEPG);
            this.panel1.Location = new System.Drawing.Point(403, 373);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 210);
            this.panel1.TabIndex = 25;
            // 
            // logoEPG
            // 
            this.logoEPG.Location = new System.Drawing.Point(491, 2);
            this.logoEPG.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.logoEPG.Name = "logoEPG";
            this.logoEPG.Size = new System.Drawing.Size(20, 19);
            this.logoEPG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoEPG.TabIndex = 11;
            this.logoEPG.TabStop = false;
            this.logoEPG.Click += new System.EventHandler(this.logoEPG_Click);
            this.logoEPG.MouseHover += new System.EventHandler(this.logoEPG_MouseHover);
            // 
            // lbEndTime
            // 
            this.lbEndTime.AutoSize = true;
            this.lbEndTime.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEndTime.Location = new System.Drawing.Point(315, 176);
            this.lbEndTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbEndTime.Name = "lbEndTime";
            this.lbEndTime.Size = new System.Drawing.Size(12, 14);
            this.lbEndTime.TabIndex = 10;
            this.lbEndTime.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(209, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 16);
            this.label8.TabIndex = 9;
            this.label8.Text = "End Time:";
            // 
            // lbStartTime
            // 
            this.lbStartTime.AutoSize = true;
            this.lbStartTime.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStartTime.Location = new System.Drawing.Point(110, 174);
            this.lbStartTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbStartTime.Name = "lbStartTime";
            this.lbStartTime.Size = new System.Drawing.Size(12, 14);
            this.lbStartTime.TabIndex = 8;
            this.lbStartTime.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Start Time:";
            // 
            // lbStars
            // 
            this.lbStars.AutoSize = true;
            this.lbStars.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStars.Location = new System.Drawing.Point(110, 150);
            this.lbStars.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbStars.Name = "lbStars";
            this.lbStars.Size = new System.Drawing.Size(12, 14);
            this.lbStars.TabIndex = 6;
            this.lbStars.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Stars:";
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescription.Location = new System.Drawing.Point(110, 62);
            this.lbDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDescription.MaximumSize = new System.Drawing.Size(400, 0);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(12, 13);
            this.lbDescription.TabIndex = 4;
            this.lbDescription.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Description:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Title:";
            // 
            // lbTitleEPG
            // 
            this.lbTitleEPG.AutoSize = true;
            this.lbTitleEPG.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitleEPG.Location = new System.Drawing.Point(110, 13);
            this.lbTitleEPG.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTitleEPG.Name = "lbTitleEPG";
            this.lbTitleEPG.Size = new System.Drawing.Size(12, 14);
            this.lbTitleEPG.TabIndex = 1;
            this.lbTitleEPG.Text = "-";
            // 
            // lbDuration
            // 
            this.lbDuration.AutoSize = true;
            this.lbDuration.Location = new System.Drawing.Point(595, 317);
            this.lbDuration.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDuration.Name = "lbDuration";
            this.lbDuration.Size = new System.Drawing.Size(83, 13);
            this.lbDuration.TabIndex = 29;
            this.lbDuration.Text = "Video Time: --/--";
            // 
            // lbVersionText
            // 
            this.lbVersionText.AutoSize = true;
            this.lbVersionText.Location = new System.Drawing.Point(816, 592);
            this.lbVersionText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbVersionText.Name = "lbVersionText";
            this.lbVersionText.Size = new System.Drawing.Size(45, 13);
            this.lbVersionText.TabIndex = 30;
            this.lbVersionText.Text = "Version:";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(872, 592);
            this.lbVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(40, 13);
            this.lbVersion.TabIndex = 31;
            this.lbVersion.Text = "0.0.0.0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(790, 611);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "EPG Loaded:";
            // 
            // lbProcessingEPG
            // 
            this.lbProcessingEPG.AutoSize = true;
            this.lbProcessingEPG.Location = new System.Drawing.Point(872, 611);
            this.lbProcessingEPG.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbProcessingEPG.Name = "lbProcessingEPG";
            this.lbProcessingEPG.Size = new System.Drawing.Size(23, 13);
            this.lbProcessingEPG.TabIndex = 33;
            this.lbProcessingEPG.Text = "NO";
            // 
            // btnFixId
            // 
            this.btnFixId.Location = new System.Drawing.Point(318, 150);
            this.btnFixId.Margin = new System.Windows.Forms.Padding(2);
            this.btnFixId.Name = "btnFixId";
            this.btnFixId.Size = new System.Drawing.Size(140, 24);
            this.btnFixId.TabIndex = 19;
            this.btnFixId.Text = "Fix Identification";
            this.btnFixId.UseVisualStyleBackColor = true;
            this.btnFixId.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 641);
            this.Controls.Add(this.lbProcessingEPG);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.lbVersionText);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            ((System.ComponentModel.ISupportInitialize)(this.logoEPG)).EndInit();
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
        private System.Windows.Forms.Label lbTitleEPG;
        private System.Windows.Forms.TrackBar seekBar;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Label lbDuration;
        private System.Windows.Forms.PictureBox logoEPG;
        private System.Windows.Forms.Label lbEndTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbStartTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbStars;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbVersionText;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbProcessingEPG;
        private System.Windows.Forms.ToolStripMenuItem refreshEPGToolStripMenuItem;
        private System.Windows.Forms.Button btnFixId;
    }
}

