﻿using IronmouseMod.Survivors.Ironmouse.SkillStates;

namespace IronmouseMod.Survivors.Ironmouse
{
    public static class IronmouseStates
    {
        public static void Init()
        {
            Modules.Content.AddEntityState(typeof(Spin));

            Modules.Content.AddEntityState(typeof(Pewpew));

            Modules.Content.AddEntityState(typeof(Dash));

            Modules.Content.AddEntityState(typeof(ReadySetGo));
        }
    }
}
