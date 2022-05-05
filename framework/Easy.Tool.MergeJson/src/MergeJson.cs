using System.Diagnostics;
using Microsoft.Build.Framework;
using Newtonsoft.Json.Linq;
using Task = Microsoft.Build.Utilities.Task;

namespace Easy.Tool.MergeJson;

/// <summary>
/// 实现合并json任务
/// </summary>
public class MergeJson : Task
{
    #region 内部字段
    /// <summary>
    /// 程序版本信息
    /// </summary>
    private readonly FileVersionInfo _assemblyInfo;
    private readonly string _mergeLogFile = $"MergLogs-{DateTime.Now:yyyy-MM-dd HH_mm_ss}.log";
    private readonly string _logPartingStr;
    private readonly List<ITaskItem> _mergeJsonItems = new();
    #endregion
    public MergeJson()
    {
        _assemblyInfo = FileVersionInfo.GetVersionInfo(GetType().Assembly.Location);
        _logPartingStr = $"{new string('-', 15)}{_assemblyInfo.ProductVersion}{new string('-', 15)}{_assemblyInfo.FileVersion}{new string('-', 15)}";
    }

    /// <summary>
    /// 日志文件夹
    /// </summary>
    public string MergeLogDir => Path.Combine(OutputDirectory, "mergeLogs");

    #region 任务变量
    /// <summary>
    /// 所有可能要参与合并的Json项
    /// </summary>
    [Required]
    public ITaskItem[] JsonItems { get; set; }

    /// <summary>
    /// 主Json项
    /// </summary>
    [Required]
    public ITaskItem[] MainJsonItems { get; set; }

    /// <summary>
    /// 工作目录
    /// </summary>
    [Required]
    public string WorkDirectory { get; set; }

    /// <summary>
    /// 输出目录
    /// </summary>
    [Required]
    public string OutputDirectory { get; set; }

    /// <summary>
    /// 保存合并信息
    /// </summary>
    [Required]
    public bool SaveMergeLog { get; set; }

    /// <summary>
    /// 【输出】合并Json项
    /// </summary>
    [Output]
    public ITaskItem[] MergeJsonItems => _mergeJsonItems.ToArray();
    #endregion


    /// <summary>
    /// msbuild 执行方法
    /// </summary>
    /// <returns>返回true表示任务成功，返回false表示任务失败</returns>
    public override bool Execute()
    {
        // 确保工作路径和输出路径以分隔符结尾
        WorkDirectory = EnsureEndDirectorySeparator(WorkDirectory);
        OutputDirectory = EnsureEndDirectorySeparator(OutputDirectory);
        // 确保输出路径存在
        if (!Directory.Exists(OutputDirectory)) Directory.CreateDirectory(OutputDirectory);

        // 得到所有主文件路径
        List<(string path, ITaskItem item)> mainJsonItems = MainJsonItems.Select(m => (m.GetMetadata("FullPath"), m)).ToList();
        // 得到所有可能参加合并的文件
        List<string> allJsonPaths = JsonItems.Select(m => m.GetMetadata("FullPath")).ToList();

        try
        {
            // 添加日志基本信息
            if (SaveMergeLog)
            {
                if (!Directory.Exists(MergeLogDir)) Directory.CreateDirectory(MergeLogDir);
                File.AppendAllText(Path.Combine(MergeLogDir, _mergeLogFile), $"工作目录:\t{WorkDirectory}\r\n输出目录:\t{OutputDirectory}\r\n{_logPartingStr}\r\n");
            }
            foreach ((string path, ITaskItem item) in mainJsonItems)
            {
                // 如果没有该文件跳过
                if (!allJsonPaths.Any(j => j == path)) continue;

                JObject jObj = JObject.Parse(File.ReadAllText(path));
                // 得到要合并json的子目录
                string[] subDirectorys = jObj.SelectToken(item.GetMetadata("SubDirectoryNode"))?.ToArray().Select(jt => EnsureEndDirectorySeparator(jt.ToString())).Where(p => Directory.Exists(p)).ToArray() ?? Array.Empty<string>();
                // 得到要排除的子文件(文件名)节点
                string[] excludeSubFiles = jObj.SelectToken(item.GetMetadata("ExcludeSubFilesNode"))?.ToArray().Select(jt => jt.ToString()).ToArray() ?? Array.Empty<string>();

                // 得到所有要合并的文件
                IEnumerable<FileInfo> subFiles = subDirectorys.SelectMany(subDir => new DirectoryInfo(subDir).GetFiles("*.json", SearchOption.TopDirectoryOnly)).Where(subf => !excludeSubFiles.Any(esf => esf == subf.Name));
                List<string> mergeJsonFiles = allJsonPaths.Where(j => subFiles.Any(sf => sf.FullName == j)).ToList();

                // 合并json
                foreach (string mergePath in mergeJsonFiles) jObj.Merge(JObject.Parse(File.ReadAllText(mergePath)));

                // 得到合并后文件名
                string fullFileName = GetOutFileName(OutputDirectory, WorkDirectory, path);

                // 写入文件
                File.WriteAllText(fullFileName, jObj.ToString());

                // 合并后的文件名
                item.SetMetadata("MergeFileFullName", fullFileName);
                // 合并了那些文件
                item.SetMetadata("MergeJsonFiles", string.Join("\r\n", mergeJsonFiles));
                MarkMerge(item);
            }
        }
        catch (Exception ex)
        {
            Log.LogErrorFromException(ex);
        }
        // 有错误自动返回任务失败
        return !Log.HasLoggedErrors;
    }

    /// <summary>
    /// 标记合并
    /// </summary>
    /// <param name="taskItem"></param>
    private void MarkMerge(ITaskItem taskItem)
    {
        // 添加到合并集合
        _mergeJsonItems.Add(taskItem);
        // 拼接合并信息
        string mergeInfo = $@"主文件:
{taskItem.GetMetadata("FullPath")}
合并后文件:
{taskItem.GetMetadata("MergeFileFullName")}
子文件:
{taskItem.GetMetadata("MergeJsonFiles")}
{_logPartingStr}" + "\r\n\r\n";
        // 记录msbuild日志
        Log.LogMessage(mergeInfo);
        // 添加文本日志
        if (SaveMergeLog)
        {
            if (!Directory.Exists(MergeLogDir)) Directory.CreateDirectory(MergeLogDir);
            File.AppendAllText(Path.Combine(MergeLogDir, _mergeLogFile), mergeInfo);
        }
    }

    #region 静态方法
    /// <summary>
    /// 取保以目录分隔符结尾
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>以目录分隔符结尾的路径</returns>
    public static string EnsureEndDirectorySeparator(string path)
    {
        if (string.IsNullOrEmpty(path)) return path;
        char last = path[path.Length - 1];
        if (last != Path.DirectorySeparatorChar && last != Path.AltDirectorySeparatorChar) return path += Path.DirectorySeparatorChar;
        return path;
    }

    /// <summary>
    /// 得到输出文件完整路径
    /// </summary>
    /// <param name="outputDir">输出目录</param>
    /// <param name="workDir">工作目录</param>
    /// <param name="rawPath">原路径</param>
    /// <param name="isCreateDir">如果路径上的目录不存在是否创建</param>
    /// <returns></returns>
    public static string GetOutFileName(string outputDir, string workDir, string rawPath,bool isCreateDir = true)
    {
        // 得到文件名
        string fileName = Path.GetFileName(rawPath);
        string fullFileName = Path.Combine(outputDir, fileName);
        // 如果是工作目录中的文件，获取相对路径
        if (rawPath.StartsWith(workDir,StringComparison.CurrentCultureIgnoreCase))
        {
            string middlePath = rawPath.Remove(0,workDir.Length).Replace(fileName, "");
            string fullPath = Path.Combine(outputDir, middlePath);
            if (!Directory.Exists(fullPath) && isCreateDir) Directory.CreateDirectory(fullPath);
            fullFileName = Path.Combine(fullPath, fileName);
        }
        return fullFileName;
    }
    #endregion
}
