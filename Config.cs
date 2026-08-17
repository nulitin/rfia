using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace 资源文件方向智能助手
{
    /// <summary>
    /// INI 配置管理 — 保存到程序目录下的 settings.ini
    /// </summary>
    public class Config
    {
        private static readonly string IniPath = Path.Combine(
            Path.GetDirectoryName(Application.ExecutablePath), "settings.ini");

        private Dictionary<string, string> _values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        #region 默认配置
        private static readonly Dictionary<string, string> Defaults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // 默认值：PotPlayer 标准安装路径
            ["PotPlayer"] = @"C:\Program Files\DAUM\PotPlayer\PotPlayer.exe",
            // 默认值：程序当前目录
            ["FFmpeg"] = "",
            ["FFprobe"] = "",
            ["HorizontalPrefix"] = "横向_",
            ["VerticalPrefix"] = "竖向_",
            ["IncludeSubdir"] = "false"
        };
        #endregion

        public Config()
        {
            Load();
            // FFmpeg/FFprobe 默认指向程序目录
            if (string.IsNullOrEmpty(Get("FFmpeg")))
                Set("FFmpeg", Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ffmpeg.exe"));
            if (string.IsNullOrEmpty(Get("FFprobe")))
                Set("FFprobe", Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ffprobe.exe"));
        }

        #region 加载 / 保存
        public void Load()
        {
            _values.Clear();
            foreach (var kv in Defaults) _values[kv.Key] = kv.Value;

            try
            {
                if (!File.Exists(IniPath)) { Save(); return; }

                foreach (var line in File.ReadAllLines(IniPath))
                {
                    string trimmed = line.Trim();
                    if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith(";") || trimmed.StartsWith("#"))
                        continue;

                    int eq = trimmed.IndexOf('=');
                    if (eq > 0)
                    {
                        string key = trimmed.Substring(0, eq).Trim();
                        string val = trimmed.Substring(eq + 1).Trim();
                        _values[key] = val;
                    }
                }
            }
            catch { }
        }

        public void Save()
        {
            try
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine("; 资源文件方向智能助手 - 配置文件");
                sb.AppendLine("; 可在「⚙ 设置」窗口中修改，或手动编辑此文件");
                sb.AppendLine();

                sb.AppendLine("[Player]");
                sb.AppendLine("PotPlayer=" + Get("PotPlayer"));
                sb.AppendLine();

                sb.AppendLine("[Converter]");
                sb.AppendLine("FFmpeg=" + Get("FFmpeg"));
                sb.AppendLine("FFprobe=" + Get("FFprobe"));
                sb.AppendLine();

                sb.AppendLine("[Naming]");
                sb.AppendLine("HorizontalPrefix=" + Get("HorizontalPrefix"));
                sb.AppendLine("VerticalPrefix=" + Get("VerticalPrefix"));
                sb.AppendLine();

                sb.AppendLine("[Options]");
                sb.AppendLine("IncludeSubdir=" + Get("IncludeSubdir"));

                File.WriteAllText(IniPath, sb.ToString());
            }
            catch { }
        }
        #endregion

        #region 取值 / 设值
        public string Get(string key)
        {
            if (_values.TryGetValue(key, out string val)) return val;
            if (Defaults.TryGetValue(key, out string def)) return def;
            return "";
        }

        public void Set(string key, string value)
        {
            _values[key] = value ?? "";
        }

        public bool GetBool(string key)
        {
            string v = Get(key).ToLower();
            return v == "true" || v == "1" || v == "yes";
        }
        #endregion

        #region 便捷属性
        public string PotPlayerPath
        {
            get { return Get("PotPlayer"); }
            set { Set("PotPlayer", value); }
        }

        public string FFmpegPath
        {
            get { return Get("FFmpeg"); }
            set { Set("FFmpeg", value); }
        }

        public string FFprobePath
        {
            get { return Get("FFprobe"); }
            set { Set("FFprobe", value); }
        }

        public string HorizontalPrefix
        {
            get { return Get("HorizontalPrefix"); }
            set { Set("HorizontalPrefix", value); }
        }

        public string VerticalPrefix
        {
            get { return Get("VerticalPrefix"); }
            set { Set("VerticalPrefix", value); }
        }

        /// <summary>
        /// 重命名/复位时是否包含子目录
        /// </summary>
        public bool IncludeSubdir
        {
            get { return GetBool("IncludeSubdir"); }
            set { Set("IncludeSubdir", value ? "true" : "false"); }
        }
        #endregion
    }
}
