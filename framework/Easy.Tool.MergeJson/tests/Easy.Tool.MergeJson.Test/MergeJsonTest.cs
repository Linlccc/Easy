using Microsoft.Build.Framework;
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

    public MergeJsonTest()
    {
        buildEngine = new Mock<IBuildEngine>();
        errors = new List<BuildErrorEventArgs>();
    }


    [Fact]
    public void Test1()
    {
        var item = new Mock<ITaskItem>();
        item.Setup(x => x.GetMetadata("FullPath")).Returns("appsettings.json");
        MergeJson mergeJson = new() { OutputPath=Path.Combine("TestGenerate",""),MainJsonFiles = "appsettings.json", JsonFileItems = new[] { item.Object } };
        mergeJson.BuildEngine = buildEngine.Object;

        bool success = mergeJson.Execute();


        Assert.True(success);
    }
}
