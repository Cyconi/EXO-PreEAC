using ConsoleLogger;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Networking;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using Wrapper.PlayerWrapper;
using Wrapper.WorldWrapper;
using static EXO.Modules.Util;

namespace EXO
{
    public class Patches : BaseModule
    {
        public static HarmonyLib.Harmony instance;        
        private static HarmonyMethod GetLocalPatch(string methodName)
        {
            return new HarmonyMethod(typeof(Patches).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static));
        }
        public override void OnApplicationStart()
        {
            instance = new HarmonyLib.Harmony("EXO");
            instance.Patch(typeof(UdonSync).GetMethod("UdonSyncRunProgramAsRPC"), GetLocalPatch("ForceUdon"));
        }
        private static bool ForceUdon(string __0, VRC.Player __1, UdonSync __instance)
        {
            if (EXO.Modules.Murder4.M4_GoldGun)
            {
                if (__0 == "NonPatronSkin" && __1.field_Private_APIUser_0.id == UserUtils.LocalDownload().field_Private_APIUser_0.id)
                    GameObject.Find("/Game Logic").transform.Find("Weapons/Revolver").gameObject.GetComponent<VRC.Udon.UdonBehaviour>().SendCustomNetworkEvent(0, "PatronSkin");
            }
            else if (EXO.Modules.Murder4.M4_GoldGun)
                if (__0 == "NonPatronSkin")
                    GameObject.Find("/Game Logic").transform.Find("Weapons/Revolver").gameObject.GetComponent<VRC.Udon.UdonBehaviour>().SendCustomNetworkEvent(0, "PatronSkin");

            if (EXO.Modules.Murder4.M4_NoCoolDown)
                if (__0 == "SyncDryFire" && __1.field_Private_APIUser_0.id == UserUtils.LocalDownload().field_Private_APIUser_0.id)
                    switch (__instance.gameObject.name)
                    {
                        case "Revolver":
                            GameObject.Find("Game Logic/Weapons/Revolver").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.Owner, "Fire");
                            break;
                        case "Shotgun (0)":
                            GameObject.Find("Game Logic/Weapons/Unlockables/Shotgun (0)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.Owner, "Fire");
                            break;
                        case "Luger (0)":
                            GameObject.Find("Game Logic/Weapons/Unlockables/Luger (0)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.Owner, "Fire");
                            break;
                    }
            if (EXO.Modules.Murder4.M4_DeityMode)
                if (__0 == "SyncKill")
                {
                    if (__1.field_Private_APIUser_0.displayName != UserUtils.LocalDownload().DisplayName() && Vector3.Distance(VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position, __1.transform.position) > 3.5f)
                    {
                        CLog.L($"Prevented Death From {__1.field_Private_APIUser_0.displayName}");
                    }
                    return false;
                }
                     
            if (EXO.Modules.Ghost.G_DeityMode)
            {
                if (__0.ToLower().Contains("hitdamage"))
                {
                    if (__1.field_Private_APIUser_0.displayName != UserUtils.LocalDownload().DisplayName())
                    {
                        CLog.L($"Prevented Damage From {__1.field_Private_APIUser_0.displayName}");
                    }
                    return false;
                }

                if (__0 == "BackStabDamage")
                {
                    if (__1.field_Private_APIUser_0.displayName != UserUtils.LocalDownload().DisplayName())
                    {
                        CLog.L($"Prevented Death From {__1.field_Private_APIUser_0.displayName}");                        
                    }
                    return false;
                }

                if (__0 == "BackStab")
                {
                    if (__1.field_Private_APIUser_0.displayName != UserUtils.LocalDownload().DisplayName())
                    {
                        CLog.L($"Prevented Death From {__1.field_Private_APIUser_0.displayName}");
                    }
                    return false;
                }
            }
            if (EXO.Modules.PrisonEsc.P_DeityMode)
                if (__0.ToLower().Contains("damage") && __1.field_Private_APIUser_0.id != UserUtils.LocalDownload().field_Private_APIUser_0.id)
                {
                    if (__1.field_Private_APIUser_0.displayName != UserUtils.LocalDownload().DisplayName())
                    {
                        CLog.L($"Prevented Damage From {__1.field_Private_APIUser_0.displayName}");
                    }
                    return false;
                }
            if (__0.ToLower().Contains("damage") && __1.field_Private_APIUser_0.id == UserUtils.LocalDownload().field_Private_APIUser_0.id)
            {
                if (__1.field_Private_APIUser_0.displayName == UserUtils.LocalDownload().DisplayName())
                {
                    CLog.L($"Prevented Damage From {__1.field_Private_APIUser_0.displayName}");
                }
                return false;
            }
            return true;
        }        
    }
}
