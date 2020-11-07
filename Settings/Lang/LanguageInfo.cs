using System.Globalization;

namespace Essentials.Settings.Lang
{
    public class LanguageInfo
    {
        public LanguageInfo() { }
        public LanguageInfo(string langCode)
        {
            var lang = CultureInfo.GetCultureInfo(langCode.Replace("_","-"));
            Code = lang.Name.Replace("-", "_");
            Code = Code.Contains("_") ? (Code.Split("_")[0].ToLower() == Code.Split("_")[1].ToLower() ? Code.Split("_")[0] : Code) : Code;
            Name = lang.DisplayName;
            EnglishName = lang.EnglishName;
        }

        public string Code { get; internal set; }
        public string Name { get; internal set; }
        public string EnglishName { get; internal set; }

    }
}