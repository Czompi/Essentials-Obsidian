using Essentials.Plugin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Essentials.Settings.Lang
{

    public class LanguageManager
    {
        public string Code { get; }
        public string Name { get; }
        public string EnglishName { get; }
        public Language Messages { get; }

        public LanguageManager()
        {
            CultureInfo lang = null;
            var langs = LoadLanguageList();
            //lang = CultureInfo.GetCultureInfo(Globals.Configs.Config.Global.Locale);
            lang = CultureInfo.CurrentUICulture;
            if (string.IsNullOrEmpty(lang.Name) || langs.Select(x => x.Contains($"messages_{lang.Name}.json")).ToList().Count >= 1)
                lang = CultureInfo.GetCultureInfo("en");
            Code = lang.Name.Replace("-", "_");
            Name = lang.DisplayName;
            EnglishName = lang.EnglishName;
            Messages = GetLanguage(lang.Name);
        }

        #region Loading stuff
        protected Language GetLanguage(String code)
        {
            Language res = null;
            code = code.Replace("-", "_");
            if (!Globals.FileReader.FileExists(Globals.Files.LanguageFile(code)))
            {
                var lang_sel_ = LoadLanguageList().Where(x => x.Contains($"messages_{code}.json")).ToList();
                var lang_def_ = LoadLanguageList().Where(x => x.Contains($"messages.json")).ToList();
                if (lang_sel_.Count >= 1)
                {
                    try
                    {
                        var lang_sel = lang_sel_.FirstOrDefault();
                        res = LoadLanguageFromAssembly(lang_sel);
                        Globals.Logger.Log($"§7[Language]§r §a{code}§r language loaded.");
                    }
                    catch (Exception ex)
                    {
                        Globals.Logger.LogWarning($"§7[Language]§r Cannot load §e{code}§r language. Falling back to default...");
                    }
                }
                else if (lang_def_.Count >= 1)
                {
                    try
                    {
                        var lang_def = lang_def_.FirstOrDefault();
                        res = LoadLanguageFromAssembly(lang_def);
                        Globals.Logger.Log($"§7[Language]§r §adefault (en)§r language loaded.");
                    }
                    catch (Exception)
                    {
                        Globals.Logger.LogError($"§7[Language]§r Cannot load §edefault (en)§r language!");
                    }
                    var lang = LoadLanguageList().Where(x => x.Contains($"messages.json")).ToList().FirstOrDefault();
                    res = LoadLanguageFromAssembly(lang);
                }
                else
                {
                    Globals.Logger.LogError($"§7[Language]§r Cannot load §c{code}§r language!");
                    throw new MissingLanguageException(code);
                }
            }
            else
            {
                res = JsonSerializer.Deserialize<Language>(Globals.Files.LanguageFile(code));
                Globals.Logger.Log($"§7[Language]§r §a{code}§r language loaded.");
            }
            return res;
        }

        protected Language LoadLanguageFromAssembly(string entry)
        {
            if (!entry.StartsWith("Essentials."))
                entry = $"Essentials.{entry.Replace("/", ".")}";
            try
            {
                var resourceStream = Assembly.GetAssembly(typeof(EssentialsPlugin)).GetManifestResourceStream(entry);

                var sr = new StreamReader(resourceStream);
                var text = sr.ReadToEnd();
                Language language = JsonSerializer.Deserialize<Language>(text, Globals.JsonSerializerOptions);
                return language;
            }
            catch (Exception ex)
            {
                throw new Exception($"Problem with '{entry}'.", ex);
            }
        }

        protected List<string> LoadLanguageList()
        {
            try
            {
                var langs = Assembly.GetAssembly(typeof(EssentialsPlugin)).GetManifestResourceNames().ToList();
                return langs;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }
        #endregion

    }
}
