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

---

## 计划

- [x] 添加自动发布包任务
- [x] 处理vscode自动生成的文件
- [x] (不重要)研究一下附加调试
- [ ] 将包发布添加成任务
- [ ] 查看 msbuild 的 (发布)PublishOnly 任务

  ~~~text
  使用 dotnet publish 项目时,发布文件中包含应该被忽略的json,合并的json文件也没有被发布  
  msbuild 项目 -t:publishonly 是安装文件，但是也有上述情况

  解决方案：
  1.将要合并的json中内容中移除添加到none（不复制）
  2.发布后将和并的json复制到发布文件
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
