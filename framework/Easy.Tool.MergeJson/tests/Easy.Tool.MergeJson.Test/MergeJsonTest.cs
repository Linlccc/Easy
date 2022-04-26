using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Moq;
using Xunit;

namespace Easy.Tool.MergeJson.Test;

/// <summary>
/// 测试合并json文件
/// </summary>
public class MergeJsonTest
{
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
    private Mock<IBuildEngine> buildEngine;
    private List<BuildErrorEventArgs> errors;
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
    private string deaultPath;

    private ITaskItem[] MainJsonFiles;
    private ITaskItem[] JsonFileItems;

    public MergeJsonTest()
    {
        buildEngine = new Mock<IBuildEngine>();
        errors = new List<BuildErrorEventArgs>();
        // 得到主要json文件
        MainJsonFiles = new[]
        {
            new TaskItem("appsettings.json"),
            new TaskItem("appsettings.pro.json"),
            new TaskItem("appsettings.dev.json"),
            new TaskItem("config\\appsettings.json"),
            new TaskItem("config\\appsettings.d.json"),
        };
        foreach (var item in MainJsonFiles)
        {
            item.SetMetadata("SubDirectoryNode", "CustomConfigInfo.ConfigFileFolders");
            item.SetMetadata("ExcludeSubFilesNode", "CustomConfigInfo.ExcludeSubFiles");
        }

        // 得到所有json文件
        List<ITaskItem> taskItems = new List<ITaskItem>();
        Directory.GetParent(AppContext.BaseDirectory)!.GetFiles("*.json", SearchOption.AllDirectories).ToList().ForEach(file =>
        {
            taskItems.Add(new TaskItem(file.FullName));
        });
        JsonFileItems = taskItems.ToArray();
    }

    /// <summary>
    /// 合并测试
    /// </summary>
    [Fact]
    public void MergeTest()
    {
        MergeJson mergeJson = new() { OutputDirectory = "TestGenerate\\", MainJsonItems = MainJsonFiles, JsonItems = JsonFileItems, WorkDirectory = AppContext.BaseDirectory , SaveMergeLog =true};
        mergeJson.BuildEngine = buildEngine.Object;

        bool success = mergeJson.Execute();

        Assert.True(success);
        
        Assert.True(File.Exists(@"TestGenerate\appsettings.json"));
    }

    /// <summary>
    /// 清理合并测试
    /// </summary>
    [Fact]
    public void CleanMergeTest()
    {
        CleanMergeJson cleanMergeJson = new() { OutputDirectory = "TestGenerate\\", MainJsonItems = MainJsonFiles, JsonItems = JsonFileItems, WorkDirectory = AppContext.BaseDirectory };
        cleanMergeJson.BuildEngine = buildEngine.Object;

        bool success = cleanMergeJson.Execute();

        Assert.True(success);
        
        Assert.False(File.Exists(@"TestGenerate\appsettings.json"));
    }


}
