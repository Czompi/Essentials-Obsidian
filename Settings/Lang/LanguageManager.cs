using Essentials.Plugin;
using Obsidian.API;
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

    public class LanguageManager: LanguageInfo
    {

        #region Properties
        protected LanguageInfo DefaultLangInfo = new LanguageInfo("en");
        protected LanguageInfo LangInfo = null;
        public Language Messages { get; internal set; }
        #endregion

        public LanguageManager()
        {
            CultureInfo lang = null;
            var langs = LoadLanguageList();
            //lang = CultureInfo.GetCultureInfo(Globals.Configs.Config.Global.Locale);
            lang = CultureInfo.CurrentUICulture; LangInfo = new LanguageInfo(lang.Name);
            Code = LangInfo.Code;
            Name = LangInfo.Name;
            EnglishName = LangInfo.EnglishName;
            /*if (string.IsNullOrEmpty(Code) || langs.Where(x => x.Contains($"messages_{Code}.json")).ToList().Count != 1)
            {
                Code = DefaultLangInfo.Code;
                Name = DefaultLangInfo.Name;
                EnglishName = DefaultLangInfo.EnglishName;
            }*/
            Messages = GetLanguage(Code);
        }

        #region Loading stuff
        protected Language GetLanguage(String code)
        {
            Language res = null;
            code = code.Replace("-", "_");
            var lang_def_ = LoadLanguageList().Where(x => x.Contains($"messages.json")).ToList();
            if (!Globals.FileReader.FileExists(Globals.Files.LanguageFile(Code)))
            {
                var lang_sel_ = LoadLanguageList().Where(x => x.Contains($"messages_{Code}.json")).ToList();
                if (lang_sel_.Count >= 1)
                {
                    try
                    {
                        var lang_sel = lang_sel_.FirstOrDefault();
                        res = LoadLanguageFromAssembly(lang_sel);
                        Globals.Logger.Log($"§7[Language/{Code}]{ChatColor.Reset} {ChatColor.BrightGreen}{EnglishName}{ChatColor.Reset} language loaded.");
                    }
                    catch (Exception exSel)
                    {
                        Globals.Logger.LogWarning($"§7[Language/{Code}]{ChatColor.Reset} Cannot load §e{EnglishName}{ChatColor.Reset} language. Falling back to default...");
#if DEBUG || SNAPSHOT
                        Globals.Logger.LogError($"§7[Language/{Code}]{ChatColor.Reset} {ChatColor.Red}{exSel}!");
#endif
                        try
                        {
                            var lang_def = lang_def_.FirstOrDefault();
                            res = LoadLanguageFromAssembly(lang_def);
                            Globals.Logger.Log($"§7[Language/{DefaultLangInfo.EnglishName}]{ChatColor.Reset} {ChatColor.BrightGreen}{DefaultLangInfo.EnglishName}{ChatColor.Reset} language loaded.");
                        }
                        catch (Exception exDef)
                        {
                            Globals.Logger.LogError($"§7[Language/{DefaultLangInfo.EnglishName}]{ChatColor.Reset} Cannot load §e{DefaultLangInfo.EnglishName}{ChatColor.Reset} language!");
#if DEBUG || SNAPSHOT
                            Globals.Logger.LogError($"§7[Language/{DefaultLangInfo.Code}]{ChatColor.Reset} {ChatColor.Red}{exDef}!");
#endif
                        }
                    }
                }
                else if (lang_def_.Count >= 1)
                {
                    try
                    {
                        var lang_def = lang_def_.FirstOrDefault();
                        res = LoadLanguageFromAssembly(lang_def);
                        Globals.Logger.Log($"§7[Language/{DefaultLangInfo.EnglishName}]{ChatColor.Reset} {ChatColor.BrightGreen}{DefaultLangInfo.EnglishName}{ChatColor.Reset} language loaded.");
                    }
                    catch (Exception exDef)
                    {
                        Globals.Logger.LogError($"§7[Language/{DefaultLangInfo.EnglishName}]{ChatColor.Reset} Cannot load §e{DefaultLangInfo.EnglishName}{ChatColor.Reset} language!");
#if DEBUG || SNAPSHOT
                        Globals.Logger.LogError($"§7[Language/{DefaultLangInfo.Code}]{ChatColor.Reset} {ChatColor.Red}{exDef}!");
#endif
                    }
                }
                else
                {
                    Globals.Logger.LogError($"§7[Language/{Code}]{ChatColor.Reset} Cannot load {ChatColor.Red}{EnglishName}{ChatColor.Reset} language!");
                    throw new MissingLanguageException(EnglishName);
                }
            }
            else
            {
                try
                {
                    res = JsonSerializer.Deserialize<Language>(Globals.Files.LanguageFile(code));
                    Globals.Logger.Log($"§7[Language/{Code}]{ChatColor.Reset} {ChatColor.BrightGreen}{EnglishName}{ChatColor.Reset} language loaded.");
                }
                catch (Exception exSel)
                {
#if DEBUG || SNAPSHOT
                    Globals.Logger.LogError($"§7[Language/{Code}]{ChatColor.Reset} {ChatColor.Red}{exSel}!");
#endif
                    try
                    {
                        var lang_def = lang_def_.FirstOrDefault();
                        res = LoadLanguageFromAssembly(lang_def);
                        Globals.Logger.Log($"§7[Language/{DefaultLangInfo.Code}]{ChatColor.Reset} {ChatColor.BrightGreen}{DefaultLangInfo.EnglishName}{ChatColor.Reset} language loaded.");
                    }
                    catch (Exception exDef)
                    {
                        Globals.Logger.LogError($"§7[Language/{DefaultLangInfo.Code}]{ChatColor.Reset} Cannot load §e{DefaultLangInfo.EnglishName}{ChatColor.Reset} language!");
#if DEBUG || SNAPSHOT
                        Globals.Logger.LogError($"§7[Language/{DefaultLangInfo.Code}]{ChatColor.Reset} {ChatColor.Red}{exDef}!");
#endif
                    }
                }
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
            catch (Exception)
            {
                return new List<string>();
            }
        }
        #endregion

        #region TranslateMessage
        //public String TranslateMessage(string key) => TranslateMessage(key, "");
        public String TranslateMessage(string key, params object?[] format)
        {
            var message = Messages.ContainsKey(key) ? Messages[key] : key.ToLower();
            return string.Format(message, format);
        }
        #endregion

    }
}
