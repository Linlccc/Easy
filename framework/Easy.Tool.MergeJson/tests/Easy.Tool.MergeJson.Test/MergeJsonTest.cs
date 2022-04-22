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
            new TaskItem("appsettings.Development.json"),
            new TaskItem("appsettings.1.json"),
            new TaskItem("CustomConfig\\AppSettings.json"),
        };
        foreach (var item in MainJsonFiles)
        {
            item.SetMetadata("SubDirectoryNode", "CustomConfigInfo:ConfigFileFolders");
            item.SetMetadata("ExcludeSubFilesNode", "CustomConfigInfo:ExcludeSubFiles");
        }

        // 得到所有json文件
        List<ITaskItem> taskItems = new List<ITaskItem>();
        Directory.GetParent(AppContext.BaseDirectory)!.GetFiles("*.json", SearchOption.AllDirectories).ToList().ForEach(file =>
        {
            taskItems.Add(new TaskItem(file.FullName));
        });
        JsonFileItems = taskItems.ToArray();
    }


    [Fact]
    public void Test1()
    {       
        MergeJson mergeJson = new() { OutputPath = "TestGenerate\\", MainJsonFiles = MainJsonFiles, JsonFileItems = JsonFileItems, WorkRootDirectory= Directory.GetCurrentDirectory() };
        mergeJson.BuildEngine = buildEngine.Object;

        bool success = mergeJson.Execute();


        Assert.True(success);

        Assert.Equal(34, mergeJson.ParticipateMergeJsonFiles.Length);
    }
}
