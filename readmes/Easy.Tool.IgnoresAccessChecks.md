# 实现"MSBuild"构建使用[IgnoresAccessChecksToAttribute]特性的程序集

我可以让你使用本不该属于你的"力量",但是你也得谨慎使用,希望你不会因为这股"力量"而受伤
声明：本程序集只对开发时的引用程序集做处理

## 配置

~~~ text
一切的配置都在 YourProject.csproj 中

忽略访问检查的程序集名称集合,使用';'/','分割
默认  null,不会生成任何忽略检查的程序集
<PropertyGroup>
  <IgnoresAccessChecksAssemblyNames>Assembly1;Assembly2</IgnoresAccessChecksAssemblyNames>
</PropertyGroup>

排除类型(不访问的类型)的完全限定名集合,使用';'/','分割
默认  null,不排除任何类型
<PropertyGroup>
  <ExcludeTypeFullNames>Type1;Type2</ExcludeTypeFullNames>
</PropertyGroup>

使用空方法体生成公开程序集
默认  true,生成的程序集都是空方法体
<PropertyGroup>
  <UseEmptyMethodBody>true</UseEmptyMethodBody>
</PropertyGroup>
~~~
