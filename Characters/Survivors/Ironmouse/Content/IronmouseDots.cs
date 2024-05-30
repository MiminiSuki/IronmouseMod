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
                interval = 0.3f,
                damageCoefficient = 1f,
                damageColorIndex = DamageColorIndex.Void,
                associatedBuff = IronmouseBuffs.mouseyburnDebuff
            });

            BubiBurn = DotAPI.RegisterDotDef(new DotController.DotDef
            {
                interval = 0.3f,
                damageCoefficient = 1f,
                damageColorIndex = DamageColorIndex.Void,
                associatedBuff = IronmouseBuffs.bubiburnDebuff
            });

            MouseyDamage = DamageAPI.ReserveDamageType();
            BubiDamage = DamageAPI.ReserveDamageType();

        }
    }
}
