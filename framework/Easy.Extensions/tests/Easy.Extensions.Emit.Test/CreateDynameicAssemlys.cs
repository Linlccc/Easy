namespace Easy.Extensions.Emit.Test
{

    /// <summary>
    /// 创建动态程序集
    /// </summary>
    public class CreateDynameicAssemlys
    {
        #region 动态程序集基础
        /// <summary>
        /// 动态程序集名称
        /// </summary>
        private static readonly string _assemblyName = nameof(CreateDynameicAssemlys);
        /// <summary>
        /// 动态程序集构建器
        /// </summary>
        private static readonly AssemblyBuilder _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(_assemblyName), AssemblyBuilderAccess.RunAndSave);
        /// <summary>
        /// 模块构建器
        /// </summary>
        private static readonly ModuleBuilder _moduleBuilder = _assemblyBuilder.DefineDynamicModule(_assemblyName, $"{_assemblyName}.dll");
        /// <summary>
        /// 创建动态程序集
        /// </summary>
        private void CreateCynameicAssemlys() => _assemblyBuilder.Save($"{_assemblyName}.dll");
        #endregion

        /// <summary>
        /// 创建程序集
        /// </summary>
        [Fact]
        public void CreateAssemlys()
        {
            _moduleBuilder.DefineType_HelloWorld();
            _moduleBuilder.DefineType_EmitOpCodesVerify();
            CreateCynameicAssemlys();
        }

        /// <summary>
        /// 创建类型 HelloWorld
        /// </summary>
        [Fact]
        public void Create_HelloWorld()
        {
            Type type = _moduleBuilder.DefineType_HelloWorld();

            // 创建实例
            object instance = Activator.CreateInstance(type, 100, "abc");

            // 验证
            Assert.Equal(100, type.GetProperty("Number").GetValue(instance));
            Assert.Equal("abc", type.GetField("m_number2", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance));
            Assert.Equal(20 * 100, type.InvokeMember("Mul", BindingFlags.InvokeMethod, null, instance, new object[] { 20 }));
        }

        /// <summary>
        /// 创建类型 EmitOpCodesVerify
        /// </summary>
        [Fact]
        public void Create_EmitOpCodesVerify()
        {
            Type type = _moduleBuilder.DefineType_EmitOpCodesVerify();


            object? addValue = type.InvokeMember("Add1", BindingFlags.InvokeMethod, null, null, new object[] { 1, 2 });
            object? addValue1 = type.InvokeMember("Add2", BindingFlags.InvokeMethod, null, null, new object[] { 123, 1 });
        }


    }
}
