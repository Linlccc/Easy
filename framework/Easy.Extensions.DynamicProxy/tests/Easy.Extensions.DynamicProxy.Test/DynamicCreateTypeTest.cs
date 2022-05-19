using System.Reflection;
using System.Reflection.Emit;
using Xunit;

namespace Easy.Extensions.DynamicProxy.Test;

public class DynamicCreateTypeTest
{
    /// <summary>
    /// 创建动态类型测试
    /// </summary>
    [Fact]
    public void CreateDynamicTypeTest()
    {
        Type dynamicType = DynamicCreateType.CreateMyDynamicType1();

        // 获取构造函数
        ConstructorInfo[] constructorInfos = dynamicType.GetConstructors();
        // 获取属性
        PropertyInfo propertyInfo = dynamicType.GetProperty("Number")!;
        // 获取方法
        MethodInfo methodInfo = dynamicType.GetMethod("MyMethod")!;

        Assert.Equal(4, constructorInfos.Length);
        Assert.NotNull(propertyInfo);
        Assert.NotNull(methodInfo);

        // 使用默认构造函数创建 MyDynamicType 的实例
        object o1 = Activator.CreateInstance(dynamicType)!;
        // 测试属性
        Assert.Equal(42, propertyInfo.GetValue(o1));
        propertyInfo.SetValue(o1, 99);
        Assert.Equal(99, propertyInfo.GetValue(o1));
        // 测试方法
        Assert.Equal(99 * 100, methodInfo.Invoke(o1, new object[] { 100 }));


        // 使用一个参数的构造函数创建实例
        object o2 = Activator.CreateInstance(dynamicType, new object[] { 100 })!;
        // 测试属性
        Assert.Equal(100, propertyInfo.GetValue(o2));

        // 使用一个参数的构造函数创建实例
        object o3 = Activator.CreateInstance(dynamicType, new object[] { 101,"123" })!;
        // 测试属性
        Assert.Equal(101, propertyInfo.GetValue(o3));

        // 使用一个参数的构造函数创建实例
        object o4 = Activator.CreateInstance(dynamicType, new object[] { "122133", 102 })!;
        // 测试属性
        Assert.Equal(102, propertyInfo.GetValue(o4));
    }

    /// <summary>
    /// 本地参数参数
    /// </summary>
    [Fact]
    public void LocalVarTest()
    {
        Type dynamicType = DynamicCreateType.CreateMyDynamicType2();

        // 使用默认构造函数创建 MyDynamicType 的实例
        object o1 = Activator.CreateInstance(dynamicType)!;

        // 执行方法
        object v1 = dynamicType.InvokeMember("TestLoaclVar1", BindingFlags.InvokeMethod, null, o1, new object[] { 13, 22 })!;
        object v2 = dynamicType.InvokeMember("TestLoaclVar2", BindingFlags.InvokeMethod, null, o1, new object[] { 43, 22 })!;
        object v3 = dynamicType.InvokeMember("TestLoaclVar3", BindingFlags.InvokeMethod, null, o1, new object[] { 43, 22 })!;
        object v4 = dynamicType.InvokeMember("TestLoaclVar4", BindingFlags.InvokeMethod, null, o1, new object[] { 43, 22 })!;
        object v5 = dynamicType.InvokeMember("TestLoaclVar5", BindingFlags.InvokeMethod, null, o1, new object[] { 43, 22 })!;
        object v6 = dynamicType.InvokeMember("TestLoaclVar6", BindingFlags.InvokeMethod, null, o1, new object[] { 43, 22 })!;
        object v7 = dynamicType.InvokeMember("TestLoaclVar7", BindingFlags.InvokeMethod, null, o1, new object[] { 43, 22 })!;
        object v8 = dynamicType.InvokeMember("TestLoaclVar8", BindingFlags.InvokeMethod, null, o1, new object[] { 43, 22 })!;
        object v9 = dynamicType.InvokeMember("TestLoaclVar9", BindingFlags.InvokeMethod, null, o1, new object[] { 43, 22 })!;
        object v10 = dynamicType.InvokeMember("TestLoaclVar10", BindingFlags.InvokeMethod, null, o1, new object[] { 43, 22 })!;
        object v11 = dynamicType.InvokeMember("TestLoaclVar11", BindingFlags.InvokeMethod, null, o1, new object[] { typeof(int) })!;
        object v12 = dynamicType.InvokeMember("TestLoaclVar12", BindingFlags.InvokeMethod, null, o1, new object[] { new IntPtr(5), new IntPtr(11) })!;
        object v13 = dynamicType.InvokeMember("TestLoaclVar13", BindingFlags.InvokeMethod, null, o1, new object[] { new IntPtr(5), new IntPtr(11) })!;
        object v14 = dynamicType.InvokeMember("TestLoaclVar14", BindingFlags.InvokeMethod, null, o1, new object[] { 5, "123" })!;
        object v15 = dynamicType.InvokeMember("TestLoaclVar15", BindingFlags.InvokeMethod, null, o1, new object[] { (float)14, (float)9 })!;
        object v16 = dynamicType.InvokeMember("TestLoaclVar16", BindingFlags.InvokeMethod, null, o1, new object[] { "abcdefghijklmnopqrstuvwxyz", 8 })!;
        object v17 = dynamicType.InvokeMember("TestLoaclVar17", BindingFlags.InvokeMethod, null, o1, new object[] { new Test1(1), 8 })!;
        object v18 = dynamicType.InvokeMember("TestLoaclVar18", BindingFlags.InvokeMethod, null, o1, new object[] { new Test1(1), "123" })!;
        object v19 = dynamicType.InvokeMember("TestLoaclVar19", BindingFlags.InvokeMethod, null, o1, new object[] { new Test1(1), 8 })!;
        object v20 = dynamicType.InvokeMember("TestLoaclVar20", BindingFlags.InvokeMethod, null, o1, new object[] { 127,128 })!;
        object v21 = dynamicType.InvokeMember("TestLoaclVar21", BindingFlags.InvokeMethod, null, o1, new object[] { new Test1(1), 128 })!;
        object v22 = dynamicType.InvokeMember("TestLoaclVar22", BindingFlags.InvokeMethod, null, o1, new object[] { new Test1(1), 128 })!;
        object v23 = dynamicType.InvokeMember("TestLoaclVar23", BindingFlags.InvokeMethod, null, o1, new object[] { new Test1(1), 128 })!;
        object v24 = dynamicType.InvokeMember("TestLoaclVar24", BindingFlags.InvokeMethod, null, o1, new object[] { new Test1(1), 128 })!;
    }


    /// <summary>
    /// 零时测试
    /// </summary>
    [Fact]
    public void LTest1()
    {
        ModuleBuilder moduleBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("test"), AssemblyBuilderAccess.RunAndCollect).DefineDynamicModule("test");
        TypeBuilder typeBuilder = moduleBuilder.DefineType("MyDynamicType", TypeAttributes.Public);
        
        MethodBuilder methodBuilder1 = typeBuilder.DefineMethod("Test1", MethodAttributes.Public);
        ILGenerator mbIl = methodBuilder1.GetILGenerator();

        mbIl.LoadInt(13);
        mbIl.LoadInt64(13);

        ILGenerator mbI2 = null!;
        Assert.Throws<ArgumentNullException>(() => mbI2.LoadInt64(12));
        Assert.Throws<ArgumentNullException>(() => mbI2.LoadInt(12));
        
    }

    [Fact]
    public void LTest2()
    {
        Type myType = DynamicCreateType.CreateMyDynamicType3();

        int theValue = 1;

        Object myInstance = Activator.CreateInstance(myType, new object[0]);

        var v1 = myType.InvokeMember("SwitchMe",BindingFlags.InvokeMethod, null,myInstance,new object[] { theValue });
    }    


}
