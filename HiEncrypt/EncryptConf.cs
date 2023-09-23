using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HiCmd;
/// <summary>
/// 根配置实体
/// </summary>
internal class EncryptConf
{
    /// <summary>
    /// 源目录
    /// </summary>
    public string SourceDir { get; set; }

    /// <summary>
    /// 目标目录
    /// </summary>
    public string TargetDir { get; set; }

    /// <summary>
    /// 加密程序
    /// </summary>
    public string EncryptionProgram { get; set; }

    /// <summary>
    /// 程序参数
    /// </summary>
    public string Arguments { get; set; }

    /// <summary>
    /// 模块列表
    /// </summary>
    public List<Module> Modules { get; set; }
}

/// <summary>
/// 模块实体
/// </summary>
public class Module
{
    /// <summary>
    /// 模块名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 程序集列表
    /// </summary>
    public List<AssemblyFile> Assemblies { get; set; }
}

/// <summary>
/// 程序集实体
/// </summary>
public class AssemblyFile
{ 
    /// <summary>
    /// 源文件
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// 目标文件
    /// </summary>
    public string Target { get; set; }
}
