using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 资源文件方向智能助手
{
    public partial class MainForm : Form
    {
        #region 字段
        private bool _showHidden = false;
        private string _currentPath = "";
        private readonly string[] _videoExt = { ".mp4", ".avi", ".mkv", ".mov", ".wmv", ".flv", ".ts", ".mpg", ".mpeg", ".webm", ".3gp" };
        private readonly string[] _imageExt = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".ico", ".webp", ".svg" };

        private Config _config;

        // 图标缓存
        private Dictionary<string, string> _folderIconKeys = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, string> _fileIconKeys = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private string _folderIconKey = "folder_closed"; // ListView 用的文件夹图标

        // ListView 条目 Tag 类型标识
        private const string TAG_FOLDER = "FOLDER:";
        private const string TAG_FILE = "FILE:";
        #endregion

        #region 构造
        public MainForm()
        {
            InitializeComponent();
            if (IsDesignMode()) return;

            try
            {
                _config = new Config();
                InitSystemIcons();
                WireUpEvents();
                LoadDrives();
            }
            catch (Exception ex) { Debug.WriteLine("Init: " + ex.Message); }
        }

        private bool IsDesignMode()
        {
            try
            {
                string exe = Application.ExecutablePath.ToLower();
                return exe.Contains("devenv.exe") || exe.Contains("vshost.exe");
            }
            catch { return false; }
        }
        #endregion

        #region OnLoad
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (IsDesignMode()) return;

            try
            {
                // 主 SplitContainer（左右分栏）
                const int p1 = 160, p2 = 320, dist = 300;
                splitContainer.Panel1MinSize = p1;
                splitContainer.Panel2MinSize = p2;
                int min = p1, max = splitContainer.Width - p2;
                if (max < min) max = min;
                splitContainer.SplitterDistance = Math.Max(min, Math.Min(dist, max));
            }
            catch { }

            try
            {
                // 文件列表列头
                lvFiles.Columns.Clear();
                lvFiles.Columns.Add(colName);
                lvFiles.Columns.Add(colType);
                lvFiles.Columns.Add(colSize);
                lvFiles.Columns.Add(colDate);
            }
            catch { }

            // 路径框初始文本
            try { txtPath.Text = "(我的电脑)"; } catch { }

            // 调整路径框宽度
            LayoutToolStrip2();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            LayoutToolStrip2();
        }

        /// <summary>
        /// 手动布局 toolStrip2：btnUp 居左，txtPath 右对齐占满剩余空间
        /// 不使用 Spring 属性（Mono 不支持）
        /// </summary>
        private void LayoutToolStrip2()
        {
            try
            {
                if (txtPath == null || toolStrip2 == null) return;

                // 计算 btnUp + separator 占用的宽度
                int leftW = 0;
                foreach (ToolStripItem item in toolStrip2.Items)
                {
                    if (item == txtPath) break;
                    if (item.Visible) leftW += item.Width + 2;
                }

                int margin = 8;
                int w = toolStrip2.Width - leftW - margin;
                if (w < 200) w = 200;
                if (w > 1200) w = 1200;
                txtPath.Size = new Size(w, 23);
            }
            catch { }
        }
        #endregion

        #region 事件绑定
        private void WireUpEvents()
        {
            btnRename.Click += BtnRename_Click;
            btnReset.Click += BtnReset_Click;
            btnShowHidden.Click += BtnShowHidden_Click;
            btnPlayH.Click += (s, e) => PlayByPrefix(_config.HorizontalPrefix.TrimEnd('_'));
            btnPlayV.Click += (s, e) => PlayByPrefix(_config.VerticalPrefix.TrimEnd('_'));
            btnTsMp4.Click += BtnTsToMp4_Click;
            btnmonitors.Click += btnmonitors_Click;
            btnSettings.Click += BtnSettings_Click;
            btnClose.Click += (s, e) => Close();
            tm_tc.Click += (s, e) => Close();
            btnUp.Click += BtnUp_Click;

            tvFolders.AfterSelect += Tv_AfterSelect;
            tvFolders.BeforeExpand += Tv_BeforeExpand;

            // 文件列表：双击
            lvFiles.DoubleClick += LvFiles_DoubleClick;
            lvFiles.ItemSelectionChanged += Lv_SelectionChanged;


            // 全局快捷键
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.F5) RefreshAll();
                if (e.KeyCode == Keys.Back) BtnUp_Click(null, null);
            };
        }
        #endregion

        #region 进度条管理
        private void ShowProgress()
        {
            toolStripProgress.Visible = true;
            progressBar.Value = 0;
        }

        private void HideProgress()
        {
            toolStripProgress.Visible = false;
            progressBar.Value = 0;
        }
        #endregion

        #region 设置窗口
        private void BtnSettings_Click(object s, EventArgs e)
        {
            using (var dlg = new SettingsForm(_config))
            {
                dlg.ShowDialog(this);
                _config.Load(); // 无论保存还是关闭，都重新加载（保存时已写盘）
                statusLabel.Text = "✅ 设置已更新";
            }
        }
        #endregion

        #region 系统图标
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes,
            ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        [DllImport("user32.dll")]
        private static extern bool DestroyIcon(IntPtr hIcon);

        private const uint SHGFI_ICON = 0x100;
        private const uint SHGFI_SMALLICON = 0x1;
        private const uint SHGFI_OPENICON = 0x2;
        private const uint SHGFI_USEFILEATTRIBUTES = 0x10;
        private const uint FILE_ATTRIBUTE_DIRECTORY = 0x10;

        private void InitSystemIcons()
        {
            try
            {
                // 使用 CSIDL_DRIVES 或系统目录获取通用文件夹图标
                // 关键：用 SHGFI_USEFILEATTRIBUTES 标志，不依赖实际路径
                string userDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                // === TreeView 图标 ===
                // 驱动器图标：用系统目录路径
                string sysDir = Environment.GetFolderPath(Environment.SpecialFolder.System);
                AddIcon(imgTree, "drive", sysDir);

                // 文件夹图标（关闭态）：用 SHGFI_USEFILEATTRIBUTES 确保获取标准文件夹图标
                AddFolderIcon(imgTree, "folder_closed", false);
                AddFolderIcon(imgTree, "folder_open", true);

                // === ListView 图标 ===
                // 文件图标
                string dummy = Path.Combine(userDir, "dummy.txt");
                try { File.WriteAllText(dummy, ""); } catch { }
                AddIcon(imgFiles, "file", dummy);
                try { File.Delete(dummy); } catch { }

                // ListView 用的文件夹图标（标准黄色文件夹，绝不可能是用户头像）
                AddFolderIcon(imgFiles, "folder_list", false);
                _folderIconKey = imgFiles.Images.ContainsKey("folder_list") ? "folder_list" : "folder_closed";

                // 兜底：如果依然没有，画一个简单文件夹图标
                if (_folderIconKey == null || imgFiles.Images[_folderIconKey] == null)
                {
                    var bmp = DrawFolderBitmap(Color.FromArgb(255, 200, 80), 16, 16);
                    imgFiles.Images.Add("folder_fallback", bmp);
                    _folderIconKey = "folder_fallback";
                }
            }
            catch { }
        }

        /// <summary>
        /// 用 SHGFI_USEFILEATTRIBUTES 获取标准文件夹图标（不依赖实际路径，不会返回用户头像）
        /// </summary>
        private void AddFolderIcon(ImageList list, string key, bool open)
        {
            try
            {
                var shfi = new SHFILEINFO();
                uint flags = SHGFI_ICON | SHGFI_SMALLICON | SHGFI_USEFILEATTRIBUTES;
                if (open) flags |= SHGFI_OPENICON;
                // pszPath 传任意名称即可，因为 USEFILEATTRIBUTES 不看路径
                SHGetFileInfo("folder", FILE_ATTRIBUTE_DIRECTORY, ref shfi, (uint)Marshal.SizeOf(shfi), flags);
                if (shfi.hIcon != IntPtr.Zero)
                {
                    using (var ico = Icon.FromHandle(shfi.hIcon))
                        list.Images.Add(key, ico.ToBitmap());
                    DestroyIcon(shfi.hIcon);
                }
            }
            catch { }
        }

        private void AddIcon(ImageList list, string key, string path)
        {
            try
            {
                var shfi = new SHFILEINFO();
                SHGetFileInfo(path, 0, ref shfi, (uint)Marshal.SizeOf(shfi), SHGFI_ICON | SHGFI_SMALLICON);
                if (shfi.hIcon != IntPtr.Zero)
                {
                    using (var ico = Icon.FromHandle(shfi.hIcon))
                        list.Images.Add(key, ico.ToBitmap());
                    DestroyIcon(shfi.hIcon);
                }
            }
            catch { }
        }

        /// <summary>
        /// 用 GDI+ 绘制一个简单文件夹位图作为终极兜底
        /// </summary>
        private Bitmap DrawFolderBitmap(Color color, int w, int h)
        {
            var bmp = new Bitmap(w, h);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                using (var brush = new SolidBrush(color))
                using (var pen = new Pen(Color.FromArgb(180, 120, 30), 1))
                {
                    // 文件夹背部
                    g.FillRectangle(brush, 1, 4, w - 2, h - 5);
                    // 文件夹翻盖
                    g.FillRectangle(brush, 1, 2, (w / 2) + 1, 3);
                    g.DrawRectangle(pen, 1, 4, w - 2, h - 5);
                }
            }
            return bmp;
        }

        private string GetFolderIconKey(string path, bool open)
        {
            string ck = path + (open ? "_o" : "_c");
            if (_folderIconKeys.TryGetValue(ck, out string v)) return v;
            try
            {
                var shfi = new SHFILEINFO();
                // 使用 SHGFI_USEFILEATTRIBUTES：不访问实际路径，避免返回用户头像等特殊图标
                uint flags = SHGFI_ICON | SHGFI_SMALLICON | SHGFI_USEFILEATTRIBUTES | (open ? SHGFI_OPENICON : 0);
                SHGetFileInfo(path, FILE_ATTRIBUTE_DIRECTORY, ref shfi, (uint)Marshal.SizeOf(shfi), flags);
                string key = open ? "folder_open" : "folder_closed";
                if (shfi.hIcon != IntPtr.Zero)
                {
                    using (var ico = Icon.FromHandle(shfi.hIcon))
                    {
                        key = "fd_" + shfi.iIcon + (open ? "o" : "c");
                        if (imgTree.Images[key] == null) imgTree.Images.Add(key, ico.ToBitmap());
                    }
                    DestroyIcon(shfi.hIcon);
                }
                // 兜底：如果获取失败，用全局默认
                if (imgTree.Images[key] == null)
                    key = open ? "folder_open" : "folder_closed";
                _folderIconKeys[ck] = key;
                return key;
            }
            catch { return open ? "folder_open" : "folder_closed"; }
        }

        private string GetFileIconKey(string filePath)
        {
            string ext = Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(ext)) return "file";
            if (_fileIconKeys.TryGetValue(ext, out string cached)) return cached;
            try
            {
                var shfi = new SHFILEINFO();
                SHGetFileInfo(filePath, 0, ref shfi, (uint)Marshal.SizeOf(shfi), SHGFI_ICON | SHGFI_SMALLICON);
                string key = "file";
                if (shfi.hIcon != IntPtr.Zero)
                {
                    using (var ico = Icon.FromHandle(shfi.hIcon))
                    {
                        key = "fx_" + shfi.iIcon;
                        if (imgFiles.Images[key] == null) imgFiles.Images.Add(key, ico.ToBitmap());
                    }
                    DestroyIcon(shfi.hIcon);
                }
                _fileIconKeys[ext] = key;
                return key;
            }
            catch { return "file"; }
        }
        #endregion

        #region 目录树
        private void LoadDrives()
        {
            tvFolders.Nodes.Clear();
            try
            {
                foreach (var d in DriveInfo.GetDrives())
                {
                    if (!d.IsReady) continue;
                    string label = d.Name;
                    try { if (!string.IsNullOrEmpty(d.VolumeLabel)) label = d.Name + " (" + d.VolumeLabel + ")"; } catch { }

                    string ck = GetFolderIconKey(d.RootDirectory.FullName, false);
                    string ok = GetFolderIconKey(d.RootDirectory.FullName, true);

                    var node = new TreeNode(label) { Tag = d.RootDirectory.FullName, ImageKey = ck, SelectedImageKey = ok };
                    node.Nodes.Add(new TreeNode("..."));
                    tvFolders.Nodes.Add(node);
                }
                statusLabel.Text = "就绪 | " + tvFolders.Nodes.Count + " 个磁盘 | v1.3";
            }
            catch (Exception ex) { statusLabel.Text = "错误: " + ex.Message; }
        }

        private void Tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = e.Node.Tag as string;
            if (string.IsNullOrEmpty(path)) return;
            // 规范化路径
            try { path = Path.GetFullPath(path); } catch { }
            _currentPath = path;
            txtPath.Text = path;
            e.Node.ImageKey = GetFolderIconKey(path, false);
            e.Node.SelectedImageKey = GetFolderIconKey(path, true);
            LoadAll(path);
        }

        private void Tv_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var node = e.Node;
            string path = node.Tag as string;
            if (string.IsNullOrEmpty(path)) return;
            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "...")
            {
                node.Nodes.Clear();
                LoadChildDirs(path, node);
            }
        }

        private void LoadChildDirs(string parentPath, TreeNode parentNode)
        {
            try
            {
                var dirs = new DirectoryInfo(parentPath).GetDirectories()
                    .Where(d => _showHidden || (d.Attributes & FileAttributes.Hidden) == 0)
                    .Where(d => (d.Attributes & FileAttributes.System) == 0)
                    .OrderBy(d => d.Name);
                foreach (var dir in dirs)
                {
                    string ck = GetFolderIconKey(dir.FullName, false);
                    string ok2 = GetFolderIconKey(dir.FullName, true);
                    var child = new TreeNode(dir.Name) { Tag = dir.FullName, ImageKey = ck, SelectedImageKey = ok2 };
                    try { if (dir.GetDirectories().Length > 0) child.Nodes.Add(new TreeNode("...")); } catch { }
                    parentNode.Nodes.Add(child);
                }
            }
            catch (UnauthorizedAccessException) { parentNode.Nodes.Add(new TreeNode("⚠ 拒绝访问") { ForeColor = Color.Red }); }
            catch (Exception ex) { parentNode.Nodes.Add(new TreeNode("⚠ " + ex.Message) { ForeColor = Color.Red }); }
        }
        #endregion

        #region 右侧列表（".." + 子文件夹 + 文件混合显示）
        /// <summary>
        /// 加载：先添加".."返回上级，再添加子文件夹，最后添加媒体文件
        /// selectName: 可选，加载后默认选中该名称的项（用于向上返回时记住原文件夹）
        /// </summary>
        private void LoadAll(string path, string selectName = null)
        {
            lvFiles.Items.Clear();
            lvFiles.Enabled = true;

            // 规范化路径（避免末尾反斜杠、盘符格式导致 Parent 解析异常）
            string normPath = "";
            try { normPath = Path.GetFullPath(path).TrimEnd('\\', '/'); }
            catch { normPath = path.TrimEnd('\\', '/'); }

            // === 0. ".." 返回上级（仅当有上级目录时显示）===
            try
            {
                DirectoryInfo di = new DirectoryInfo(normPath);
                DirectoryInfo parent = di.Parent;
                if (parent != null)
                {
                    var upItem = new ListViewItem("..");
                    upItem.Tag = TAG_FOLDER + parent.FullName; // 指向父目录
                    upItem.ImageKey = _folderIconKey; // 系统文件夹图标
                    upItem.SubItems.Add("返回上级");
                    upItem.SubItems.Add("");
                    try { upItem.SubItems.Add(parent.LastWriteTime.ToString("yyyy-MM-dd HH:mm")); }
                    catch { upItem.SubItems.Add(""); }
                    upItem.BackColor = Color.FromArgb(240, 240, 245); // 灰色背景区分
                    upItem.Font = new Font(lvFiles.Font, FontStyle.Bold);
                    lvFiles.Items.Add(upItem);
                }
            }
            catch (Exception ex)
            {
                // 不要吞掉异常，记录到状态栏便于排查
                statusLabel.Text = "⚠ 无法获取上级目录: " + ex.Message;
            }

            // === 1. 子文件夹 ===
            int folderCount = 0;
            try
            {
                var dirs = new DirectoryInfo(path).GetDirectories()
                    .Where(d => _showHidden || (d.Attributes & FileAttributes.Hidden) == 0)
                    .Where(d => (d.Attributes & FileAttributes.System) == 0)
                    .OrderBy(d => d.Name);

                foreach (var dir in dirs)
                {
                    var item = new ListViewItem(dir.Name);
                    item.Tag = TAG_FOLDER + dir.FullName;
                    item.ImageKey = _folderIconKey; // 系统文件夹图标
                    item.SubItems.Add("文件夹");
                    item.SubItems.Add("");
                    item.SubItems.Add(dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm"));
                    // 文件夹用浅蓝背景区分
                    item.BackColor = Color.FromArgb(245, 248, 255);
                    lvFiles.Items.Add(item);
                    folderCount++;
                }
            }
            catch { }

            // === 2. 媒体文件（排在后面）===
            DirectoryInfo dirInfo;
            try { dirInfo = new DirectoryInfo(path); } catch { ShowEmpty("⚠ 无法访问此路径"); return; }
            FileInfo[] all;
            try { all = dirInfo.GetFiles(); } catch { ShowEmpty("⚠ 无法读取此文件夹"); return; }

            var media = all
                .Where(f => _videoExt.Contains(f.Extension, StringComparer.OrdinalIgnoreCase)
                         || _imageExt.Contains(f.Extension, StringComparer.OrdinalIgnoreCase))
                .Where(f => _showHidden || (f.Attributes & FileAttributes.Hidden) == 0)
                .OrderBy(f => f.Name).ToList();

            if (folderCount == 0 && media.Count == 0)
            {
                // 有".."但没有任何子文件夹和媒体文件 → 显示友好提示
                // 注意：此时".."已在前面添加，不应被 Clear 掉
                // 所以这里不再调用 ShowEmpty（它会 Clear），而是直接返回
                statusLabel.Text = "📂 " + path + " | 空文件夹（仅有返回上级）";
                return;
            }

            int vc = 0, ic = 0;
            foreach (var f in media)
            {
                bool isVid = _videoExt.Contains(f.Extension, StringComparer.OrdinalIgnoreCase);
                if (isVid) vc++; else ic++;
                var item = new ListViewItem(f.Name);
                item.Tag = TAG_FILE + f.FullName;
                item.ImageKey = GetFileIconKey(f.FullName);
                item.ForeColor = (f.Attributes & FileAttributes.Hidden) != 0 ? Color.Gray : Color.Black;
                item.SubItems.Add(isVid ? ("视频 (" + f.Extension.ToUpper().TrimStart('.') + ")") : ("图片 (" + f.Extension.ToUpper().TrimStart('.') + ")"));
                item.SubItems.Add(FormatSize(f.Length));
                item.SubItems.Add(f.LastWriteTime.ToString("yyyy-MM-dd HH:mm"));
                lvFiles.Items.Add(item);
            }

            statusLabel.Text = "📂 " + path + " | 子文件夹 " + folderCount + " | 视频 " + vc + " | 图片 " + ic + " | 共 " + (folderCount + media.Count) + " 项";

            // 如果有指定名称，默认选中该项（用于向上返回时记住原文件夹）
            if (!string.IsNullOrEmpty(selectName))
            {
                foreach (ListViewItem item in lvFiles.Items)
                {
                    if (item.Text == selectName || item.Text == "..")
                    {
                        item.Selected = true;
                        item.Focused = true;
                        item.EnsureVisible();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 双击列表项：文件夹→进入，文件→打开
        /// <summary>
        /// 双击列表项：..→返回上级并选中原文件夹，文件夹→进入并选中，文件→打开
        /// </summary>
        private void LvFiles_DoubleClick(object s, EventArgs e)
        {
            if (lvFiles.SelectedItems.Count == 0) return;
            var item = lvFiles.SelectedItems[0];
            string tag = item.Tag as string;
            if (string.IsNullOrEmpty(tag)) return;

            // ".." 特殊项：返回上级
            if (item.Text == "..")
            {
                BtnUp_Click(null, null); // 内部已记住并选中原文件夹
                return;
            }

            if (tag.StartsWith(TAG_FOLDER))
            {
                // 进入子文件夹，记住刚进入的文件夹名以便高亮
                string path = tag.Substring(TAG_FOLDER.Length);
                // 规范化路径
                try { path = Path.GetFullPath(path); } catch { path = path.TrimEnd('\\', '/'); }
                string folderName = Path.GetFileName(path.TrimEnd('\\', '/'));
                _currentPath = path;
                txtPath.Text = path;
                SelectNodeByPath(path);   // 内部会逐级展开树
                LoadAll(path, folderName);
            }
            else if (tag.StartsWith(TAG_FILE))
            {
                // 打开文件
                string path = tag.Substring(TAG_FILE.Length);
                try { Process.Start(path); }
                catch (Exception ex) { MessageBox.Show("无法打开: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void ShowEmpty(string msg)
        {
            lvFiles.Enabled = false;
            lvFiles.Items.Add(new ListViewItem(msg) { ForeColor = SystemColors.GrayText, Font = new Font(lvFiles.Font, FontStyle.Italic) });
        }

        private void Lv_SelectionChanged(object s, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected) return;
            string tag = e.Item.Tag as string;
            if (string.IsNullOrEmpty(tag)) return;
            if (tag.StartsWith(TAG_FOLDER)) statusLabel.Text = "📁 " + tag.Substring(TAG_FOLDER.Length);
            else if (tag.StartsWith(TAG_FILE)) statusLabel.Text = "📄 " + Path.GetFileName(tag.Substring(TAG_FILE.Length));
        }
        #endregion

        #region 刷新
        private void RefreshAll()
        {
            if (string.IsNullOrEmpty(_currentPath))
            {
                LoadDrives();
                lvFiles.Items.Clear();
                lvFiles.Enabled = true;
                txtPath.Text = "(我的电脑)";
                return;
            }

            // 刷新树当前节点：清空后重新加载子目录（不再只放"..."）
            var node = tvFolders.SelectedNode;
            if (node != null)
            {
                string path = node.Tag as string;
                if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                {
                    node.Nodes.Clear();
                    LoadChildDirs(path, node);  // 重新加载真实子目录
                    node.Expand();              // 保持展开状态
                }
            }

            LoadAll(_currentPath);
            statusLabel.Text = "🔄 已刷新: " + _currentPath;
        }
        #endregion

        #region 向上导航
        private void BtnUp_Click(object s, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentPath))
            {
                LoadDrives();
                lvFiles.Items.Clear();
                lvFiles.Enabled = true;
                _currentPath = "";
                txtPath.Text = "(我的电脑)";
                return;
            }
            DirectoryInfo di = new DirectoryInfo(_currentPath);
            if (di.Parent != null)
            {
                string prevName = di.Name; // 记住刚才的文件夹名
                _currentPath = di.Parent.FullName;
                txtPath.Text = _currentPath;
                SelectNodeByPath(_currentPath);
                LoadAll(_currentPath, prevName); // 传入要高亮的项
            }
            else
            {
                _currentPath = "";
                txtPath.Text = "(我的电脑)";
                lvFiles.Items.Clear();
                lvFiles.Enabled = true;
                tvFolders.SelectedNode = null;
                statusLabel.Text = "已在根目录";
            }
        }

        private void SelectNodeByPath(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            // 先尝试在已加载节点中精确匹配
            TreeNode found = FindNodeByPath(tvFolders.Nodes, path);
            if (found != null)
            {
                tvFolders.SelectedNode = found;
                found.Expand();
                found.EnsureVisible();
                return;
            }

            // 未找到 → 逐级展开路径，边展开边匹配
            ExpandToPath(path);
        }

        /// <summary>
        /// 递归在已加载节点中查找精确路径匹配
        /// </summary>
        private TreeNode FindNodeByPath(TreeNodeCollection nodes, string path)
        {
            foreach (TreeNode n in nodes)
            {
                if ((n.Tag as string) == path) return n;
                var r = FindNodeByPath(n.Nodes, path);
                if (r != null) return r;
            }
            return null;
        }

        /// <summary>
        /// 逐级展开树，使目标路径可见并选中。
        /// 从根节点开始，逐段匹配路径，展开并加载子目录。
        /// </summary>
        private void ExpandToPath(string targetPath)
        {
            // 规范化路径分隔符
            string norm = Path.GetFullPath(targetPath).TrimEnd('\\');
            string[] parts = norm.Split('\\');

            TreeNode current = null;

            // 第一层：匹配盘符
            foreach (TreeNode r in tvFolders.Nodes)
            {
                string rPath = (r.Tag as string) ?? "";
                if (rPath.TrimEnd('\\').Equals(parts[0].TrimEnd('\\'), StringComparison.OrdinalIgnoreCase))
                { current = r; break; }
            }
            if (current == null) return;

            current.Expand();
            // 确保子节点已加载
            if (current.Nodes.Count == 1 && current.Nodes[0].Text == "...")
            {
                current.Nodes.Clear();
                LoadChildDirs(current.Tag as string ?? "", current);
            }

            // 逐层深入
            for (int i = 1; i < parts.Length; i++)
            {
                string segment = parts[i];
                bool found = false;
                foreach (TreeNode child in current.Nodes)
                {
                    if (child.Text.Equals(segment, StringComparison.OrdinalIgnoreCase))
                    {
                        current = child;
                        found = true;
                        break;
                    }
                }
                if (!found) break; // 路径不存在，停在最近一级

                current.Expand();
                // 确保子节点已加载
                if (current.Nodes.Count == 1 && current.Nodes[0].Text == "...")
                {
                    current.Nodes.Clear();
                    LoadChildDirs(current.Tag as string ?? "", current);
                }
            }

            tvFolders.SelectedNode = current;
            current.EnsureVisible();
        }
        #endregion

        #region 显示/隐藏文件夹
        private void BtnShowHidden_Click(object s, EventArgs e)
        {
            _showHidden = !_showHidden;
            btnShowHidden.Text = _showHidden ? "不显示隐藏文件夹" : "显示隐藏文件夹";
            RefreshAll();
        }
        #endregion

        #region 打开 / 播放
        private void OpenSelected()
        {
            if (lvFiles.SelectedItems.Count == 0) return;
            string tag = lvFiles.SelectedItems[0].Tag as string;
            if (string.IsNullOrEmpty(tag)) return;

            if (tag.StartsWith(TAG_FOLDER))
            {
                string path = tag.Substring(TAG_FOLDER.Length);
                // 规范化路径
                try { path = Path.GetFullPath(path); } catch { }
                _currentPath = path;
                txtPath.Text = path;
                SelectNodeByPath(path);   // 内部会逐级展开
                LoadAll(path);
            }
            else if (tag.StartsWith(TAG_FILE))
            {
                try { Process.Start(tag.Substring(TAG_FILE.Length)); }
                catch (Exception ex) { MessageBox.Show("无法打开: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void PlaySelected()
        {
            if (lvFiles.SelectedItems.Count == 0) return;
            string tag = lvFiles.SelectedItems[0].Tag as string;
            if (string.IsNullOrEmpty(tag) || !tag.StartsWith(TAG_FILE)) return;
            string path = tag.Substring(TAG_FILE.Length);
            PlayWithPotPlayer(new[] { path });
        }

        private void PlayByPrefix(string prefix)
        {
            if (string.IsNullOrEmpty(_currentPath)) { MessageBox.Show("请先选择一个文件夹", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            try
            {
                var files = new DirectoryInfo(_currentPath).GetFiles()
                    .Where(f => f.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    .Where(f => _videoExt.Any(ext => string.Equals(ext, f.Extension, StringComparison.OrdinalIgnoreCase)))
                    .OrderBy(f => f.Name)
                    .Select(f => f.FullName).ToArray();
                if (files.Length == 0) { MessageBox.Show("未找到以 \"" + prefix + "\" 开头的视频文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                PlayWithPotPlayer(files);
            }
            catch (Exception ex) { MessageBox.Show("错误: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// 将所有文件写入一个 .pls 播放列表，然后用 PotPlayer 打开该列表。
        /// 这样只启动一次播放器，文件之间连续播放。
        /// </summary>
        private void PlayWithPotPlayer(string[] files)
        {
            string pp = _config.PotPlayerPath;
            if (string.IsNullOrEmpty(pp) || !File.Exists(pp))
            {
                pp = FindPotPlayer();
                if (pp == null) { MessageBox.Show("未找到 PotPlayer，请在「⚙ 设置」中配置路径", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                _config.PotPlayerPath = pp; _config.Save();
            }

            // 创建播放列表文件
            string playlistPath = Path.Combine(Path.GetTempPath(), "ResourceAssistant_" + Guid.NewGuid().ToString("N").Substring(0, 8) + ".pls");
            try
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine("[playlist]");
                for (int i = 0; i < files.Length; i++)
                {
                    sb.AppendLine("File" + (i + 1) + "=" + files[i]);
                    sb.AppendLine("Title" + (i + 1) + "=" + Path.GetFileName(files[i]));
                }
                sb.AppendLine("NumberOfEntries=" + files.Length);
                sb.AppendLine("Version=2");
                File.WriteAllText(playlistPath, sb.ToString(), System.Text.Encoding.UTF8);
            }
            catch (Exception ex) { MessageBox.Show("创建播放列表失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            try
            {
                // 只启动一次播放器，打开播放列表
                Process.Start(pp, "\"" + playlistPath + "\"");
                statusLabel.Text = "▶ 已发送到 PotPlayer 播放列表: " + files.Length + " 个文件";
            }
            catch (Exception ex) { MessageBox.Show("播放失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private string FindPotPlayer()
        {
            string[] candidates = {
                _config.PotPlayerPath,
                @"C:\Program Files\DAUM\PotPlayer\PotPlayer.exe",
                @"C:\Program Files (x86)\DAUM\PotPlayer\PotPlayer.exe"
            };
            foreach (var c in candidates) if (!string.IsNullOrEmpty(c) && File.Exists(c)) return c;
            try
            {
                using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\PotPlayer.exe"))
                { if (key != null) { var v = key.GetValue("") as string; if (!string.IsNullOrEmpty(v) && File.Exists(v)) return v; } }
                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\PotPlayer.exe"))
                { if (key != null) { var v = key.GetValue("") as string; if (!string.IsNullOrEmpty(v) && File.Exists(v)) return v; } }
            }
            catch { }
            return null;
        }
        #endregion

        #region 重命名
        private void BtnRename_Click(object s, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentPath)) { MessageBox.Show("请先选择一个文件夹", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (IsRoot(_currentPath)) { MessageBox.Show("根目录禁止操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var confirm = MessageBox.Show(
                "将对以下范围执行重命名：\n\n" +
                (_config.IncludeSubdir ? "✅ 包含子目录（递归）\n" : "❌ 仅当前目录\n") +
                "\n横向媒体 → 加前缀 \"" + _config.HorizontalPrefix + "\"\n" +
                "竖向媒体 → 加前缀 \"" + _config.VerticalPrefix + "\"\n\n" +
                "确定继续吗？",
                "确认重命名", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (confirm != DialogResult.OK) return;

            ShowProgress();
            lblProgress.Text = "正在重命名...";
            progressBar.Value = 0;

            string root = _currentPath;
            string hPre = _config.HorizontalPrefix;
            string vPre = _config.VerticalPrefix;
            bool includeSub = _config.IncludeSubdir;

            Task.Run(() =>
            {
                var files = GetAllMedia(root, includeSub);
                int total = files.Count, done = 0, ok = 0, skip = 0, err = 0;

                foreach (var f in files)
                {
                    try
                    {
                        string name = f.Name;
                        if (name.StartsWith(hPre, StringComparison.OrdinalIgnoreCase) ||
                            name.StartsWith(vPre, StringComparison.OrdinalIgnoreCase)) { skip++; }
                        else
                        {
                            bool isH = IsHorizontal(f.FullName);
                            string pre = isH ? hPre : vPre;
                            string newName = pre + name;
                            string newPath = Path.Combine(f.DirectoryName, newName);
                            if (!File.Exists(newPath)) { File.Move(f.FullName, newPath); ok++; }
                            else skip++;
                        }
                    }
                    catch { err++; }
                    done++;
                    if (done % 5 == 0 || done == total)
                    {
                        int pct = total > 0 ? (done * 100 / total) : 0;
                        this.BeginInvoke(new Action(() =>
                        {
                            progressBar.Value = Math.Min(pct, 100);
                            lblProgress.Text = "重命名中... (" + done + "/" + total + ")";
                        }));
                    }
                }

                this.BeginInvoke(new Action(() =>
                {
                    HideProgress();
                    MessageBox.Show("重命名完成！\n\n成功: " + ok + "\n跳过(已命名): " + skip + "\n失败: " + err, "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshAll();
                }));
            });
        }
        #endregion

        #region 复位
        private void BtnReset_Click(object s, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentPath)) { MessageBox.Show("请先选择一个文件夹", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (IsRoot(_currentPath)) { MessageBox.Show("根目录禁止操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var confirm = MessageBox.Show(
                "将移除文件名中的前缀：\n\n" +
                (_config.IncludeSubdir ? "✅ 包含子目录（递归）\n" : "❌ 仅当前目录\n") +
                "\n\"" + _config.HorizontalPrefix + "\" 和 \"" + _config.VerticalPrefix + "\"\n\n" +
                "确定继续吗？",
                "确认复位", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (confirm != DialogResult.OK) return;

            ShowProgress();
            lblProgress.Text = "正在复位...";
            progressBar.Value = 0;

            string root = _currentPath;
            string hPre = _config.HorizontalPrefix.TrimEnd('_');
            string vPre = _config.VerticalPrefix.TrimEnd('_');
            bool includeSub = _config.IncludeSubdir;

            Task.Run(() =>
            {
                var files = GetAllMedia(root, includeSub);
                int total = files.Count, done = 0, ok = 0, skip = 0, err = 0;

                foreach (var f in files)
                {
                    try
                    {
                        string name = f.Name;
                        string newName = name;
                        if (name.StartsWith(hPre + "_", StringComparison.OrdinalIgnoreCase))
                            newName = name.Substring((hPre + "_").Length);
                        else if (name.StartsWith(vPre + "_", StringComparison.OrdinalIgnoreCase))
                            newName = name.Substring((vPre + "_").Length);
                        else if (name.StartsWith(hPre, StringComparison.OrdinalIgnoreCase))
                            newName = name.Substring(hPre.Length);
                        else if (name.StartsWith(vPre, StringComparison.OrdinalIgnoreCase))
                            newName = name.Substring(vPre.Length);

                        if (newName != name)
                        {
                            string newPath = Path.Combine(f.DirectoryName, newName);
                            if (!File.Exists(newPath)) { File.Move(f.FullName, newPath); ok++; }
                            else skip++;
                        }
                        else skip++;
                    }
                    catch { err++; }
                    done++;
                    if (done % 5 == 0 || done == total)
                    {
                        int pct = total > 0 ? (done * 100 / total) : 0;
                        this.BeginInvoke(new Action(() =>
                        {
                            progressBar.Value = Math.Min(pct, 100);
                            lblProgress.Text = "复位中... (" + done + "/" + total + ")";
                        }));
                    }
                }

                this.BeginInvoke(new Action(() =>
                {
                    HideProgress();
                    MessageBox.Show("复位完成！\n\n成功: " + ok + "\n跳过(无需复位): " + skip + "\n失败: " + err, "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshAll();
                }));
            });
        }
        #endregion
        private string RunCmd(string command)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process proc = Process.Start(psi))
            {
                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
                return output;
            }
        }
        #region 显示器方向
        private void btnmonitors_Click(object s, EventArgs e)
        {

            //RunCmd("iRotate/iRotate.exe");

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = Application.StartupPath+"\\iRotate\\iRotate.exe", // 你要执行的软件
                UseShellExecute = true,   //  关键：true 才不会阻塞
                CreateNoWindow = false
            };

            Process.Start(psi);
        }
        #endregion

        #region TS 转 MP4
        private void BtnTsToMp4_Click(object s, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentPath)) { MessageBox.Show("请先选择一个文件夹", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            ShowProgress();
            lblProgress.Text = "正在转换 TS→MP4...";
            progressBar.Value = 0;

            string root = _currentPath;
            string ffmpeg = _config.FFmpegPath;

            Task.Run(() =>
            {
                var tsFiles = new DirectoryInfo(root).GetFiles("*.ts", SearchOption.TopDirectoryOnly)
                    .OrderBy(f => f.Name).ToList();
                int total = tsFiles.Count, done = 0, ok = 0, fail = 0;

                foreach (var ts in tsFiles)
                {
                    string outPath = Path.Combine(ts.DirectoryName, Path.GetFileNameWithoutExtension(ts.Name) + ".mp4");
                    bool converted = false;

                    if (!string.IsNullOrEmpty(ffmpeg) && File.Exists(ffmpeg))
                    {
                        try
                        {
                            var psi = new ProcessStartInfo(ffmpeg, "-y -i \"" + ts.FullName + "\" -c copy \"" + outPath + "\"")
                            {
                                CreateNoWindow = true, UseShellExecute = false, RedirectStandardError = true
                            };
                            using (var p = Process.Start(psi)) { p.WaitForExit(); converted = p.ExitCode == 0 && File.Exists(outPath); }
                        }
                        catch { converted = false; }
                    }
                    if (!converted) { try { File.Copy(ts.FullName, outPath, false); converted = File.Exists(outPath); } catch { converted = false; } }
                    if (converted) { try { File.Delete(ts.FullName); ok++; } catch { fail++; } }
                    else fail++;

                    done++;
                    int pct = total > 0 ? (done * 100 / total) : 0;
                    this.BeginInvoke(new Action(() =>
                    {
                        progressBar.Value = Math.Min(pct, 100);
                        lblProgress.Text = "转换中... (" + done + "/" + total + ")";
                    }));
                }

                this.BeginInvoke(new Action(() =>
                {
                    HideProgress();
                    MessageBox.Show("TS→MP4 转换完成！\n\n成功: " + ok + "\n失败: " + fail, "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshAll();
                }));
            });
        }
        #endregion

        #region 辅助
        private List<FileInfo> GetAllMedia(string root, bool includeSubdir)
        {
            var ext = _videoExt.Concat(_imageExt).ToArray();
            var results = new List<FileInfo>();

            if (includeSubdir)
            {
                var stack = new Stack<DirectoryInfo>();
                stack.Push(new DirectoryInfo(root));
                while (stack.Count > 0)
                {
                    var d = stack.Pop();
                    FileInfo[] files = null; DirectoryInfo[] dirs = null;
                    try { files = d.GetFiles("*.*", SearchOption.TopDirectoryOnly); } catch { }
                    try { dirs = d.GetDirectories(); } catch { }
                    if (files != null) foreach (var f in files) if (ext.Contains(f.Extension, StringComparer.OrdinalIgnoreCase)) results.Add(f);
                    if (dirs != null) foreach (var sub in dirs) if (_showHidden || (sub.Attributes & FileAttributes.Hidden) == 0) stack.Push(sub);
                }
            }
            else
            {
                try { foreach (var f in new DirectoryInfo(root).GetFiles("*.*", SearchOption.TopDirectoryOnly)) if (ext.Contains(f.Extension, StringComparer.OrdinalIgnoreCase)) results.Add(f); } catch { }
            }
            return results;
        }

        private bool IsHorizontal(string filePath)
        {
            // ffprobe
            string ffprobe = _config.FFprobePath;
            if (!string.IsNullOrEmpty(ffprobe) && File.Exists(ffprobe))
            {
                try
                {
                    var psi = new ProcessStartInfo(ffprobe, "-v quiet -show_streams \"" + filePath + "\"")
                    {
                        CreateNoWindow = true, UseShellExecute = false, RedirectStandardOutput = true
                    };
                    using (var p = Process.Start(psi))
                    {
                        string output = p.StandardOutput.ReadToEnd();
                        int w = 0, h = 0;
                        foreach (var line in output.Split('\n'))
                        {
                            if (line.StartsWith("width=")) int.TryParse(line.Substring(6).Trim(), out w);
                            if (line.StartsWith("height=")) int.TryParse(line.Substring(7).Trim(), out h);
                        }
                        if (w > 0 && h > 0) return w >= h;
                    }
                }
                catch { }
            }
            // 文件头
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buf = new byte[64];
                    fs.Read(buf, 0, buf.Length);
                    for (int i = 0; i < buf.Length - 8; i++)
                        if (buf[i] == 'w' && buf[i + 1] == 'i' && buf[i + 2] == 'd' && buf[i + 3] == 't')
                        { int w = (buf[i + 4] << 8) | buf[i + 5]; int h = (buf[i + 6] << 8) | buf[i + 7]; if (w > 0 && h > 0) return w >= h; }
                }
            }
            catch { }
            // Image
            try { using (var img = Image.FromFile(filePath)) { if (img.Width > 0 && img.Height > 0) return img.Width >= img.Height; } } catch { }
            // 文件名
            string n = Path.GetFileNameWithoutExtension(filePath).ToLower();
            if (n.Contains("竖") || n.Contains("portrait") || n.Contains("vertical")) return false;
            if (n.Contains("横") || n.Contains("landscape")) return true;
            return true;
        }

        private bool IsRoot(string p) { try { return new DirectoryInfo(p).Parent == null; } catch { return false; } }


        private string FormatSize(long b)
        {
            string[] u = { "B", "KB", "MB", "GB", "TB" };
            double v = b; int i = 0;
            while (v >= 1024 && i < u.Length - 1) { i++; v /= 1024; }
            return string.Format("{0:0.##} {1}", v, u[i]);
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void tm_tc_Click(object sender, EventArgs e)
        {

        }

        private void ts_about_Click(object sender, EventArgs e)
        {
            using (HelpForm dlg = new HelpForm())
            {
                dlg.ShowDialog(this); // ✅ 模态对话框
            }
        }
    }
}
