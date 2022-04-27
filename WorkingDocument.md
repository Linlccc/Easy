#### 0427

- [x] 添加自动发布包任务

  ~~~
  1.直接使用 msbuild <Project/solution> -t:ReleaseNuGetToRemote 即可自动发布包
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
---