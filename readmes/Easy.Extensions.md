# 系统类型拓展

- [x] String

  ~~~text
  判断字符串是否为空 <string>.IsNullOrEmpty();
  ~~~

- [x] Type

  ~~~text
  判断类型的接口定义是否包含执行类型  <type>.IsInterfaceDefinitionInclude(type);

  判断类型是否继承自指定类型  <type>.IsInheritFrom(type);

  判断是否是完全开放泛型  <type>.IsAllOpenGeneric();

  获取类型定义 <type>.GetTypeDefinition();

  获取类型的名称 <type>.Name();

  获取类型的完全限定名 <type>.FullName();
  ~~~

- [x] IEnumerable<T>

  ~~~text
  判读集合是否为空 <IEnumerable<T>>.IsNullOrEmpty();
  ~~~

- [x] ICustomAttributeProvider

  ~~~text
  获取指定类型特性  <ICustomAttributeProvider>.GetAttribute<attributeType>(bool);

  获取指定类型特性集合  <ICustomAttributeProvider>.GetAttributes<attributeType>(bool);

  检查指定类型特性是否存在,如果存在则获取 <ICustomAttributeProvider>.IsExistAttribute<attributeType>(bool,out attributeType);

  设置属性值  <PropertyInfo>.SetPropertyValue(object,object);
  ~~~

## 更新日志

### 1.0.0

~~~text
发布第一个正式版
~~~

### 1.0.0-beta1

~~~text
1.解决 Easy.Extensions.DependencyInjection 依赖问题
~~~

### 1.0.1(待发布)

~~~text
1.修改 Type 拓展 IsOpenGeneric 为 IsAllOpenGeneric
  a.判断是否为完全开放泛型
  b.如果只是判断是否有没有指定特定类型的类型参数使用 ContainsGenericParameters 属性
  
2.移除以下方法的泛型约束
  a.<ICustomAttributeProvider>.GetAttribute<T>(bool);
  b.<ICustomAttributeProvider>.GetAttributes<T>(bool):
  c.<ICustomAttributeProvider>.IsExistAttribute<T>(bool,out T?);
~~~
