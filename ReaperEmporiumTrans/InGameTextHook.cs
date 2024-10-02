using System.Collections;
using System.Diagnostics;
using HarmonyLib;

namespace MagicalAstrogy.ReaperEmporiumTrans
{
    [HarmonyPatch(typeof(CommonGameBase), nameof(CommonGameBase.GetSystemMessage))]
    public class InGameTextHook
    {
        public static bool Prefix(string sMessage, ref string __result)
        {
            const string transName = "db_InCode_translated";
            if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
            {
                if (dict.TryGetValue(sMessage, out var ele))
                {
                    __result = ele;
                    return false;
                }
            }

            return true;
        }
    }
    
    /*
    [HarmonyPatch(typeof(CommonGameBase), nameof(CommonGameBase.Awake))]
    public class InGameTextHook
    {
        public static void Postfix(CommonGameBase __instance)
        {
            var accessor = AccessTools.Field(typeof(CommonGameBase), "HstSystemMessage");
            var hs = accessor.GetValue(__instance) as Hashtable;
            if (hs == null)
            {
                Logger.Log("Failed to get hashtable");
                return;
            }

            int counter = 0;
            
            const string transName = "db_InCode_translated";
            if (TranslationDB.AllTranslation.TryGetValue(transName, out var dict))
            {
                foreach (var trans in dict)
                {
                    if (!hs.ContainsKey(trans.Key))
                    {
                        hs.Add(trans.Key, new DbSystemMessageDetail(){Format = trans.Value, IsEnable = true});
                        counter++;
                    }
                }
            }
            
            
            Logger.Log($"Loaded {transName}, translated {counter}.");
        }
    }
    */
}