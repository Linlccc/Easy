# 工作文档

## 0427

添加自动发布包任务

 ~~~text
  1.使用
    a.任意位置 msbuild <Project/solution> -t:ReleaseNuGetToRemote 即可自动发布包
    b.在项目目录直接使用 msbuild -t:ReleaseNuGetToRemote
  2.发布过后会将发布日志保存到 \artifacts\logs\nuGetLogs\<NuGetPackageName>.log

  注:
    1.使用解决方案[solution]发布包,会将解决方案中所有会生成包的项目都发布
    2.使用项目发布包自会发布当前项目
  ~~~

---

## 0428

处理vscode自动生成的文件

  ~~~text
  当前看到：https://github.com/OmniSharp/omnisharp-vscode/blob/master/debugger-launchjson.md#just-my-code
  ~~~

---

## 0429

续-处理vscode自动生成的文件

  ~~~text
  tasks.json    自定义任务文件
  launch.json   调试文件

  处理完成添加
  tasks.json 的文档文件 taskDocument.ts
  launch.json 的实例文件 launchExamples.json
  ~~~

研究附加进程调试

  ~~~text
  1.调试文件(launch.json) 添加一个 attach 案例
  2.启动调试，选择要附加的进程即可(好像需要代码和进程是同一版本)

  attach 案例：
  {
    "name": ".NET Core Attach",
    "type": "coreclr",
    "request": "attach",
  }
  ~~~

MergeJson 发布任务时原json被发布,合并的json没有被发布

  ~~~text
  问题描述
  1.发布项目中存在 Easy.Tool.MergeJosn.dll
  2.合并的json文件没有被发布
  3.原json被发布

  解决方案
  1.在包引用上添加 ExcludeAssets="runtime" ，标记排除资产(直接使用dotnet build [项目]（会先执行 restore 还原） 时还是会复制到项目)
  2.在合并json后将合并成功的主json添加到 ContentWithTargetPath 项中，publish 任务自动会发布里面的文件
  3.将原json从内容中移除（或者设置内容的 CopyToPublishDirectory 元数据不等于 PreserveNewest ----该想法待验证）
  ~~~

---

## 0430

MergeJson 生成时会将 Easy.Tool.MergeJson.dll 复制到生成文件

  ~~~text
  问题描述：
  1.使用 dotnet build [项目] 时,会将 Easy.Tool.MergeJson.dll 复制到生成文件
  2.使用 dotnet restore [项目] 还原后,使用msbuild [项目] ,会将 Easy.Tool.MergeJson.dll 复制到生成文件
  3.但是使用 visual studio 生成项目时并不会将 Easy.Tool.MergeJson.dll 复制到生成文件
    (个人感觉visual studio还原项目时就导入nuget包的targets文件,但是使用 dotnet restore 还原时并没有导入)

  解决方案：
  1.在解析引用包过后标记包不复制到生成文件中
    在 ResolvePackageAssets 执行后，修改 @(RuntimeCopyLocalItems) 中 %(NuGetPackageId) == 'Easy.Tool.MergeJson.dll' 的 %(CopyLocal) 值为 false

  注：
  1.dotnet build [项目] 会执行 restore 和 build
  ~~~

---

## 0501

0430处理 解决MergeJson在项目生成时异常 后的新想法(废弃)

  ~~~text
  想法：
  1.在 ResolvePackageAssets 执行之前 将@(PackageReference) 中的 'Easy.Tool.MergeJson' 移除
  2.在 ResolvePackageAssets 执行执行之后再将'Easy.Tool.MergeJson'加入

  废弃原因：
  1.获取应该导入的程序集是从 project.assets.json 中读取,修改引用的包不会照成影响

  废弃主要 terget

  <Target Name="CutMergeJsonPackageReference" BeforeTargets="ResolvePackageAssets">
    <ItemGroup>
      <MergeJsonPackageReference Include="@(PackageReference)" Condition="%(Identity) == $(MSBuildThisFileName)" />
      <PackageReference Remove="@(MergeJsonPackageReference)"/>
    </ItemGroup>
  </Target>

  <Target Name="PasteMergeJsonPackageReference" AfterTargets="ResolvePackageAssets">
    <ItemGroup>
      <PackageReference Include="@(MergeJsonPackageReference)"/>
    </ItemGroup>
  </Target>
  ~~~

查看 msbuild 的 (发布)PublishOnly 任务

  ~~~text
  想法：
  1.msbuild 项目 -t:publishonly 是安装文件(不知道有什么用)

  结果：
  只执行 PublishOnly 任务没有意义

  可以参考 https://docs.microsoft.com/zh-cn/visualstudio/msbuild/msbuild-targets?view=vs-2022 链接中的信息
  ~~~

将包发布添加成任务

  ~~~text
  无细节
  ~~~

---

## 0504

根据不同的sdk添加默认全局 using

  ~~~text
  根据以下属性判断使用使用了该sdk，然后再到导入全局 using
  UsingILLinkTasksSdk
  UsingMicrosoftNETSdk
  UsingMicrosoftNETSdkBlazorWebAssembly
  UsingMicrosoftNETSdkRazor
  UsingMicrosoftNETSdkWeb
  UsingMicrosoftNETSdkWorker

  _MicrosoftWindowsDesktopSdkImported
  _MicrosoftNETSdkWindowsDesktop
  ~~~

---

## 0505

添加发布当前项目包

  ~~~text
  1.在vscode中选中解决方案(.sln)文件或者项目(.csproj)文件
  2.执行 "(发布当前项目或解决方案包)ReleaseNuGet Current Project Or Solution" 任务,将自动发布项目包
  ~~~

---

## 0512

检查 Type 的 ContainsGenericParameters 属性 是否是判断开放泛型

  ~~~text
  只要有一个类型参数没有指定特定类型就返回true

  typeof(Tuple<T1,string>).ContainsGenericParameters  返回true
  ~~~

---

## 0521

使用 emit 动态生成dll,然后再反编译

~~~text
1.在.net framework框架中可以使用 emit 动态生成dll(程序集)
2.可以使用以下方式进行反编译
  a.使用 ildasm.exe(IL 反汇编程序) 读dll文件的中间语言指令 (不推荐,可读性不高)
  b.在vs中引用该动态生成的dll,读取反编译后的的源码 (可用,可读性可以,但是不太方便)
  c.下载 https://github.com/icsharpcode/ILSpy 链接中的工具，反编译动态生成的dll查看源码 (推荐)
~~~

## 0524

Easy.Extensions.DependencyInjection 的服务提供商实现AOP

~~~text
可为 DI 配置 AOP ,每一次获取服务时会触发,获取前/获取后(成员注册前)/注册完成三个事件

使用：
1.自定义一个类型继承自 Easy.Extensions.DependencyInjection.EasyServiceProviderEvents 类型
2.重写 BeforeGetService、AfterGetService、GetServiceCompleted 方法
3.将自定义的类型赋值给 EasyServiceProviderOptions.ServiceProviderEventsType
4.构建服务提供商 IServiceCollection.BuildEasyServiceProvider(EasyServiceProviderOptions);
~~~

## 0525

处理版本控制问题

~~~text
1.版本主要在自己项目文件(.csproj)中管理
2.如果所有项目统一更新时,修改 baseConfigure.props 文件 UnifiedVersion(统一版本) 属性值为true,所有项目就会使用一个版本
~~~

修复 Easy.Extensions.DependencyInjection 事件(AOP) 传递的服务提供商类型错误问题

~~~text
在 1.0.1 版本 Easy.Extensions.DependencyInjection 添加了添加事件(AOP)但是传递的服务提供商类型是默认的 serviceProviderEngineScope 类型,在1.0.2中修复
~~~

## 计划

- [x] 添加自动发布包任务 (研究) [0427]
- [x] 处理vscode自动生成的文件 (研究) [0429]
- [x] 研究附加进程调试(研究) [0429]
- [x] MergeJson 发布任务时原json被发布,合并的json没有被发布 (bug) [0429]
- [x] MergeJson 生成时会将 Easy.Tool.MergeJson.dll 复制到生成文件 (bug) [0430]
- [x] 查看 msbuild 的 (发布)PublishOnly 任务 (研究) [0501]
- [x] 将包发布添加成任务 (实现) [0501]
- [x] 根据不同的sdk添加默认全局 using (实现) [0504]
- [x] 检查 Type 的 ContainsGenericParameters 属性 是否是判断开放泛型 (实验) [0512]
- [x] 使用 emit 动态生成dll,然后再反编译 (实验) [0521]
- [x] 了解一下动态启停web项目 (在TourCar项目中写了一个web管理,处理web项目启停) [0524]
- [x] 处理版本控制问题 [0525]
- [ ] 了解为什么应该将 IHttpContextAccessor 作为Singleton注入

  ~~~text
  1.看网上解释说因为【后备存储是异步本地】,没懂是什么意思
  2.如果只是在控制器中使用的话不用注册 IHttpContextAccessor 服务，看看控制器中的httpcontext怎么来的


  可以看看
  https://github.com/aspnet/Hosting/issues/793#issuecomment-224924030
  ~~~

### Easy.Extensions 项目升级/修改计划

~~~text
1.检查Emit拓展的参数空检查
2.整理拓展
3.拓展根据int值（索引）做操作的核实一下 短格式的最大值和最小值
4.再核实一下有没有遗漏的指令
5.将方法精简一下

后期计划
1.将默认类型的拓展方法分成不同的项目,步骤
  a.拆分成多个项目,如：Easy.Extensions.System/Easy.Extensions.Microsoft 等等
  b.Easy.Extensions 项目只需引入所有默认类型拓展项目,这样就可按需引入和全部引入
~~~

### Easy.Extensions.DependencyInjection 项目升级/修改/开发计划

~~~text
后期计划
1.添加多框架版本更换服务容器
  // 该方式是 IHostBuilder 拓展,直接配置使用 EasyDependencyInjection
  // 不同框架是 (IHostBuilder) 类型可能不同
  public static IHostBuilder UseEasyServiceProvider(this IHostBuilder hostBuilder, Action<EasyServiceProviderOptions>? optionsAction = null)
  {
      EasyServiceProviderOptions options = new();
      optionsAction?.Invoke(options);
      hostBuilder.UseServiceProviderFactory(new EasyServiceProviderFactory(options));
      return hostBuilder;
  }

2.看看 https://github.com/autofac/Autofac.AspNetCore.Multitenant 项目
  DI的多租户
~~~

### Easy.Extensions.DependencyInjection.Abstractions 项目升级/修改/开发计划

~~~text
~~~

### Easy.Tool.MergeJson 项目升级/修改/开发计划

~~~text
使用 winform 之类的项目引用 web 应用生成后的文件中没有 合并的json

不进行部署可参考
https://docs.microsoft.com/zh-cn/ef/core/what-is-new/nuget-packages#tools
Microsoft.EntityFrameworkCore.Design 包的安装方式有所不同，它不会随应用程序一起部署。 这也意味着，其类型不能在其他项目中传递使用

实现
1.使用 FindUnderPath,ConvertToAbsolutePath msbuild 任务处理文件夹路径，在程序中尽量少的处理路径
~~~

### Easy.Extensions.DynamicProxy 项目升级/修改/开发计划

~~~text
开发
1.看官方项目的实现
2.需要实现的功能有:
  a.根据接口和类创建代理类型
  b.根据类实现代理类型
    I.在后期看怎么实现非虚方法的代理实现
  c.直接对对象的代理


可以看的开源项目
1.https://github.com/pamidur/aspect-injector(编译时 AOP 框架)
2.https://github.com/Jishun/RoslynWeave(编译时注入 C# 代码的目标，通过使用原始 C# 代码而不是 IL 进行编织)
3.https://github.com/vescon/MethodBoundaryAspect.Fody
4.https://github.com/htrlq/NCoreCoder.Framework(动态代理)
~~~

### 暂时不处理

- [ ] 不同的项目使用一个 .vscode 文件夹 (研究)
- [ ] IHostBuilder 添加使用 Easy.Extensions.DependencyInjection 的拓展方法 UseEasyServiceProvider

---

### 很棒的项目

~~~text
对系统的集合类型进行 内存、性能 优化(池化处理)
https://github.com/jtmueller/Collections.Pooled

对文件操作优化 一个为 .NET 对象提供池MemoryStream以提高应用程序性能的库，尤其是在垃圾收集领域
https://github.com/microsoft/Microsoft.IO.RecyclableMemoryStream
~~~
