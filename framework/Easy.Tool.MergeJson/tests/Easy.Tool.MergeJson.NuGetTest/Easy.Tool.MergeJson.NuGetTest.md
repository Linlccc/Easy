### 引用 Easy.Tool.MergeJson NuGet 包测试

#### 变量

~~~
项：
	JsonFilePaths	可能会合并的json文件
	<ItemGroup>
		// 移除默认的所有json
		<JsonFilePaths Remove="**\*.*" /> 
		// 加入e盘下的所有json文件
		<JsonFilePaths Include="e:\**\*.json" />
	</ItemGroup>
~~~

