using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Task = Microsoft.Build.Utilities.Task;

namespace Easy.Tool.IgnoresAccessChecks;

/// <summary>
///公开内部
/// </summary>
public class MakePublicInternal : Task
{
    /// <summary>
    /// 分隔符号
    /// </summary>
    private readonly char[] _separates = { ';', ',' };
    /// <summary>
    /// 程序集解析器
    /// </summary>
    private readonly AssemblyResolver _resolver = new();

    #region 参数处理结果
    /// <summary>
    /// 排除类型完全限定名称集
    /// </summary>
    private string[] _excludeTypeFullNames = Array.Empty<string>();

    /// <summary>
    /// 忽略访问检查的程序集名称集
    /// </summary>
    private HashSet<string> _ignoresAccessChecksAssemblyNames = new();
    #endregion

    #region 输入(变量)
    /// <summary>
    /// 项目生成期间的中间输出路径<br />
    /// 用于输出生成的文件与程序集
    /// </summary>
    [Required]
    public string IntermediateOutputPath { get; set; }

    /// <summary>
    /// 原引用集合
    /// </summary>
    [Required]
    public ITaskItem[] SourceRefs { get; set; }

    /// <summary>
    /// 忽略访问检查的程序集名称集<br />
    /// 使用 <see cref="_separates"/> 中的符号分隔
    /// </summary>
    public string IgnoresAccessChecksAssemblyNames { set { if (!string.IsNullOrEmpty(value)) _ignoresAccessChecksAssemblyNames = new(value.Split(_separates, StringSplitOptions.RemoveEmptyEntries), StringComparer.OrdinalIgnoreCase); } }

    /// <summary>
    /// 排除类型完全限定名称集
    /// </summary>
    public string ExcludeTypeFullNames { set { if (!string.IsNullOrEmpty(value)) _excludeTypeFullNames = value.Split(_separates); } }

    /// <summary>
    /// 使用空方法体
    /// </summary>
    public bool UseEmptyMethodBody { get; set; }
    #endregion

    #region 输出
    /// <summary>
    /// 公开引用集合
    /// </summary>
    [Output]
    public ITaskItem[] MakePublicRefs { get; set; }

    /// <summary>
    /// 移除的引用集合
    /// </summary>
    [Output]
    public ITaskItem[] RemoveRefs { get; set; }

    /// <summary>
    /// 生成的代码文件集合
    /// </summary>
    [Output]
    public ITaskItem[] GeneratedCodeFiles { get; set; }
    #endregion


    public override bool Execute()
    {
        // 没有原引用程序集 || 没有要忽略访问检查的程序集
        if (SourceRefs is null || SourceRefs.Length == 0 || _ignoresAccessChecksAssemblyNames.Count == 0) return !Log.HasLoggedErrors;

        // 生成访问忽略类型
        GenerateIgnoresAccessChecksToAttribute(_ignoresAccessChecksAssemblyNames);

        foreach (string sourceDir in SourceRefs.Select(s => Path.GetDirectoryName(Path.GetFullPath(s.ItemSpec)))) _resolver.AddSearchDir(sourceDir);

        List<ITaskItem> makePublicRefs = new();
        List<ITaskItem> removeRefs = new();

        foreach (ITaskItem assembly in SourceRefs)
        {
            string assemblyFullName = Path.GetFullPath(assembly.ItemSpec);
            string assemblyWithoutExtensionName = Path.GetFileNameWithoutExtension(assemblyFullName);
            if (!_ignoresAccessChecksAssemblyNames.Contains(assemblyWithoutExtensionName)) continue;

            // 生成的公开程序集完全名称
            string makePublicRefFullName = Path.Combine(IntermediateOutputPath, Path.GetFileName(assemblyFullName));
            FileInfo makePublicRefAssemblyFile = new(makePublicRefFullName);
            if (!makePublicRefAssemblyFile.Exists || makePublicRefAssemblyFile.Length == 0)
            {
                // 生成公开程序集
                try { MakePublicAssembly(assemblyFullName, makePublicRefFullName); }
                catch (Exception ex) { Log.LogMessageFromText($"生成'{assemblyFullName}'公开程序集时产生异常： {ex.Message}", MessageImportance.High); continue; }

                Log.LogMessageFromText($"成功生成公开程序集：{makePublicRefFullName}", MessageImportance.Normal);
            }
            else Log.LogMessageFromText($"公开程序集已存在：{makePublicRefFullName}", MessageImportance.Low);

            makePublicRefs.Add(new TaskItem(makePublicRefFullName));
            removeRefs.Add(assembly);
        }

        MakePublicRefs = makePublicRefs.ToArray();
        RemoveRefs = removeRefs.ToArray();
        return !Log.HasLoggedErrors;
    }

    /// <summary>
    /// 生成忽略访问检查特性
    /// </summary>
    /// <param name="ignoresAccessChecksAssemblyNames">要忽略的程序集名称集合</param>
    private void GenerateIgnoresAccessChecksToAttribute(IEnumerable<string> ignoresAccessChecksAssemblyNames)
    {
        string content = string.Join(Environment.NewLine, ignoresAccessChecksAssemblyNames.Select(ia => $"[assembly: System.Runtime.CompilerServices.IgnoresAccessChecksTo(\"{ia}\")]")) + @"
namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    internal sealed class IgnoresAccessChecksToAttribute : Attribute
    {
        public string AssemblyName { get; set; }
        public IgnoresAccessChecksToAttribute(string assemblyName)
        {
            AssemblyName = assemblyName;
        }
    }
}";
        string IgnoresAccessChecksToAttributeFileFullName = Path.Combine(IntermediateOutputPath, "System.Runtime.CompilerServices.IgnoresAccessChecksToAttribute.cs");
        if (!Directory.Exists(IntermediateOutputPath)) Directory.CreateDirectory(IntermediateOutputPath);
        File.WriteAllText(IgnoresAccessChecksToAttributeFileFullName, content);

        GeneratedCodeFiles = new ITaskItem[] { new TaskItem(IgnoresAccessChecksToAttributeFileFullName) };

        Log.LogMessageFromText($"成功创建'System.Runtime.CompilerServices.IgnoresAccessChecksToAttribute.cs'文件：{IgnoresAccessChecksToAttributeFileFullName}", MessageImportance.Normal);
    }

    /// <summary>
    /// 生成公开程序集
    /// </summary>
    /// <param name="sourceFile"></param>
    /// <param name="makePublicFile"></param>
    private void MakePublicAssembly(string sourceFile, string makePublicFile)
    {
        AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(sourceFile, new ReaderParameters() { AssemblyResolver = _resolver });
        IEnumerable<TypeDefinition> types = assembly.Modules.SelectMany(m => m.GetTypes()).Where(t => !_excludeTypeFullNames.Contains(t.FullName));
        foreach (TypeDefinition type in types)
        {
            // 公开类型
            if (!type.IsNested && type.IsNotPublic) type.IsPublic = true;
            if (type.IsNested && !type.IsNestedPublic) type.IsNestedPublic = true;

            // 公开字段
            foreach (FieldDefinition field in type.Fields.Where(f => !f.IsPublic)) field.IsPublic = true;

            // 公开方法
            foreach (MethodDefinition method in type.Methods)
            {
                if (UseEmptyMethodBody && method.HasBody)
                {
                    method.Body = new MethodBody(method);
                    method.Body.Instructions.Add(Instruction.Create(OpCodes.Ldnull));
                    method.Body.Instructions.Add(Instruction.Create(OpCodes.Throw));
                }

                if (!method.IsPublic) method.IsPublic = true;
            }
        }
        assembly.Write(makePublicFile);
    }
}

/// <summary>
/// 程序集解析器
/// </summary>
class AssemblyResolver : IAssemblyResolver
{
    // 程序集的文件夹集合
    private readonly HashSet<string> _dirs = new(StringComparer.OrdinalIgnoreCase);

    public void AddSearchDir(string dir) => _dirs.Add(dir);


    public AssemblyDefinition Resolve(AssemblyNameReference name) => Resolve(name, new());

    public AssemblyDefinition Resolve(AssemblyNameReference name, ReaderParameters parameters)
    {
        string[] extensions = name.IsWindowsRuntime ? new[] { ".dll", ".winmd", } : new[] { ".dll", ".exe" };
        parameters.AssemblyResolver ??= this;

        foreach (string dir in _dirs)
        {
            foreach (string ext in extensions)
            {
                string fullFileName = Path.Combine(dir, name.Name + ext);
                if (!File.Exists(fullFileName)) continue;

                try
                {
                    return ModuleDefinition.ReadModule(fullFileName, parameters).Assembly;
                }
                catch (BadImageFormatException) { }
            }
        }

        return null;
    }


    public void Dispose() { }
}
