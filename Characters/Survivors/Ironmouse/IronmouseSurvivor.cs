﻿using BepInEx.Configuration;
using IronmouseMod.Modules;
using IronmouseMod.Modules.Characters;
using IronmouseMod.Survivors.Ironmouse.Components;
using IronmouseMod.Survivors.Ironmouse.SkillStates;
using R2API;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IronmouseMod.Survivors.Ironmouse
{
    public class IronmouseSurvivor : SurvivorBase<IronmouseSurvivor>
    {
        //used to load the assetbundle for this character. must be unique
        public override string assetBundleName => "mouseassetbundle"; //the unity asset bundle. All the other shit is in this

        //the name of the prefab we will create. conventionally ending in "Body". must be unique
        public override string bodyName => "IronmouseBody"; //Model or what ever

        //name of the ai master for vengeance and goobo. must be unique
        public override string masterName => "IronmouseMonsterMaster"; //yeah

        //the names of the prefabs you set up in unity that we will use to build your character
        public override string modelPrefabName => "mdlIronmouse"; //in game model
        public override string displayPrefabName => "IronmouseDisplay"; //model that is used in the lobby

        public const string IRONMOUSE_PREFIX = IronmousePlugin.DEVELOPER_PREFIX + "_IRONMOUSE_";

        //used when registering your survivor's language tokens
        public override string survivorTokenPrefix => IRONMOUSE_PREFIX;

        public override BodyInfo bodyInfo => new BodyInfo
        {
            bodyName = bodyName,
            bodyNameToken = IRONMOUSE_PREFIX + "NAME",
            subtitleNameToken = IRONMOUSE_PREFIX + "SUBTITLE",

            characterPortrait = assetBundle.LoadAsset<Texture>("texIronmouseIcon"),
            bodyColor = Color.white,
            sortPosition = 100,

            crosshair = Modules.Assets.LoadCrosshair("Standard"),
            podPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/SurvivorPod"),
            // 110
            maxHealth = 110f,
            healthRegen = 1f,
            armor = 12f,

            jumpCount = 3,
        };

        // all the parts of the model. Each part has 1 material/texture, so like you can break it down into different parts
        public override CustomRendererInfo[] customRendererInfos => new CustomRendererInfo[]
        {
                new CustomRendererInfo
                {
                    childName = "IronmouseHair",
                    material = assetBundle.LoadMaterial("hairMaterial"),
                },
                new CustomRendererInfo
                {
                    childName = "IronmouseShoeBow",
                    material = assetBundle.LoadMaterial("mouthMaterial"),
                },
                new CustomRendererInfo
                {
                    childName = "IronmouseFace",
                    material = assetBundle.LoadMaterial("faceMaterial"),
                },
                new CustomRendererInfo
                {
                    childName = "IronmouseBody",
                    material = assetBundle.LoadMaterial("bodyMaterial"),
                }
        };

        public override UnlockableDef characterUnlockableDef => IronmouseUnlockables.characterUnlockableDef;

        //set in base classes
        public override AssetBundle assetBundle { get; protected set; }

        public override GameObject bodyPrefab { get; protected set; }
        public override CharacterBody prefabCharacterBody { get; protected set; }
        public override GameObject characterModelObject { get; protected set; }
        public override CharacterModel prefabCharacterModel { get; protected set; }
        public override GameObject displayPrefab { get; protected set; }

        public override void Initialize()
        {
            //uncomment if you have multiple characters
            //i do not have multiple character, thank you rob
            //ConfigEntry<bool> characterEnabled = Config.CharacterEnableConfig("Survivors", "Henry");

            //if (!characterEnabled.Value)
            //    return;

            base.Initialize();
        }

        public override void InitializeCharacter()
        {
            //need the character unlockable before you initialize the survivordef
            IronmouseUnlockables.Init();

            base.InitializeCharacter();

            IronmousePlugin.ironmouseBodyPrefab = this.bodyPrefab;

            IronmouseConfig.Init();
            IronmouseStates.Init();
            IronmouseTokens.Init();

            IronmouseDots.Init();

            IronmouseAssets.Init(assetBundle);
            IronmouseBuffs.Init(assetBundle);

            InitializeEntityStateMachines();
            InitializeSkills();
            InitializeSkins();
            InitializeCharacterMaster();

            AdditionalBodySetup();

            CharacterBody HenryBody = bodyPrefab.GetComponent<CharacterBody>();
            HenryBody.bodyFlags |= CharacterBody.BodyFlags.SprintAnyDirection;
        }

        private void AdditionalBodySetup()
        {
            AddHitboxes();
            bodyPrefab.AddComponent<IronmouseWeaponComponent>();
            //bodyPrefab.AddComponent<HuntressTrackerComopnent>();
            //anything else here
        }

        public void AddHitboxes()
        {
            //example of how to create a HitBoxGroup. see summary for more details
            //change later
            Prefabs.SetupHitBoxGroup(characterModelObject, "SpinGroup", "SpinHitbox");
        }

        public override void InitializeEntityStateMachines()
        {
            //clear existing state machines from your cloned body (probably commando)
            //omit all this if you want to just keep theirs
            Prefabs.ClearEntityStateMachines(bodyPrefab);

            //the main "Body" state machine has some special properties
            Prefabs.AddMainEntityStateMachine(bodyPrefab, "Body", typeof(EntityStates.GenericCharacterMain), typeof(EntityStates.SpawnTeleporterState));
            //if you set up a custom main characterstate, set it up here
            //don't forget to register custom entitystates in your HenryStates.cs

            Prefabs.AddEntityStateMachine(bodyPrefab, "Weapon");
            Prefabs.AddEntityStateMachine(bodyPrefab, "Weapon2");
        }

        #region skills
        public override void InitializeSkills()
        {
            //remove the genericskills from the commando body we cloned
            Skills.ClearGenericSkills(bodyPrefab);
            //add our own
            AddPassiveSkill();
            AddPrimarySkills();
            AddSecondarySkills();
            AddUtiitySkills();
            AddSpecialSkills();
        }

        //skip if you don't have a passive
        //also skip if this is your first look at skills
        private void AddPassiveSkill()
        {
            bodyPrefab.GetComponent<SkillLocator>().passiveSkill = new SkillLocator.PassiveSkill
            {
                enabled = true,
                skillNameToken = IRONMOUSE_PREFIX + "PASSIVE_NAME",
                skillDescriptionToken = IRONMOUSE_PREFIX + "PASSIVE_DESCRIPTION",
                icon = assetBundle.LoadAsset<Sprite>("texPassiveIcon"),

            };
        }

        //if this is your first look at skilldef creation, take a look at Secondary first
        private void AddPrimarySkills()
        {
            Skills.CreateGenericSkillWithSkillFamily(bodyPrefab, SkillSlot.Primary);

            //the primary skill is created using a constructor for a typical primary
            //it is also a SteppedSkillDef. Custom Skilldefs are very useful for custom behaviors related to casting a skill. see ror2's different skilldefs for reference
            SkillDef primarySkillDef1 = Skills.CreateSkillDef(new SkillDefInfo
                (
                    "IronmousePewpew",
                    IRONMOUSE_PREFIX + "PRIMARY_PEWPEW_NAME",
                    IRONMOUSE_PREFIX + "PRIMARY_PEWPEW_DESCRIPTION",
                    assetBundle.LoadAsset<Sprite>("texPewpewIcon"),
                    new EntityStates.SerializableEntityStateType(typeof(SkillStates.Pewpew)),
                    "Weapon",
                    true
                ));

            Skills.AddPrimarySkills(bodyPrefab, primarySkillDef1);
        }

        private void AddSecondarySkills()
        {
            Skills.CreateGenericSkillWithSkillFamily(bodyPrefab, SkillSlot.Secondary);

            //here is a basic skill def with all fields accounted for
            SkillDef secondarySkillDef1 = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "IronmouseSpin",
                skillNameToken = IRONMOUSE_PREFIX + "SECONDARY_SPIN_NAME",
                skillDescriptionToken = IRONMOUSE_PREFIX + "SECONDARY_SPIN_DESCRIPTION",
                keywordTokens = new string[] { 
                    Tokens.agileKeyword,
                    Tokens.heavyKeyword, },
                skillIcon = assetBundle.LoadAsset<Sprite>("texSpinIcon"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Spin)),
                activationStateMachineName = "Weapon",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseRechargeInterval = 4f,
                baseMaxStock = 1,

                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = true,
                dontAllowPastMaxStocks = false,
                mustKeyPress = false,
                beginSkillCooldownOnSkillEnd = false,

                isCombatSkill = true,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = false,
                forceSprintDuringState = false,

            });

            Skills.AddSecondarySkills(bodyPrefab, secondarySkillDef1);
        }

        private void AddUtiitySkills()
        {
            Skills.CreateGenericSkillWithSkillFamily(bodyPrefab, SkillSlot.Utility);

            //here's a skilldef of a typical movement skill.
            SkillDef utilitySkillDef1 = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "IronmouseRoll",
                skillNameToken = IRONMOUSE_PREFIX + "UTILITY_DASH_NAME",
                skillDescriptionToken = IRONMOUSE_PREFIX + "UTILITY_DASH_DESCRIPTION",
                skillIcon = assetBundle.LoadAsset<Sprite>("texDashIcon"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(Dash)),
                activationStateMachineName = "Body",
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,

                baseRechargeInterval = 4f,
                baseMaxStock = 1,

                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = true,
                dontAllowPastMaxStocks = false,
                mustKeyPress = false,
                beginSkillCooldownOnSkillEnd = false,

                isCombatSkill = false,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = false,
                forceSprintDuringState = true,
            });

            Skills.AddUtilitySkills(bodyPrefab, utilitySkillDef1);
        }

        private void AddSpecialSkills()
        {
            Skills.CreateGenericSkillWithSkillFamily(bodyPrefab, SkillSlot.Special);

            //a basic skill. some fields are omitted and will just have default values
            SkillDef specialSkillDef1 = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "IronmouseSpeed",
                skillNameToken = IRONMOUSE_PREFIX + "SPECIAL_SPEED_NAME",
                skillDescriptionToken = IRONMOUSE_PREFIX + "SPECIAL_SPEED_DESCRIPTION",
                skillIcon = assetBundle.LoadAsset<Sprite>("texSpeedIcon"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.ReadySetGo)),
                //setting this to the "weapon2" EntityStateMachine allows us to cast this skill at the same time primary, which is set to the "weapon" EntityStateMachine
                activationStateMachineName = "Weapon2",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseMaxStock = 1,
                baseRechargeInterval = 25f,

                isCombatSkill = false,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = false,
                forceSprintDuringState = false,
                mustKeyPress = false,
            });

            Skills.AddSpecialSkills(bodyPrefab, specialSkillDef1);
        }
        #endregion skills

        #region skins
        public override void InitializeSkins()
        {
            ModelSkinController skinController = prefabCharacterModel.gameObject.AddComponent<ModelSkinController>();
            ChildLocator childLocator = prefabCharacterModel.GetComponent<ChildLocator>();

            CharacterModel.RendererInfo[] defaultRendererinfos = prefabCharacterModel.baseRendererInfos;

            List<SkinDef> skins = new List<SkinDef>();

            #region DefaultSkin
            //this creates a SkinDef with all default fields
            SkinDef defaultSkin = Modules.Skins.CreateSkinDef("DEFAULT_SKIN",
                assetBundle.LoadAsset<Sprite>("texMainSkin"),
                defaultRendererinfos,
                prefabCharacterModel.gameObject);

            //these are your Mesh Replacements. The order here is based on your CustomRendererInfos from earlier
            //pass in meshes as they are named in your assetbundle
            //currently not needed as with only 1 skin they will simply take the default meshes
            //uncomment this when you have another skin
            //defaultSkin.meshReplacements = Modules.Skins.getMeshReplacements(assetBundle, defaultRendererinfos,
            //    "meshHenrySword",
            //    "meshHenryGun",
            //    "meshHenry");

            //add new skindef to our list of skindefs. this is what we'll be passing to the SkinController
            skins.Add(defaultSkin);
            #endregion

            //uncomment this when you have a mastery skin
            #region MasterySkin

            ////creating a new skindef as we did before
            //SkinDef masterySkin = Modules.Skins.CreateSkinDef(HENRY_PREFIX + "MASTERY_SKIN_NAME",
            //    assetBundle.LoadAsset<Sprite>("texMasteryAchievement"),
            //    defaultRendererinfos,
            //    prefabCharacterModel.gameObject,
            //    HenryUnlockables.masterySkinUnlockableDef);

            ////adding the mesh replacements as above. 
            ////if you don't want to replace the mesh (for example, you only want to replace the material), pass in null so the order is preserved
            //masterySkin.meshReplacements = Modules.Skins.getMeshReplacements(assetBundle, defaultRendererinfos,
            //    "meshHenrySwordAlt",
            //    null,//no gun mesh replacement. use same gun mesh
            //    "meshHenryAlt");

            ////masterySkin has a new set of RendererInfos (based on default rendererinfos)
            ////you can simply access the RendererInfos' materials and set them to the new materials for your skin.
            //masterySkin.rendererInfos[0].defaultMaterial = assetBundle.LoadMaterial("matHenryAlt");
            //masterySkin.rendererInfos[1].defaultMaterial = assetBundle.LoadMaterial("matHenryAlt");
            //masterySkin.rendererInfos[2].defaultMaterial = assetBundle.LoadMaterial("matHenryAlt");

            ////here's a barebones example of using gameobjectactivations that could probably be streamlined or rewritten entirely, truthfully, but it works
            //masterySkin.gameObjectActivations = new SkinDef.GameObjectActivation[]
            //{
            //    new SkinDef.GameObjectActivation
            //    {
            //        gameObject = childLocator.FindChildGameObject("GunModel"),
            //        shouldActivate = false,
            //    }
            //};
            ////simply find an object on your child locator you want to activate/deactivate and set if you want to activate/deacitvate it with this skin

            //skins.Add(masterySkin);

            #endregion

            skinController.skins = skins.ToArray();
        }
        #endregion skins

        //Character Master is what governs the AI of your character when it is not controlled by a player (artifact of vengeance, goobo)
        public override void InitializeCharacterMaster()
        {
            //you must only do one of these. adding duplicate masters breaks the game.

            //if you're lazy or prototyping you can simply copy the AI of a different character to be used
            //Modules.Prefabs.CloneDopplegangerMaster(bodyPrefab, masterName, "Merc");

            //how to set up AI in code
            //IronmouseAI.Init(bodyPrefab, masterName);

            //how to load a master set up in unity, can be an empty gameobject with just AISkillDriver components
            assetBundle.LoadMaster(bodyPrefab, masterName);
        }
    }
}