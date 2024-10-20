using BepInEx;
using IronmouseMod.Survivors.Ironmouse;
using IronmouseMod.Survivors.Ironmouse.SkillStates;
using R2API;
using R2API.Utils;
using RoR2;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Networking;
//using EmotesAPI;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

//imma say this here, dont recommend using this code for reference cus i dont know what i am doing
//if you're reading this i hope you have a good day

//rename this namespace
namespace IronmouseMod
{
    //[BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.weliveinasociety.CustomEmotesAPI", BepInDependency.DependencyFlags.SoftDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]
    public class IronmousePlugin : BaseUnityPlugin
    {
        // if you do not change this, you are giving permission to deprecate the mod-
        //  please change the names to your own stuff, thanks
        //   this shouldn't even have to be said
        public const string MODUID = "com.mimi.IronmouseMod"; //i changed them please dont deprecate the mod i have a family
        public const string MODNAME = "IronmouseMod";
        public const string MODVERSION = "0.1.0"; //how does this even work? like how do people decide the numbers for version?
        //are versions just random numbers? I think the first number means a big patch, but the others idk, gonna assume its importance of patch?

        // a prefix for name tokens to prevent conflicts- please capitalize all name tokens for convention
        public const string DEVELOPER_PREFIX = "MIMI";

        public static IronmousePlugin instance;

        public static GameObject ironmouseBodyPrefab;
        public static BodyIndex ironmouseBodyIndex;

        void Awake()
        {
            instance = this;

            //easy to use logger
            Log.Init(Logger);

            // used when you want to properly set up language folders
            Modules.Language.Init();

            // character initialization
            new IronmouseSurvivor().Initialize();

            // make a content pack and add it. this has to be last
            new Modules.ContentPacks().Initialize();

            Hooks();
        }

        #region hooks
        private void Hooks()
        {
            On.RoR2.BodyCatalog.SetBodyPrefabs += BodyCatalog_SetBodyPrefabs;
            R2API.RecalculateStatsAPI.GetStatCoefficients += RecalculateStatsAPI_GetStatCoefficients;
            GlobalEventManager.onServerDamageDealt += this.GlobalEventManager_onServerDamageDealt;

            //if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.weliveinasociety.CustomEmotesAPI"))
            //{
            //    On.RoR2.SurvivorCatalog.Init += SurvivorCatalog_Init;
            //}

        }

        private void BodyCatalog_SetBodyPrefabs(On.RoR2.BodyCatalog.orig_SetBodyPrefabs orig, GameObject[] newBodyPrefabs)
        {
            orig(newBodyPrefabs);
            ironmouseBodyIndex = BodyCatalog.FindBodyIndex(ironmouseBodyPrefab);
            //Log.Warning("Deputy's body index is: " + deputyBodyIndex);
            //yes i got this part from the deputy code, shoutout to bog
        }

        private void RecalculateStatsAPI_GetStatCoefficients(CharacterBody sender, R2API.RecalculateStatsAPI.StatHookEventArgs args)
        {

            if (sender.HasBuff(IronmouseBuffs.zoomiesBuff))
            {
                args.baseMoveSpeedAdd += IronmouseStaticValues.zoomiesSpeedMultiplier * (sender.GetBuffCount(IronmouseBuffs.zoomiesBuff));
            }

            if (sender.HasBuff(IronmouseBuffs.speedyBuff))
            {
                args.baseMoveSpeedAdd += 8 * Spin.statAttackSpeed;
            }

            if (sender.HasBuff(IronmouseBuffs.readyBuff))
            {
                args.baseMoveSpeedAdd += IronmouseStaticValues.readySetGoBaseSpeed + (IronmouseStaticValues.readySetGoSpeedMultiplier * (sender.GetBuffCount(IronmouseBuffs.zoomiesBuff)));
            }

            if (sender.HasBuff(IronmouseBuffs.setBuff))
            {
                args.baseMoveSpeedAdd += IronmouseStaticValues.readySetGoBaseSpeed + (IronmouseStaticValues.readySetGoSpeedMultiplier * (sender.GetBuffCount(IronmouseBuffs.zoomiesBuff)));
            }

            if (sender.HasBuff(IronmouseBuffs.goBuff))
            {
                args.baseMoveSpeedAdd += IronmouseStaticValues.readySetGoBaseSpeed + (IronmouseStaticValues.readySetGoSpeedMultiplier * (sender.GetBuffCount(IronmouseBuffs.zoomiesBuff)));
            }
        }

        private void GlobalEventManager_onServerDamageDealt(DamageReport damageReport)
        {
            //System.Console.WriteLine("getting damage report from server");
            if (DamageAPI.HasModdedDamageType(damageReport.damageInfo, IronmouseDots.MouseyDamage))
            {
                if (!damageReport.victimBody.HasBuff(IronmouseBuffs.mouseyburnDebuff) && damageReport.attackerBody.bodyIndex == ironmouseBodyIndex)
                {
                    if (NetworkServer.active)
                    {
                        damageReport.attackerBody.AddBuff(IronmouseBuffs.zoomiesBuff);
                    }
                }

                //System.Console.WriteLine("sending information for dot");
                DotController.InflictDot(damageReport.victim.gameObject, damageReport.attacker, IronmouseDots.MouseyBurn, 3, 1, 1);
                damageReport.victimBody.AddTimedBuff(IronmouseBuffs.mouseyburnDebuff, 3);
            }
        }

        //private void SurvivorCatalog_Init(On.RoR2.SurvivorCatalog.orig_Init orig)
        //{
        //    orig();
        //
        //    foreach (var survivor in SurvivorCatalog.survivorDefs)
        //    {
        //        if (survivor.bodyPrefab.name == "IronmouseBody")
        //        {
        //            var skeleton = IronmouseAssets._assetBundle.LoadAsset<GameObject>("IronmouseEmotes");
        //            CustomEmotesAPI.ImportArmature(survivor.bodyPrefab, skeleton);
        //            skeleton.GetComponentInChildren<BoneMapper>().scale = 1f;
        //        }
        //    }
        //}

        #endregion hooks
    }
}