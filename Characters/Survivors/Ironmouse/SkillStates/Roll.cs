using EntityStates;
using IronmouseMod.Survivors.Ironmouse;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace IronmouseMod.Survivors.Ironmouse.SkillStates
{
    public class Roll : BaseSkillState
    {
        public static float duration = 1f;
        public static float initialSpeedCoefficient = 5f;
        public static float finalSpeedCoefficient = 2.5f;

        public static string dodgeSoundString = "HenryRoll";
        public static float dodgeFOV = global::EntityStates.Commando.DodgeState.dodgeFOV;

        private float rollSpeed;
        private float rollSpeedFallOff;
        private Animator animator;

        Vector3 dashVector;

        public override void OnEnter()
        {
            base.OnEnter();
            animator = GetModelAnimator();

            rollSpeed = 20;
            rollSpeedFallOff = 0.97f;

            Ray aimRay = GetAimRay();

            dashVector = inputBank.aimDirection;

            PlayAnimation("FullBody, Override", "Roll", "Roll.playbackRate", duration);
            Util.PlaySound(dodgeSoundString, gameObject);

            if (NetworkServer.active)
            {
                characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 0.5f * duration);
            }

            if (NetworkServer.active)
            {
                AddPassive();
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            characterMotor.rootMotion += dashVector * (moveSpeedStat / 2) * rollSpeed * Time.fixedDeltaTime;

            rollSpeedFallOff -= 0.02f * Time.fixedDeltaTime;
            rollSpeed = rollSpeed * rollSpeedFallOff;

            if (isAuthority && fixedAge >= duration)
            {
                outer.SetNextStateToMain();
                return;
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            characterMotor.disableAirControlUntilCollision = false;
        }
        private void AddPassive()
        {
            characterBody.AddTimedBuff(IronmouseBuffs.zoomiesBuff, 5, 5);

            if (characterBody.GetBuffCount(IronmouseBuffs.zoomiesBuff) > 0)
            {
                foreach (CharacterBody.TimedBuff timedBuff in characterBody.timedBuffs)
                {
                    if (timedBuff.buffIndex == IronmouseBuffs.zoomiesBuff.buffIndex)
                    {
                        timedBuff.timer = 5;
                    }
                }
            }
        }
    }
}