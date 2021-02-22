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
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.lbFilterList = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbYear = new System.Windows.Forms.Label();
            this.lbYearsTitle = new System.Windows.Forms.Label();
            this.btnFixId = new System.Windows.Forms.Button();
            this.logoEPG = new System.Windows.Forms.PictureBox();
            this.lbEndTime = new System.Windows.Forms.Label();
            this.lbEndTimeTitle = new System.Windows.Forms.Label();
            this.lbStartTime = new System.Windows.Forms.Label();
            this.lbStartTimeTitle = new System.Windows.Forms.Label();
            this.lbStars = new System.Windows.Forms.Label();
            this.lbStarsTitle = new System.Windows.Forms.Label();
            this.lbDescription = new System.Windows.Forms.Label();
            this.lbDescriptionTitle = new System.Windows.Forms.Label();
            this.lbTitleTitle = new System.Windows.Forms.Label();
            this.lbTitleEPG = new System.Windows.Forms.Label();
            this.lbVersionText = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.lbEPGLoaded = new System.Windows.Forms.Label();
            this.lbProcessingEPG = new System.Windows.Forms.Label();
            this.txtLoadCh = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnURLInfo = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbGroups = new System.Windows.Forms.ComboBox();
            this.lbGroups = new System.Windows.Forms.Label();
            this.contextMenuChannel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemChnSeen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChnResum = new System.Windows.Forms.ToolStripMenuItem();
            this.requestNewVODToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.loadingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoChannel)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoEPG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.contextMenuChannel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1022, 24);
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
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountInfoToolStripMenuItem,
            this.requestNewVODToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // accountInfoToolStripMenuItem
            // 
            this.accountInfoToolStripMenuItem.Name = "accountInfoToolStripMenuItem";
            this.accountInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.accountInfoToolStripMenuItem.Text = "Account Info";
            this.accountInfoToolStripMenuItem.Click += new System.EventHandler(this.accountInfoToolStripMenuItem_Click);
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
            this.chList.Location = new System.Drawing.Point(8, 197);
            this.chList.Margin = new System.Windows.Forms.Padding(2);
            this.chList.Name = "chList";
            this.chList.Size = new System.Drawing.Size(383, 616);
            this.chList.TabIndex = 5;
            this.chList.UseCompatibleStateImageBehavior = false;
            this.chList.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.chList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chList_MouseDown);
            this.chList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chList_MouseUp);
            // 
            // chNumber
            // 
            this.chNumber.Text = "Nº";
            // 
            // chName
            // 
            this.chName.Text = "Channel Name";
            this.chName.Width = 310;
            // 
            // loadingPanel
            // 
            this.loadingPanel.Controls.Add(this.label1);
            this.loadingPanel.Controls.Add(this.pgBar);
            this.loadingPanel.Location = new System.Drawing.Point(0, 26);
            this.loadingPanel.Margin = new System.Windows.Forms.Padding(2);
            this.loadingPanel.Name = "loadingPanel";
            this.loadingPanel.Size = new System.Drawing.Size(13, 10);
            this.loadingPanel.TabIndex = 9;
            this.loadingPanel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(417, 297);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Loading...";
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(300, 379);
            this.pgBar.Margin = new System.Windows.Forms.Padding(2);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(370, 55);
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            this.pgBar.UseWaitCursor = true;
            // 
            // logoChannel
            // 
            this.logoChannel.Location = new System.Drawing.Point(17, 32);
            this.logoChannel.Margin = new System.Windows.Forms.Padding(2);
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
            this.txtFilter.Location = new System.Drawing.Point(17, 169);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(2);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(139, 20);
            this.txtFilter.TabIndex = 17;
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(175, 165);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(50, 24);
            this.btnFilter.TabIndex = 18;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(238, 166);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(50, 24);
            this.btnClear.TabIndex = 21;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click_1);
            // 
            // lbFilterList
            // 
            this.lbFilterList.AutoSize = true;
            this.lbFilterList.Location = new System.Drawing.Point(21, 151);
            this.lbFilterList.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFilterList.Name = "lbFilterList";
            this.lbFilterList.Size = new System.Drawing.Size(47, 13);
            this.lbFilterList.TabIndex = 22;
            this.lbFilterList.Text = "Filter list:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbYear);
            this.panel1.Controls.Add(this.lbYearsTitle);
            this.panel1.Controls.Add(this.btnFixId);
            this.panel1.Controls.Add(this.logoEPG);
            this.panel1.Controls.Add(this.lbEndTime);
            this.panel1.Controls.Add(this.lbEndTimeTitle);
            this.panel1.Controls.Add(this.lbStartTime);
            this.panel1.Controls.Add(this.lbStartTimeTitle);
            this.panel1.Controls.Add(this.lbStars);
            this.panel1.Controls.Add(this.lbStarsTitle);
            this.panel1.Controls.Add(this.lbDescription);
            this.panel1.Controls.Add(this.lbDescriptionTitle);
            this.panel1.Controls.Add(this.lbTitleTitle);
            this.panel1.Controls.Add(this.lbTitleEPG);
            this.panel1.Location = new System.Drawing.Point(403, 551);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(594, 250);
            this.panel1.TabIndex = 25;
            // 
            // lbYear
            // 
            this.lbYear.AutoSize = true;
            this.lbYear.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbYear.Location = new System.Drawing.Point(110, 175);
            this.lbYear.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbYear.Name = "lbYear";
            this.lbYear.Size = new System.Drawing.Size(12, 14);
            this.lbYear.TabIndex = 21;
            this.lbYear.Text = "-";
            this.lbYear.Visible = false;
            // 
            // lbYearsTitle
            // 
            this.lbYearsTitle.AutoSize = true;
            this.lbYearsTitle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbYearsTitle.Location = new System.Drawing.Point(4, 174);
            this.lbYearsTitle.Name = "lbYearsTitle";
            this.lbYearsTitle.Size = new System.Drawing.Size(105, 16);
            this.lbYearsTitle.TabIndex = 20;
            this.lbYearsTitle.Text = "Year release:";
            this.lbYearsTitle.Visible = false;
            // 
            // btnFixId
            // 
            this.btnFixId.Location = new System.Drawing.Point(428, 197);
            this.btnFixId.Margin = new System.Windows.Forms.Padding(2);
            this.btnFixId.Name = "btnFixId";
            this.btnFixId.Size = new System.Drawing.Size(140, 24);
            this.btnFixId.TabIndex = 19;
            this.btnFixId.Text = "Fix Identification";
            this.btnFixId.UseVisualStyleBackColor = true;
            this.btnFixId.Visible = false;
            this.btnFixId.Click += new System.EventHandler(this.btnFixId_Click);
            // 
            // logoEPG
            // 
            this.logoEPG.Location = new System.Drawing.Point(562, 2);
            this.logoEPG.Margin = new System.Windows.Forms.Padding(2);
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
            this.lbEndTime.Location = new System.Drawing.Point(210, 207);
            this.lbEndTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbEndTime.Name = "lbEndTime";
            this.lbEndTime.Size = new System.Drawing.Size(12, 14);
            this.lbEndTime.TabIndex = 10;
            this.lbEndTime.Text = "-";
            // 
            // lbEndTimeTitle
            // 
            this.lbEndTimeTitle.AutoSize = true;
            this.lbEndTimeTitle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEndTimeTitle.Location = new System.Drawing.Point(4, 206);
            this.lbEndTimeTitle.Name = "lbEndTimeTitle";
            this.lbEndTimeTitle.Size = new System.Drawing.Size(79, 16);
            this.lbEndTimeTitle.TabIndex = 9;
            this.lbEndTimeTitle.Text = "End Time:";
            // 
            // lbStartTime
            // 
            this.lbStartTime.AutoSize = true;
            this.lbStartTime.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStartTime.Location = new System.Drawing.Point(210, 174);
            this.lbStartTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbStartTime.Name = "lbStartTime";
            this.lbStartTime.Size = new System.Drawing.Size(12, 14);
            this.lbStartTime.TabIndex = 8;
            this.lbStartTime.Text = "-";
            // 
            // lbStartTimeTitle
            // 
            this.lbStartTimeTitle.AutoSize = true;
            this.lbStartTimeTitle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStartTimeTitle.Location = new System.Drawing.Point(4, 174);
            this.lbStartTimeTitle.Name = "lbStartTimeTitle";
            this.lbStartTimeTitle.Size = new System.Drawing.Size(88, 16);
            this.lbStartTimeTitle.TabIndex = 7;
            this.lbStartTimeTitle.Text = "Start Time:";
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
            // lbStarsTitle
            // 
            this.lbStarsTitle.AutoSize = true;
            this.lbStarsTitle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStarsTitle.Location = new System.Drawing.Point(4, 148);
            this.lbStarsTitle.Name = "lbStarsTitle";
            this.lbStarsTitle.Size = new System.Drawing.Size(51, 16);
            this.lbStarsTitle.TabIndex = 5;
            this.lbStarsTitle.Text = "Stars:";
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
            // lbDescriptionTitle
            // 
            this.lbDescriptionTitle.AutoSize = true;
            this.lbDescriptionTitle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescriptionTitle.Location = new System.Drawing.Point(4, 62);
            this.lbDescriptionTitle.Name = "lbDescriptionTitle";
            this.lbDescriptionTitle.Size = new System.Drawing.Size(95, 16);
            this.lbDescriptionTitle.TabIndex = 3;
            this.lbDescriptionTitle.Text = "Description:";
            // 
            // lbTitleTitle
            // 
            this.lbTitleTitle.AutoSize = true;
            this.lbTitleTitle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitleTitle.Location = new System.Drawing.Point(4, 11);
            this.lbTitleTitle.Name = "lbTitleTitle";
            this.lbTitleTitle.Size = new System.Drawing.Size(44, 16);
            this.lbTitleTitle.TabIndex = 2;
            this.lbTitleTitle.Text = "Title:";
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
            // lbVersionText
            // 
            this.lbVersionText.AutoSize = true;
            this.lbVersionText.Location = new System.Drawing.Point(892, 803);
            this.lbVersionText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbVersionText.Name = "lbVersionText";
            this.lbVersionText.Size = new System.Drawing.Size(45, 13);
            this.lbVersionText.TabIndex = 30;
            this.lbVersionText.Text = "Version:";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(948, 803);
            this.lbVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(40, 13);
            this.lbVersion.TabIndex = 31;
            this.lbVersion.Text = "0.0.0.0";
            // 
            // lbEPGLoaded
            // 
            this.lbEPGLoaded.AutoSize = true;
            this.lbEPGLoaded.Location = new System.Drawing.Point(866, 822);
            this.lbEPGLoaded.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbEPGLoaded.Name = "lbEPGLoaded";
            this.lbEPGLoaded.Size = new System.Drawing.Size(71, 13);
            this.lbEPGLoaded.TabIndex = 32;
            this.lbEPGLoaded.Text = "EPG Loaded:";
            // 
            // lbProcessingEPG
            // 
            this.lbProcessingEPG.AutoSize = true;
            this.lbProcessingEPG.Location = new System.Drawing.Point(948, 822);
            this.lbProcessingEPG.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbProcessingEPG.Name = "lbProcessingEPG";
            this.lbProcessingEPG.Size = new System.Drawing.Size(23, 13);
            this.lbProcessingEPG.TabIndex = 33;
            this.lbProcessingEPG.Text = "NO";
            // 
            // txtLoadCh
            // 
            this.txtLoadCh.Location = new System.Drawing.Point(8, 197);
            this.txtLoadCh.Multiline = true;
            this.txtLoadCh.Name = "txtLoadCh";
            this.txtLoadCh.Size = new System.Drawing.Size(383, 616);
            this.txtLoadCh.TabIndex = 34;
            this.txtLoadCh.Text = "LOADwwwwwwwwwwwwwwwwwwwwwwwww";
            this.txtLoadCh.Visible = false;
            // 
            // btnURLInfo
            // 
            this.btnURLInfo.Location = new System.Drawing.Point(403, 516);
            this.btnURLInfo.Margin = new System.Windows.Forms.Padding(2);
            this.btnURLInfo.Name = "btnURLInfo";
            this.btnURLInfo.Size = new System.Drawing.Size(135, 27);
            this.btnURLInfo.TabIndex = 42;
            this.btnURLInfo.Text = "Get Track URL";
            this.btnURLInfo.UseVisualStyleBackColor = true;
            this.btnURLInfo.Click += new System.EventHandler(this.btnURLInfo_Click);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(403, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(594, 478);
            this.panel2.TabIndex = 44;
            // 
            // cmbGroups
            // 
            this.cmbGroups.FormattingEnabled = true;
            this.cmbGroups.Location = new System.Drawing.Point(17, 127);
            this.cmbGroups.Name = "cmbGroups";
            this.cmbGroups.Size = new System.Drawing.Size(227, 21);
            this.cmbGroups.TabIndex = 45;
            this.cmbGroups.SelectedIndexChanged += new System.EventHandler(this.cmbGroups_SelectedIndexChanged);
            // 
            // lbGroups
            // 
            this.lbGroups.AutoSize = true;
            this.lbGroups.Location = new System.Drawing.Point(20, 110);
            this.lbGroups.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbGroups.Name = "lbGroups";
            this.lbGroups.Size = new System.Drawing.Size(44, 13);
            this.lbGroups.TabIndex = 46;
            this.lbGroups.Text = "Groups:";
            // 
            // contextMenuChannel
            // 
            this.contextMenuChannel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemChnSeen,
            this.menuItemChnResum});
            this.contextMenuChannel.Name = "contextMenuChannel";
            this.contextMenuChannel.Size = new System.Drawing.Size(68, 48);
            // 
            // menuItemChnSeen
            // 
            this.menuItemChnSeen.Name = "menuItemChnSeen";
            this.menuItemChnSeen.Size = new System.Drawing.Size(67, 22);
            this.menuItemChnSeen.Click += new System.EventHandler(this.menuItemChnSeen_Click);
            // 
            // menuItemChnResum
            // 
            this.menuItemChnResum.Name = "menuItemChnResum";
            this.menuItemChnResum.Size = new System.Drawing.Size(67, 22);
            this.menuItemChnResum.Click += new System.EventHandler(this.menuItemChnResum_Click);
            // 
            // requestNewVODToolStripMenuItem
            // 
            this.requestNewVODToolStripMenuItem.Name = "requestNewVODToolStripMenuItem";
            this.requestNewVODToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.requestNewVODToolStripMenuItem.Text = "Request new VOD";
            this.requestNewVODToolStripMenuItem.Click += new System.EventHandler(this.requestNewVODToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 851);
            this.Controls.Add(this.lbGroups);
            this.Controls.Add(this.cmbGroups);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnURLInfo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbProcessingEPG);
            this.Controls.Add(this.lbEPGLoaded);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.lbVersionText);
            this.Controls.Add(this.lbFilterList);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.lbChName);
            this.Controls.Add(this.logoChannel);
            this.Controls.Add(this.chList);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.loadingPanel);
            this.Controls.Add(this.txtLoadCh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoEPG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.contextMenuChannel.ResumeLayout(false);
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
        private System.Windows.Forms.Label lbFilterList;
        private System.Windows.Forms.ToolStripMenuItem refreshListToolStripMenuItem;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbTitleEPG;
        private System.Windows.Forms.PictureBox logoEPG;
        private System.Windows.Forms.Label lbEndTime;
        private System.Windows.Forms.Label lbEndTimeTitle;
        private System.Windows.Forms.Label lbStartTime;
        private System.Windows.Forms.Label lbStartTimeTitle;
        private System.Windows.Forms.Label lbStars;
        private System.Windows.Forms.Label lbStarsTitle;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.Label lbDescriptionTitle;
        private System.Windows.Forms.Label lbTitleTitle;
        private System.Windows.Forms.Label lbVersionText;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label lbEPGLoaded;
        private System.Windows.Forms.Label lbProcessingEPG;
        private System.Windows.Forms.ToolStripMenuItem refreshEPGToolStripMenuItem;
        private System.Windows.Forms.Button btnFixId;
        private System.Windows.Forms.Label lbYear;
        private System.Windows.Forms.Label lbYearsTitle;
        private System.Windows.Forms.TextBox txtLoadCh;
        private System.Windows.Forms.Button btnURLInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbGroups;
        private System.Windows.Forms.Label lbGroups;
        private System.Windows.Forms.ContextMenuStrip contextMenuChannel;
        private System.Windows.Forms.ToolStripMenuItem menuItemChnSeen;
        private System.Windows.Forms.ToolStripMenuItem menuItemChnResum;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem requestNewVODToolStripMenuItem;
    }
}

