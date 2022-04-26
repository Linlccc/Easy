using Microsoft.Build.Framework;
using Task = Microsoft.Build.Utilities.Task;

namespace Easy.Tool.MergeJson;

/// <summary>
/// 实现合并json清理任务
/// </summary>
public class CleanMergeJson : Task
{
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
    #endregion


    /// <summary>
    /// msbuild 执行方法
    /// </summary>
    /// <returns>返回true表示任务成功，返回false表示任务失败</returns>
    public override bool Execute()
    {
        // 确保工作路径和输出路径以分隔符结尾
        WorkDirectory = MergeJson.EnsureEndDirectorySeparator(WorkDirectory);
        OutputDirectory = MergeJson.EnsureEndDirectorySeparator(OutputDirectory);

        // 删除日志文件
        if (Directory.Exists(MergeLogDir)) Directory.Delete(MergeLogDir, true);

        // 得到所有主文件路径
        List<string> mainJsonPaths = MainJsonItems.Select(m => m.GetMetadata("FullPath")).ToList();
        // 得到所有可能参加合并的文件
        List<string> allJsonPaths = JsonItems.Select(m => m.GetMetadata("FullPath")).ToList();

        try
        {
            mainJsonPaths.Where(mj => allJsonPaths.Contains(mj)).ToList().ForEach(mj =>
              {
                  string fullFileName = MergeJson.GetOutFileName(OutputDirectory, WorkDirectory, mj,false);
                  if (File.Exists(fullFileName)) File.Delete(fullFileName);
                  // 如果目录中没有内容删除
                  DirectoryInfo dir = Directory.GetParent(fullFileName);
                  if(dir.Exists && !dir.GetDirectories().Any() && !dir.GetFiles().Any()) dir.Delete();
              });
        }
        catch (Exception ex)
        {
            Log.LogErrorFromException(ex);
        }
        // 有错误自动返回任务失败
        return !Log.HasLoggedErrors;
    }
}
