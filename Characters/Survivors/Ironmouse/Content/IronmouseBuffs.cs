using RoR2;
using UnityEngine;

namespace IronmouseMod.Survivors.Ironmouse
{
    public static class IronmouseBuffs
    {
        public static BuffDef speedyBuff;
        public static BuffDef zoomiesBuff;

        public static BuffDef mouseyburnDebuff;
        public static BuffDef bubiburnDebuff;

        public static void Init(AssetBundle assetBundle)
        {
            speedyBuff = Modules.Content.CreateAndAddBuff("IronmouseSpeedyBuff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/HiddenInvincibility").iconSprite,
                Color.grey,
                false,
                false);

            zoomiesBuff = Modules.Content.CreateAndAddBuff("IronmouseZoomiesBuff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/HiddenInvincibility").iconSprite,
                Color.blue,
                true,
                false);

            mouseyburnDebuff = Modules.Content.CreateAndAddBuff("IronmouseBurnDebuff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/HiddenInvincibility").iconSprite,
                Color.magenta,
                false,
                true);

            bubiburnDebuff = Modules.Content.CreateAndAddBuff("IronmouseBubiDebuff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/HiddenInvincibility").iconSprite,
                Color.magenta,
                false,
                true);

        }
    }
}
