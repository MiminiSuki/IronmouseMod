using RoR2;
using UnityEngine;
using R2API;

namespace IronmouseMod.Survivors.Ironmouse
{
    public static class IronmouseDots
    {
        public static DotController.DotIndex MouseyBurn;

        public static DamageAPI.ModdedDamageType MouseyDamage;

        public static DamageAPI.ModdedDamageType MouseyDashDamage;

        public static void Init()
        {
            MouseyBurn = DotAPI.RegisterDotDef(new DotController.DotDef
            {
                interval = 0.3f,
                damageCoefficient = 0.4f,
                damageColorIndex = DamageColorIndex.Void,
                associatedBuff = IronmouseBuffs.mouseyburnDebuff
            });

            MouseyDamage = DamageAPI.ReserveDamageType();

            MouseyDashDamage = DamageAPI.ReserveDamageType();

        }
    }
}
