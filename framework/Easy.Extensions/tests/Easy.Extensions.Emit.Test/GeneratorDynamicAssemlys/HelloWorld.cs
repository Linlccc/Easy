namespace Easy.Extensions.Emit.Test.GeneratorDynamicAssemlys;

public class HelloWorld
{
    public static void Create()
    {
        var assemblyName = "HelloWorld";
        var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run);
        var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName);
        var typeBuilder = moduleBuilder.DefineType("HelloWorld", TypeAttributes.Public);
        var methodBuilder = typeBuilder.DefineMethod("Main", MethodAttributes.Public | MethodAttributes.Static, typeof(void), new Type[] { });
        var il = methodBuilder.GetILGenerator();
        il.Emit(OpCodes.Ldstr, "Hello World!");
        il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
        il.Emit(OpCodes.Ret);
        var type = typeBuilder.CreateType();
        assemblyBuilder.Save(assemblyName + ".dll");
    }

    public static void GeneratorDynamicAssemly1()
    {
        var assemblyName = new AssemblyName("DynamicAssembly1");
        var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
        var moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicModule1", "DynamicAssembly1.dll");
        var typeBuilder = moduleBuilder.DefineType("DynamicType1", TypeAttributes.Public);
        var methodBuilder = typeBuilder.DefineMethod("DynamicMethod1", MethodAttributes.Public | MethodAttributes.Static, typeof(void), new Type[] { typeof(string) });
        var il = methodBuilder.GetILGenerator();
        il.Emit(OpCodes.Ldstr, "Hello, World!");
        il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
        il.Emit(OpCodes.Ret);
        typeBuilder.CreateType();
        assemblyBuilder.Save("F:\\DynamicAssembly1.dll");
    }
}
