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
- [ ] 处理 Easy.Extension.DenpendencyInjection 项目

  ~~~text
  1.DI添加多框架版本更换服务容器(实现)
  2.处理容器的Dispose方法(实现)
  3.添加获取服务前/后aop功能
    a.添加抽象类/接口实例
    b.代理方法
  ~~~

- [ ] 添加项目 Easy.Extensions.DynamicProxy 项目(aop)

  ~~~text
  查看开源项目
  1.https://github.com/pamidur/aspect-injector(编译时 AOP 框架)
  2.https://github.com/Jishun/RoslynWeave(编译时注入 C# 代码的目标，通过使用原始 C# 代码而不是 IL 进行编织)
  3.https://github.com/vescon/MethodBoundaryAspect.Fody

  看看https://github.com/htrlq/NCoreCoder.Framework项目
  1.先看官方实现
  2.需要实现的有：
    a.根据接口和类创建代理类型
    b.根据类实现代理类型
      I.在后期看怎么实现非虚方法的代理实现
    c.直接对对象的代理

  ~~~

- [ ] Emit(动态生成代码)学习

  ~~~text
  1.看是否有办法将 emit 的代码生成dll，然后再反编译
  2.看看该链接的方法，不过暂时不是很推荐，因为可能会花大量的时间看汇编 https://www.bilibili.com/video/BV1b5411U7M5?spm_id_from=333.337.search-card.all.click
  ~~~

- [ ] 考虑将 Easy.Extensions.DependencyInjection.Abstractions 项目中的 ServiceTypeProxy 修改成 ServiceTypeMask ,服务类型面具的意思 (实现)
- [ ] 升级 Easy.Tool.MergeJson 项目

  ~~~text
  1.考虑使用 FindUnderPath,ConvertToAbsolutePath msbuild 任务处理文件夹路径，在程序中尽量少的处理路径
  ~~~

### 暂时不处理

- [ ] 不同的项目使用一个 .vscode 文件夹 (研究)
- [ ] IHostBuilder 添加使用 Easy.Extensions.DependencyInjection 的拓展方法 UseEasyServiceProvider

  ~~~C#

  //该方式是 IHostBuilder 拓展,直接配置使用 EasyDependencyInjection
    public static IHostBuilder UseEasyServiceProvider(this IHostBuilder hostBuilder, Action<EasyServiceProviderOptions>? optionsAction = null)
    {
        EasyServiceProviderOptions options = new();
        optionsAction?.Invoke(options);
        hostBuilder.UseServiceProviderFactory(new EasyServiceProviderFactory(options));
        return hostBuilder;
    }
  ~~~

---
