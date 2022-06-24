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
    /// 源代码文件夹(项目所在文件夹)
    /// </summary>
    private readonly string _sourceDir = Directory.GetCurrentDirectory();

    private readonly AssemblyResolver _resolver = new();

    #region 排除的类型名称集合
    private string[] _excludeTypeNames;
    /// <summary>
    /// 
    /// </summary>
    private string[] ExcludeTypeNames
    {
        get
        {
            _excludeTypeNames ??= ExcludeTypeFullNames is null ? Array.Empty<string>() : ExcludeTypeFullNames.Split(_separates);
            return _excludeTypeNames;
        }
    }
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
    public string IgnoresAccessChecksAssemblyNames { get; set; }

    /// <summary>
    /// 排除类型完全限定名称集
    /// </summary>
    public string ExcludeTypeFullNames { get; set; }

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
        if (SourceRefs is null || SourceRefs.Length == 0) return !Log.HasLoggedErrors;

        HashSet<string> iACANames = new((IgnoresAccessChecksAssemblyNames ?? string.Empty).Split(_separates, StringSplitOptions.RemoveEmptyEntries), StringComparer.OrdinalIgnoreCase);
        if (iACANames.Count == 0) return !Log.HasLoggedErrors;

        // 生成访问忽略类型
        GenerateIgnoresAccessChecksToAttribute(iACANames);

        foreach (string sourceDir in SourceRefs.Select(s => Path.GetDirectoryName(GetFullPath(s.ItemSpec)))) _resolver.AddSearchDir(sourceDir);

        List<ITaskItem> makePublicRefs = new();
        List<ITaskItem> removeRefs = new();

        foreach (ITaskItem assembly in SourceRefs)
        {
            string assemblyFullName = GetFullPath(assembly.ItemSpec);
            string assemblyWithoutExtensionName = Path.GetFileNameWithoutExtension(assemblyFullName);
            if (!iACANames.Contains(assemblyWithoutExtensionName)) continue;

            // 生成的公开程序集完全名称
            string makePublicRefFullName = Path.Combine(IntermediateOutputPath, Path.GetFileName(assemblyFullName));
            FileInfo makePublicRefAssemblyFile = new(makePublicRefFullName);
            if (!makePublicRefAssemblyFile.Exists || makePublicRefAssemblyFile.Length == 0)
            {
                MakePublicAssembly(assemblyFullName, makePublicRefFullName);
                Log.LogMessageFromText($"成功创建公开程序集：{makePublicRefFullName}", MessageImportance.Normal);
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

    private void MakePublicAssembly(string sourceFile, string makePublicFile)
    {
        AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(sourceFile, new ReaderParameters() { AssemblyResolver = _resolver });
        IEnumerable<TypeDefinition> types = assembly.Modules.SelectMany(m => m.GetTypes()).Where(t => !ExcludeTypeNames.Contains(t.FullName));
        foreach (TypeDefinition type in types)
        {
            if (!type.IsNested && type.IsNotPublic) type.IsPublic = true;
            else if (type.IsNestedAssembly || type.IsNestedFamilyOrAssembly || type.IsNestedFamilyAndAssembly) type.IsNestedPublic = true;

            foreach (FieldDefinition field in type.Fields.Where(f => f.IsAssembly || f.IsFamilyOrAssembly || f.IsFamilyAndAssembly)) field.IsPublic = true;

            foreach (MethodDefinition method in type.Methods)
            {
                if (UseEmptyMethodBody && method.HasBody)
                {
                    method.Body = new MethodBody(method);
                    method.Body.Instructions.Add(Instruction.Create(OpCodes.Ldnull));
                    method.Body.Instructions.Add(Instruction.Create(OpCodes.Throw));
                }

                if (method.IsAssembly || method.IsFamilyOrAssembly || method.IsFamilyAndAssembly) method.IsPublic = true;
            }
        }
        assembly.Write(makePublicFile);
    }


    private string GetFullPath(string path)
    {
        if (!Path.IsPathRooted(path)) path = Path.Combine(_sourceDir, path);
        path = Path.GetFullPath(path);
        return path;
    }
}


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
