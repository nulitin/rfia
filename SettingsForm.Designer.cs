namespace 资源文件方向智能助手
{
    partial class SettingsForm
    {
        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblGroupPlayer = new System.Windows.Forms.Label();
            this.lblPotPlayer = new System.Windows.Forms.Label();
            this.txtPotPlayer = new System.Windows.Forms.TextBox();
            this.btnBrowsePot = new System.Windows.Forms.Button();
            this.lblGroupConv = new System.Windows.Forms.Label();
            this.lblFFmpeg = new System.Windows.Forms.Label();
            this.txtFFmpeg = new System.Windows.Forms.TextBox();
            this.btnBrowseFfmpeg = new System.Windows.Forms.Button();
            this.lblFFprobe = new System.Windows.Forms.Label();
            this.txtFFprobe = new System.Windows.Forms.TextBox();
            this.btnBrowseFfprobe = new System.Windows.Forms.Button();
            this.lblGroupNaming = new System.Windows.Forms.Label();
            this.lblHPrefix = new System.Windows.Forms.Label();
            this.txtHPrefix = new System.Windows.Forms.TextBox();
            this.lblVPrefix = new System.Windows.Forms.Label();
            this.txtVPrefix = new System.Windows.Forms.TextBox();
            this.lblGroupScope = new System.Windows.Forms.Label();
            this.chkIncludeSubdir = new System.Windows.Forms.CheckBox();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(155)))));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(310, 412);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 38);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "💾 保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnClose.Location = new System.Drawing.Point(440, 412);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 38);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.AutoScroll = true;
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.lblGroupPlayer);
            this.panelMain.Controls.Add(this.lblPotPlayer);
            this.panelMain.Controls.Add(this.txtPotPlayer);
            this.panelMain.Controls.Add(this.btnBrowsePot);
            this.panelMain.Controls.Add(this.lblGroupConv);
            this.panelMain.Controls.Add(this.lblFFmpeg);
            this.panelMain.Controls.Add(this.txtFFmpeg);
            this.panelMain.Controls.Add(this.btnBrowseFfmpeg);
            this.panelMain.Controls.Add(this.lblFFprobe);
            this.panelMain.Controls.Add(this.txtFFprobe);
            this.panelMain.Controls.Add(this.btnBrowseFfprobe);
            this.panelMain.Controls.Add(this.lblGroupNaming);
            this.panelMain.Controls.Add(this.lblHPrefix);
            this.panelMain.Controls.Add(this.txtHPrefix);
            this.panelMain.Controls.Add(this.lblVPrefix);
            this.panelMain.Controls.Add(this.txtVPrefix);
            this.panelMain.Controls.Add(this.lblGroupScope);
            this.panelMain.Controls.Add(this.chkIncludeSubdir);
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.panelMain.Size = new System.Drawing.Size(580, 400);
            this.panelMain.TabIndex = 3;
            // 
            // lblGroupPlayer
            // 
            this.lblGroupPlayer.AutoSize = true;
            this.lblGroupPlayer.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblGroupPlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(155)))));
            this.lblGroupPlayer.Location = new System.Drawing.Point(8, 8);
            this.lblGroupPlayer.Name = "lblGroupPlayer";
            this.lblGroupPlayer.Size = new System.Drawing.Size(87, 19);
            this.lblGroupPlayer.TabIndex = 0;
            this.lblGroupPlayer.Text = "▎播放器设置";
            // 
            // lblPotPlayer
            // 
            this.lblPotPlayer.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblPotPlayer.Location = new System.Drawing.Point(16, 40);
            this.lblPotPlayer.Name = "lblPotPlayer";
            this.lblPotPlayer.Size = new System.Drawing.Size(120, 22);
            this.lblPotPlayer.TabIndex = 1;
            this.lblPotPlayer.Text = "PotPlayer 路径:";
            // 
            // txtPotPlayer
            // 
            this.txtPotPlayer.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtPotPlayer.Location = new System.Drawing.Point(140, 38);
            this.txtPotPlayer.Name = "txtPotPlayer";
            this.txtPotPlayer.Size = new System.Drawing.Size(280, 23);
            this.txtPotPlayer.TabIndex = 2;
            this.txtPotPlayer.Text = "C:\\Program Files\\DAUM\\PotPlayer\\PotPlayer.exe";
            // 
            // btnBrowsePot
            // 
            this.btnBrowsePot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnBrowsePot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePot.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.btnBrowsePot.Location = new System.Drawing.Point(426, 37);
            this.btnBrowsePot.Name = "btnBrowsePot";
            this.btnBrowsePot.Size = new System.Drawing.Size(80, 26);
            this.btnBrowsePot.TabIndex = 3;
            this.btnBrowsePot.Text = "浏览...";
            this.btnBrowsePot.UseVisualStyleBackColor = false;
            this.btnBrowsePot.Click += new System.EventHandler(this.BtnBrowsePot_Click);
            // 
            // lblGroupConv
            // 
            this.lblGroupConv.AutoSize = true;
            this.lblGroupConv.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblGroupConv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(155)))));
            this.lblGroupConv.Location = new System.Drawing.Point(8, 80);
            this.lblGroupConv.Name = "lblGroupConv";
            this.lblGroupConv.Size = new System.Drawing.Size(101, 19);
            this.lblGroupConv.TabIndex = 4;
            this.lblGroupConv.Text = "▎转码工具设置";
            // 
            // lblFFmpeg
            // 
            this.lblFFmpeg.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblFFmpeg.Location = new System.Drawing.Point(16, 112);
            this.lblFFmpeg.Name = "lblFFmpeg";
            this.lblFFmpeg.Size = new System.Drawing.Size(120, 22);
            this.lblFFmpeg.TabIndex = 5;
            this.lblFFmpeg.Text = "FFmpeg 路径:";
            // 
            // txtFFmpeg
            // 
            this.txtFFmpeg.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtFFmpeg.Location = new System.Drawing.Point(140, 110);
            this.txtFFmpeg.Name = "txtFFmpeg";
            this.txtFFmpeg.Size = new System.Drawing.Size(280, 23);
            this.txtFFmpeg.TabIndex = 6;
            this.txtFFmpeg.Text = "C:\\Program Files\\Microsoft Visual Studio\\18\\Community\\Common7\\IDE\\ffmpeg.exe";
            // 
            // btnBrowseFfmpeg
            // 
            this.btnBrowseFfmpeg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnBrowseFfmpeg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseFfmpeg.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.btnBrowseFfmpeg.Location = new System.Drawing.Point(426, 109);
            this.btnBrowseFfmpeg.Name = "btnBrowseFfmpeg";
            this.btnBrowseFfmpeg.Size = new System.Drawing.Size(80, 26);
            this.btnBrowseFfmpeg.TabIndex = 7;
            this.btnBrowseFfmpeg.Text = "浏览...";
            this.btnBrowseFfmpeg.UseVisualStyleBackColor = false;
            this.btnBrowseFfmpeg.Click += new System.EventHandler(this.BtnBrowseFfmpeg_Click);
            // 
            // lblFFprobe
            // 
            this.lblFFprobe.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblFFprobe.Location = new System.Drawing.Point(16, 144);
            this.lblFFprobe.Name = "lblFFprobe";
            this.lblFFprobe.Size = new System.Drawing.Size(120, 22);
            this.lblFFprobe.TabIndex = 8;
            this.lblFFprobe.Text = "FFprobe 路径:";
            // 
            // txtFFprobe
            // 
            this.txtFFprobe.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtFFprobe.Location = new System.Drawing.Point(140, 142);
            this.txtFFprobe.Name = "txtFFprobe";
            this.txtFFprobe.Size = new System.Drawing.Size(280, 23);
            this.txtFFprobe.TabIndex = 9;
            this.txtFFprobe.Text = "C:\\Program Files\\Microsoft Visual Studio\\18\\Community\\Common7\\IDE\\ffprobe.exe";
            // 
            // btnBrowseFfprobe
            // 
            this.btnBrowseFfprobe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnBrowseFfprobe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseFfprobe.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.btnBrowseFfprobe.Location = new System.Drawing.Point(426, 141);
            this.btnBrowseFfprobe.Name = "btnBrowseFfprobe";
            this.btnBrowseFfprobe.Size = new System.Drawing.Size(80, 26);
            this.btnBrowseFfprobe.TabIndex = 10;
            this.btnBrowseFfprobe.Text = "浏览...";
            this.btnBrowseFfprobe.UseVisualStyleBackColor = false;
            this.btnBrowseFfprobe.Click += new System.EventHandler(this.BtnBrowseFfprobe_Click);
            // 
            // lblGroupNaming
            // 
            this.lblGroupNaming.AutoSize = true;
            this.lblGroupNaming.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblGroupNaming.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(155)))));
            this.lblGroupNaming.Location = new System.Drawing.Point(8, 184);
            this.lblGroupNaming.Name = "lblGroupNaming";
            this.lblGroupNaming.Size = new System.Drawing.Size(101, 19);
            this.lblGroupNaming.TabIndex = 11;
            this.lblGroupNaming.Text = "▎命名规则设置";
            // 
            // lblHPrefix
            // 
            this.lblHPrefix.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblHPrefix.Location = new System.Drawing.Point(16, 216);
            this.lblHPrefix.Name = "lblHPrefix";
            this.lblHPrefix.Size = new System.Drawing.Size(120, 22);
            this.lblHPrefix.TabIndex = 12;
            this.lblHPrefix.Text = "横向文件前缀:";
            // 
            // txtHPrefix
            // 
            this.txtHPrefix.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtHPrefix.Location = new System.Drawing.Point(140, 214);
            this.txtHPrefix.Name = "txtHPrefix";
            this.txtHPrefix.Size = new System.Drawing.Size(160, 23);
            this.txtHPrefix.TabIndex = 13;
            this.txtHPrefix.Text = "横向_";
            // 
            // lblVPrefix
            // 
            this.lblVPrefix.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblVPrefix.Location = new System.Drawing.Point(16, 248);
            this.lblVPrefix.Name = "lblVPrefix";
            this.lblVPrefix.Size = new System.Drawing.Size(120, 22);
            this.lblVPrefix.TabIndex = 14;
            this.lblVPrefix.Text = "竖向文件前缀:";
            // 
            // txtVPrefix
            // 
            this.txtVPrefix.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtVPrefix.Location = new System.Drawing.Point(140, 246);
            this.txtVPrefix.Name = "txtVPrefix";
            this.txtVPrefix.Size = new System.Drawing.Size(160, 23);
            this.txtVPrefix.TabIndex = 15;
            this.txtVPrefix.Text = "竖向_";
            // 
            // lblGroupScope
            // 
            this.lblGroupScope.AutoSize = true;
            this.lblGroupScope.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblGroupScope.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(155)))));
            this.lblGroupScope.Location = new System.Drawing.Point(8, 284);
            this.lblGroupScope.Name = "lblGroupScope";
            this.lblGroupScope.Size = new System.Drawing.Size(73, 19);
            this.lblGroupScope.TabIndex = 16;
            this.lblGroupScope.Text = "▎应用范围";
            // 
            // chkIncludeSubdir
            // 
            this.chkIncludeSubdir.AutoSize = true;
            this.chkIncludeSubdir.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.chkIncludeSubdir.Location = new System.Drawing.Point(16, 312);
            this.chkIncludeSubdir.Name = "chkIncludeSubdir";
            this.chkIncludeSubdir.Size = new System.Drawing.Size(332, 21);
            this.chkIncludeSubdir.TabIndex = 17;
            this.chkIncludeSubdir.Text = "应用范围包含子目录（重命名/复位时递归处理子文件夹）";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnSave;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(580, 460);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(550, 400);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置 - 资源文件方向智能助手";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        #region 控件声明

        private System.Windows.Forms.Panel panelMain;

        private System.Windows.Forms.Label lblGroupPlayer;
        private System.Windows.Forms.Label lblPotPlayer;
        private System.Windows.Forms.TextBox txtPotPlayer;
        private System.Windows.Forms.Button btnBrowsePot;

        private System.Windows.Forms.Label lblGroupConv;
        private System.Windows.Forms.Label lblFFmpeg;
        private System.Windows.Forms.TextBox txtFFmpeg;
        private System.Windows.Forms.Button btnBrowseFfmpeg;
        private System.Windows.Forms.Label lblFFprobe;
        private System.Windows.Forms.TextBox txtFFprobe;
        private System.Windows.Forms.Button btnBrowseFfprobe;

        private System.Windows.Forms.Label lblGroupNaming;
        private System.Windows.Forms.Label lblHPrefix;
        private System.Windows.Forms.TextBox txtHPrefix;
        private System.Windows.Forms.Label lblVPrefix;
        private System.Windows.Forms.TextBox txtVPrefix;

        private System.Windows.Forms.Label lblGroupScope;
        private System.Windows.Forms.CheckBox chkIncludeSubdir;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;

        #endregion
    }
}
