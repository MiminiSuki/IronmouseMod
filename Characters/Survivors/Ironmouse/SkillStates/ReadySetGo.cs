using EntityStates;
using IronmouseMod.Survivors.Ironmouse;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace IronmouseMod.Survivors.Ironmouse.SkillStates
{
    public class ReadySetGo : BaseSkillState
    {
        private float duration;

        private float setStart;
        private float goStart;

        private float readyDuration;
        private float setDuration;
        private float goDuration;

        private static bool isSet;
        private static bool isGo;
        private static bool isJoever;

        private GameObject readyEffectL;
        private GameObject readyEffectR;
        private GameObject setEffectL;
        private GameObject setEffectR;
        private GameObject goEffectL;
        private GameObject goEffectR;

        public static string readySoundString = "Play_voidman_R_activate";
        public static string setSoundString = "Play_voidman_R_activate";
        public static string goSoundString = "Play_voidman_R_pop";
        public static string speedendSoundString = "Play_voidman_shift_end";
        public override void OnEnter()
        {
            base.OnEnter();

            isSet = false;
            isGo = false;
            isJoever = false;

            duration = 8f;
            setStart = 2f;
            goStart = 4f;

            readyDuration = 8f;
            setDuration = 6f;
            goDuration = 4f;

            characterBody.AddTimedBuff(IronmouseBuffs.readyBuff, readyDuration);
            Util.PlaySound(readySoundString, gameObject);

            ChildLocator childLocator = GetModelChildLocator();
            if (childLocator)
            {
                readyEffectL = childLocator.FindChild("ReadyTrailEffectL").gameObject;
                readyEffectR = childLocator.FindChild("ReadyTrailEffectR").gameObject;
                setEffectL = childLocator.FindChild("SetTrailEffectL").gameObject;
                setEffectR = childLocator.FindChild("SetTrailEffectR").gameObject;
                goEffectL = childLocator.FindChild("GoTrailEffectL").gameObject;
                goEffectR = childLocator.FindChild("GoTrailEffectR").gameObject;

                readyEffectL.SetActive(true);
                readyEffectR.SetActive(true);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (isAuthority && fixedAge >= setStart && !isSet)
            {
                isSet = true;
                characterBody.AddTimedBuff(IronmouseBuffs.setBuff, setDuration);
                Util.PlaySound(setSoundString, gameObject);

                readyEffectL.SetActive(false);
                readyEffectR.SetActive(false);

                setEffectL.SetActive(true);
                setEffectR.SetActive(true);
            }

            if (isAuthority && fixedAge >= goStart && !isGo)
            {
                isGo = true;
                characterBody.AddTimedBuff(IronmouseBuffs.goBuff, goDuration);
                Util.PlaySound(goSoundString, gameObject);

                setEffectL.SetActive(false);
                setEffectR.SetActive(false);

                goEffectL.SetActive(true);
                goEffectR.SetActive(true);
            }

            if (!isJoever && isAuthority && fixedAge >= duration)
            {
                isJoever = true;

                outer.SetNextStateToMain();
                return;
            }
        }
        public override void OnExit()
        {
            base.OnExit();

            Util.PlaySound(speedendSoundString, gameObject);

            characterBody.ClearTimedBuffs(IronmouseBuffs.readyBuff);
            characterBody.ClearTimedBuffs(IronmouseBuffs.setBuff);
            characterBody.ClearTimedBuffs(IronmouseBuffs.goBuff);

            goEffectL.SetActive(false);
            goEffectR.SetActive(false);
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }
    }
}