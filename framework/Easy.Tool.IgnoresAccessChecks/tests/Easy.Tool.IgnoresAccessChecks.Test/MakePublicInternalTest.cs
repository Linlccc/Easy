using System.Reflection;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Moq;

namespace Easy.Tool.IgnoresAccessChecks.Test
{
    public class MakePublicInternalTest
    {
        /// <summary>
        /// 生成公开程序集测试
        /// </summary>
        [Fact]
        public void MakePublicTest()
        {
            MakePublicInternal make = new()
            {
                IgnoresAccessChecksAssemblyNames = "System.Runtime",
                SourceRefs = new ITaskItem[]
                {
                    new TaskItem(Assembly.Load("System.Runtime").Location),
                },
                IntermediateOutputPath = Path.Combine(Directory.GetCurrentDirectory(), "IgnoresAccessChecks"),
                UseEmptyMethodBody = false,
                BuildEngine = new Mock<IBuildEngine>().Object,
            };

            bool result = make.Execute();
        }

        /// <summary>
        /// 生成公开程序集测试
        /// </summary>
        [Fact]
        public void MakePublicTest_DI()
        {
            string userDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            MakePublicInternal make = new()
            {
                IgnoresAccessChecksAssemblyNames = "Microsoft.Extensions.DependencyInjection",
                SourceRefs = new ITaskItem[]
                {
                    new TaskItem(Path.Combine(userDir,@".nuget\packages\microsoft.extensions.dependencyinjection\6.0.0\lib\net6.0\Microsoft.Extensions.DependencyInjection.dll")),
                    new TaskItem(@"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.5\ref\net6.0\System.Runtime.dll")
                },
                IntermediateOutputPath = Path.Combine(Directory.GetCurrentDirectory(), "IgnoresAccessChecks"),
                UseEmptyMethodBody = false,
                BuildEngine = new Mock<IBuildEngine>().Object,
            };

            bool result = make.Execute();
        }
    }
}
