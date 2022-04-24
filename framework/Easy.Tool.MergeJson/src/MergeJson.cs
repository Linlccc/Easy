using System.Text;
using Microsoft.Build.Framework;
using Newtonsoft.Json.Linq;
using Task = Microsoft.Build.Utilities.Task;

namespace Easy.Tool.MergeJson;

/// <summary>
/// 实现合并json任务
/// </summary>
public class MergeJson : Task
{
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
    public ITaskItem[] MergeJsonItems { get; set; }


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
        // 合并过的项
        List<ITaskItem> mergeItems = new();

        try
        {
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
                List<FileInfo> subFiles = new();
                foreach (string subDir in subDirectorys) subFiles.AddRange(Directory.GetParent(subDir).GetFiles("*.json"));
                subFiles = subFiles.Where(sf => !excludeSubFiles.Any(esf => esf == sf.Name)).ToList();
                List<string> mergeJsonFiles = allJsonPaths.Where(j => subFiles.Any(sf => sf.FullName == j)).ToList();
                // 标记当前主json合并了那些json
                string subFileFullNames = string.Join("\r\n", mergeJsonFiles);

                // 合并json
                foreach (string mergePath in mergeJsonFiles) jObj.Merge(JObject.Parse(File.ReadAllText(mergePath)));

                // 得到文件名
                string fileName = Path.GetFileName(path);
                string fullFileName = Path.Combine(OutputDirectory, fileName);
                // 如果是工作目录中的文件，获取相对路径
                if (path.StartsWith(WorkDirectory))
                {
                    string middlePath = path.Replace(WorkDirectory, "").Replace(fileName, "");
                    string fullPath = Path.Combine(OutputDirectory, middlePath);
                    if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);
                    fullFileName = Path.Combine(fullPath, fileName);
                }

                // 写入文件
                File.WriteAllText(fullFileName, jObj.ToString());

                // 合并后的文件名
                item.SetMetadata("MergeFileFullName", fullFileName);
                // 合并了那些文件
                item.SetMetadata("MergeJsonFiles", subFileFullNames);
                // 标记合并
                mergeItems.Add(item);
            }
        }
        catch (Exception ex)
        {
            Log.LogErrorFromException(ex);
        }
        finally
        {
            MergeJsonItems = mergeItems.ToArray();
            
            // 保存合并信息
            if (SaveMergeLog)
            {
                string dir = Path.Combine(OutputDirectory, "MergeLog");
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                string fileName = Path.Combine(dir, $"MergLogs-{DateTime.Now:yyyy-MM-dd HH_FF_ss}.log");
                StringBuilder log = new();
                foreach (ITaskItem item in mergeItems)
                {
                    string cLog = $@"主文件:
{item.GetMetadata("FullPath")}
合并后文件:
{item.GetMetadata("MergeFileFullName")}
子文件:
{item.GetMetadata("MergeJsonFiles")}
{new string('-', 15)}";
                    // 记录msbuild日志
                    Log.LogMessage(cLog);
                    // 添加文本日志
                    log.AppendLine(cLog);
                    log.AppendLine();
                }
                File.WriteAllText(fileName, log.ToString());
            }
        }
        // 有错误自动返回任务失败
        return !Log.HasLoggedErrors;
    }


    /// <summary>
    /// 取保以目录分隔符结尾
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>以目录分隔符结尾的路径</returns>
    private string EnsureEndDirectorySeparator(string path)
    {
        if (string.IsNullOrEmpty(path)) Log.LogErrorFromException(new ArgumentNullException(nameof(path)));
        char last = path[path.Length - 1];
        if (last != Path.DirectorySeparatorChar && last != Path.AltDirectorySeparatorChar) return path += Path.DirectorySeparatorChar;
        return path;
    }
}
