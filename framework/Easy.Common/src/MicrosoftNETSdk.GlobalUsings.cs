// Microsoft.Net.Sdk 全局 using
global using global::System;
global using global::System.Collections.Generic;
global using global::System.IO;
global using global::System.Linq;
global using global::System.Threading;
global using global::System.Threading.Tasks;

// 下面根据版本 using
#if !NET462 && !NET472
global using global::System.Net.Http;
#endif
