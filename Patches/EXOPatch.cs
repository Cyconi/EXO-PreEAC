using ConsoleLogger;
using HarmonyLib;
using System;
using System.Reflection;

namespace EXO.Patches
{
    internal class EXOPatch
    {
        public new static HarmonyLib.Harmony Harmony { get; set; }
        public static HarmonyLib.Harmony Instance = new HarmonyLib.Harmony("Trinity");

        public static HarmonyMethod GetLocalPatch(Type type, string methodName)
        {
            return new HarmonyMethod(type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic));
        }
        public static void InitPatches()
        {
            try
            {
                JoinLeavePatch.Init();
            }
            catch (Exception ERR){
                CLog.E(ERR);
            } 
        }
    }
}
