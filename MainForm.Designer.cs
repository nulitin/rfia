namespace 资源文件方向智能助手
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imgTree = new System.Windows.Forms.ImageList(this.components);
            this.imgFiles = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.sep3 = new System.Windows.Forms.ToolStripSeparator();
            this.sep4 = new System.Windows.Forms.ToolStripSeparator();
            this.sep6 = new System.Windows.Forms.ToolStripSeparator();
            this.sep5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnUp = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2Sep = new System.Windows.Forms.ToolStripSeparator();
            this.txtPath = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripProgress = new System.Windows.Forms.ToolStrip();
            this.lblProgress = new System.Windows.Forms.ToolStripLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tvFolders = new System.Windows.Forms.TreeView();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.ctxFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tmClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tm_tc = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRename = new System.Windows.Forms.ToolStripButton();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.btnShowHidden = new System.Windows.Forms.ToolStripButton();
            this.btnPlayH = new System.Windows.Forms.ToolStripButton();
            this.btnPlayV = new System.Windows.Forms.ToolStripButton();
            this.btnTsMp4 = new System.Windows.Forms.ToolStripButton();
            this.btnmonitors = new System.Windows.Forms.ToolStripButton();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.ts_about = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStripProgress.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.ctxFiles.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgTree
            // 
            this.imgTree.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgTree.ImageSize = new System.Drawing.Size(16, 16);
            this.imgTree.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imgFiles
            // 
            this.imgFiles.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgFiles.ImageSize = new System.Drawing.Size(16, 16);
            this.imgFiles.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRename,
            this.btnReset,
            this.sep1,
            this.btnShowHidden,
            this.sep2,
            this.btnPlayH,
            this.btnPlayV,
            this.sep3,
            this.btnTsMp4,
            this.sep4,
            this.btnmonitors,
            this.sep6,
            this.btnSettings,
            this.sep5,
            this.btnClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.MinimumSize = new System.Drawing.Size(0, 30);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.toolStrip1.Size = new System.Drawing.Size(909, 30);
            this.toolStrip1.TabIndex = 3;
            // 
            // sep1
            // 
            this.sep1.Name = "sep1";
            this.sep1.Size = new System.Drawing.Size(6, 24);
            // 
            // sep2
            // 
            this.sep2.Name = "sep2";
            this.sep2.Size = new System.Drawing.Size(6, 24);
            // 
            // sep3
            // 
            this.sep3.Name = "sep3";
            this.sep3.Size = new System.Drawing.Size(6, 24);
            // 
            // sep4
            // 
            this.sep4.Name = "sep4";
            this.sep4.Size = new System.Drawing.Size(6, 24);
            // 
            // sep6
            // 
            this.sep6.Name = "sep6";
            this.sep6.Size = new System.Drawing.Size(6, 24);
            // 
            // sep5
            // 
            this.sep5.Name = "sep5";
            this.sep5.Size = new System.Drawing.Size(6, 24);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUp,
            this.toolStrip2Sep,
            this.txtPath});
            this.toolStrip2.Location = new System.Drawing.Point(0, 55);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.toolStrip2.Size = new System.Drawing.Size(909, 28);
            this.toolStrip2.TabIndex = 2;
            // 
            // btnUp
            // 
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(36, 21);
            this.btnUp.Text = "向上";
            this.btnUp.ToolTipText = "返回上一级目录";
            // 
            // toolStrip2Sep
            // 
            this.toolStrip2Sep.Name = "toolStrip2Sep";
            this.toolStrip2Sep.Size = new System.Drawing.Size(6, 24);
            // 
            // txtPath
            // 
            this.txtPath.AutoSize = false;
            this.txtPath.BackColor = System.Drawing.Color.White;
            this.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPath.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(500, 23);
            // 
            // toolStripProgress
            // 
            this.toolStripProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.toolStripProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripProgress.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripProgress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblProgress,
            this.progressBar});
            this.toolStripProgress.Location = new System.Drawing.Point(0, 463);
            this.toolStripProgress.Name = "toolStripProgress";
            this.toolStripProgress.Size = new System.Drawing.Size(909, 25);
            this.toolStripProgress.TabIndex = 4;
            this.toolStripProgress.Visible = false;
            // 
            // lblProgress
            // 
            this.lblProgress.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblProgress.ForeColor = System.Drawing.Color.DarkRed;
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(53, 22);
            this.lblProgress.Text = "处理中...";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 22);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(155)))));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 488);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(909, 22);
            this.statusStrip.TabIndex = 5;
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.statusLabel.ForeColor = System.Drawing.Color.White;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(227, 17);
            this.statusLabel.Text = "就绪 | 资源文件方向智能助手 V2026-8-8";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 83);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(253)))));
            this.splitContainer.Panel1.Controls.Add(this.tvFolders);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer.Panel2.Controls.Add(this.lvFiles);
            this.splitContainer.Size = new System.Drawing.Size(909, 405);
            this.splitContainer.SplitterDistance = 303;
            this.splitContainer.TabIndex = 1;
            // 
            // tvFolders
            // 
            this.tvFolders.BackColor = System.Drawing.Color.White;
            this.tvFolders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFolders.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tvFolders.HideSelection = false;
            this.tvFolders.ImageIndex = 0;
            this.tvFolders.ImageList = this.imgTree;
            this.tvFolders.Location = new System.Drawing.Point(0, 0);
            this.tvFolders.Name = "tvFolders";
            this.tvFolders.SelectedImageIndex = 0;
            this.tvFolders.Size = new System.Drawing.Size(303, 405);
            this.tvFolders.TabIndex = 0;
            // 
            // lvFiles
            // 
            this.lvFiles.BackColor = System.Drawing.Color.White;
            this.lvFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvFiles.ContextMenuStrip = this.ctxFiles;
            this.lvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFiles.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.HideSelection = false;
            this.lvFiles.Location = new System.Drawing.Point(0, 0);
            this.lvFiles.MultiSelect = false;
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(602, 405);
            this.lvFiles.SmallImageList = this.imgFiles;
            this.lvFiles.TabIndex = 0;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            // 
            // ctxFiles
            // 
            this.ctxFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRefresh});
            this.ctxFiles.Name = "ctxFiles";
            this.ctxFiles.Size = new System.Drawing.Size(101, 26);
            // 
            // menuRefresh
            // 
            this.menuRefresh.Name = "menuRefresh";
            this.menuRefresh.Size = new System.Drawing.Size(100, 22);
            this.menuRefresh.Text = "刷新";
            // 
            // colName
            // 
            this.colName.Name = "colName";
            this.colName.Text = "名称";
            this.colName.Width = 380;
            // 
            // colType
            // 
            this.colType.Name = "colType";
            this.colType.Text = "类型";
            this.colType.Width = 80;
            // 
            // colSize
            // 
            this.colSize.Name = "colSize";
            this.colSize.Text = "大小";
            this.colSize.Width = 90;
            // 
            // colDate
            // 
            this.colDate.Name = "colDate";
            this.colDate.Text = "修改日期";
            this.colDate.Width = 140;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmClose,
            this.帮助HToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(909, 25);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tmClose
            // 
            this.tmClose.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tm_tc});
            this.tmClose.Name = "tmClose";
            this.tmClose.Size = new System.Drawing.Size(58, 21);
            this.tmClose.Text = "文件(&F)";
            // 
            // tm_tc
            // 
            this.tm_tc.Name = "tm_tc";
            this.tm_tc.Size = new System.Drawing.Size(180, 22);
            this.tm_tc.Text = "退出(&E)";
            this.tm_tc.Click += new System.EventHandler(this.tm_tc_Click);
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_about});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // btnRename
            // 
            this.btnRename.Image = ((System.Drawing.Image)(resources.GetObject("btnRename.Image")));
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(88, 21);
            this.btnRename.Text = "批量重命名";
            // 
            // btnReset
            // 
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(76, 21);
            this.btnReset.Text = "批量复位";
            // 
            // btnShowHidden
            // 
            this.btnShowHidden.Image = ((System.Drawing.Image)(resources.GetObject("btnShowHidden.Image")));
            this.btnShowHidden.Name = "btnShowHidden";
            this.btnShowHidden.Size = new System.Drawing.Size(112, 21);
            this.btnShowHidden.Text = "显示隐藏文件夹";
            // 
            // btnPlayH
            // 
            this.btnPlayH.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayH.Image")));
            this.btnPlayH.Name = "btnPlayH";
            this.btnPlayH.Size = new System.Drawing.Size(100, 21);
            this.btnPlayH.Text = "播放横向视频";
            // 
            // btnPlayV
            // 
            this.btnPlayV.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayV.Image")));
            this.btnPlayV.Name = "btnPlayV";
            this.btnPlayV.Size = new System.Drawing.Size(100, 21);
            this.btnPlayV.Text = "播放竖向视频";
            // 
            // btnTsMp4
            // 
            this.btnTsMp4.Image = ((System.Drawing.Image)(resources.GetObject("btnTsMp4.Image")));
            this.btnTsMp4.Name = "btnTsMp4";
            this.btnTsMp4.Size = new System.Drawing.Size(80, 21);
            this.btnTsMp4.Text = "TS转MP4";
            // 
            // btnmonitors
            // 
            this.btnmonitors.Image = ((System.Drawing.Image)(resources.GetObject("btnmonitors.Image")));
            this.btnmonitors.Name = "btnmonitors";
            this.btnmonitors.Size = new System.Drawing.Size(112, 21);
            this.btnmonitors.Text = "显示器方向切换";
            // 
            // btnSettings
            // 
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(52, 21);
            this.btnSettings.Text = "设置";
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(52, 21);
            this.btnClose.Text = "关闭";
            // 
            // ts_about
            // 
            this.ts_about.Name = "ts_about";
            this.ts_about.Size = new System.Drawing.Size(180, 22);
            this.ts_about.Text = "关于(&A)";
            this.ts_about.Click += new System.EventHandler(this.ts_about_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 510);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStripProgress);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "资源文件方向智能助手 V2026-8-8";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStripProgress.ResumeLayout(false);
            this.toolStripProgress.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ctxFiles.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region 控件声明

        private System.Windows.Forms.ImageList imgTree;
        private System.Windows.Forms.ImageList imgFiles;

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRename;
        private System.Windows.Forms.ToolStripButton btnReset;
        private System.Windows.Forms.ToolStripSeparator sep1;
        private System.Windows.Forms.ToolStripButton btnShowHidden;
        private System.Windows.Forms.ToolStripSeparator sep2;
        private System.Windows.Forms.ToolStripButton btnPlayH;
        private System.Windows.Forms.ToolStripButton btnPlayV;
        private System.Windows.Forms.ToolStripSeparator sep3;
        private System.Windows.Forms.ToolStripButton btnTsMp4;
        private System.Windows.Forms.ToolStripSeparator sep4;
        private System.Windows.Forms.ToolStripSeparator sep6;
        private System.Windows.Forms.ToolStripButton btnSettings;
        private System.Windows.Forms.ToolStripSeparator sep5;
        private System.Windows.Forms.ToolStripButton btnClose;

        // 路径显示 TextBox（嵌入工具栏）
        private System.Windows.Forms.ToolStripTextBox txtPath;

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnUp;
        private System.Windows.Forms.ToolStripSeparator toolStrip2Sep;

        private System.Windows.Forms.ToolStrip toolStripProgress;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripLabel lblProgress;

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView tvFolders;

        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colDate;

        private System.Windows.Forms.ContextMenuStrip ctxFiles;
        private System.Windows.Forms.ToolStripMenuItem menuRefresh;

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tmClose;
        private System.Windows.Forms.ToolStripMenuItem tm_tc;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnmonitors;
        private System.Windows.Forms.ToolStripMenuItem ts_about;
    }
}
