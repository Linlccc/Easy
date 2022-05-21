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

            // ** 数学
            // +
            int[] add1 = (int[])type.InvokeMember("Add1", BindingFlags.InvokeMethod, null, null, new object[] { 10, 2 });
            int[] add2 = (int[])type.InvokeMember("Add2", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(12, add1[0]);
            Assert.Equal(125, add2[0]);
            // -
            int[] sub1 = (int[])type.InvokeMember("Sub1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(121, sub1[0]);
            // *
            int[] mul1 = (int[])type.InvokeMember("Mul1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(246, mul1[0]);
            // /
            int[] div1 = (int[])type.InvokeMember("Div1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(61, div1[0]);
            // %
            int[] rem1 = (int[])type.InvokeMember("Rem1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(1, rem1[0]);
            // -arg
            int neg1 = (int)type.InvokeMember("Neg1", BindingFlags.InvokeMethod, null, null, new object[] { 123 });
            Assert.Equal(-123, neg1);

            // ** 按位计算
            // &
            int and1 = (int)type.InvokeMember("And1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(123 & 2, and1);
            // |
            int or1 = (int)type.InvokeMember("Or1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(123 | 2, or1);
            // ^
            int xor1 = (int)type.InvokeMember("Xor1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(123 ^ 2, xor1);
            // ~arg
            int not1 = (int)type.InvokeMember("Not1", BindingFlags.InvokeMethod, null, null, new object[] { 123 });
            Assert.Equal(~123, not1);
            // <<
            int shiftLeft1 = (int)type.InvokeMember("ShiftLeft1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(123 << 2, shiftLeft1);
            // >>
            int[] shiftRight1 = (int[])type.InvokeMember("ShiftRight1", BindingFlags.InvokeMethod, null, null, new object[] { 123, 2 });
            Assert.Equal(123 >> 2, shiftRight1[0]);
        }


    }
}
