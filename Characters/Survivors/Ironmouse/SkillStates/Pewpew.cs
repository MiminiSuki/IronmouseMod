﻿using EntityStates;
using IronmouseMod.Survivors.Ironmouse;
using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace IronmouseMod.Survivors.Ironmouse.SkillStates
{
    //literally just the shoot skill from henry
    //but cooler
    public class Pewpew : BaseSkillState
    {
        public static float damageCoefficient = IronmouseStaticValues.pewpewDamageCoefficient;
        public static float procCoefficient = 1f;
        public static float baseDuration = 1f;
        public static float firePercentTime = 0.0f;
        public static float force = 800f;
        public static float recoil = 3f;
        public static float range = 999f;
        public static GameObject tracerEffectPrefab = Modules.Assets.voidFiendBeamTracer;
        
        private BulletAttack bulletAttack;

        private float duration;
        private float fireTime;
        private bool hasFired;
        private string muzzleString;

        public override void OnEnter()
        {
            base.OnEnter();
            duration = baseDuration / attackSpeedStat;
            fireTime = firePercentTime * duration;
            characterBody.SetAimTimer(2f);
            muzzleString = "Muzzle"; //this is telling the game where to place effects

            PlayAnimation("Arms, Override", "PewPew", "PewPew.playbackRate", 1.8f);
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (fixedAge >= fireTime)
            {
                Fire();
            }

            if (fixedAge >= duration && isAuthority)
            {
                outer.SetNextStateToMain();
                return;
            }
        }

        private void Fire()
        {
            if (!hasFired)
            {
                hasFired = true;

                characterBody.AddSpreadBloom(1.5f);
                EffectManager.SimpleMuzzleFlash(Modules.Assets.voidFiendBeamMuzzleEffect, gameObject, muzzleString, false);
                //Util.PlaySound(EntityStates.VoidJailer.Weapon.ChargeFire.attackSoundEffect, gameObject);
                Util.PlaySound("Play_voidman_m1_shoot", gameObject);

                if (isAuthority)
                {
                    Ray aimRay = GetAimRay();
                    AddRecoil(-1f * recoil, -2f * recoil, -0.5f * recoil, 0.5f * recoil);

                    bulletAttack = new BulletAttack
                    {
                            bulletCount = 1,
                            aimVector = aimRay.direction,
                            origin = aimRay.origin,
                            damage = damageCoefficient * damageStat,
                            damageColorIndex = DamageColorIndex.Default,
                            damageType = DamageType.Generic,
                            falloffModel = BulletAttack.FalloffModel.None,
                            maxDistance = range,
                            force = force,
                            hitMask = LayerIndex.CommonMasks.bullet,
                            minSpread = 0f,
                            maxSpread = 0f,
                            isCrit = RollCrit(),
                            owner = gameObject,
                            muzzleName = muzzleString,
                            smartCollision = true,
                            procChainMask = default,
                            procCoefficient = procCoefficient,
                            radius = 0.75f,
                            sniper = false,
                            stopperMask = LayerIndex.CommonMasks.bullet,
                            weapon = null,
                            tracerEffectPrefab = tracerEffectPrefab,
                            spreadPitchScale = 0f,
                            spreadYawScale = 0f,
                            queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                            hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FirePistol2.hitEffectPrefab,
                    };

                    bulletAttack.AddModdedDamageType(IronmouseDots.MouseyDamage);

                    bulletAttack.Fire();
                }
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }
    }
}