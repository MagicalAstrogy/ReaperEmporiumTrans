using System.Collections.Generic;
using HarmonyLib;

namespace MagicalAstrogy.ReaperEmporiumTrans
{
    internal static class DbTextHookSupport
    {
        public static bool IsValidString(string str)
        {
            return !string.IsNullOrEmpty(str) && str != "DUMMY" && str != "-";
        }

        public static bool TryTranslate(Dictionary<string, string> dict, ref string str)
        {
            if (!IsValidString(str))
            {
                return false;
            }

            if (!dict.TryGetValue(str, out var translated))
            {
                return false;
            }

            str = translated;
            return true;
        }

        public static bool TryGetTranslation(string strFile, string transName, out Dictionary<string, string> dict)
        {
            if (TranslationDB.AllTranslation.TryGetValue(transName, out dict))
            {
                return true;
            }

            Logger.Log($"Failed to translate {strFile}, {transName} not found.");
            return false;
        }

        public static void LogLoaded(string strFile, int total, int translated)
        {
            Logger.Log($"Loaded {strFile}, total strings {total}, translated {translated}.");
        }
    }

    [HarmonyPatch(typeof(DbEventInfo), nameof(DbEventInfo.Load))]
    internal static class DbEventInfoHook
    {
        public static void Postfix(string strFile, DbEventInfo __instance)
        {
            const string transName = "events_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (element.EventCmd != 0 && element.MessageValue != "DUMMY" &&
                    !(element.EventCmd == 21 && element.MessageValue == "-"))
                {
                    if (dict.TryGetValue(element.MessageValue, out var translated))
                    {
                        element.MessageValue = translated;
                        counter++;
                    }
                }
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbAchievementDestiny), nameof(DbAchievementDestiny.Load))]
    internal static class DbAchievementDestinyHook
    {
        public static void Postfix(string strFile, DbAchievementDestiny __instance)
        {
            const string transName = "db_AchievementDestiny_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.CommentDst)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.CommentSrc)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbAchievementGuide), nameof(DbAchievementGuide.Load))]
    internal static class DbAchievementGuideHook
    {
        public static void Postfix(string strFile, DbAchievementGuide __instance)
        {
            const string transName = "db_AchievementGuide_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbAchievementHistory), nameof(DbAchievementHistory.Load))]
    internal static class DbAchievementHistoryHook
    {
        public static void Postfix(string strFile, DbAchievementHistory __instance)
        {
            const string transName = "db_AchievementHistory_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbAchievementTips), nameof(DbAchievementTips.Load))]
    internal static class DbAchievementTipsHook
    {
        public static void Postfix(string strFile, DbAchievementTips __instance)
        {
            const string transName = "db_AchievementTips_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubBuild), nameof(DbClubBuild.Load))]
    internal static class DbClubBuildHook
    {
        public static void Postfix(string strFile, DbClubBuild __instance)
        {
            const string transName = "db_ClubBuild_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.LogUseMessage)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubGoods), nameof(DbClubGoods.Load))]
    internal static class DbClubGoodsHook
    {
        public static void Postfix(string strFile, DbClubGoods __instance)
        {
            const string transName = "db_ClubGoods_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubGoodsKb), nameof(DbClubGoodsKb.Load))]
    internal static class DbClubGoodsKbHook
    {
        public static void Postfix(string strFile, DbClubGoodsKb __instance)
        {
            const string transName = "db_ClubGoodsKb_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubGoodsMaterial), nameof(DbClubGoodsMaterial.Load))]
    internal static class DbClubGoodsMaterialHook
    {
        public static void Postfix(string strFile, DbClubGoodsMaterial __instance)
        {
            const string transName = "db_ClubGoodsMaterial_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubGoodsMaterialKb), nameof(DbClubGoodsMaterialKb.Load))]
    internal static class DbClubGoodsMaterialKbHook
    {
        public static void Postfix(string strFile, DbClubGoodsMaterialKb __instance)
        {
            const string transName = "db_ClubGoodsMaterialKb_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubHRoomPlay), nameof(DbClubHRoomPlay.Load))]
    internal static class DbClubHRoomPlayHook
    {
        public static void Postfix(string strFile, DbClubHRoomPlay __instance)
        {
            const string transName = "db_ClubHRoomPlay_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubToushiDungeon), nameof(DbClubToushiDungeon.Load))]
    internal static class DbClubToushiDungeonHook
    {
        public static void Postfix(string strFile, DbClubToushiDungeon __instance)
        {
            const string transName = "db_ClubToushiDungeon_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubToushiDungeonHakenInfo), nameof(DbClubToushiDungeonHakenInfo.Load))]
    internal static class DbClubToushiDungeonHakenInfoHook
    {
        public static void Postfix(string strFile, DbClubToushiDungeonHakenInfo __instance)
        {
            const string transName = "db_ClubToushiDungeonHakenInfo_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Title)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubToushiDungeonInfo), nameof(DbClubToushiDungeonInfo.Load))]
    internal static class DbClubToushiDungeonInfoHook
    {
        public static void Postfix(string strFile, DbClubToushiDungeonInfo __instance)
        {
            const string transName = "db_ClubToushiDungeonInfo_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Memo)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Title)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubToushiOrganization), nameof(DbClubToushiOrganization.Load))]
    internal static class DbClubToushiOrganizationHook
    {
        public static void Postfix(string strFile, DbClubToushiOrganization __instance)
        {
            const string transName = "db_ClubToushiOrganization_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name_disp)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubToushiOrganizationInfo), nameof(DbClubToushiOrganizationInfo.Load))]
    internal static class DbClubToushiOrganizationInfoHook
    {
        public static void Postfix(string strFile, DbClubToushiOrganizationInfo __instance)
        {
            const string transName = "db_ClubToushiOrganizationInfo_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Title)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubToushiSetubi), nameof(DbClubToushiSetubi.Load))]
    internal static class DbClubToushiSetubiHook
    {
        public static void Postfix(string strFile, DbClubToushiSetubi __instance)
        {
            const string transName = "db_ClubToushiSetubi_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubToushiSetubiInfo), nameof(DbClubToushiSetubiInfo.Load))]
    internal static class DbClubToushiSetubiInfoHook
    {
        public static void Postfix(string strFile, DbClubToushiSetubiInfo __instance)
        {
            const string transName = "db_ClubToushiSetubiInfo_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Title)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubToushiUnitSkillLock), nameof(DbClubToushiUnitSkillLock.Load))]
    internal static class DbClubToushiUnitSkillLockHook
    {
        public static void Postfix(string strFile, DbClubToushiUnitSkillLock __instance)
        {
            const string transName = "db_ClubToushiUnitSkillLock_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.NameHScene)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubToushiUnitSkillTreeInfo), nameof(DbClubToushiUnitSkillTreeInfo.Load))]
    internal static class DbClubToushiUnitSkillTreeInfoHook
    {
        public static void Postfix(string strFile, DbClubToushiUnitSkillTreeInfo __instance)
        {
            const string transName = "db_ClubToushiUnitSkillTreeInfo_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Title)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbClubToushiZigyou), nameof(DbClubToushiZigyou.Load))]
    internal static class DbClubToushiZigyouHook
    {
        public static void Postfix(string strFile, DbClubToushiZigyou __instance)
        {
            const string transName = "db_ClubToushiZigyou_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbDefineValue), nameof(DbDefineValue.Load))]
    internal static class DbDefineValueHook
    {
        public static void Postfix(string strFile, DbDefineValue __instance)
        {
            const string transName = "db_DefineValue_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Format)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbDefineValueIndivid), nameof(DbDefineValueIndivid.Load))]
    internal static class DbDefineValueIndividHook
    {
        public static void Postfix(string strFile, DbDefineValueIndivid __instance)
        {
            const string transName = "db_DefineValueIndivid_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbDungeon), nameof(DbDungeon.Load))]
    internal static class DbDungeonHook
    {
        public static void Postfix(string strFile, DbDungeon __instance)
        {
            const string transName = "db_Dungeon_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name_disp)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbEventChara), nameof(DbEventChara.Load))]
    internal static class DbEventCharaHook
    {
        public static void Postfix(string strFile, DbEventChara __instance)
        {
            const string transName = "db_EventChara_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name_s)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbMessageClubInfo), nameof(DbMessageClubInfo.Load))]
    internal static class DbMessageClubInfoHook
    {
        public static void Postfix(string strFile, DbMessageClubInfo __instance)
        {
            const string transName = "db_MessageClubInfo_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Message)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbMessagePartnerAccessInfo), nameof(DbMessagePartnerAccessInfo.Load))]
    internal static class DbMessagePartnerAccessInfoHook
    {
        public static void Postfix(string strFile, DbMessagePartnerAccessInfo __instance)
        {
            const string transName = "db_MessagePartnerAccessInfo_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Message)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbMessagePartnerCommonInfo), nameof(DbMessagePartnerCommonInfo.Load))]
    internal static class DbMessagePartnerCommonInfoHook
    {
        public static void Postfix(string strFile, DbMessagePartnerCommonInfo __instance)
        {
            const string transName = "db_MessagePartnerCommonInfo_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Message)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbMessagePartnerMission), nameof(DbMessagePartnerMission.Load))]
    internal static class DbMessagePartnerMissionHook
    {
        public static void Postfix(string strFile, DbMessagePartnerMission __instance)
        {
            const string transName = "db_MessagePartnerMission_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbMessagePartnerMissionInfo), nameof(DbMessagePartnerMissionInfo.Load))]
    internal static class DbMessagePartnerMissionInfoHook
    {
        public static void Postfix(string strFile, DbMessagePartnerMissionInfo __instance)
        {
            const string transName = "db_MessagePartnerMissionInfo_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Message)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbScenarioEnding), nameof(DbScenarioEnding.Load))]
    internal static class DbScenarioEndingHook
    {
        public static void Postfix(string strFile, DbScenarioEnding __instance)
        {
            const string transName = "db_ScenarioEnding_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbScenarioEpisodeSelecter), nameof(DbScenarioEpisodeSelecter.Load))]
    internal static class DbScenarioEpisodeSelecterHook
    {
        public static void Postfix(string strFile, DbScenarioEpisodeSelecter __instance)
        {
            const string transName = "db_ScenarioEpisodeSelecter_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Title)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbScenarioHScene), nameof(DbScenarioHScene.Load))]
    internal static class DbScenarioHSceneHook
    {
        public static void Postfix(string strFile, DbScenarioHScene __instance)
        {
            const string transName = "db_ScenarioHScene_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Title)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.ParentEpisodeName)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Hint)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbScenarioTownPlace), nameof(DbScenarioTownPlace.Load))]
    internal static class DbScenarioTownPlaceHook
    {
        public static void Postfix(string strFile, DbScenarioTownPlace __instance)
        {
            const string transName = "db_ScenarioTownPlace_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbScenarioTownUnit), nameof(DbScenarioTownUnit.Load))]
    internal static class DbScenarioTownUnitHook
    {
        public static void Postfix(string strFile, DbScenarioTownUnit __instance)
        {
            const string transName = "db_ScenarioTownUnit_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Group)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbSystemCommand), nameof(DbSystemCommand.Load))]
    internal static class DbSystemCommandHook
    {
        public static void Postfix(string strFile, DbSystemCommand __instance)
        {
            const string transName = "db_SystemCommand_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Title)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbSystemHelp), nameof(DbSystemHelp.Load))]
    internal static class DbSystemHelpHook
    {
        public static void Postfix(string strFile, DbSystemHelp __instance)
        {
            const string transName = "db_SystemHelp_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Group)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name_disp)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbSystemMessage), nameof(DbSystemMessage.Load))]
    internal static class DbSystemMessageHook
    {
        public static void Postfix(string strFile, DbSystemMessage __instance)
        {
            const string transName = "db_SystemMessage_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Format)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaBattlerStatus), nameof(DbUnitCharaBattlerStatus.Load))]
    internal static class DbUnitCharaBattlerStatusHook
    {
        public static void Postfix(string strFile, DbUnitCharaBattlerStatus __instance)
        {
            const string transName = "db_UnitCharaBattlerStatus_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaClub), nameof(DbUnitCharaClub.Load))]
    internal static class DbUnitCharaClubHook
    {
        public static void Postfix(string strFile, DbUnitCharaClub __instance)
        {
            const string transName = "db_UnitCharaClub_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name_disp)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaClubClass), nameof(DbUnitCharaClubClass.Load))]
    internal static class DbUnitCharaClubClassHook
    {
        public static void Postfix(string strFile, DbUnitCharaClubClass __instance)
        {
            const string transName = "db_UnitCharaClubClass_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name_format)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaClubClassHSkill), nameof(DbUnitCharaClubClassHSkill.Load))]
    internal static class DbUnitCharaClubClassHSkillHook
    {
        public static void Postfix(string strFile, DbUnitCharaClubClassHSkill __instance)
        {
            const string transName = "db_UnitCharaClubClassHSkill_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaClubClassKb), nameof(DbUnitCharaClubClassKb.Load))]
    internal static class DbUnitCharaClubClassKbHook
    {
        public static void Postfix(string strFile, DbUnitCharaClubClassKb __instance)
        {
            const string transName = "db_UnitCharaClubClassKb_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaClubKosei), nameof(DbUnitCharaClubKosei.Load))]
    internal static class DbUnitCharaClubKoseiHook
    {
        public static void Postfix(string strFile, DbUnitCharaClubKosei __instance)
        {
            const string transName = "db_UnitCharaClubKosei_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaPartner), nameof(DbUnitCharaPartner.Load))]
    internal static class DbUnitCharaPartnerHook
    {
        public static void Postfix(string strFile, DbUnitCharaPartner __instance)
        {
            const string transName = "db_UnitCharaPartner_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaPartnerAccessApproach), nameof(DbUnitCharaPartnerAccessApproach.Load))]
    internal static class DbUnitCharaPartnerAccessApproachHook
    {
        public static void Postfix(string strFile, DbUnitCharaPartnerAccessApproach __instance)
        {
            const string transName = "db_UnitCharaPartnerAccessApproach_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Log_normal)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Log_partner_after)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Log_partner_before)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaPartnerAccessApproachReact), nameof(DbUnitCharaPartnerAccessApproachReact.Load))]
    internal static class DbUnitCharaPartnerAccessApproachReactHook
    {
        public static void Postfix(string strFile, DbUnitCharaPartnerAccessApproachReact __instance)
        {
            const string transName = "db_UnitCharaPartnerAccessApproachReact_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Log_output_after)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Log_output_before)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaPartnerAccessCombine), nameof(DbUnitCharaPartnerAccessCombine.Load))]
    internal static class DbUnitCharaPartnerAccessCombineHook
    {
        public static void Postfix(string strFile, DbUnitCharaPartnerAccessCombine __instance)
        {
            const string transName = "db_UnitCharaPartnerAccessCombine_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Title)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaPartnerAccessMode), nameof(DbUnitCharaPartnerAccessMode.Load))]
    internal static class DbUnitCharaPartnerAccessModeHook
    {
        public static void Postfix(string strFile, DbUnitCharaPartnerAccessMode __instance)
        {
            const string transName = "db_UnitCharaPartnerAccessMode_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaPartnerAccessPositionMes), nameof(DbUnitCharaPartnerAccessPositionMes.Load))]
    internal static class DbUnitCharaPartnerAccessPositionMesHook
    {
        public static void Postfix(string strFile, DbUnitCharaPartnerAccessPositionMes __instance)
        {
            const string transName = "db_UnitCharaPartnerAccessPositionMes_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment1)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment2)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment3)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment4)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment5)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment6)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment7)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment8)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaPartnerDungeonResult), nameof(DbUnitCharaPartnerDungeonResult.Load))]
    internal static class DbUnitCharaPartnerDungeonResultHook
    {
        public static void Postfix(string strFile, DbUnitCharaPartnerDungeonResult __instance)
        {
            const string transName = "db_UnitCharaPartnerDungeonResult_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaPartnerHotelResult), nameof(DbUnitCharaPartnerHotelResult.Load))]
    internal static class DbUnitCharaPartnerHotelResultHook
    {
        public static void Postfix(string strFile, DbUnitCharaPartnerHotelResult __instance)
        {
            const string transName = "db_UnitCharaPartnerHotelResult_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaPartnerLove), nameof(DbUnitCharaPartnerLove.Load))]
    internal static class DbUnitCharaPartnerLoveHook
    {
        public static void Postfix(string strFile, DbUnitCharaPartnerLove __instance)
        {
            const string transName = "db_UnitCharaPartnerLove_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name_ero)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaPartnerProfile), nameof(DbUnitCharaPartnerProfile.Load))]
    internal static class DbUnitCharaPartnerProfileHook
    {
        public static void Postfix(string strFile, DbUnitCharaPartnerProfile __instance)
        {
            const string transName = "db_UnitCharaPartnerProfile_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitCharaPartnerProfileInfo), nameof(DbUnitCharaPartnerProfileInfo.Load))]
    internal static class DbUnitCharaPartnerProfileInfoHook
    {
        public static void Postfix(string strFile, DbUnitCharaPartnerProfileInfo __instance)
        {
            const string transName = "db_UnitCharaPartnerProfileInfo_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitEnemy), nameof(DbUnitEnemy.Load))]
    internal static class DbUnitEnemyHook
    {
        public static void Postfix(string strFile, DbUnitEnemy __instance)
        {
            const string transName = "db_UnitEnemy_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitEnemyIndivid), nameof(DbUnitEnemyIndivid.Load))]
    internal static class DbUnitEnemyIndividHook
    {
        public static void Postfix(string strFile, DbUnitEnemyIndivid __instance)
        {
            const string transName = "db_UnitEnemyIndivid_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name_library)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitEnemyName), nameof(DbUnitEnemyName.Load))]
    internal static class DbUnitEnemyNameHook
    {
        public static void Postfix(string strFile, DbUnitEnemyName __instance)
        {
            const string transName = "db_UnitEnemyName_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitSkill), nameof(DbUnitSkill.Load))]
    internal static class DbUnitSkillHook
    {
        public static void Postfix(string strFile, DbUnitSkill __instance)
        {
            const string transName = "db_UnitSkill_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment_append_normal)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Log_normal)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitSkillAction), nameof(DbUnitSkillAction.Load))]
    internal static class DbUnitSkillActionHook
    {
        public static void Postfix(string strFile, DbUnitSkillAction __instance)
        {
            const string transName = "db_UnitSkillAction_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitSkillPassive), nameof(DbUnitSkillPassive.Load))]
    internal static class DbUnitSkillPassiveHook
    {
        public static void Postfix(string strFile, DbUnitSkillPassive __instance)
        {
            const string transName = "db_UnitSkillPassive_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment_append_format)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbUnitStatusChange), nameof(DbUnitStatusChange.Load))]
    internal static class DbUnitStatusChangeHook
    {
        public static void Postfix(string strFile, DbUnitStatusChange __instance)
        {
            const string transName = "db_UnitStatusChange_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment_append)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Log_actcancel)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Log_happen)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Log_recovery)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }

    [HarmonyPatch(typeof(DbWorldPlace), nameof(DbWorldPlace.Load))]
    internal static class DbWorldPlaceHook
    {
        public static void Postfix(string strFile, DbWorldPlace __instance)
        {
            const string transName = "db_WorldPlace_translated";
            if (!DbTextHookSupport.TryGetTranslation(strFile, transName, out var dict))
            {
                return;
            }

            int counter = 0;
            foreach (var element in __instance.Info)
            {
                if (DbTextHookSupport.TryTranslate(dict, ref element.Comment)) counter++;
                if (DbTextHookSupport.TryTranslate(dict, ref element.Name)) counter++;
            }

            DbTextHookSupport.LogLoaded(strFile, __instance.Info.Length, counter);
        }
    }
}
