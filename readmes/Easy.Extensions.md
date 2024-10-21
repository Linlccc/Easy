# 系统类型扩展

## StringExtensions 类

`StringExtensions` 类提供了一些对 `string` 类型的扩展方法，用于简化字符串的操作。

### 方法

```csharp
// 检查指定的字符串是否为 null 或空字符串。
public static bool IsNullOrEmpty(this string? value);
```

## TypeExtensions 类

`TypeExtensions` 类提供了一些对 `Type` 类型的扩展方法，用于简化类型的操作和检查。

### 方法

```csharp
// 检查指定类型是否实现了指定的接口类型定义。
public static bool IsImplementsInterfaceDefinition(this Type type, Type interfaceType);

// 检查指定类型是否实现了指定的接口。
public static bool IsImplementsInterface(this Type type, Type interfaceType);

// 检查指定类型是否是完全开放的泛型类型。
public static bool IsAllOpenGeneric(this Type type);

// 检查指定类型是否为可空类型（Nullable[T]）。
public static bool IsNullableType(this Type type);

// 获取类型的名称。
public static string Name(this Type type);

// 获取类型的完整名称。
public static string? FullName(this Type type);

// 获取指定类型的类型定义。
public static Type GetTypeDefinition(this Type type);

// 获取 Nullable<> 类型中的基础类型。
public static Type GetTypeFromNullable(this Type type);
```

## IEnumerableExtensions 类

`IEnumerableExtensions` 类提供了一些对 `IEnumerable<T>` 类型的扩展方法，用于简化集合的操作。

### 方法

```csharp
// 检查集合是否为 null 或为空。
public static bool IsNullOrEmpty<T>(this IEnumerable<T>? collection);
```

## ICustomAttributeProviderExtensions 类

`ICustomAttributeProviderExtensions` 类提供了一些对 `ICustomAttributeProvider` 接口的扩展方法，用于简化从对象中获取自定义特性的操作。

### 方法

```csharp
// 获取指定类型的自定义特性（Attributes）。
public static IEnumerable<TAttribute?> GetAttributes<TAttribute>(this ICustomAttributeProvider customAttributeProvider, bool inherit);

// 获取指定类型的自定义特性（Attribute）。
public static TAttribute? GetAttribute<TAttribute>(this ICustomAttributeProvider customAttributeProvider, bool inherit);

// 检查指定类型的自定义特性是否存在。
public static bool IsExistAttribute<TAttribute>(this ICustomAttributeProvider customAttributeProvider, bool inherit, out TAttribute? attribute);
```

## MethodInfoExtensions 类

`MethodInfoExtensions` 类提供了一些对 `MethodInfo` 类型的扩展方法，用于简化与方法相关的属性信息的获取。

### 方法

```csharp
// 获取与指定方法相关联的属性信息（PropertyInfo）。
public static PropertyInfo? GetBindProperty(this MethodInfo method);

// 检查指定的方法是否与一个属性相关联。
public static bool IsBindProperty(this MethodInfo method);
```

## PropertyInfoExtensions 类

`PropertyInfoExtensions` 类提供了一些对 `PropertyInfo` 类型的扩展方法，用于简化属性值的设置。

### 方法

```csharp
// 设置指定对象的属性值。
public static void SetPropertyValue(this PropertyInfo propertyInfo, object? obj, object? value);
```

## ILGeneratorExtensions 类

`ILGeneratorExtensions` 类提供了一些对 `ILGenerator` 类型的扩展方法，用于简化 IL 代码的生成。
| 该扩展方法众多，具体请查看源码。

## 更新日志

### 1.0.0

```text
发布第一个正式版
```

### 1.0.1-beta1

```text
1.解决 Easy.Extensions.DependencyInjection 依赖问题
```

### 1.0.1

```text
1.修改 Type 扩展 IsOpenGeneric 为 IsAllOpenGeneric
  a.判断是否为完全开放泛型
  b.如果只是判断是否有没有指定特定类型的类型参数使用 ContainsGenericParameters 属性

2.移除以下方法的泛型约束
  a.<ICustomAttributeProvider>.GetAttribute<T>(bool);
  b.<ICustomAttributeProvider>.GetAttributes<T>(bool):
  c.<ICustomAttributeProvider>.IsExistAttribute<T>(bool,out T?);
```

### next

```text
1. 整理 Easy.Extensions 项目
2. 修改项目 Readme 文档
```
