using CommandLine;

namespace ReaperEmporiumTrans;

class Program
{
    private static readonly DbExtractionSpec[] DbSpecs =
    {
        new("db_AchievementDestiny", "CommentDst", "CommentSrc", "Name"),
        new("db_AchievementGuide", "Name"),
        new("db_AchievementHikitugi", new ColumnExtraction[] { new(5, "Comment"), new(6, "Name") }),
        new("db_AchievementHistory", "Comment", "Name"),
        new("db_AchievementTips", "Name"),
        new("db_ClubBuild", "Comment", "LogUseMessage", "Name"),
        new("db_ClubGoods", "Comment", "Name"),
        new("db_ClubGoodsKb", "Name"),
        new("db_ClubGoodsMaterial", "Comment", "Name"),
        new("db_ClubGoodsMaterialKb", "Name"),
        new("db_ClubHRoomPlay", "Name"),
        new("db_ClubToushiDungeon", "Comment", "Name"),
        new("db_ClubToushiDungeonHakenInfo", "Title"),
        new("db_ClubToushiDungeonInfo", "Memo", "Title"),
        new("db_ClubToushiOrganization", "Comment", "Name_disp"),
        new("db_ClubToushiOrganizationInfo", "Title"),
        new("db_ClubToushiSetubi", "Comment", "Name"),
        new("db_ClubToushiSetubiInfo", "Title"),
        new("db_ClubToushiUnitSkillLock", "NameHScene"),
        new("db_ClubToushiUnitSkillTreeInfo", "Title"),
        new("db_ClubToushiZigyou", "Comment", "Name"),
        new("db_DefineValue", "Format"),
        new("db_DefineValueIndivid", "Name"),
        new("db_Dungeon", "Comment", "Name_disp"),
        new("db_EventChara", "Name_s", "Name"),
        new("db_MessageClubInfo", "Message"),
        new("db_MessagePartnerAccessInfo", "Message"),
        new("db_MessagePartnerCommonInfo", "Message"),
        new("db_MessagePartnerMission", "Name"),
        new("db_MessagePartnerMissionInfo", "Message"),
        new("db_ScenarioEnding", "Name"),
        new("db_ScenarioEpisodeSelecter", "Title"),
        new("db_ScenarioHScene", "Title", "ParentEpisodeName", "Hint", "Comment"),
        new("db_ScenarioTownPlace", "Name"),
        new("db_ScenarioTownUnit", "Group", "Name"),
        new("db_SystemCommand", "Title"),
        new("db_SystemHelp", "Comment", "Group", "Name_disp", "Name"),
        new("db_SystemMessage", "Format"),
        new("db_UnitCharaBattlerStatus", "Comment"),
        new("db_UnitCharaClub", "Name_disp", "Name"),
        new("db_UnitCharaClubClass", "Name_format", "Name"),
        new("db_UnitCharaClubClassHSkill", "Name"),
        new("db_UnitCharaClubClassKb", "Name"),
        new("db_UnitCharaClubKosei", "Comment", "Name"),
        new("db_UnitCharaPartner", "Name"),
        new("db_UnitCharaPartnerAccessApproach", "Log_normal", "Log_partner_after", "Log_partner_before", "Comment", "Name"),
        new("db_UnitCharaPartnerAccessApproachReact", "Log_output_after", "Log_output_before"),
        new("db_UnitCharaPartnerAccessCombine", "Title"),
        new("db_UnitCharaPartnerAccessMode", "Comment", "Name"),
        new("db_UnitCharaPartnerAccessPositionMes", "Comment1", "Comment2", "Comment3", "Comment4", "Comment5", "Comment6", "Comment7", "Comment8"),
        new("db_UnitCharaPartnerDungeonResult", "Name"),
        new("db_UnitCharaPartnerHotelResult", "Name"),
        new("db_UnitCharaPartnerLove", "Name_ero"),
        new("db_UnitCharaPartnerProfile", "Name"),
        new("db_UnitCharaPartnerProfileInfo", "Comment"),
        new("db_UnitEnemy", "Name"),
        new("db_UnitEnemyIndivid", "Name_library", "Name"),
        new("db_UnitEnemyName", "Name"),
        new("db_UnitSkill", "Comment_append_normal", "Comment", "Log_normal", "Name"),
        new("db_UnitSkillAction", "Name"),
        new("db_UnitSkillPassive", "Comment_append_format"),
        new("db_UnitStatusChange", "Comment", "Comment_append", "Log_actcancel", "Log_happen", "Log_recovery", "Name"),
        new("db_WorldPlace", "Comment", "Name"),
    };

    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(RunOptionsAndReturnExitCode)
            .WithNotParsed(HandleParseError);
    }

    private static void HandleParseError(IEnumerable<Error> errs)
    {
    }

    private static void RunOptionsAndReturnExitCode(Options opts)
    {
        string inputDirectory = opts.InputDirectory;
        string outputDirectory = opts.OutputDirectory;

        if (string.IsNullOrEmpty(inputDirectory) || !Directory.Exists(inputDirectory))
        {
            Console.WriteLine("请指定一个有效的输入目录！");
            return;
        }

        if (string.IsNullOrEmpty(outputDirectory))
        {
            Console.WriteLine("请指定一个有效的输出目录！");
            return;
        }

        if (!TryResolveScriptsDirectory(inputDirectory, opts.ScriptsDirectory, out var scriptsDirectory))
        {
            Console.WriteLine("无法定位 Assets/Scripts/Assembly-CSharp 目录，请使用 --scripts 指定。");
            return;
        }

        try
        {
            Directory.CreateDirectory(outputDirectory);
        }
        catch (Exception e)
        {
            Console.WriteLine($"创建目录失败：{outputDirectory}, {e.Message}");
            return;
        }

        var schema = DbLoadSchema.Load(scriptsDirectory);
        ExtractEventText(inputDirectory, outputDirectory, schema);

        foreach (var spec in DbSpecs)
        {
            ExtractGeneric(inputDirectory, outputDirectory, schema, spec);
        }

        Console.WriteLine($"处理完成，结果已保存到 {outputDirectory} 中。");
    }

    private static void ExtractEventText(string inputDirectory, string outputDirectory, DbLoadSchema schema)
    {
        if (!schema.TryGetFieldColumns("db_EventInfo", "MessageValue", out var columns))
        {
            Console.WriteLine("跳过 events.json：DbEventInfo.MessageValue 字段未找到。");
            return;
        }

        if (!schema.TryGetColumnCount("db_EventInfo", out var expectedColumnCount))
        {
            Console.WriteLine("跳过 events.json：DbEventInfo 列数未找到。");
            return;
        }

        ExtractEvent.DoExtractEvent(inputDirectory, Path.Join(outputDirectory, "events.json"), columns[0], expectedColumnCount);
    }

    private static void ExtractGeneric(string inputDir, string outputDirectory, DbLoadSchema schema, DbExtractionSpec spec)
    {
        var columns = ResolveColumns(schema, spec).ToArray();
        if (columns.Length == 0)
        {
            Console.WriteLine($"跳过 {spec.FileSig}：没有可用字段。");
            return;
        }

        schema.TryGetColumnCount(spec.FileSig, out var expectedColumnCount);
        ReaperEmporiumTrans.ExtractGeneric.DoExtractGeneric(inputDir, Path.Join(outputDirectory, $"{spec.FileSig}.json"),
            $"{spec.FileSig}*.txt", columns, expectedColumnCount == 0 ? null : expectedColumnCount);
    }

    private static IEnumerable<ColumnExtraction> ResolveColumns(DbLoadSchema schema, DbExtractionSpec spec)
    {
        if (spec.FallbackColumns is not null && !schema.HasDb(spec.FileSig))
        {
            return spec.FallbackColumns;
        }

        var columns = new List<ColumnExtraction>();
        foreach (var fieldName in spec.FieldNames)
        {
            if (!schema.TryGetFieldColumns(spec.FileSig, fieldName, out var fieldColumns))
            {
                Console.WriteLine($"警告：{spec.FileSig}.{fieldName} 字段未找到。");
                continue;
            }

            columns.AddRange(fieldColumns.Select(index => new ColumnExtraction(index, fieldName)));
        }

        return columns;
    }

    private static bool TryResolveScriptsDirectory(string inputDirectory, string configuredScriptsDirectory, out string scriptsDirectory)
    {
        if (!string.IsNullOrWhiteSpace(configuredScriptsDirectory))
        {
            scriptsDirectory = configuredScriptsDirectory;
            return Directory.Exists(scriptsDirectory);
        }

        var current = new DirectoryInfo(inputDirectory);
        while (current is not null)
        {
            var exportedProject = Path.Join(current.FullName, "Extracted", "ExportedProject", "Assets", "Scripts", "Assembly-CSharp");
            if (Directory.Exists(exportedProject))
            {
                scriptsDirectory = exportedProject;
                return true;
            }

            current = current.Parent;
        }

        scriptsDirectory = "";
        return false;
    }
}

internal sealed record DbExtractionSpec
{
    public DbExtractionSpec(string fileSig, params string[] fieldNames)
    {
        FileSig = fileSig;
        FieldNames = fieldNames;
    }

    public DbExtractionSpec(string fileSig, IReadOnlyList<ColumnExtraction> fallbackColumns)
    {
        FileSig = fileSig;
        FieldNames = Array.Empty<string>();
        FallbackColumns = fallbackColumns;
    }

    public string FileSig { get; }

    public IReadOnlyList<string> FieldNames { get; }

    public IReadOnlyList<ColumnExtraction>? FallbackColumns { get; }
}
