// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using HiCmd;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        CommandLineApplication.Execute<Program>(args);
    }

    [Option(ShortName = "f", Description = "加密配置文件路径", LongName = "File")]
    public string File { get; set; }

    [Option(ShortName = "c", Description = "执行命令", LongName = "Cmd")]
    public string Command { get; set; }

    private void OnExecute()
    {
        switch (Command.ToLower())
        {
            case DicCmd.encrypt:
                OnEncrypt();
                break;
        }
    }

    private void OnEncrypt()
    {
        var json = System.IO.File.ReadAllText(File);
        var encryptor = JsonConvert.DeserializeObject<EncryptConf>(json);
        var commands = new List<string>();

        foreach (var module in encryptor.Modules)
        {
            foreach (var assembly in module.Assemblies)
            {
                commands.Add($"{encryptor.EncryptionProgram} {encryptor.Arguments} {assembly.Source} {assembly.Target}");
            }
        }

        ExecuteMultipleCommands(commands);
    }

    /// <summary>
    /// 使用标准输入流执行多行命令
    /// </summary>
    /// <param name="commands">命令集合</param>
    static void ExecuteMultipleCommands(List<string> commands)
    {
        // 创建一个ProcessStartInfo对象
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        // 启动进程
        using (Process process = new Process { StartInfo = psi })
        {
            process.OutputDataReceived += (s, e) => Console.WriteLine(e.Data);
            process.ErrorDataReceived += (s, e) => Console.WriteLine(e.Data);
            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            // 获取标准输入流并写入多行命令
            using (StreamWriter sw = process.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    foreach (var command in commands)
                    {
                        sw.WriteLine(command);
                    }
                }
            }

            // 等待进程执行完毕
            process.WaitForExit();
        }
    }
}

static class DicCmd
{
    /// <summary>
    /// 加密
    /// </summary>
    public const string encrypt = nameof(encrypt);
}