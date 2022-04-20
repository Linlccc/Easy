using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Task = Microsoft.Build.Utilities.Task;

namespace Easy.Tool.MergeJson;

/// <summary>
/// 实现合并json任务
/// </summary>
public class MergeJson : Task
{
    /// <summary>
    /// 主要json文件
    /// </summary>
    [Required]
    public string MainJsonFiles { get; set; }

    /// <summary>
    /// 需要合并的json文件
    /// </summary>
    [Required]
    public ITaskItem[] JsonFileItems { get; set; }

    /// <summary>
    /// 合并json输出路径
    /// </summary>
    [Required]
    public string OutputPath { get; set; }

    /// <summary>
    /// 【输出】确认参与合并的json文件
    /// </summary>
    [Output]
    public ITaskItem[] ParticipateMergeJsonFiles { get; set; }

    /// <summary>
    /// 文件路径集合
    /// </summary>
    private List<string> JsonFilePaths => JsonFileItems.Select(j => j.GetMetadata("FullPath")).ToList();

    /// <summary>
    /// msbuild 执行方法
    /// </summary>
    /// <returns>返回true表示任务成功，返回false表示任务失败</returns>
    public override bool Execute()
    {
        // 可用 Directory.GetCurrentDirectory()
        Directory.GetParent(AppContext.BaseDirectory).GetFiles(MainJsonFiles, SearchOption.TopDirectoryOnly);

        System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string value = File.ReadAllText(MainJsonFiles);
        File.WriteAllText(OutputPath+"A.G.S.json", value+"\r\n"+ JsonFilePaths[0].GetType().FullName + "\r\n" + AppContext.BaseDirectory + "\r\n" +
            Directory.GetCurrentDirectory());

        ParticipateMergeJsonFiles = JsonFileItems;


        return true;
    }
}
