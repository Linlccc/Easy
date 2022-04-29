# Easy

让一切变得简单

----

## 文档目录

### 架构

[系统类型拓展](./readmes/Easy.Extensions.md)

[依赖注入-抽象](./readmes/Easy.Extensions.DependencyInjection.Abstractions.md)

[依赖注入](./readmes/Easy.Extensions.DependencyInjection.md)

### 工具

[Json合并工具](./readmes/Easy.Tool.MergeJson.md)

----

## 脚本

- [x] 自动发布包任务

  ~~~text
  1.使用 
    a.任意位置 msbuild <Project/solution> -t:ReleaseNuGetToRemote 即可自动发布包
    b.在项目目录直接使用 msbuild -t:ReleaseNuGetToRemote
  2.发布过后会将发布日志保存到 \artifacts\logs\nuGetLogs\<NuGetPackageName>.log

  注:
    1.使用解决方案[solution]发布包,会将解决方案中所有会生成包的项目都发布
    2.使用项目发布包自会发布当前项目
  ~~~

----

~~~text
查看所有自己发布的包的信息,在项目根目录执行
.\nuget.exe list Linlccc -PreRelease -Verbosity detailed
~~~
