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

            string desc = "Ironmouse is a skilled fighter who makes use of a wide arsenal of weaponry to take down his foes.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine
             + "< ! > Sword is a good all-rounder while Boxing Gloves are better for laying a beatdown on more powerful foes." + Environment.NewLine + Environment.NewLine
             + "< ! > Pistol is a powerful anti air, with its low cooldown and high damage." + Environment.NewLine + Environment.NewLine
             + "< ! > Roll has a lingering armor buff that helps to use it aggressively." + Environment.NewLine + Environment.NewLine
             + "< ! > Bomb can be used to wipe crowds with ease." + Environment.NewLine + Environment.NewLine;

            string outro = "..and so she left, the end fuck you.";
            string outroFailure = "..and so she died, lol. lmao.";

            Language.Add(prefix + "NAME", "Ironmouse");
            Language.Add(prefix + "DESCRIPTION", desc);
            Language.Add(prefix + "SUBTITLE", "The Chosen One");
            Language.Add(prefix + "LORE", "sample lore");
            Language.Add(prefix + "OUTRO_FLAVOR", outro);
            Language.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            Language.Add(prefix + "MASTERY_SKIN_NAME", "Alternate");
            #endregion

            #region Passive
            Language.Add(prefix + "PASSIVE_NAME", "Zoomies!");
            Language.Add(prefix + "PASSIVE_DESCRIPTION", "Gains a movement speed bonus every time you use a skill");
            #endregion

            #region Primary
            Language.Add(prefix + "PRIMARY_PEWPEW_NAME", "Pew Pew!");
            Language.Add(prefix + "PRIMARY_PEWPEW_DESCRIPTION", Tokens.agilePrefix + $"Shoot a beam that deals <style=cIsDamage>{100f * IronmouseStaticValues.spinDamageCoefficient}% damage</style>.");
            #endregion

            #region Secondary
            Language.Add(prefix + "SECONDARY_GUN_NAME", "Spin!");
            Language.Add(prefix + "SECONDARY_GUN_DESCRIPTION", Tokens.agilePrefix + Tokens.heavyPrefix + $"Spin around and deal <style=cIsDamage>{100f * IronmouseStaticValues.spinDamageCoefficient}% damage</style>. Hitting an enemy with this skill applies " + Tokens.mouseyburnPrefix + ". Using the skill also speeds you up a bit!");
            #endregion

            #region Utility
            Language.Add(prefix + "UTILITY_ROLL_NAME", "Roll");
            Language.Add(prefix + "UTILITY_ROLL_DESCRIPTION", "Roll a short distance, gaining <style=cIsUtility>300 armor</style>. <style=cIsUtility>You cannot be hit during the roll.</style>");
            #endregion

            #region Special
            Language.Add(prefix + "SPECIAL_BOMB_NAME", "Bomb");
            Language.Add(prefix + "SPECIAL_BOMB_DESCRIPTION", $"Throw a bomb for <style=cIsDamage>{100f * IronmouseStaticValues.bombDamageCoefficient}% damage</style>.");
            #endregion

            #region Achievements
            Language.Add(Tokens.GetAchievementNameToken(IronmouseMasteryAchievement.identifier), "Ironmouse: Mastery");
            Language.Add(Tokens.GetAchievementDescriptionToken(IronmouseMasteryAchievement.identifier), "As Ironmouse, beat the game or obliterate on Monsoon.");
            #endregion
        }
    }
}
