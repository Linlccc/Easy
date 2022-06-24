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
                IntermediateOutputPath = Directory.GetCurrentDirectory(),
                UseEmptyMethodBody = false,
                BuildEngine = new Mock<IBuildEngine>().Object,
            };

            bool result = make.Execute();
        }
    }
}
