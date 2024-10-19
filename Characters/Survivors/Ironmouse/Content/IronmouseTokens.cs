using System;
using IronmouseMod.Modules;
using IronmouseMod.Survivors.Ironmouse.Achievements;

namespace IronmouseMod.Survivors.Ironmouse
{
    public static class IronmouseTokens
    {
        public static void Init()
        {
            AddIronmouseTokens();

            ////uncomment this to spit out a lanuage file with all the above tokens that people can translate
            ////make sure you set Language.usingLanguageFolder and printingEnabled to true
            //Language.PrintOutput("Henry.txt");
            ////refer to guide on how to build and distribute your mod with the proper folders
        }

        public static void AddIronmouseTokens()
        {
            string prefix = IronmouseSurvivor.IRONMOUSE_PREFIX;

            string desc = "Ironmouse is a mobile survivor, utilizing her mobile weave between enemies and defeat them gracefully.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine
             + "< ! > Ironmouse plays a hit and run play style. Get in, deal damage with Spin, get out and attack safely with Pew pew" + Environment.NewLine + Environment.NewLine
             + "< ! > Hitting enemies that already have the Zoomies! debuff won't give you extra stacks of the passive. Focus on attacking different enemies to maximize stacks." + Environment.NewLine + Environment.NewLine
             + "< ! > Ironmouse synergizes best with items that will help your burst damage, so you can maximize your Spin damage." + Environment.NewLine + Environment.NewLine
             + "< ! > Ironmouse has more jumps than the average survivor. She can jump up to 3 times!" + Environment.NewLine + Environment.NewLine;

            string outro = "..and so she left, ready for her next big stream.";
            string outroFailure = "..and so she died. lol. lmao even.";

            Language.Add(prefix + "NAME", "Ironmouse");
            Language.Add(prefix + "DESCRIPTION", desc);
            Language.Add(prefix + "SUBTITLE", "The Demon Queen");
            Language.Add(prefix + "LORE", "idk go look at a vtuber wiki or something");
            Language.Add(prefix + "OUTRO_FLAVOR", outro);
            Language.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            Language.Add(prefix + "MASTERY_SKIN_NAME", "Alternate");
            #endregion

            #region Passive
            Language.Add(prefix + "PASSIVE_NAME", "Zoomies!");
            Language.Add(prefix + "PASSIVE_DESCRIPTION", "Your attacks cause enemies to burn, taking damage over 6 seconds. Gains a stacking movement speed bonus every time you apply the effect.");
            #endregion

            #region Primary
            Language.Add(prefix + "PRIMARY_PEWPEW_NAME", "Pew Pew");
            Language.Add(prefix + "PRIMARY_PEWPEW_DESCRIPTION", Tokens.agilePrefix + $" Shoot a beam that deals <style=cIsDamage>{100f * IronmouseStaticValues.pewpewDamageCoefficient}% damage</style>.");
            #endregion

            #region Secondary
            Language.Add(prefix + "SECONDARY_SPIN_NAME", "Spin");
            Language.Add(prefix + "SECONDARY_SPIN_DESCRIPTION", Tokens.agilePrefix + " " + Tokens.heavyPrefix + $" Spin around and deal <style=cIsDamage>{100f * IronmouseStaticValues.spinDamageCoefficient}% damage</style>.");
            #endregion

            #region Utility
            Language.Add(prefix + "UTILITY_DASH_NAME", "Dash");
            Language.Add(prefix + "UTILITY_DASH_DESCRIPTION", "Dash a short distance in any direction.");
            #endregion

            #region Special
            Language.Add(prefix + "SPECIAL_SPEED_NAME", "Ready, Set... Go!");
            Language.Add(prefix + "SPECIAL_SPEED_DESCRIPTION", $"Gain an increasing movement speed buff based on the amount of " + Tokens.mouseyzoomisPrefix + ".");
            #endregion

            #region Achievements
            Language.Add(Tokens.GetAchievementNameToken(IronmouseMasteryAchievement.identifier), "Ironmouse: Mastery");
            Language.Add(Tokens.GetAchievementDescriptionToken(IronmouseMasteryAchievement.identifier), "As Ironmouse, beat the game or obliterate on Monsoon.");
            #endregion
        }
    }
}
