namespace Easy.Extensions.DynamicProxy.Utils;

/// <summary>
/// 自定义特性实用程序
/// </summary>
public class CustomAttributeUtils
{
    public static CustomAttributeBuilder GetCustomAttributeBuilder(CustomAttributeData customAttributeData)
    {
        // 构造函数
        ConstructorInfo constructor = customAttributeData.Constructor;
        // 构造函数参数
        object?[] constrictorArgs = customAttributeData.ConstructorArguments.Select(ReadAttributeValue).ToArray();
        // 如果没有命名参数直接返回特性构建器
        if (customAttributeData.NamedArguments.IsNullOrEmpty()) return new CustomAttributeBuilder(constructor, constrictorArgs);

        Type attributeType = customAttributeData.AttributeType;
        // 得到命名属性和值
        IEnumerable<CustomAttributeNamedArgument> namedPropertyArgs = customAttributeData.NamedArguments.Where(n => !n.IsField).ToArray();
        PropertyInfo[] namedPropertys = namedPropertyArgs.Select(n => (PropertyInfo)n.MemberInfo).ToArray();
        object[] propertyValues = namedPropertyArgs.Select(n => ReadAttributeValue(n.TypedValue)).ToArray();
        // 得到命名字段和值
        IEnumerable<CustomAttributeNamedArgument> namedFieldArgs = customAttributeData.NamedArguments.Where(n => n.IsField).ToArray();
        FieldInfo[] namedFields = namedFieldArgs.Select(n => (FieldInfo)n.MemberInfo).ToArray();
        object[] fieldsValues = namedFieldArgs.Select(n => ReadAttributeValue(n.TypedValue)).ToArray();
        
        // 返回特性构建器(添加了命名参数)
        return new CustomAttributeBuilder(constructor, constrictorArgs, namedPropertys, propertyValues, namedFields, fieldsValues);
    }

    private static object ReadAttributeValue(CustomAttributeTypedArgument argument)
    {
        object value = argument.Value!;
        if (value is null) return value!;

        if (argument.ArgumentType.IsArray && value is IEnumerable<CustomAttributeTypedArgument> arrayValue) return arrayValue.Select(v => v.Value).ToArray();

        return value;
    }


}
