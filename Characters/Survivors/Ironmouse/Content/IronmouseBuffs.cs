using RoR2;
using UnityEngine;
using R2API;

namespace IronmouseMod.Survivors.Ironmouse
{
    public static class IronmouseBuffs
    {
        public static BuffDef speedyBuff;
        public static BuffDef zoomiesBuff;

        public static BuffDef mouseyburnDebuff;

        public static BuffDef readyBuff;
        public static BuffDef setBuff;
        public static BuffDef goBuff;

        public static void Init(AssetBundle assetBundle)
        {
            speedyBuff = Modules.Content.CreateAndAddBuff("IronmouseSpeedyBuff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/whipboost").iconSprite,
                Color.grey,
                false,
                false);

            zoomiesBuff = Modules.Content.CreateAndAddBuff("IronmouseZoomiesBuff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/onfire").iconSprite,
                Color.magenta,
                true,
                false);

            mouseyburnDebuff = Modules.Content.CreateAndAddBuff("IronmouseBurnDebuff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/onfire").iconSprite,
                Color.magenta,
                false,
                true);

            readyBuff = Modules.Content.CreateAndAddBuff("IronmouseSpeedyBuff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/whipboost").iconSprite,
                Color.red,
                false,
                false);

            setBuff = Modules.Content.CreateAndAddBuff("IronmouseSpeedyBuff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/whipboost").iconSprite,
                Color.yellow,
                false,
                false);

            goBuff = Modules.Content.CreateAndAddBuff("IronmouseSpeedyBuff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/whipboost").iconSprite,
                Color.green,
                false,
                false);
        }
    }
}
