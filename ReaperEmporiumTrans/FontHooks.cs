using System.IO;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Utage;

namespace MagicalAstrogy.ReaperEmporiumTrans
{
    public static class FontRes
    {
        public static UnityEngine.Font FntYuruka = null;
        public static UnityEngine.Font FntNewRodinB = null;
        public static UnityEngine.Font FntNewRodinEB = null;
        public static UnityEngine.Font FntLXGW = null;

        public static UnityEngine.Font LoadFontFromAsset(string fileName)
        {
            var modPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var allAssets = AssetBundle.LoadFromFile(Path.Join(modPath, fileName));
            
            foreach (var asset in allAssets.LoadAllAssets())
            {
                Logger.Log($"Found type {asset} {asset.GetType()}");
                if (asset is UnityEngine.Font font)
                {
                    return font;
                }
            }

            return null;
        }
        
        public static void InitFont()
        {

            FntYuruka = LoadFontFromAsset("allseto");
            FntNewRodinEB = LoadFontFromAsset("hanshb");
            FntNewRodinB = LoadFontFromAsset("hansb");
            FntLXGW = LoadFontFromAsset("lxgw");
            if (FntYuruka != null)
            {
                
                Logger.Log($"Initialized Font {FntYuruka.name} {string.Join(",",FntYuruka.fontNames)}");
            }
            else
            {
                Logger.Log("Failed to Init Font");
            }
        }
    }
    /*
    [HarmonyPatch(typeof(UnityEngine.UI.Text), nameof(UguiNovelText.text),MethodType.Setter)]
    public class FontHooks
    {
        //TT_Yuruka-UB
        public static void Postfix(UnityEngine.UI.Text __instance)
        {
            string fontName = __instance?.font?.name;
            if (fontName is "TT_Yuruka-UB")
            {
                if (FontRes.FntYuruka == null) FontRes.InitFont();
                if (FontRes.FntYuruka != null)
                {
                    __instance.font = FontRes.FntYuruka;
                }
            }
        }
    }
    */

    [HarmonyPatch(typeof(UguiNovelText), "Awake")]
    public class FontHooksAwake
    {
        //TT_Yuruka-UB
        public static void Postfix(UnityEngine.UI.Text __instance)
        {
            string fontName = __instance?.font?.name;
            switch (fontName)
            {
                case "TT_Yuruka-UB":
                {
                    if (FontRes.FntYuruka == null) FontRes.InitFont();
                    if (FontRes.FntLXGW != null)
                    {
                        __instance.font = FontRes.FntLXGW;
                        __instance.fontStyle = FontStyle.Bold;
                    }

                    break;
                }
                case "FOT-HummingStd-B":
                {
                    if (FontRes.FntYuruka == null) FontRes.InitFont();
                    if (FontRes.FntYuruka != null)
                    {
                        __instance.font = FontRes.FntYuruka;
                        //__instance.fontStyle = FontStyle.Bold;
                    }

                    break;
                }
                case "FOT-KurokaneStd-EB":
                {
                    if (FontRes.FntYuruka == null) FontRes.InitFont();
                    if (FontRes.FntLXGW != null)
                    {
                        __instance.font = FontRes.FntLXGW;
                        //__instance.fontStyle = FontStyle.Bold;
                    }

                    break;
                }
                case "FOT-NewRodinPro-EB":
                {
                    if (FontRes.FntYuruka == null) FontRes.InitFont();
                    if (FontRes.FntNewRodinEB != null)
                    {
                        __instance.font = FontRes.FntNewRodinEB;
                    }

                    break;
                }
                case "FOT-NewRodinPro-B":
                {
                    if (FontRes.FntYuruka == null) FontRes.InitFont();
                    if (FontRes.FntNewRodinB != null)
                    {
                        __instance.font = FontRes.FntNewRodinB;
                    }

                    break;
                }
                case "LXGWMarkerGothic-Regular":
                case "SarasaUiJ-Bold":
                case "SarasaUiJ-SemiBold":
                case "SourceHanSansSC-Bold":
                case "SourceHanSansSC-Heavy":
                    break;
                default:
                {
                    if (!string.IsNullOrEmpty(fontName))
                    {
                        Logger.Log($"Unknown Font {fontName}");
                    }

                    break;
                }
            }
        }
    }
}