using System.Collections.Concurrent;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Configuration;
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
    public ITaskItem[] MainJsonFiles { get; set; }

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
    /// msbuild 执行方法
    /// </summary>
    /// <returns>返回true表示任务成功，返回false表示任务失败</returns>
    public override bool Execute()
    {
        if (!Directory.Exists(OutputPath)) Directory.CreateDirectory(OutputPath);
        // 确认参与合并的json文件
        ConcurrentDictionary<string, ITaskItem> participateMergeJsonFiles = new();
        // 得到所有主文件路径
        List<(string Path, ITaskItem Item)> mainFiles = MainJsonFiles.Select(m => (m.GetMetadata("FullPath"), m)).ToList();
        // 得到所有可能参加合并的文件
        List<(string Path, ITaskItem Item)> jsonFiles = JsonFileItems.Select(m => (m.GetMetadata("FullPath"), m)).ToList();
        try
        {
            foreach ((string Path, ITaskItem Item) in mainFiles)
            {
                // 如果没有该文件跳过
                if (!jsonFiles.Any(j => j.Path == Path)) continue;

                // 标记当前json已经参与合并
                participateMergeJsonFiles.TryAdd(Path, Item);

                // 加载json配置
                IConfigurationRoot json = new ConfigurationBuilder().AddJsonFile(Path).Build();

                // 得到要合并json的子目录
                string[] subDirectorys = json.GetSection(Item.GetMetadata("SubDirectoryNode")).Get<string[]>()?.Select(sd => sd.EndsWith("\\") ? sd : sd + "\\").Where(sd => Directory.Exists(sd)).ToArray() ?? Array.Empty<string>();
                // 得到要排除的子文件(文件名)节点
                string[] excludeSubFiles = json.GetSection(Item.GetMetadata("ExcludeSubFilesNode")).Get<string[]>() ?? Array.Empty<string>();

                // 得到所有要合并的文件
                List<FileInfo> subFiles = new();
                foreach (string subDir in subDirectorys) subFiles.AddRange(Directory.GetParent(subDir).GetFiles("*.json"));
                subFiles = subFiles.Where(sf => !excludeSubFiles.Any(esf => esf == sf.Name)).ToList();
                IEnumerable<(string Path, ITaskItem Item)> mergeJsonFiles = jsonFiles.Where(jf => subFiles.Any(sf => sf.FullName == jf.Path));

                // 得到合并json
                StringBuilder stringBuilder = new("{");
                stringBuilder.AppendLine(File.ReadAllText(Path).Trim().Trim('{', '}'));
                foreach ((string mergePath, ITaskItem mergeItem) in mergeJsonFiles)
                {
                    stringBuilder.AppendLine(File.ReadAllText(mergePath).Trim().Trim('{', '}'));
                    participateMergeJsonFiles.TryAdd(mergePath, mergeItem);
                }
                stringBuilder.Append("}");

                // 写入文件
                File.WriteAllText(OutputPath + Item.GetMetadata("Filename") + Item.GetMetadata("Extension"), stringBuilder.ToString());
            }
        }
        catch (Exception ex)
        {
            Log.LogErrorFromException(ex);
        }
        finally
        {
            ParticipateMergeJsonFiles = participateMergeJsonFiles.Values.ToArray();
        }
        // 有错误自动返回任务失败
        return !Log.HasLoggedErrors;
    }
}
