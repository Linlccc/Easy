# 自动合并json工具

只需引入该包，在生成时自动将指定的多个json文件合并，生成到项目的输出目录中
自动移除json中的注释(有些系统不支持读取带注释的json)

## 最快使用

~~~~text
只需要对项目根目录的 appsetting*.json 添加一下json即可

// 自定义配置信息
"CustomConfigInfo": {
    // 子文件目录集合
    "ConfigFileFolders": [ "CustomConfig1", "CustomConfig2" ],
    // 要排除的文件名集合
    "ExcludeSubFiles": [ "json2.json" ]
}
~~~~

## 配置

~~~text
保存合并日志【在输出目录中生成mergeLogs文件夹存放合并日志】
<PropertyGroup><SaveMergeLog>true</SaveMergeLog></PropertyGroup>

合并包含的json文件（存放所有可能会参与合并的json）
<ItemGroup>
 <JsonItems Include="要包含的json文件" Exclude="排除的json文件" />
</ItemGroup>

主要json文件，这些文件都需要在 <JsonItems> 里面存在
<ItemGroup>
 <MainJsonItems Include="主要json文件" />
</ItemGroup>
~~~

### 默认

~~~text
<PropertyGroup>
    <!--保存合并日志-->
    <SaveMergeLog>false</SaveMergeLog>
</PropertyGroup>
<ItemGroup>
    <!--默认导入当前项目所有json文件,排除生成文件夹中的-->
    <JsonItems Include="**\*.json" Exclude="$(BaseOutputPath)**\*;$(BaseIntermediateOutputPath)**\*;" />
    <!--默认主要json文件-->
    <MainJsonItems Include="appsettings*.json" />
</ItemGroup>

日志：默认情况下不会保存合并日志
包含的json：工作目录中所有的json文件
主要json文件：工作根目录中所有以【appsettings】开头【.json】后缀的文件
 寻找子json：
  1.子json被包含在<JsonItems>中
  2.主json中存在【CustomConfigInfo.ConfigFileFolders】节点（数组类型），存放子json目录路径
  3.主json中【CustomConfigInfo.ExcludeSubFiles】节点，存放排除的子json文件名，这里面的json都不会被合并

注：如果主json是在工作目录中，合并后的文件也会在输出目录的相对位置，否者就直接输出到输出文件中
~~~

### 自定

~~~text
?  通配符匹配单个字符。
*  通配符匹配零个或多个字符。
**  通配符序列匹配部分路径。


修改包含的json文件
<ItemGroup>
 移除当前包含的所有文件
 <JsonItems Remove="**\*" />
 包含工作根目录中的所有json,排除a.json
 <JsonItems Include="*.json" Exclude="a.json" />
 包含工作目录中的所有json,排除所有a前缀的json文件
 <JsonItems Include="**\*.json" Exclude="**\a*.json" />
 包含e盘中的所有json
 <JsonItems Include="e:\**\*.json" />
</ItemGroup>

修改主json文件
<ItemGroup>
 包含 CusuomConfig 目录中的config.json
 <MainJsonItems Include="CusuomConfig\config.json" />

 修改存放子文件的节点
 <MainJsonItems Update="包含的文件">
  <!--子目录（包含要合并的json文件），主Json文件中的节点-->
          <SubDirectoryNode>CustomConfigInfo.ConfigFileFolders</SubDirectoryNode>
          <!--排除子文件节点-->
          <ExcludeSubFilesNode>CustomConfigInfo.ExcludeSubFiles</ExcludeSubFilesNode>
 </MainJsonItems>
</ItemGroup>
~~~

## 更新日志

### 1.0.0

~~~text
发布第一个正式版
~~~

### 1.0.1-beta1

~~~text
1.修复包含子json文件夹路径是盘符根路径(e:)引发的异常
~~~

### 1.0.1-beta2

~~~text
1.处理获取文件路径问题
~~~

### 1.0.1

~~~text
1.无功能性更新,统一版本
~~~
