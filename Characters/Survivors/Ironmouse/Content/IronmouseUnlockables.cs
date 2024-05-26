using IronmouseMod.Survivors.Ironmouse.Achievements;
using RoR2;
using UnityEngine;

namespace IronmouseMod.Survivors.Ironmouse
{
    public static class IronmouseUnlockables
    {
        public static UnlockableDef characterUnlockableDef = null;
        public static UnlockableDef masterySkinUnlockableDef = null;

        public static void Init()
        {
            masterySkinUnlockableDef = Modules.Content.CreateAndAddUnlockbleDef(
                IronmouseMasteryAchievement.unlockableIdentifier,
                Modules.Tokens.GetAchievementNameToken(IronmouseMasteryAchievement.identifier),
                IronmouseSurvivor.instance.assetBundle.LoadAsset<Sprite>("texMasteryAchievement"));
        }
    }
}
