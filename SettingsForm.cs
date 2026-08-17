using System;
using System.IO;
using System.Windows.Forms;

namespace 资源文件方向智能助手
{
    /// <summary>
    /// 设置窗口 — 配置 PotPlayer / FFmpeg 等第三方软件路径
    /// </summary>
    public partial class SettingsForm : Form
    {
        private Config _cfg;

        public SettingsForm(Config cfg)
        {
            _cfg = cfg;
            InitializeComponent();
            LoadValues();
        }

        #region 加载值
        private void LoadValues()
        {
            // 文本框：配置文件有值则用之，否则保留 Designer 中的默认值
            if (!string.IsNullOrEmpty(_cfg.PotPlayerPath))
                txtPotPlayer.Text = _cfg.PotPlayerPath;
            if (!string.IsNullOrEmpty(_cfg.FFmpegPath))
                txtFFmpeg.Text = _cfg.FFmpegPath;
            if (!string.IsNullOrEmpty(_cfg.FFprobePath))
                txtFFprobe.Text = _cfg.FFprobePath;

            txtHPrefix.Text = _cfg.HorizontalPrefix;
            txtVPrefix.Text = _cfg.VerticalPrefix;
            chkIncludeSubdir.Checked = _cfg.IncludeSubdir;
        }
        #endregion

        #region 保存按钮
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 写入配置并保存到 INI
            _cfg.PotPlayerPath = txtPotPlayer.Text.Trim();
            _cfg.FFmpegPath = txtFFmpeg.Text.Trim();
            _cfg.FFprobePath = txtFFprobe.Text.Trim();
            _cfg.HorizontalPrefix = string.IsNullOrEmpty(txtHPrefix.Text.Trim()) ? "横向_" : txtHPrefix.Text.Trim();
            _cfg.VerticalPrefix = string.IsNullOrEmpty(txtVPrefix.Text.Trim()) ? "竖向_" : txtVPrefix.Text.Trim();
            _cfg.IncludeSubdir = chkIncludeSubdir.Checked;
            _cfg.Save();

            MessageBox.Show("设置已保存！", "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        #endregion

        #region 关闭按钮
        private void BtnClose_Click(object sender, EventArgs e)
        {
            // 不保存，直接关闭
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region 浏览按钮
        private void BtnBrowsePot_Click(object sender, EventArgs e)
        {
            string f = BrowseExe("选择 PotPlayer.exe");
            if (f != null) txtPotPlayer.Text = f;
        }

        private void BtnBrowseFfmpeg_Click(object sender, EventArgs e)
        {
            string f = BrowseExe("选择 ffmpeg.exe");
            if (f != null) txtFFmpeg.Text = f;
        }

        private void BtnBrowseFfprobe_Click(object sender, EventArgs e)
        {
            string f = BrowseExe("选择 ffprobe.exe");
            if (f != null) txtFFprobe.Text = f;
        }

        private string BrowseExe(string title)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "可执行文件 (*.exe)|*.exe|所有文件 (*.*)|*.*";
                ofd.Title = title;
                if (ofd.ShowDialog(this) == DialogResult.OK) return ofd.FileName;
                return null;
            }
        }
        #endregion

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
