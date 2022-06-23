using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Moq;

namespace Easy.Tool.IgnoresAccessChecks.Test
{
    public class MakePublicInternalTest
    {
        /// <summary>
        /// 公开测试
        /// </summary>
        [Fact]
        public void MakePublicTest()
        {
            MakePublicInternal make = new()
            {
                IgnoresAccessChecksAssemblyNames = "Microsoft.Extensions.DependencyInjection;System.Runtime",
                SourceRefs = new ITaskItem[]
                {
                    new TaskItem(@"C:\Users\safetech3\.nuget\packages\microsoft.extensions.dependencyinjection\6.0.0\lib\net6.0\Microsoft.Extensions.DependencyInjection.dll"),
                    //new TaskItem(@"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.5\ref\net6.0\System.Runtime.dll"),
                },
                IntermediateOutputPath = Directory.GetCurrentDirectory(),
                UseEmptyMethodBody = true,
                BuildEngine = new Mock<IBuildEngine>().Object,
            };

            bool result = make.Execute();
        }
    }
}
