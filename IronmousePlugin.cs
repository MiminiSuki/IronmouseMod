using BepInEx;
using IronmouseMod.Survivors.Ironmouse;
using R2API.Utils;
using RoR2;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

//imma say this here, dont recommend using this code for reference cus i dont know what i am doing
//if you're reading this i hope you have a good day

//rename this namespace
namespace IronmouseMod
{
    //[BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.SoftDependency)]
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
        }
    }
}
