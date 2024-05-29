using RoR2;
using UnityEngine;
using R2API;

namespace IronmouseMod.Survivors.Ironmouse
{
    public static class IronmouseDots
    {
        public static DotController.DotIndex MouseyBurn;
        public static DotController.DotIndex BubiBurn;
        public static DamageAPI.ModdedDamageType MouseyDamage;
        public static DamageAPI.ModdedDamageType BubiDamage;
        public static void Init()
        {
            MouseyBurn = DotAPI.RegisterDotDef(new DotController.DotDef
            {
                damageCoefficient = 10f,
                damageColorIndex = DamageColorIndex.SuperBleed,
                associatedBuff = IronmouseBuffs.mouseyburnDebuff
            });

            BubiBurn = DotAPI.RegisterDotDef(new DotController.DotDef
            {
                damageCoefficient = 10f,
                damageColorIndex = DamageColorIndex.Void,
                associatedBuff = IronmouseBuffs.bubiburnDebuff
            });

            MouseyDamage = DamageAPI.ReserveDamageType();
            BubiDamage = DamageAPI.ReserveDamageType();

        }
    }
}
