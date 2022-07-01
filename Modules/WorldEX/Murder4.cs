using ConsoleLogger;
using DruUdonStuff;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using xButtonAPI.Controls;
using xButtonAPI.Controls.Grouping;
using VRC.SDKBase;
using VRC.UI.Elements.Menus;
using VRC.UI;
using VRC;
using VRC.Networking;
using static EXO.Modules.Util;
using Wrapper.PlayerWrapper;
using Wrapper.WorldWrapper;

namespace EXO.Modules
{
    internal class Murder4 : BaseModule
    {
        private static bool DoorCol;
        private static bool DeityMode;
        private static bool GoldGun;
        private static bool NoCoolDown;        
        public static void RunFirst()
        {
            var Murder4 = new CollapsibleButtonGroup(MainModule.WorldEX, "<color=#9b0000>Murder 4</color>");            
            new ToggleButton(Murder4, "Door Colliders", "Toggle On Door Colliders", "Toggle Off Door Colliders", (value) =>
            {
                if (value)
                {
                    DoorCol = true;
                    MelonCoroutines.Start(DoorColliders());
                }
                if (!value)
                {
                    DoorCol = false;
                    MelonCoroutines.Start(DoorColliders());
                }
            }).SetToggleState(true);            
            new SingleButton(Murder4, "Force Start", "Force Starts The Match Countdown", () =>
            {
                GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Btn_Start");                
            });
            new SingleButton(Murder4, "Self Murderer", "Set Yourself as Murderer", () =>
            {
                VRCPlayer component = VRCPlayer.field_Internal_Static_VRCPlayer_0.gameObject.GetComponent<VRCPlayer>();
                string value = component._player.ToString();
                for (int i = 0; i < 24; i++)
                {
                    string YourNode = "Player Node (" + i.ToString() + ")";
                    string Path = "Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text";
                    bool val = GameObject.Find(Path).GetComponent<Text>().text.Equals(value);
                    if (val)
                    {
                        MelonLogger.Msg(YourNode);
                        UdonBehaviour component2 = GameObject.Find(YourNode).GetComponent<UdonBehaviour>();
                        component2.SendCustomNetworkEvent(0, "SyncAssignM");
                    }
                }
            });
            new SingleButton(Murder4, "Self Bystander", "Set Yourself as Bystander", () =>
            {
                VRCPlayer component = VRCPlayer.field_Internal_Static_VRCPlayer_0.gameObject.GetComponent<VRCPlayer>();
                string value = component._player.ToString();
                for (int i = 0; i < 24; i++)
                {
                    string YourNode = "Player Node (" + i.ToString() + ")";
                    string Path = "Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text";
                    bool val = GameObject.Find(Path).GetComponent<Text>().text.Equals(value);
                    if (val)
                    {
                        MelonLogger.Msg(YourNode);
                        UdonBehaviour component2 = GameObject.Find(YourNode).GetComponent<UdonBehaviour>();
                        component2.SendCustomNetworkEvent(0, "SyncAssignB");
                    }
                }
            });
            new SingleButton(Murder4, "Open Everything", "Opens Everything That Can Be Opened", () =>
            {
                UdonStuff.SendUdonEventsWithName("SyncOpen");
                UdonStuff.SendUdonEventsWithName("SyncUnlockR");
                UdonStuff.SendUdonEventsWithName("SyncOpenR");
                UdonStuff.SendUdonEventsWithName("SyncBreakR");
            });
            new SingleButton(Murder4, "Close Everything", "Opens Everything That Can Be Closed", () =>
            {
                UdonStuff.SendUdonEventsWithName("SyncClose");
                UdonStuff.SendUdonEventsWithName("SyncLock");
            });
            new SingleButton(Murder4, "Kill All", "Kills Everybody", () =>
            {
                GameObject KA = GameObject.Find("Game Logic");
                if (KA)
                {
                    KA.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "KillLocalPlayer");
                }
            });
            new ToggleButton(Murder4, "Kill Spam All", "Get Fucked Monkies", "No More Death", (value) =>
            {
                KillAllStateM = value;
                if (value) MelonLoader.MelonCoroutines.Start(KillLoopM());
            });            
        }        
        internal static bool KillAllStateM;
        internal static IEnumerator KillLoopM()
        {
            for (; ; )
            {
                GameObject KA = GameObject.Find("Game Logic");
                if (KA)
                {
                    KA.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "KillLocalPlayer");
                }
                for (int i = 0; i < 25; i++)
                {
                    GameObject AM = GameObject.Find("Player Node (" + i.ToString() + ")");
                    if (AM)
                    {
                        AM.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncAssignM");
                    }
                }
                yield return new WaitForSeconds(0f);
                if (!KillAllStateM)
                    yield break;
            }
        }
        //Door Colliders (ty Edward)
        private static IEnumerator DoorColliders()
        {
            foreach (var Doors in Resources.FindObjectsOfTypeAll<BoxCollider>())
            {
                if (Doors.gameObject.name.Contains("Closed collision geo"))
                {
                    Doors.GetComponent<BoxCollider>().enabled = DoorCol;
                }
                yield return null;
            }
            yield return null;
        }        

        private static bool ForceUdon(string __0, VRC.Player __1, UdonSync __instance)
        {
            if (GoldGun)
            {
                if (__0 == "NonPatronSkin" && __1.field_Private_APIUser_0.id == UserUtils.LocalDownload().field_Private_APIUser_0.id)
                    GameObject.Find("/Game Logic").transform.Find("Weapons/Revolver").gameObject.GetComponent<VRC.Udon.UdonBehaviour>().SendCustomNetworkEvent(0, "PatronSkin");
            }
            else if (GoldGun)
                if (__0 == "NonPatronSkin")
                    GameObject.Find("/Game Logic").transform.Find("Weapons/Revolver").gameObject.GetComponent<VRC.Udon.UdonBehaviour>().SendCustomNetworkEvent(0, "PatronSkin");

            if (NoCoolDown)
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

            if (DeityMode)
                if (__0 == "SyncKill")
                {
                    if (__1.field_Private_APIUser_0.displayName != UserUtils.LocalDownload().DisplayName() && Vector3.Distance(VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position, __1.transform.position) > 3.5f)
                    {
                        CLog.L($"Prevented Death From {__1.field_Private_APIUser_0.displayName}");
                    }
                    return false;
                }
            return true;
        }        
    }
}