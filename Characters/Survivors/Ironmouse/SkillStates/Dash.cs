using EntityStates;
using IronmouseMod.Modules.BaseStates;
using IronmouseMod.Survivors.Ironmouse;
using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace IronmouseMod.Survivors.Ironmouse.SkillStates
{
    public class Dash : BaseSkillState
    {
        public static float duration = 1f;
        public static float initialSpeedCoefficient = 5f;
        public static float finalSpeedCoefficient = 2.5f;

        public static string dashSoundString = "Play_voidman_m2_chargeUp";
        public static string dashendSoundString = "Play_voidman_shift_end";
        public static float dodgeFOV = global::EntityStates.Commando.DodgeState.dodgeFOV;
        private GameObject spinEffect;

        private float rollSpeed;
        private float rollSpeedFallOff;
        private Animator animator;


        Vector3 dashVector;

        public override void OnEnter()
        {
            base.OnEnter();
            animator = GetModelAnimator();

            rollSpeed = 40;
            rollSpeedFallOff = 0.97f;

            Ray aimRay = GetAimRay();

            dashVector = inputBank.aimDirection;

            PlayAnimation("FullBody, Override", "Dash", "Roll.playbackRate", duration);
            Util.PlaySound(dashSoundString, gameObject);

            if (NetworkServer.active)
            {
                characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, duration);
            }

            ChildLocator childLocator = GetModelChildLocator();
            if (childLocator)
            {
                spinEffect = childLocator.FindChild("SpinTrailEffect").gameObject;
                spinEffect.SetActive(true);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            characterMotor.rootMotion += dashVector * rollSpeed * Time.fixedDeltaTime;

            rollSpeedFallOff -= 0.02f * Time.fixedDeltaTime;
            rollSpeed = (rollSpeed * rollSpeedFallOff) + (moveSpeedStat / 2);

            if (isAuthority && fixedAge >= duration)
            {
                outer.SetNextStateToMain();
                return;
            }
        }
        public override void OnExit()
        {
            base.OnExit();

            spinEffect.SetActive(false);

            characterMotor.disableAirControlUntilCollision = false;
        }
    }
}