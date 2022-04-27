#### 0427

- [x] 添加自动发布包任务

  ~~~
  1.使用 
    a.任意位置 msbuild <Project/solution> -t:ReleaseNuGetToRemote 即可自动发布包
    b.在项目目录直接使用 msbuild -t:ReleaseNuGetToRemote
  2.发布过后会将发布日志保存到 \artifacts\logs\nuGetLogs\<NuGetPackageName>.log

  注:
    1.使用解决方案[solution]发布包,会将解决方案中所有会生成包的项目都发布
    2.使用项目发布包自会发布当前项目
  ~~~

---

#### 0428

---

### 计划

- [ ] 实现aop
- [ ] 看是否可以获取项目使用的sdk,根据不同的sdk添加默认全局 using
- [ ] 看怎么实现自动化命令[脚本]

  ~~~
  1.直接在解决方案上实现包含项目的包发布
  2.直接在项目上实现项目的包发布
  3.实现配置apikay脚本
  ~~~

---