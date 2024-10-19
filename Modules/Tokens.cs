namespace IronmouseMod.Modules
{
    internal static class Tokens
    {
        //i know this is redundant but i just couldn't get it to work otherwise
        public const string agilePrefix = "<style=cIsUtility>Agile.</style>";
        public static string agileKeyword = KeywordText("Agile", "The skill can be used while sprinting.");
        public const string heavyPrefix = "<style=cIsUtility>Heavy.</style>";
        public static string heavyKeyword = KeywordText("Heavy", "The skill deals more damage the faster you are moving.");

        //had no ideia how to add theses things, used the Seamstress code for reference, thanks to suyoikenko
        public const string mouseyzoomisPrefix = "<color=#B55385>Zoomies!</color>";



        public static string DamageText(string text)
        {
            return $"<style=cIsDamage>{text}</style>";
        }
        public static string DamageValueText(float value)
        {
            return $"<style=cIsDamage>{value * 100}% damage</style>";
        }
        public static string UtilityText(string text)
        {
            return $"<style=cIsUtility>{text}</style>";
        }
        public static string RedText(string text) => HealthText(text);
        public static string HealthText(string text)
        {
            return $"<style=cIsHealth>{text}</style>";
        }
        public static string KeywordText(string keyword, string sub)
        {
            return $"<style=cKeywordName>{keyword}</style><style=cSub>{sub}</style>";
        }
        public static string ScepterDescription(string desc)
        {
            return $"\n<color=#d299ff>SCEPTER: {desc}</color>";
        }

        /// <summary>
        /// gets langauge token from achievement's registered identifier
        /// </summary>
        public static string GetAchievementNameToken(string identifier)
        {
            return $"ACHIEVEMENT_{identifier.ToUpperInvariant()}_NAME";
        }
        /// <summary>
        /// gets langauge token from achievement's registered identifier
        /// </summary>
        public static string GetAchievementDescriptionToken(string identifier)
        {
            return $"ACHIEVEMENT_{identifier.ToUpperInvariant()}_DESCRIPTION";
        }
    }
}