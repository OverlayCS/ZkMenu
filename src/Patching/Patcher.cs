using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZkMenu.src
{
    public class Patcher
    {
        private static Harmony instance;

        public static bool IsPatched { get; private set; }
        public const string InstanceID = PluginInfo.GUID;

        internal static void Patch()
        {
            if(!IsPatched)
            {
                if(instance == null)
                {
                    instance = new Harmony(InstanceID);
                }

                instance.PatchAll();
                IsPatched = true;
            }
        }

        internal static void Unpatch()
        {
            if (instance != null && IsPatched)
            {
                instance.UnpatchSelf();
                IsPatched = false;
            }
        }
    }
}
