# 实现"MSBuild"构建使用[IgnoresAccessChecksToAttribute]特性的工具

我可以让你使用本不该属于你的"力量",但是你也得谨慎使用,希望你不会因为这股"力量"而受伤
声明：本程序集只对开发时的引用程序集做处理

## 配置

一切的配置都在 YourProject.csproj 中

### 设置忽略访问检查的程序集

- 在"IgnoresAccessChecksAssemblyNames"属性中写入程序集名即可,支持多个使用';'/','分割
- 默认  null,不会生成任何忽略检查的程序集

 ~~~text
<PropertyGroup>
  <IgnoresAccessChecksAssemblyNames>Assembly1;Assembly2</IgnoresAccessChecksAssemblyNames>
</PropertyGroup>
~~~

### 设置排除类型(不需要访问的类型)

- 在"ExcludeTypeFullNames"属性中写入类型完全限定名即可,支持多个使用';'/','分割
- 默认  null,不排除任何类型

 ~~~text
<PropertyGroup>
  <ExcludeTypeFullNames>Type1;Type2</ExcludeTypeFullNames>
</PropertyGroup>
~~~

### 设置设置生成的公开程序集是否是空方法体

- 在"UseEmptyMethodBody"属性中写入true/false即可
- 默认  true,生成的程序集都是空方法体

 ~~~text
<PropertyGroup>
  <UseEmptyMethodBody>true</UseEmptyMethodBody>
</PropertyGroup>
~~~

## 更新日志

### 1.0.0

- 项目发布

### 1.0.1

- 修改项目readme文档
