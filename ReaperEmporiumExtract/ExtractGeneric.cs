using Newtonsoft.Json;

namespace ReaperEmporiumTrans;

public class ExtractGeneric
{
        
    public static void DoExtractGeneric(string inputDirectory, string outputFile, string filePattern, int[] columnIndexes)
    {
        
        HashSet<string> occuredStrings = new HashSet<string>();
        List<object> output = new List<object>();
        var files = Directory.GetFiles(inputDirectory, filePattern);

        foreach (var file in files)
        {
            ProcessFile(file, output, columnIndexes, occuredStrings);
        }

        string jsonOutput = JsonConvert.SerializeObject(output, Formatting.Indented);
        File.WriteAllText(Path.Combine(inputDirectory, outputFile), jsonOutput);
        Console.WriteLine("处理完成，结果已保存到 " + outputFile + " 文件中。");
    }

    static void ProcessFile(string filePath, List<object> output, int[] columnIndexes, HashSet<string> occuredStrings)
    {
        string[] lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var parts = line.Replace("\r", "").Split('\t', StringSplitOptions.None);
            foreach (var index in columnIndexes)
            {
                if (parts.Length > index && occuredStrings.Add(parts[index])) // 确保索引在范围内并且字符串是新的
                {
                    var entry = new
                    {
                        key = $"{Path.GetFileName(filePath)}.{parts[0]}.{index}",
                        original = parts[index],
                        translation = parts[index],
                        context = (object)null
                    };
                    output.Add(entry);
                }
            }
        }
    }
}