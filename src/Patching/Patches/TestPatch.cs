using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZkMenu.src.Patching.Patches
{
    [HarmonyPatch(typeof(GorillaLocomotion.GTPlayer))]
    [HarmonyPatch("Awake", MethodType.Normal)]
    internal class TestPatch
    {
        private static void Postfix(GorillaLocomotion.GTPlayer __instance)
        {
            Console.WriteLine(__instance.maxJumpSpeed);
        }
    }
}
