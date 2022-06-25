# 自动合并json工具

只需引入该包，在生成时自动将指定的多个json文件合并，生成到项目的输出目录中
自动移除json中的注释(有些系统不支持读取带注释的json)

## 配置

只需要在两个地方进行配置即可
1.主Json文件中(会生成到项目的Json文件)
2.YourProject.csproj 中

### 秒上手

只需要对项目根目录的 appsetting*.json 添加以下json即可

~~~~json
// 自定义配置信息
"CustomConfigInfo": {
  // 子文件目录集合
  "ConfigFileFolders": [ "CustomConfig1", "CustomConfig2" ],
  // 要排除的文件名集合
  "ExcludeSubFiles": [ "json2.json" ]
}
~~~~

### 设置所有需要参与合并的Json文件

- 在"JsonItems"属性中包含你可能会参与合并的所有Json文件
- 默认导入当前项目所有json文件,排除生成(bin\obj)文件夹中的文件

 ~~~xml
<ItemGroup>
  <JsonItems Include="**\*.json" Exclude="$(BaseOutputPath)**\*;$(BaseIntermediateOutputPath)**\*;" />
</ItemGroup>
~~~

自定义配置帮助

~~~xml
<ItemGroup>
  <!-- 移除当前包含的所有文件 -->
  <JsonItems Remove="**\*" />
  <!-- 包含工作根目录中的所有json,排除a.json -->
  <JsonItems Include="*.json" Exclude="a.json" />
  <!-- 包含工作目录中的所有json,排除所有a前缀的json文件 -->
  <JsonItems Include="**\*.json" Exclude="**\a*.json" />
  <!-- 包含e盘中的所有json -->
  <JsonItems Include="e:\**\*.json" />
</ItemGroup>
~~~

### 设置主要Json文件

注：如果主json是在工作目录中，合并后的文件也会在输出目录的相对位置，否者就直接输出到输出文件中

- 在"MainJsonItems"属性中包含你想要生成到项目Json文件
- 主Json寻子Json规则
  - 子json被包含在"JsonItems"中
  - 主json中存在"CustomConfigInfo.ConfigFileFolders"节点（数组类型）,存放子json目录路径
  - 主json中"CustomConfigInfo.ExcludeSubFiles"节点，存放排除的子json文件名，这里面的json都不会被合并
- 默认项目根目录中的appsettings前缀的Json文件
  - 默认子Json目录节点"CustomConfigInfo.ConfigFileFolders"
  - 默认排除子文件节点"CustomConfigInfo.ExcludeSubFiles"

 ~~~xml
<ItemGroup>
  <MainJsonItems Include="appsettings*.json" />
</ItemGroup>
~~~

每一个主json文件中需要有以下节点,以下节点名称时可以自定义的

~~~json
// 自定义配置信息
"CustomConfigInfo": {
  // 子文件目录集合
  "ConfigFileFolders": [ "CustomConfig1", "CustomConfig2" ],
  // 要排除的文件名集合
  "ExcludeSubFiles": [ "json2.json" ]
}
~~~

自定义配置帮助

~~~xml
<ItemGroup>
  <!-- 包含 CusuomConfig 目录中的config.json -->
  <MainJsonItems Include="CusuomConfig\config.json" />

  <!-- 修改存放子文件的节点 -->
  <MainJsonItems Update="包含的文件">
    <!--子目录（包含要合并的json文件），主Json文件中的节点-->
    <SubDirectoryNode>CustomConfigInfo.ConfigFileFolders</SubDirectoryNode>
    <!--排除子文件节点-->
    <ExcludeSubFilesNode>CustomConfigInfo.ExcludeSubFiles</ExcludeSubFilesNode>
  </MainJsonItems>
</ItemGroup>
~~~

### 设置是否生成合并日志

- 在"SaveMergeLog"属性中写入true/false即可
- 生成的合并日志保存在你项目输出目录的"mergeLogs"文件夹中
- 默认  false,不会生成合并日志

 ~~~xml
<PropertyGroup>
  <SaveMergeLog>false</SaveMergeLog>
</PropertyGroup>
~~~

### 基本概念

~~~text
?  通配符匹配单个字符。
*  通配符匹配零个或多个字符。
**  通配符序列匹配部分路径。
~~~

## 更新日志

### 1.0.0

- 发布第一个正式版

### 1.0.1-beta1

- 修复包含子json文件夹路径是盘符根路径(e:)引发的异常

### 1.0.1-beta2

- 处理获取文件路径问题

### 1.0.1

- 无功能性更新,统一版本

### 1.0.2

- 修改工具程序集不生成到项目中的方式
- 修改readme文档格式
