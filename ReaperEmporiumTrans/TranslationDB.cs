using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace MagicalAstrogy.ReaperEmporiumTrans
{
    public static class TranslationDB
    {

        public static Dictionary<string, Dictionary<string, string>> AllTranslation = null;
        
        public static void LoadAllTranslations()
        {
            var modPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var result = new Dictionary<string, Dictionary<string, string>>();

            // 获取所有 JSON 文件
            string[] files = Directory.GetFiles(modPath, "*.json");
            foreach (var file in files)
            {
                try
                {
                    // 读取 JSON 文件内容
                    string jsonContent = File.ReadAllText(file);

                    // 反序列化 JSON 字符串为 TranslationItem 结构体的列表
                    var items = JsonConvert.DeserializeObject<List<TranslationItem>>(jsonContent);
                    if (items == null)
                    {
                        continue;
                    }

                    var dst = new Dictionary<string, string>();
                    foreach (var item in items)
                    {
                        if (!dst.TryAdd(item.Original, item.Translation))
                        {
                            Console.WriteLine($"Find Duplicated Item {item.Key}, content {item.Translation}");
                            continue;
                        }
                    }

                    // 使用文件名（不包含扩展名）作为键存储翻译字典
                    var fileName = Path.GetFileNameWithoutExtension(file);
                    result.Add(fileName, dst);
                    Logger.Log($"Loaded translation file {file}.");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Error processing file {file}: {ex.Message}");
                }
            }

            AllTranslation = result;
        }


    }
}