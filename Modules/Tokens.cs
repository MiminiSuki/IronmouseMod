namespace IronmouseMod.Modules
{
    internal static class Tokens
    {
        //i know this is redundant but i just couldn't get it to work otherwise
        public const string agilePrefix = "<style=cIsUtility>Agile.</style>";
        public static string agileKeyword = KeywordText("Agile", "The skill can be used while sprinting.");
        public const string heavyPrefix = "<style=cIsUtility>Heavy.</style>";
        public static string heavyKeyword = KeywordText("Heavy", "The skill deals more damage the faster you are moving.");

        //these are the unique dot debuffs, one for mouse and one for bubi
        public const string mouseyburnPrefix = "<color=#B55385>Mousey Burn</color>";
        public static string mouseyburnKeyword = KeywordText("Mousey Burn", "Apply an unique burn effect that deals some damage idk.");
        //had no ideia how to add theses things, used the Seamstress code for reference, thanks to suyoikenko
        public const string bubiburnPrefix = "<color=#B55385>Bubi Burn</color>";
        public static string bubiburnKeyword = KeywordText("Bubi Burn", "Apply an unique burn effect that deals some damage idk.");

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