using Newtonsoft.Json;

namespace ReaperEmporiumTrans;

public static class ExtractEvent
{
    
    public static void DoExtractEvent(string inputDirectory, string outputFile, int messageColumnIndex, int expectedColumnCount)
    {
        List<object> output = new List<object>();
        HashSet<string> occuredStrings = new HashSet<string>();
        var files = Directory.GetFiles(inputDirectory, "db_EventInfo*.txt")
            .Where(file => IsSameTableFile(file, "db_EventInfo"));

        foreach (var file in files)
        {
            ProcessFile(file, output, occuredStrings, messageColumnIndex, expectedColumnCount);
        }

        string jsonOutput = JsonConvert.SerializeObject(output, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(outputFile, jsonOutput);
    }

    static void ProcessFile(string filePath, List<object> output, HashSet<string> occuredStrings, int messageColumnIndex, int expectedColumnCount)
    {
        var index = messageColumnIndex + GetFirstLineColumnCount(filePath) - expectedColumnCount;
        string[] lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var parts = line.Replace("\r", "").Split('\t', StringSplitOptions.None);
            if (index >= 0 && parts.Length > index)  // 确保数组长度足够
            {
                if (!occuredStrings.Add(parts[index])) continue;
                var entry = new
                {
                    key = $"{Path.GetFileName(filePath)}.{parts[0]}",
                    original = parts[index],
                    translation = parts[index],
                    context = "MessageValue"
                };
                output.Add(entry);
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
