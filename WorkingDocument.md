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

处理vscode自动生成的文件

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

解决MergeJson在项目发布时的异常

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

解决MergeJson在项目生成时异常

  ~~~text
  问题描述：
  1.使用 dotnet build [项目] 时,会将 Easy.Tool.MergeJson.dll 复制到生成文件
  2.使用 dotnet restore [项目] 还原后,使用msbuild [项目] ,会将 Easy.Tool.MergeJson.dll 复制到生成文件
  3.但是使用 visual studio 生成项目时并不会将 Easy.Tool.MergeJson.dll 复制到生成文件
    (个人感觉visual studio还原项目时就导入nuget包的targets文件,但是使用 dotnet restore 还原时并没有导入)

  解决方案：
  1.在解析引用包过后标记包不复制到生成文件中
    在 ResolvePackageAssets 执行后，修改 @(RuntimeCopyLocalItems) 中 %(NuGetPackageId) == 'Easy.Tool.MergeJson.dll' 的 %(CopyLocal) 值为 false

  0501 新的想法
  1.在 ResolvePackageAssets 执行之前 将@(PackageReference) 中的 'Easy.Tool.MergeJson' 移除
  2.在 ResolvePackageAssets 执行执行之后再将'Easy.Tool.MergeJson'加入


  注：
  1.dotnet build [项目] 会执行 restore 和 build
  ~~~

---

## 计划

- [x] 添加自动发布包任务
- [x] 处理vscode自动生成的文件
- [x] (不重要)研究一下附加调试
- [x] 解决 MergeJson 发布任务时原json被发布,合并的json没有被发布
- [x] 解决 MergeJson 生成时会将 Easy.Tool.MergeJson.dll 复制到生成文件
- [ ] 将包发布添加成任务
- [ ] 查看 msbuild 的 (发布)PublishOnly 任务

  ~~~text
  使用 dotnet publish 项目时,发布文件中包含应该被忽略的json,合并的json文件也没有被发布  
  msbuild 项目 -t:publishonly 是安装文件，但是也有上述情况
  ~~~

- [ ] 实现aop
- [ ] 看是否可以获取项目使用的sdk,根据不同的sdk添加默认全局 using

  ~~~text
  判断sdk是否引入的属性名
  UsingILLinkTasksSdk
  UsingMicrosoftNETSdk
  UsingMicrosoftNETSdkBlazorWebAssembly
  UsingMicrosoftNETSdkRazor
  UsingMicrosoftNETSdkWeb
  UsingMicrosoftNETSdkWorker

  _MicrosoftWindowsDesktopSdkImported
  _MicrosoftNETSdkWindowsDesktop
  ~~~

- [ ] 看怎么实现自动化命令[脚本]

  ~~~text
  1.直接在解决方案上实现包含项目的包发布
  2.直接在项目上实现项目的包发布
  3.实现配置apikay脚本
  ~~~

---
