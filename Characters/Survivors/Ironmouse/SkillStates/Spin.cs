using IronmouseMod.Modules.BaseStates;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using R2API;

namespace IronmouseMod.Survivors.Ironmouse.SkillStates
{
    public class Spin : BaseMeleeAttack
    {
        public static float statAttackSpeed;
        private GameObject spinEffect;
        public override void OnEnter()
        {
            hitboxGroupName = "SpinGroup";
            damageType = DamageType.Generic;
            damageCoefficient = IronmouseStaticValues.spinDamageCoefficient;
            procCoefficient = 1f;
            pushForce = 300f;
            bonusForce = Vector3.zero;
            baseDuration = 1.2f;
            duration = baseDuration;

            statAttackSpeed = attackSpeedStat;

            ApplyModdedDamageType(overlapAttack);

            //0-1 multiplier of baseduration, used to time when the hitbox is out (usually based on the run time of the animation)
            //for example, if attackStartPercentTime is 0.5, the attack will start hitting halfway through the ability. if baseduration is 3 seconds, the attack will start happening at 1.5 seconds
            attackStartPercentTime = 0f;
            attackEndPercentTime = 0.9f;

            //this is the point at which the attack can be interrupted by itself, continuing a combo
            earlyExitPercentTime = 0.6f;

            hitStopDuration = 0.012f;
            attackRecoil = 0.5f;
            hitHopVelocity = 15f;

            swingSoundString = "Play_halcyonite_skill3_loop";
            hitSoundString = "Play_voidman_m2_small_impact";
            muzzleString = "SpinMuzzle";
            playbackRateParam = "Spin.playbackRate";
            swingEffectPrefab = IronmouseAssets.spinSwingEffect;
            hitEffectPrefab = Modules.Assets.voidFiendBeamImpact;

            impactSound = IronmouseAssets.spinHitSoundEvent.index;

            if (NetworkServer.active)
            {
                characterBody.AddTimedBuff(IronmouseBuffs.speedyBuff, baseDuration);
            }
           
            if (characterMotor && !characterMotor.isGrounded)
            {
                 SmallHop(characterMotor, 12);
            }

            //ChildLocator childLocator = GetModelChildLocator();
            //if (childLocator)
            //{
            //    spinEffect = childLocator.FindChild("SpinTrailEffect").gameObject;
            //    spinEffect.SetActive(true);
            //}

            base.OnEnter();
        }

        protected void ApplyModdedDamageType(OverlapAttack attack)
        {
            //System.Console.WriteLine("applying modded damage");
            attack.AddModdedDamageType(IronmouseDots.MouseyDamage);
        }

        protected override void PlayAttackAnimation()
        {
            PlayCrossfade("FullBody, Override", "Spin", playbackRateParam, duration, 0.1f * duration);
        }

        protected override void PlaySwingEffect()
        {
            base.PlaySwingEffect();
        }

        protected override void OnHitEnemyAuthority()
        {
            base.OnHitEnemyAuthority();
        }

        public override void OnExit()
        {
            Util.PlaySound("Stop_halcyonite_skill3_loop", base.gameObject);
            //spinEffect.SetActive(false);
            base.OnExit();
        }

    }
}