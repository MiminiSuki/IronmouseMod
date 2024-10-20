using RoR2;
using UnityEngine;
using IronmouseMod.Modules;
using System;
using RoR2.Projectile;

namespace IronmouseMod.Survivors.Ironmouse
{
    public static class IronmouseAssets
    {
        // particle effects
        public static GameObject spinSwingEffect;

        // networked hit sounds
        public static NetworkSoundEventDef spinHitSoundEvent;


        internal static AssetBundle _assetBundle;

        public static void Init(AssetBundle assetBundle)
        {

            _assetBundle = assetBundle;

            spinHitSoundEvent = Content.CreateAndAddNetworkSoundEventDef("IronmouseSpinHit");

            CreateEffects();
        }

        #region effects
        private static void CreateEffects()
        {
            spinSwingEffect = _assetBundle.LoadEffect("SpinEffectPrefab", true);
        }
        #endregion effects
    }
}
