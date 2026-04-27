using CommandLine;

// 定义一个类来接收命令行参数
class Options
{
    [Option('i', "input", Required = true, HelpText = "输入文件夹的路径。")]
    public string InputDirectory { get; set; } = "";

    [Option('o', "output", Required = true, HelpText = "输出JSON文件的文件夹路径。")]
    public string OutputDirectory { get; set; } = "";

    [Option('s', "scripts", Required = false, HelpText = "导出工程 Assets/Scripts/Assembly-CSharp 目录。未指定时会从输入 db 目录自动查找。")]
    public string ScriptsDirectory { get; set; } = "";
}
