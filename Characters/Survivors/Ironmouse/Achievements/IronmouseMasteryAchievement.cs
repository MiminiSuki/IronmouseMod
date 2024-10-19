using RoR2;
using IronmouseMod.Modules.Achievements;

namespace IronmouseMod.Survivors.Ironmouse.Achievements
{
    //automatically creates language tokens "ACHIEVMENT_{identifier.ToUpper()}_NAME" and "ACHIEVMENT_{identifier.ToUpper()}_DESCRIPTION" 
    [RegisterAchievement(identifier, unlockableIdentifier, null, 10, null)]
    public class IronmouseMasteryAchievement : BaseMasteryAchievement
    {
        public const string identifier = IronmouseSurvivor.IRONMOUSE_PREFIX + "masteryAchievement";
        public const string unlockableIdentifier = IronmouseSurvivor.IRONMOUSE_PREFIX + "masteryUnlockable";

        public override string RequiredCharacterBody => IronmouseSurvivor.instance.bodyName;

        //difficulty coeff 3 is monsoon. 3.5 is typhoon for grandmastery skins
        public override float RequiredDifficultyCoefficient => 3;
    }
}