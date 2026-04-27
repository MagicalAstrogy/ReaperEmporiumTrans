using System.Text.RegularExpressions;

namespace ReaperEmporiumTrans;

internal sealed class DbLoadSchema
{
    private static readonly Regex DirectStringAssignmentRegex = new(
        @"\b(?:obj|[A-Za-z_][A-Za-z0-9_]*)\.(?<field>[A-Za-z_][A-Za-z0-9_]*)(?:\[[^\]]+\])?\s*=\s*array2\[num\]",
        RegexOptions.Compiled);

    private readonly Dictionary<string, DbTableLoadSchema> _tablesByDb;

    private DbLoadSchema(Dictionary<string, DbTableLoadSchema> tablesByDb)
    {
        _tablesByDb = tablesByDb;
    }

    public static DbLoadSchema Load(string scriptsDirectory)
    {
        var tablesByDb = new Dictionary<string, DbTableLoadSchema>(StringComparer.Ordinal);

        foreach (var file in Directory.GetFiles(scriptsDirectory, "Db*.cs"))
        {
            var dbName = Path.GetFileNameWithoutExtension(file);
            if (dbName.EndsWith("Detail", StringComparison.Ordinal))
            {
                continue;
            }

            var fileSig = "db_" + dbName.Substring(2);
            var table = ParseLoadColumns(file);
            if (table.Fields.Count > 0)
            {
                tablesByDb[fileSig] = table;
            }
        }

        return new DbLoadSchema(tablesByDb);
    }

    public bool TryGetFieldColumns(string fileSig, string fieldName, out IReadOnlyList<int> columns)
    {
        columns = Array.Empty<int>();

        if (!_tablesByDb.TryGetValue(NormalizeFileSig(fileSig), out var table) ||
            !table.Fields.TryGetValue(fieldName, out var fieldColumns))
        {
            return false;
        }

        columns = fieldColumns;
        return true;
    }

    public bool TryGetColumnCount(string fileSig, out int columnCount)
    {
        if (!_tablesByDb.TryGetValue(NormalizeFileSig(fileSig), out var table))
        {
            columnCount = 0;
            return false;
        }

        columnCount = table.ColumnCount;
        return true;
    }

    public bool HasDb(string fileSig)
    {
        return _tablesByDb.ContainsKey(NormalizeFileSig(fileSig));
    }

    private static DbTableLoadSchema ParseLoadColumns(string file)
    {
        var result = new Dictionary<string, List<int>>(StringComparer.Ordinal);
        var columnIndex = -1;

        foreach (var line in File.ReadLines(file))
        {
            if (!line.Contains("array2[num]", StringComparison.Ordinal))
            {
                continue;
            }

            columnIndex++;
            var match = DirectStringAssignmentRegex.Match(line);
            if (!match.Success)
            {
                continue;
            }

            var fieldName = match.Groups["field"].Value;
            if (!result.TryGetValue(fieldName, out var indexes))
            {
                indexes = new List<int>();
                result[fieldName] = indexes;
            }

            indexes.Add(columnIndex);
        }

        return new DbTableLoadSchema(result, columnIndex + 1);
    }

    private static string NormalizeFileSig(string fileSig)
    {
        var normalized = Path.GetFileNameWithoutExtension(fileSig);
        var suffixStart = normalized.LastIndexOf('_');
        if (suffixStart > "db_".Length &&
            suffixStart + 1 < normalized.Length &&
            normalized.AsSpan(suffixStart + 1).ToString().All(char.IsDigit))
        {
            normalized = normalized.Substring(0, suffixStart);
        }

        return normalized;
    }
}

internal sealed record DbTableLoadSchema(Dictionary<string, List<int>> Fields, int ColumnCount);
