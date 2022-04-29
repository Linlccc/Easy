// 任务示例文件

interface TaskConfiguration extends BaseTaskConfiguration {
  /**
   * 配置的版本号
   */
  version: '2.0.0';

  /**
   * Windows 特定任务配置
   */
  windows?: BaseTaskConfiguration;

  /**
   * macOS 特定任务配置
   */
  osx?: BaseTaskConfiguration;

  /**
   * Linux 特定任务配置
   */
  linux?: BaseTaskConfiguration;
}

interface BaseTaskConfiguration {
  /**
   * 自定义任务的类型, “shell”类型的任务在 shell 中执行（例如 bash、cmd、powershell,...）
   */
  type: 'shell' | 'process';

  /**
   * 要执行的命令。可以是外部程序或 shell 命令。
   */
  command: string;

  /**
   * 指定全局命令是否为后台任务
   */
  isBackground?: boolean;

  /**
   * 执行命令时使用的命令选项
   */
  options?: CommandOptions;

  /**
   * 传递给命令的参数
   */
  args?: string[];

  /**
   * 演示选项
   */
  presentation?: PresentationOptions;

  /**
   * 执行全局命令时使用的问题匹配器（例如,no tasksare defined）
   * tasks.json 文件可以包含全局 questionMatcher 属性或 tasks 属性,但不能同时包含两者。
   */
  problemMatcher?: string | ProblemMatcher | (string | ProblemMatcher)[];

  /**
   * 可用任务的配置。 tasks.json 文件可以包含全局 questionMatcher 属性或 tasks 属性,但不能同时包含两者
   */
  tasks?: TaskDescription[];
}

/**
 * 传递给外部程序或 shell 的选项
 */
export interface CommandOptions {
  /**
   * 执行程序或shell的当前工作目录。如果省略,则使用当前工作空间的根
   */
  cwd?: string;

  /**
   * 执行程序或shell的环境。如果省略,则使用父进程的环境
   */
  env?: { [key: string]: string };

  /**
   * 任务类型为`shell`时的shell配置
   */
  shell: {
    /**
     * 要使用的 shell
     */
    executable: string;

    /**
     * 传递给 shell 可执行文件以在命令模式下运行的参数（例如 ['-c'] 用于 bash 或 ['/S', '/C'] 用于 cmd.exe）
     */
    args?: string[];
  };
}

/**
 * 任务的描述
 */
interface TaskDescription {
  /**
   * 任务名称
   */
  label: string;

  /**
   * 自定义任务的类型。 “shell”类型的任务在 shell 中执行（例如 bash、cmd、powershell,...）
   */
  type: 'shell' | 'process';

  /**
   * 要执行的命令
   * 如果类型是“shell”,它应该是完整的命令行,包括传递给命令的任何附加参数
   */
  command: string;

  /**
   * 执行的命令是否保持活动并在后台运行
   */
  isBackground?: boolean;

  /**
   * type = "process" 使用
   * 传递给命令的附加参数
   */
  args?: string[];

  /**
   * 定义此任务所属的组。还支持将任务标记为组中的默认任务
   */
  group?: 'build' | 'test' | { kind: 'build' | 'test'; isDefault: boolean };

  /**
   * 演示选项
   */
  presentation?: PresentationOptions;

  /**
   * 用于在任务输出中捕获问题的问题匹配器
   */
  problemMatcher?: string | ProblemMatcher | (string | ProblemMatcher)[];

  /**
   * 定义任务何时以及如何运行
   */
  runOptions?: RunOptions;
}

/**
 * 演示选项
 */
interface PresentationOptions {
  /**
   * 控制任务输出是否显示在用户界面中
   * 默认为 'always'
   */
  reveal?: 'never' | 'silent' | 'always';

  /**
   * 控制与任务关联的命令是否在用户界面中回显
   * 默认为 'true'
   */
  echo?: boolean;

  /**
   * 控制显示任务输出的面板是否获得焦点
   * 默认为 'false'
   */
  focus?: boolean;

  /**
   * 控制任务面板是否仅用于此任务（专用）、在任务之间共享（共享）或是否在每次任务执行时创建新面板（新）
   * 默认为 'shared'
   */
  panel?: 'shared' | 'dedicated' | 'new';

  /**
   * 控制是否显示“终端将被任务重用,按任意键关闭”消息
   */
  showReuseMessage?: boolean;

  /**
   * 控制在此任务运行之前是否清除终端
   * 默认为 'false'
   */
  clear?: boolean;

  /**
   * 控制是否使用拆分窗格在特定终端组中执行任务。
   * 同一组中的任务（由字符串值指定）将使用拆分终端而不是新的终端面板来呈现
   */
  group?: string;
}

/**
 * 对在构建输出中检测问题的问题匹配器的描述
 */
interface ProblemMatcher {
  /**
   * 要使用的基本问题匹配器的名称。
   * 如果指定,基本问题匹配器将用作模板
   * 此处指定的属性将替换基本问题匹配器的属性
   */
  base?: string;

  /**
   * 产生的 VS Code 问题的所有者。
   * 如果问题要与语言服务或“外部”产生的问题合并,这通常是 VS Code 语言服务的标识符。
   * 如果省略,则默认为 'external'
   */
  owner?: string;

  /**
   * 此问题匹配器产生的 VS Code 问题的严重性
   *
   * 有效值为:
   *  "error"：产生错误。
   *  "warning"：产生警告。
   *  "info"：产生信息。
   *
   * 如果模式未指定严重性匹配组,则使用该值。
   * 如果省略,则默认为 "error"
   */
  severity?: string;

  /**
   * 定义如何读取问题模式中报告的文件名。
   * 有效值为：
   * - "absolute"：文件名始终被视为绝对的。
   * - "relative"：文件名始终相对于当前工作目录进行处理。这是默认设置。
   * - ["relative", "path value"]：文件名总是相对于给定的路径值处理。
   * - "autodetect"：文件名相对于当前工作空间目录处理,如果文件不存在,则被视为绝对文件名。
   * - ["autodetect", "path value"]：文件名相对于给定的路径值处理,如果不存在,则作为绝对值处理。
   */
  fileLocation?: string | string[];

  /**
   * 预定义问题模式的名称、问题模式的内联定义或用于匹配分布在多行中的问题的问题模式数组
   */
  pattern?: string | ProblemPattern | ProblemPattern[];

  /**
   * 用于检测后台任务（如 Gulp 中的监视任务）何时处于活动状态的附加信息
   */
  background?: BackgroundMatcher;
}

/**
 * 跟踪后台任务开始和结束的描述
 */
interface BackgroundMatcher {
  /**
   * 如果设置为 true,则在任务启动时观察者处于活动模式
   * 这等于发出与 beginPattern 匹配的行
   */
  activeOnStart?: boolean;

  /**
   * 如果在输出中匹配,则发出后台任务开始的信号
   */
  beginsPattern?: string;

  /**
   * 如果在输出中匹配,则发出后台任务结束的信号
   */
  endsPattern?: string;
}

/**
 * 问题模式
 */
interface ProblemPattern {
  /**
   * 在执行任务的控制台输出中查找问题的正则表达式
   */
  regexp: string;

  /**
   * 模式是否匹配整个文件或文件内某个位置的问题
   *
   * 默认为 "location".
   */
  kind?: 'file' | 'location';

  /**
   * 文件名的匹配组索引
   */
  file: number;

  /**
   * 问题位置的匹配组索引
   * 有效的位置模式为：(line)、(line,column) 和 (startLine,startColumn,endLine,endColumn)
   * 如果省略,则使用 line 和 column 属性
   */
  location?: number;

  /**
   * 源文件中问题所在行的匹配组索引
   * 如果指定了位置,则只能省略
   */
  line?: number;

  /**
   * 源文件中问题列的匹配组索引
   */
  column?: number;

  /**
   * 源文件中问题结束行的匹配组索引
   *
   * 默认为未定义,没有捕获结束行
   */
  endLine?: number;

  /**
   * 源文件中问题结束列的匹配组索引
   *
   * 默认为未定义,没有捕获结束列
   */
  endColumn?: number;

  /**
   * 问题严重性的匹配组索引
   *
   * 默认为未定义,在这种情况下,使用问题匹配器的严重性
   */
  severity?: number;

  /**
   * 问题代码的匹配组索引
   *
   * 默认为未定义,未捕获任何代码
   */
  code?: number;

  /**
   * 消息的匹配组索引
   * 默认为 0
   */
  message: number;

  /**
   * 指定多行问题匹配器中的最后一个模式是否应该循环,只要它匹配一行
   * 仅对多行问题匹配器中的最后一个问题模式有效
   */
  loop?: boolean;
}

/**
 * 对何时以及如何运行任务的描述
 */
interface RunOptions {
  /**
   * 控制通过 Rerun Last Task 命令执行任务时如何评估变量
   * 默认值为 `true`,表示重新运行任务时将重新评估变量
   * 当设置为 `false` 时,将使用上次运行任务的已解析变量值
   */
  reevaluateOnRerun?: boolean;

  /**
   * 指定任务何时运行
   *
   * 有效值为:
   *   "default": 任务只有在通过运行任务命令执行时才会运行
   *   "folderOpen": 任务将在包含文件夹打开时运行
   */
  runOn?: string;
}