using Newtonsoft.Json;

namespace ReaperEmporiumTrans;

public static class ExtractEvent
{
    
    public static void DoExtractEvent(string inputDirectory, string outputFile)
    {
        List<object> output = new List<object>();
        HashSet<string> occuredStrings = new HashSet<string>();
        var files = Directory.GetFiles(inputDirectory, "db_EventInfo*.txt");

        foreach (var file in files)
        {
            ProcessFile(file, output, occuredStrings);
        }

        string jsonOutput = JsonConvert.SerializeObject(output, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(Path.Combine(inputDirectory, outputFile), jsonOutput);
    }

    static void ProcessFile(string filePath, List<object> output, HashSet<string> occuredStrings)
    {
        string[] lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var parts = line.Replace("\r", "").Split('\t', StringSplitOptions.None);
            if (parts.Length > 41)  // 确保数组长度足够
            {
                if (!occuredStrings.Add(parts[43])) continue;
                var entry = new
                {
                    key = $"{Path.GetFileName(filePath)}.{parts[0]}",
                    original = parts[43],
                    translation = parts[43],
                    context = (object)null
                };
                output.Add(entry);
            }
        }
    }
}