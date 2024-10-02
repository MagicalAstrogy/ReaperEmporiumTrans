using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace MagicalAstrogy.ReaperEmporiumTrans
{
    [HarmonyPatch(typeof(DbEventInfo), nameof(DbEventInfo.Load))]
    class DbEventInfoHook
    {
        public static void Postfix(string strFile, DbEventInfo __instance)
        {
            const string transName = "events_translated";
            if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
            {
                int counter = 0;
                foreach (var dbEventInfoDetail in __instance.Info)
                {
                    if (dbEventInfoDetail.EventCmd != 0 && dbEventInfoDetail.MessageValue != "DUMMY" &&
                        !(dbEventInfoDetail.EventCmd == 21 & dbEventInfoDetail.MessageValue == "-"))
                    {
                        if (dict.TryGetValue(dbEventInfoDetail.MessageValue, out var translated))
                        {
                            dbEventInfoDetail.MessageValue = translated;
                            counter++;
                        }
                    }
                }

                Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
            }
            else
            {
                Logger.Log($"Failed to translate {strFile}, {transName} not found.");
            }
        }
        
          
        public static bool IsValidString(string str)
        {
            return !string.IsNullOrEmpty(str) && (str != "DUMMY" && str != "-");
        }

        public static bool TryTranslate(Dictionary<string, string> dict, ref string str)
        {
            if (IsValidString(str))
            {
                if (dict.TryGetValue(str, out var translated))
                {
                    str = translated;
                    return true;
                }
            }

            return false;
        }
    
        [HarmonyPatch(typeof(DbAchievementDestiny), nameof(DbAchievementDestiny.Load))]
        class DbAchievementDestinyHook
        {
            public static void Postfix(string strFile, DbAchievementDestiny __instance)
            {
                const string transName = "db_AchievementDestiny_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.CommentDst)) counter++;
                        if (TryTranslate(dict, ref element.CommentSrc)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbAchievementGuide), nameof(DbAchievementGuide.Load))]
        class DbAchievementGuideHook
        {
            public static void Postfix(string strFile, DbAchievementGuide __instance)
            {
                const string transName = "db_AchievementGuide_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbAchievementHikitugi), nameof(DbAchievementHikitugi.Load))]
        class DbAchievementHikitugiHook
        {
            public static void Postfix(string strFile, DbAchievementHikitugi __instance)
            {
                const string transName = "db_AchievementHikitugi_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbAchievementHistory), nameof(DbAchievementHistory.Load))]
        class DbAchievementHistoryHook
        {
            public static void Postfix(string strFile, DbAchievementHistory __instance)
            {
                const string transName = "db_AchievementHistory_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbAchievementTips), nameof(DbAchievementTips.Load))]
        class DbAchievementTipsHook
        {
            public static void Postfix(string strFile, DbAchievementTips __instance)
            {
                const string transName = "db_AchievementTips_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubBuild), nameof(DbClubBuild.Load))]
        class DbClubBuildHook
        {
            public static void Postfix(string strFile, DbClubBuild __instance)
            {
                const string transName = "db_ClubBuild_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.LogUseMessage)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubGoods), nameof(DbClubGoods.Load))]
        class DbClubGoodsHook
        {
            public static void Postfix(string strFile, DbClubGoods __instance)
            {
                const string transName = "db_ClubGoods_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubGoodsKb), nameof(DbClubGoodsKb.Load))]
        class DbClubGoodsKbHook
        {
            public static void Postfix(string strFile, DbClubGoodsKb __instance)
            {
                const string transName = "db_ClubGoodsKb_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubGoodsMaterial), nameof(DbClubGoodsMaterial.Load))]
        class DbClubGoodsMaterialHook
        {
            public static void Postfix(string strFile, DbClubGoodsMaterial __instance)
            {
                const string transName = "db_ClubGoodsMaterial_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubGoodsMaterialKb), nameof(DbClubGoodsMaterialKb.Load))]
        class DbClubGoodsMaterialKbHook
        {
            public static void Postfix(string strFile, DbClubGoodsMaterialKb __instance)
            {
                const string transName = "db_ClubGoodsMaterialKb_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubHRoomPlay), nameof(DbClubHRoomPlay.Load))]
        class DbClubHRoomPlayHook
        {
            public static void Postfix(string strFile, DbClubHRoomPlay __instance)
            {
                const string transName = "db_ClubHRoomPlay_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubToushiDungeon), nameof(DbClubToushiDungeon.Load))]
        class DbClubToushiDungeonHook
        {
            public static void Postfix(string strFile, DbClubToushiDungeon __instance)
            {
                const string transName = "db_ClubToushiDungeon_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubToushiDungeonHakenInfo), nameof(DbClubToushiDungeonHakenInfo.Load))]
        class DbClubToushiDungeonHakenInfoHook
        {
            public static void Postfix(string strFile, DbClubToushiDungeonHakenInfo __instance)
            {
                const string transName = "db_ClubToushiDungeonHakenInfo_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Title)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubToushiDungeonInfo), nameof(DbClubToushiDungeonInfo.Load))]
        class DbClubToushiDungeonInfoHook
        {
            public static void Postfix(string strFile, DbClubToushiDungeonInfo __instance)
            {
                const string transName = "db_ClubToushiDungeonInfo_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Memo)) counter++;
                        if (TryTranslate(dict, ref element.Title)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubToushiOrganization), nameof(DbClubToushiOrganization.Load))]
        class DbClubToushiOrganizationHook
        {
            public static void Postfix(string strFile, DbClubToushiOrganization __instance)
            {
                const string transName = "db_ClubToushiOrganization_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Name_disp)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubToushiOrganizationInfo), nameof(DbClubToushiOrganizationInfo.Load))]
        class DbClubToushiOrganizationInfoHook
        {
            public static void Postfix(string strFile, DbClubToushiOrganizationInfo __instance)
            {
                const string transName = "db_ClubToushiOrganizationInfo_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Title)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubToushiSetubi), nameof(DbClubToushiSetubi.Load))]
        class DbClubToushiSetubiHook
        {
            public static void Postfix(string strFile, DbClubToushiSetubi __instance)
            {
                const string transName = "db_ClubToushiSetubi_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubToushiSetubiInfo), nameof(DbClubToushiSetubiInfo.Load))]
        class DbClubToushiSetubiInfoHook
        {
            public static void Postfix(string strFile, DbClubToushiSetubiInfo __instance)
            {
                const string transName = "db_ClubToushiSetubiInfo_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Title)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubToushiUnitSkillLock), nameof(DbClubToushiUnitSkillLock.Load))]
        class DbClubToushiUnitSkillLockHook
        {
            public static void Postfix(string strFile, DbClubToushiUnitSkillLock __instance)
            {
                const string transName = "db_ClubToushiUnitSkillLock_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.NameHScene)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubToushiUnitSkillTreeInfo), nameof(DbClubToushiUnitSkillTreeInfo.Load))]
        class DbClubToushiUnitSkillTreeInfoHook
        {
            public static void Postfix(string strFile, DbClubToushiUnitSkillTreeInfo __instance)
            {
                const string transName = "db_ClubToushiUnitSkillTreeInfo_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Title)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbClubToushiZigyou), nameof(DbClubToushiZigyou.Load))]
        class DbClubToushiZigyouHook
        {
            public static void Postfix(string strFile, DbClubToushiZigyou __instance)
            {
                const string transName = "db_ClubToushiZigyou_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbDefineValue), nameof(DbDefineValue.Load))]
        class DbDefineValueHook
        {
            public static void Postfix(string strFile, DbDefineValue __instance)
            {
                const string transName = "db_DefineValue_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Format)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbDefineValueIndivid), nameof(DbDefineValueIndivid.Load))]
        class DbDefineValueIndividHook
        {
            public static void Postfix(string strFile, DbDefineValueIndivid __instance)
            {
                const string transName = "db_DefineValueIndivid_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbDungeon), nameof(DbDungeon.Load))]
        class DbDungeonHook
        {
            public static void Postfix(string strFile, DbDungeon __instance)
            {
                const string transName = "db_Dungeon_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Name_disp)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbEventChara), nameof(DbEventChara.Load))]
        class DbEventCharaHook
        {
            public static void Postfix(string strFile, DbEventChara __instance)
            {
                const string transName = "db_EventChara_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name_s)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbMessageClubInfo), nameof(DbMessageClubInfo.Load))]
        class DbMessageClubInfoHook
        {
            public static void Postfix(string strFile, DbMessageClubInfo __instance)
            {
                const string transName = "db_MessageClubInfo_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Message)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbMessagePartnerAccessInfo), nameof(DbMessagePartnerAccessInfo.Load))]
        class DbMessagePartnerAccessInfoHook
        {
            public static void Postfix(string strFile, DbMessagePartnerAccessInfo __instance)
            {
                const string transName = "db_MessagePartnerAccessInfo_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Message)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbMessagePartnerCommonInfo), nameof(DbMessagePartnerCommonInfo.Load))]
        class DbMessagePartnerCommonInfoHook
        {
            public static void Postfix(string strFile, DbMessagePartnerCommonInfo __instance)
            {
                const string transName = "db_MessagePartnerCommonInfo_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Message)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbMessagePartnerMission), nameof(DbMessagePartnerMission.Load))]
        class DbMessagePartnerMissionHook
        {
            public static void Postfix(string strFile, DbMessagePartnerMission __instance)
            {
                const string transName = "db_MessagePartnerMission_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbMessagePartnerMissionInfo), nameof(DbMessagePartnerMissionInfo.Load))]
        class DbMessagePartnerMissionInfoHook
        {
            public static void Postfix(string strFile, DbMessagePartnerMissionInfo __instance)
            {
                const string transName = "db_MessagePartnerMissionInfo_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Message)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbScenarioEnding), nameof(DbScenarioEnding.Load))]
        class DbScenarioEndingHook
        {
            public static void Postfix(string strFile, DbScenarioEnding __instance)
            {
                const string transName = "db_ScenarioEnding_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbScenarioEpisodeSelecter), nameof(DbScenarioEpisodeSelecter.Load))]
        class DbScenarioEpisodeSelecterHook
        {
            public static void Postfix(string strFile, DbScenarioEpisodeSelecter __instance)
            {
                const string transName = "db_ScenarioEpisodeSelecter_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Title)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        [HarmonyPatch(typeof(DbScenarioTownPlace), nameof(DbScenarioTownPlace.Load))]
        class DbScenarioTownPlaceHook
        {
            public static void Postfix(string strFile, DbScenarioTownPlace __instance)
            {
                const string transName = "db_ScenarioTownPlace_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbScenarioTownUnit), nameof(DbScenarioTownUnit.Load))]
        class DbScenarioTownUnitHook
        {
            public static void Postfix(string strFile, DbScenarioTownUnit __instance)
            {
                const string transName = "db_ScenarioTownUnit_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Group)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbSystemCommand), nameof(DbSystemCommand.Load))]
        class DbSystemCommandHook
        {
            public static void Postfix(string strFile, DbSystemCommand __instance)
            {
                const string transName = "db_SystemCommand_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Title)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbSystemHelp), nameof(DbSystemHelp.Load))]
        class DbSystemHelpHook
        {
            public static void Postfix(string strFile, DbSystemHelp __instance)
            {
                const string transName = "db_SystemHelp_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Group)) counter++;
                        if (TryTranslate(dict, ref element.Name_disp)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbSystemMessage), nameof(DbSystemMessage.Load))]
        class DbSystemMessageHook
        {
            public static void Postfix(string strFile, DbSystemMessage __instance)
            {
                const string transName = "db_SystemMessage_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Format)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaBattlerStatus), nameof(DbUnitCharaBattlerStatus.Load))]
        class DbUnitCharaBattlerStatusHook
        {
            public static void Postfix(string strFile, DbUnitCharaBattlerStatus __instance)
            {
                const string transName = "db_UnitCharaBattlerStatus_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaClub), nameof(DbUnitCharaClub.Load))]
        class DbUnitCharaClubHook
        {
            public static void Postfix(string strFile, DbUnitCharaClub __instance)
            {
                const string transName = "db_UnitCharaClub_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name_disp)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaClubClass), nameof(DbUnitCharaClubClass.Load))]
        class DbUnitCharaClubClassHook
        {
            public static void Postfix(string strFile, DbUnitCharaClubClass __instance)
            {
                const string transName = "db_UnitCharaClubClass_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name_format)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaClubClassHSkill), nameof(DbUnitCharaClubClassHSkill.Load))]
        class DbUnitCharaClubClassHSkillHook
        {
            public static void Postfix(string strFile, DbUnitCharaClubClassHSkill __instance)
            {
                const string transName = "db_UnitCharaClubClassHSkill_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaClubClassKb), nameof(DbUnitCharaClubClassKb.Load))]
        class DbUnitCharaClubClassKbHook
        {
            public static void Postfix(string strFile, DbUnitCharaClubClassKb __instance)
            {
                const string transName = "db_UnitCharaClubClassKb_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaClubKosei), nameof(DbUnitCharaClubKosei.Load))]
        class DbUnitCharaClubKoseiHook
        {
            public static void Postfix(string strFile, DbUnitCharaClubKosei __instance)
            {
                const string transName = "db_UnitCharaClubKosei_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaPartner), nameof(DbUnitCharaPartner.Load))]
        class DbUnitCharaPartnerHook
        {
            public static void Postfix(string strFile, DbUnitCharaPartner __instance)
            {
                const string transName = "db_UnitCharaPartner_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaPartnerAccessApproach), nameof(DbUnitCharaPartnerAccessApproach.Load))]
        class DbUnitCharaPartnerAccessApproachHook
        {
            public static void Postfix(string strFile, DbUnitCharaPartnerAccessApproach __instance)
            {
                const string transName = "db_UnitCharaPartnerAccessApproach_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Log_normal)) counter++;
                        if (TryTranslate(dict, ref element.Log_partner_after)) counter++;
                        if (TryTranslate(dict, ref element.Log_partner_before)) counter++;
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaPartnerAccessApproachReact), nameof(DbUnitCharaPartnerAccessApproachReact.Load))]
        class DbUnitCharaPartnerAccessApproachReactHook
        {
            public static void Postfix(string strFile, DbUnitCharaPartnerAccessApproachReact __instance)
            {
                const string transName = "db_UnitCharaPartnerAccessApproachReact_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Log_output_after)) counter++;
                        if (TryTranslate(dict, ref element.Log_output_before)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaPartnerAccessCombine), nameof(DbUnitCharaPartnerAccessCombine.Load))]
        class DbUnitCharaPartnerAccessCombineHook
        {
            public static void Postfix(string strFile, DbUnitCharaPartnerAccessCombine __instance)
            {
                const string transName = "db_UnitCharaPartnerAccessCombine_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Title)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaPartnerAccessMode), nameof(DbUnitCharaPartnerAccessMode.Load))]
        class DbUnitCharaPartnerAccessModeHook
        {
            public static void Postfix(string strFile, DbUnitCharaPartnerAccessMode __instance)
            {
                const string transName = "db_UnitCharaPartnerAccessMode_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaPartnerAccessPositionMes), nameof(DbUnitCharaPartnerAccessPositionMes.Load))]
        class DbUnitCharaPartnerAccessPositionMesHook
        {
            public static void Postfix(string strFile, DbUnitCharaPartnerAccessPositionMes __instance)
            {
                const string transName = "db_UnitCharaPartnerAccessPositionMes_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment1)) counter++;
                        if (TryTranslate(dict, ref element.Comment2)) counter++;
                        if (TryTranslate(dict, ref element.Comment3)) counter++;
                        if (TryTranslate(dict, ref element.Comment4)) counter++;
                        if (TryTranslate(dict, ref element.Comment5)) counter++;
                        if (TryTranslate(dict, ref element.Comment6)) counter++;
                        if (TryTranslate(dict, ref element.Comment7)) counter++;
                        if (TryTranslate(dict, ref element.Comment8)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaPartnerDungeonResult), nameof(DbUnitCharaPartnerDungeonResult.Load))]
        class DbUnitCharaPartnerDungeonResultHook
        {
            public static void Postfix(string strFile, DbUnitCharaPartnerDungeonResult __instance)
            {
                const string transName = "db_UnitCharaPartnerDungeonResult_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaPartnerHotelResult), nameof(DbUnitCharaPartnerHotelResult.Load))]
        class DbUnitCharaPartnerHotelResultHook
        {
            public static void Postfix(string strFile, DbUnitCharaPartnerHotelResult __instance)
            {
                const string transName = "db_UnitCharaPartnerHotelResult_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaPartnerLove), nameof(DbUnitCharaPartnerLove.Load))]
        class DbUnitCharaPartnerLoveHook
        {
            public static void Postfix(string strFile, DbUnitCharaPartnerLove __instance)
            {
                const string transName = "db_UnitCharaPartnerLove_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name_ero)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaPartnerProfile), nameof(DbUnitCharaPartnerProfile.Load))]
        class DbUnitCharaPartnerProfileHook
        {
            public static void Postfix(string strFile, DbUnitCharaPartnerProfile __instance)
            {
                const string transName = "db_UnitCharaPartnerProfile_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitCharaPartnerProfileInfo), nameof(DbUnitCharaPartnerProfileInfo.Load))]
        class DbUnitCharaPartnerProfileInfoHook
        {
            public static void Postfix(string strFile, DbUnitCharaPartnerProfileInfo __instance)
            {
                const string transName = "db_UnitCharaPartnerProfileInfo_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitEnemy), nameof(DbUnitEnemy.Load))]
        class DbUnitEnemyHook
        {
            public static void Postfix(string strFile, DbUnitEnemy __instance)
            {
                const string transName = "db_UnitEnemy_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitEnemyIndivid), nameof(DbUnitEnemyIndivid.Load))]
        class DbUnitEnemyIndividHook
        {
            public static void Postfix(string strFile, DbUnitEnemyIndivid __instance)
            {
                const string transName = "db_UnitEnemyIndivid_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name_library)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitEnemyName), nameof(DbUnitEnemyName.Load))]
        class DbUnitEnemyNameHook
        {
            public static void Postfix(string strFile, DbUnitEnemyName __instance)
            {
                const string transName = "db_UnitEnemyName_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitSkill), nameof(DbUnitSkill.Load))]
        class DbUnitSkillHook
        {
            public static void Postfix(string strFile, DbUnitSkill __instance)
            {
                const string transName = "db_UnitSkill_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment_append_normal)) counter++;
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Log_normal)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitSkillAction), nameof(DbUnitSkillAction.Load))]
        class DbUnitSkillActionHook
        {
            public static void Postfix(string strFile, DbUnitSkillAction __instance)
            {
                const string transName = "db_UnitSkillAction_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitSkillPassive), nameof(DbUnitSkillPassive.Load))]
        class DbUnitSkillPassiveHook
        {
            public static void Postfix(string strFile, DbUnitSkillPassive __instance)
            {
                const string transName = "db_UnitSkillPassive_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment_append_format)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbUnitStatusChange), nameof(DbUnitStatusChange.Load))]
        class DbUnitStatusChangeHook
        {
            public static void Postfix(string strFile, DbUnitStatusChange __instance)
            {
                const string transName = "db_UnitStatusChange_11_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Comment_append)) counter++;
                        if (TryTranslate(dict, ref element.Log_actcancel)) counter++;
                        if (TryTranslate(dict, ref element.Log_happen)) counter++;
                        if (TryTranslate(dict, ref element.Log_recovery)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
        [HarmonyPatch(typeof(DbWorldPlace), nameof(DbWorldPlace.Load))]
        class DbWorldPlaceHook
        {
            public static void Postfix(string strFile, DbWorldPlace __instance)
            {
                const string transName = "db_WorldPlace_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                        if (TryTranslate(dict, ref element.Name)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        [HarmonyPatch(typeof(DbScenarioHScene), nameof(DbScenarioHScene.Load))]
        class DbScenarioHSceneHook
        {
            public static void Postfix(string strFile, DbScenarioHScene __instance)
            {
                const string transName = "db_ScenarioHScene_translated";
                if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
                {
                    int counter = 0;
                    foreach (var element in __instance.Info)
                    {
                        if (TryTranslate(dict, ref element.Title)) counter++;
                        if (TryTranslate(dict, ref element.ParentEpisodeName)) counter++;
                        if (TryTranslate(dict, ref element.Hint)) counter++;
                        if (TryTranslate(dict, ref element.Comment)) counter++;
                    }

                    Logger.Log($"Loaded {strFile}, total strings {__instance.Info.Length}, translated {counter}.");
                }
                else
                {
                    Logger.Log($"Failed to translate {strFile}, {transName} not found.");
                }
            }
        }
        
        
    }
  
}
