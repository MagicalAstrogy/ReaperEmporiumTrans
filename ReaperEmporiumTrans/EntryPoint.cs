using BepInEx;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
namespace MagicalAstrogy.ReaperEmporiumTrans
{
    [BepInPlugin(GUID, "ReaperEmporiumTrans", "1.0.0")]
    public class EntryPoint : BaseUnityPlugin
    {
        public const string GUID = "com.MagicalAstrogy.ReaperEmporiumTrans";
        private void Awake()
        {
            MagicalAstrogy.ReaperEmporiumTrans.Logger.LogInstance = this.Logger;
            var harmony = new Harmony(GUID);
            
            TranslationDB.LoadAllTranslations();


            harmony.PatchAll();
        }
    }
}