namespace Easy.Extensions.Emit.Test.GeneratorDynamicAssemlys;

public class HelloWorld
{
    public static string _assemblyName = nameof(HelloWorld);

    public static AssemblyBuilder _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(_assemblyName), AssemblyBuilderAccess.RunAndSave);

    public static ModuleBuilder _moduleBuilder = _assemblyBuilder.DefineDynamicModule(_assemblyName, $"{_assemblyName}.dll");


    public static void Create()
    {
        var typeBuilder = _moduleBuilder.DefineType("HelloWorld", TypeAttributes.Public);
        
        var methodBuilder = typeBuilder.DefineMethod("Main", MethodAttributes.Public | MethodAttributes.Static, typeof(void), new Type[] { });
        var il = methodBuilder.GetILGenerator();
        il.Emit(OpCodes.Ldstr, "Hello World!");
        il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
        il.Emit(OpCodes.Ret);

        typeBuilder.CreateType();

        _assemblyBuilder.Save($"{_assemblyName}.dll");
    }
}
