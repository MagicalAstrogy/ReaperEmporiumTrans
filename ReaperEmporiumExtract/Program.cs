using CommandLine;

namespace ReaperEmporiumTrans;

class Program
{    
    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(RunOptionsAndReturnExitCode)
            .WithNotParsed<Options>(HandleParseError);
    }
    
    
    private static void HandleParseError(IEnumerable<Error> errs)
    {
        // 处理错误...
    }

    private static void ExtractGeneric(string inputDir, string outputDirectory, string fileSig, int[] columnIndexes)
    {
        ReaperEmporiumTrans.ExtractGeneric.DoExtractGeneric(inputDir, Path.Join(outputDirectory, $"{fileSig}.json"),
            $"{fileSig}*.txt", columnIndexes);
    }

    private static void RunOptionsAndReturnExitCode(Options opts)
    {
        string inputDirectory = opts.InputDirectory;
        string outputDirectory = opts.OutputDirectory;

        if (string.IsNullOrEmpty(inputDirectory) || !Directory.Exists(inputDirectory))
        {
            Console.WriteLine("请指定一个有效的目录！");
            return;
        }
        
        if (string.IsNullOrEmpty(outputDirectory))
        {
            Console.WriteLine("请指定一个有效的目录！");
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

        ExtractEvent.DoExtractEvent(inputDirectory, Path.Join(outputDirectory, "events.json"));
        //11	3	1	13335	0	0	1905	0	0	ゴブリンの存在は、この世界から消滅する。	ゴブリンをこの世界から絶滅させなければ、この世界は滅びてしまうだろう。そしてこの世界を救うため、ゴブリンを滅ぼす可能性を示した。	最後の運命改変
        ExtractGeneric(inputDirectory, outputDirectory, "db_AchievementDestiny", new[] { 9, 10, 11});
        //1	0	0	0	0	0	3	『商品』を実行する
        ExtractGeneric(inputDirectory, outputDirectory, "db_AchievementGuide", new[] { 7});
        //db_AchievementHikitugi_11
        //17	1	1	1	7	死神スタンプのクリア状況を引き継ぎます。	死神スタンプのクリア状況
        ExtractGeneric(inputDirectory, outputDirectory, "db_AchievementHikitugi", new[] { 5, 6});
        //13	1	3	2460	7	5	12	13000	0	『娯楽』の累計売上が【1億3000万ゴールド】以上	ビッグな娯楽施設デス５
        ExtractGeneric(inputDirectory, outputDirectory, "db_AchievementHistory", new[] { 9, 10});
        //27	10	0	0	アイテムが売れると、少しずつ『死神パワー』がたまるデス
        ExtractGeneric(inputDirectory, outputDirectory, "db_AchievementTips", new[] { 4});
        //43	1	1	0	0	0	1	0	1	0	1	0	0	0	0	1	5000000	0	10000000	20000000	40000000	80000000	160000000	320000000	640000000	1280000000	438	428	300	409	1	71	3	9	1	0	1	0	0	0	1967	聖都セグルメットより伝わる伝説の神像を模したもの。店内をデタラメな感じにブーストする究極の施設である。究極なので1つしか建設できない。なぜかシイネに姿が似ているが、関連は不明である。	寄付金が売上として集まった！(【{1}G】)	女神像がある庭園
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubBuild", new[] { 41, 42, 43});
        //66	6	317	1	692	7	21000	17000	15000	34000	178	201	151	0	65	12000	6	贅沢にたまごを溶きほぐし、何層にも重ねて焼き上げた一品。ふわふわで熱々なこの料理を食べると、大人も子供も笑みがこぼれる。	あつあつ厚焼きエッグ
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubGoods", new[] { 17, 18});
        //22	176	321	7	2	26	22	23	25	家庭用雑貨
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubGoodsKb", new[] { 9});
        //362	3	4480	31	2	3	12	動植物の素材を液体に溶かし込むための特殊な液体。優れた触媒だが、人間の体も溶かすため、扱いには注意が必要。	溶解オイル
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubGoodsMaterial", new[] { 7, 8});
        //22	5	100	130	80	40	4	繊維素材
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubGoodsMaterialKb", new[] { 7});
        //8	1	8	8	0	0	107	101	0	8	0	泡プレイ
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubHRoomPlay", new[] { 11});
        //4	150	3	1	13547	4	4	1562	ドワーム族が所有する鉱山だが、いまだ多くの魔獣が住み着いて荒れており、困っているようだ。ドワーム族との交流が蘇ったことで、ドルワミール鉱山も探索が可能になった。友好の証として冒険者に活躍して貰おう。	ドルワミール鉱山
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubToushiDungeon", new[] { 8, 9});
        //143	1	1	22	6	155	7	7	一緒に戦うヒーラー募集
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubToushiDungeonHakenInfo", new[] { 8});
        //43	1300	9400	13	3	2000000	13	13	42	死星	フレスミス森林道Lv13
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubToushiDungeonInfo", new[] { 9, 10});
        //8	0	300	346	340	554	3	13679	3	2024	2026	2027	263	531	535	534	2022	0	天才薬師『ウィルチ』に憧れる自称の弟子たちが、名を勝手に使って結成した医療組合。薬草の調合や医学の伝聞に努めている。裕福でない民衆の味方であるが、長寿をもたらす魔女の技を祖としているため、多くの権力者の目にさらされている。	魔女達のお茶会
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubToushiOrganization", new[] { 18, 19});
        //54	624	9	4	700000	9	53	ヒエペリオ教会Lv9
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubToushiOrganizationInfo", new[] { 7});
        //12	300	45	13647	27	7	12	15	3	ダンジョン探索中のショップ売上がアップする。スタッフに店を任せていても売上を立ててくれれば、開発ポイントやレアアイテムの収集に専念できるだろう。	頼りになるスタッフ育成
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubToushiSetubi", new[] { 9, 10});
        //45	5	6	10000	5	44	屋敷	エキサイティングな労働環境Lv5
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubToushiSetubiInfo", new[] { 7});
        //672	0	0	943	0	12	239	ミルテの泡パイズリ(熟練)
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubToushiUnitSkillLock", new[] { 7});
        //18	0	0	163	0	0	1	0	0	0	1	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	剣聖一刀両断
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubToushiUnitSkillTreeInfo", new[] { 34});
        //120	306	3	0	400000	1920	4	8	0	マリーゼルの知合いの彫刻家のパトロンになる。	教会Lv8・宗教彫刻家のパトロン
        ExtractGeneric(inputDirectory, outputDirectory, "db_ClubToushiZigyou", new[] { 9, 10});
        //18	0	0	0	0	0	0	0	0	0	0	0	0	0.5	0	-0.2	0	0	0	0	魚殺
        //属性暂时先不管 db_DefineElement_11
        
        //26	0	0	0	1	雷属性のダメージ
        ExtractGeneric(inputDirectory, outputDirectory, "db_DefineValue", new[] { 5});
        //14	命中精度+30%
        ExtractGeneric(inputDirectory, outputDirectory, "db_DefineValueIndivid", new[] { 1});
        
        //77	1	1	1	SEXアタック・PLピストン	-
        //db_Direct_11 暂时不管
        //22	0	0	0	0	0	0	0	0	360	3	300	10	25	40	20	20	20	3	19	0	22	0	0	10	13	50	80	12	25	25	海中より浮上した、かつてのゼカリウス古代王朝。その真の姿は古代の魔術により武装された要塞都市であった。空中要塞キングホエールはいまだ沈黙しているが、動き出す前に封印しなくてはならない。	-	魔海要塞キングホエール
        ExtractGeneric(inputDirectory, outputDirectory, "db_Dungeon", new[] { 31, 33});
        
        //90	0	0	0	0	脱衣・後・貧乳	cloth_after_light
        //db_Event_3 暂时不管
        //32	1	1	1	0	立ち絵の衣服を全裸に
        //db_EventAction_11 不管
        
        //3	0	0	1	15	4	1	0	420	421	18	19	941	16	17	0	2	3	2	2	3	鍛冶姫アンジュ	アンジュ
        
        ExtractGeneric(inputDirectory, outputDirectory, "db_EventChara", new[] { 21, 22});
        
        //24	0	0	1	0	0	164	100	0	0	6	-	コルローネ・キス・ツン②・中
        //db_ImageCutin_11 不管
        
        //db_ImageDotQuarterBuild_11 不管
        
        //db_ImageDotQuarterBuildType_11 不管
        //db_ImageDotQuarterCharaHAnime_11
        
        
        //341	2	1	9	どうですの、この出来栄え
        ExtractGeneric(inputDirectory, outputDirectory, "db_MessageClubInfo", new[] { 4});
        //359	0	1	0	0	10	33	0	0	止めてほしいであります！	-
        ExtractGeneric(inputDirectory, outputDirectory, "db_MessagePartnerAccessInfo", new[] { 9, 10});
        //2734	0	1	1	2	1	454	29	0	0	0	-	あ、あれ？　いつの間に、二人きりに……？	-
        ExtractGeneric(inputDirectory, outputDirectory, "db_MessagePartnerCommonInfo", new[] { 12});
        //105	1	1	114	3	2	0	2	1	【<num>ゴールド】を娯楽で稼ぐ
        ExtractGeneric(inputDirectory, outputDirectory, "db_MessagePartnerMission", new[] { 9});
        //218	0	0	7	106	5	商品開発の停滞は商いの後退です。もっと開発を進めるべきですわ！
        ExtractGeneric(inputDirectory, outputDirectory, "db_MessagePartnerMissionInfo", new[] { 6});
        //3	0	1	1	1	過労死デス
        ExtractGeneric(inputDirectory, outputDirectory, "db_ScenarioEnding", new[] { 5});
        //517	0	0	1167	0	0	0	0	0	0	0	0	1	0	0	0	0	0	0	0	0	ジャネットが案外乙女なこととか？
        ExtractGeneric(inputDirectory, outputDirectory, "db_ScenarioEpisodeSelecter", new[] { 21});
        
        //164	0	0	0	0	0	0	0	0	13	0	9	1	0	0	28	0	1	2	2	8	842	0	50	0	0	3	843	0	0	0	0	9	お兄ちゃんに何度も中出しされちゃったД　女の子が増えても、ルーシェはお兄ちゃんが大好きだからねД	-	-	ルーシェと連続エッチ	0001/01/01 0:00:00
        //db_ScenarioHScene_11 先不管
        ExtractGeneric(inputDirectory, outputDirectory, "db_ScenarioHScene", new[] { 33, 34, 35, 36});
        
        //这个是术语？
        //16	3	4	4	0	0	0	0	65	ウルフィナの部屋
        ExtractGeneric(inputDirectory, outputDirectory, "db_ScenarioTownPlace", new[] { 9});
        //1	1	2	0	0	0	11	0	0	0	0	0	0	0	0	0	2	メインキャラ	死神シイネ
        ExtractGeneric(inputDirectory, outputDirectory, "db_ScenarioTownUnit", new[] { 17, 18});
        //14	0	42	0	一掃
        ExtractGeneric(inputDirectory, outputDirectory, "db_SystemCommand", new[] { 4});
        //47	0	セクハラするデス？　ちゃんと仕事しろデス！　好感度が高いと成功しやすくなるデス。人目に付く事務所だと成功しにくいデスが、仲良くなると関係なくなるデス	-	交流・触る	触る
        ExtractGeneric(inputDirectory, outputDirectory, "db_SystemHelp", new[] { 2, 3, 4});
        //15	0	0	【自分】に【{0}】を付与
        ExtractGeneric(inputDirectory, outputDirectory, "db_SystemMessage", new[] { 3});
        //8	1	0.2	5	1	11	61	10	消耗するスキルの使用回数に影響するデス。スキルをばんばん撃つにはたくさんのMPが必要なのデス	MP
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaBattlerStatus", new[] { 8});
        
        
        
        //668	0	1	1	1	1	1	0	2	10	1	10	1	2	1	0	0	2	2	0	1	0	0	0	0	72	70	73	51	3	2	134	0	0	0	0	0	0	0	0	0	0	0	0	0	19	5	2	10	0	0	0	0	0	0	0	169	0	0	0	176	71	41	179	99	190	0	0	0	4	2	1	1	1	1	0	2	1	3	1	-1	1	2	1	2	0	0	0	アンスリア	アンスリア
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaClub", new[] { 89, 90});
        //1	1	0	0	1	0	6	32	6	39	39	0	39	0	39	0	6	39	39	200	200	501	508	218	16	0	13	4	0	戦士	戦士
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaClubClass", new[] { 29, 30});
        //22	1	0	1	御主人様に従順
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaClubClassHSkill", new[] { 4});
        //1	冒険者
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaClubClassKb", new[] { 1});
        //137	0	55	487	280	587	4	2	-10	20	10	10	12	昼間と夜では別人のような二面性を持つデス。そのギャップに魅力を感じる者もいるようだデス	夜は別の顔
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaClubKosei", new[] { 13, 14});
        //14	0	0	0	0	0	1	0	0	0	0	0	0	1	2	1	0	0	0	17	0	0	1	0	0	0	0	0	13336	2	14	14	0	衛兵さん
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaPartner", new[] { 33});
        //3	1	0	0	0	0	0	1	0	0	1	1	0	1	0	1	10	72	107	25	0	8	0	3	2	0	0	0	0	0	140	軽く抱きしめます。ハグともいいます	<pl>はゆっくりと相手を抱きしめた！	<pl>に抱きついた<pt>は、幸せそうに頭をこすりつけてくる！	<pt>がこちらに抱きついてきた！	優しく抱きしめる
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaPartnerAccessApproach", new[] { 31, 32, 33, 34, 35});
        //7	46	2	どうやら相手もその気らしい！	<pt>はゆっくりと近付いてきた！
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaPartnerAccessApproachReact", new[] { 3, 4});
        //311	0	0	1	0	0	0	1	1	0	150	921	0	0	1	15	26	0	59	0	0	3	0	ボテ腹えっちをする
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaPartnerAccessCombine", new[] { 23});
        
        //db_UnitCharaPartnerAccessCombinePlay_11 不管
        //1	20	-20	48	ハプニングが起こって気が動転している状態。神経が太すぎるキャラ(フロティナ)以外は何かしら動じる。行為のハードルは下がるが、混乱しているので普段大丈夫な行為ですら失敗することも。	ドキドキ
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaPartnerAccessMode", new[] { 4, 5});
        //db_UnitCharaPartnerAccessPosition_11 不管
        //24	2	2	3	触るなデス	触るなデス	なぜ触っているデス？	なぜ触っているデス？	ん、う、くすぐったくなるデス	触るなら、見られない所で頼むデス	それだけでいいデス？　遠慮するなデスД	ずっと触っていても、オマエなら許すデスД
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaPartnerAccessPositionMes", new[] { 4, 5, 6, 7, 8, 9, 10, 11});
        //4	15	15	0	ジャイアントキルに成功！
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaPartnerDungeonResult", new[] { 4});
        //7	30	100	0	何度も延長戦をした
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaPartnerHotelResult", new[] { 4});
        
        //3	4	30	やめてほしい
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaPartnerLove", new[] { 3});
        //9	他ユニットの関係4
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaPartnerProfile", new[] { 1});
        //72	0	1	0	35	0	冒険者のジャネットだ。この店はずっと利用してたけど、まさか直接そこで働くことになるとはねぇ。ま、よろしく頼むよ	-
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitCharaPartnerProfileInfo", new[] { 6});
        
        //db_UnitCharaPartnerQuest_11 不管
        
        //26	0	1	0	0	1	0	91	3	0	1	0	0	0	10	1	0	ゴブリン	ゴブリンシャーマン
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitEnemy", new[] { 18});
        //7	1	0	1	0	0	1	1	18	12	4	5	2	1	3	0	64	0	130	0	0	25	-5	150	160	0	50	168	280	0	0	0	523	0	0	2	3	0	0	0	大人の人間より一回り大きな体を持つ、ゴブリンの荒くれ者。ゴブリンの巣穴に招かれ、用心棒のように活動する者もいる。	ホブゴブリン	-
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitEnemyIndivid", new[] { 40, 41});
        //3	100	0	0	100	0	150	100	86	絶望の
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitEnemyName", new[] { 9});
        
        //db_UnitItemKb_11
        
        //369	0	0	3.6	6	0	0	0	0	0	0	0	0	0	0	1	292	0	0	0	70	13644	5	3	0	0	0	0	0	0	19	0	母なる海のチェイス。味方の【全ての属性スキル】に追撃する。威力が高いが通常のチェイスより発動率は低く。手数が少ないと発動しないことも。	単体攻撃 威力[B] +  命中[70%] + 【水属性】	-	-	B	【<src>】はオーシャンチェイスで追撃した！	オーシャンチェイス
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitSkill", new[] { 32, 33, 37, 38});
        //13	0	1	3	1	ドロップ上昇(大)
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitSkillAction", new[] { 5});
        //123	0	1	1	1	0	90	0	0	0	0	0	ダメージを負った相手を優先する確率が【+{0}%】アップ！
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitSkillPassive", new[] { 12});
        //64	204	0	1	0	50	21	10	属性付与・闇属性	-	属性付与	-	【<dst>】の武器に【闇属性】が付与された！	【<dst>】の属性付与が消えた……	闇付与
        ExtractGeneric(inputDirectory, outputDirectory, "db_UnitStatusChange_11", new[] { 8, 10, 12, 13, 14});
        //Legacy Problem
        
        //db_VoiceChara_11 不管
        //14	ゼカリウスを支配する地方領主ゼルカの屋敷。市民から多額の税金を徴収することで、自らの居城を豪華絢爛に飾り立てている。中には忌まわしき実験場があるというウワサもある。	地方領主ゼルカの屋敷
        ExtractGeneric(inputDirectory, outputDirectory, "db_WorldPlace", new[] { 1, 2});
        
        //
        
        
        Console.WriteLine($"处理完成，结果已保存到 {outputDirectory} 中。");
        //DbStaff可以加参与人员
        
    }

}