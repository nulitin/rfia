# 资源文件方向智能助手 v1.1

.NET Framework 4.7.2 WinForm 纯 C# 项目。

## 新增功能（v1.1）

### ★ 随时终止按钮
- 重命名、复位、TS转MP4 等长时间操作时显示「■ 终止」按钮
- 点击后立即取消当前操作（已完成的文件保留，未处理的跳过）
- 操作期间按 `Esc` 键也可触发终止
- 操作期间其他按钮自动禁用，防止重复操作

### ★ 设置窗口（⚙ 设置）
- 配置 PotPlayer 路径（可自动检测 / 手动浏览）
- 配置 FFmpeg / FFprobe 路径（用于方向检测 + TS转MP4）
- 自定义横向/竖向文件前缀（默认 `横向_` / `竖向_`）
- 其他选项（自动刷新等）
- 保存到程序目录下的 `settings.ini`
- 修改后即时生效，无需重启

### settings.ini 示例
```ini
; 资源文件方向智能助手 - 配置文件

[Player]
PotPlayer=C:\Program Files\DAUM\PotPlayer\PotPlayer.exe

[Converter]
FFmpeg=C:\ffmpeg\bin\ffmpeg.exe
FFprobe=C:\ffmpeg\bin\ffprobe.exe

[Naming]
HorizontalPrefix=横向_
VerticalPrefix=竖向_

[Options]
ImageCacheSize=500
AutoRefresh=false
```

## 文件结构

| 文件 | 说明 |
|------|------|
| `Program.cs` | 程序入口 |
| `Config.cs` | INI 配置读写类 |
| `SettingsForm.cs` | 设置窗口（可视化编辑） |
| `MainForm.cs` | 主窗体业务逻辑 |
| `MainForm.Designer.cs` | ✅ 主窗体设计器文件（VS 可编辑） |
| `Properties/AssemblyInfo.cs` | 程序集信息 |
| `*.csproj` / `*.sln` | 项目和解决方案 |

## VS 编辑窗体

1. 双击 `.sln` 打开
2. 双击 `MainForm.cs` → 设计器
3. 双击 `SettingsForm.cs` → 设置窗口也可可视化编辑
4. F4 属性 / F7 代码

## 设计器安全

`SplitContainer.Panel1MinSize / Panel2MinSize / SplitterDistance`
不在 Designer.cs 中设置，在 OnLoad 中安全设置。

## 编译

```
msbuild 资源文件方向智能助手.csproj /p:Configuration=Release
```

v1.1.0 | © 2026
