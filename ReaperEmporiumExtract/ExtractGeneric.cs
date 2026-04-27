using Newtonsoft.Json;

namespace ReaperEmporiumTrans;

public sealed record ColumnExtraction(int Index, string FieldName);

public class ExtractGeneric
{
        
    public static void DoExtractGeneric(string inputDirectory, string outputFile, string filePattern, int[] columnIndexes)
    {
        DoExtractGeneric(inputDirectory, outputFile, filePattern,
            columnIndexes.Select(index => new ColumnExtraction(index, index.ToString())).ToArray());
    }

    public static void DoExtractGeneric(string inputDirectory, string outputFile, string filePattern, IReadOnlyList<ColumnExtraction> columns, int? expectedColumnCount = null)
    {
        
        HashSet<string> occuredStrings = new HashSet<string>();
        List<object> output = new List<object>();
        var files = GetTableFiles(inputDirectory, filePattern);

        foreach (var file in files)
        {
            ProcessFile(file, output, columns, occuredStrings, expectedColumnCount);
        }

        string jsonOutput = JsonConvert.SerializeObject(output, Formatting.Indented);
        File.WriteAllText(outputFile, jsonOutput);
        Console.WriteLine("处理完成，结果已保存到 " + outputFile + " 文件中。");
    }

    static void ProcessFile(string filePath, List<object> output, IReadOnlyList<ColumnExtraction> columns, HashSet<string> occuredStrings, int? expectedColumnCount)
    {
        var columnOffset = expectedColumnCount.HasValue
            ? GetFirstLineColumnCount(filePath) - expectedColumnCount.Value
            : 0;

        string[] lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var parts = line.Replace("\r", "").Split('\t', StringSplitOptions.None);
            foreach (var column in columns)
            {
                var index = column.Index + columnOffset;
                if (index >= 0 && parts.Length > index && occuredStrings.Add(parts[index])) // 确保索引在范围内并且字符串是新的
                {
                    var entry = new
                    {
                        key = $"{Path.GetFileName(filePath)}.{parts[0]}.{index}",
                        original = parts[index],
                        translation = parts[index],
                        context = column.FieldName
                    };
                    output.Add(entry);
                }
            }
        }
    }

    private static int GetFirstLineColumnCount(string filePath)
    {
        foreach (var line in File.ReadLines(filePath))
        {
            if (line.Length == 0)
            {
                continue;
            }

            return line.Replace("\r", "").Split('\t', StringSplitOptions.None).Length;
        }

        return 0;
    }

    private static IEnumerable<string> GetTableFiles(string inputDirectory, string filePattern)
    {
        var files = Directory.GetFiles(inputDirectory, filePattern);
        if (!filePattern.EndsWith("*.txt", StringComparison.Ordinal))
        {
            return files;
        }

        var fileSig = filePattern.Substring(0, filePattern.Length - "*.txt".Length);
        return files.Where(file => IsSameTableFile(file, fileSig)).ToArray();
    }

    private static bool IsSameTableFile(string filePath, string fileSig)
    {
        var name = Path.GetFileNameWithoutExtension(filePath);
        if (name == fileSig)
        {
            return true;
        }

        var prefix = fileSig + "_";
        return name.StartsWith(prefix, StringComparison.Ordinal) &&
               name.Substring(prefix.Length).All(char.IsDigit);
    }
}
