﻿
using static EXO_Udon.UdonStuff;
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
        internal static bool M4_DeityMode;
        internal static bool M4_GoldGun;
        internal static bool M4_NoCoolDown;        
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
            new ToggleButton(Murder4, "Deity Mode", "Turn On Deity Mode", "Turn Off Diety Mode", (value) =>
            {
                M4_DeityMode = value;
            });
            new ToggleButton(Murder4, "God Gun", "OOOO SHINYYY", "Basic Bitch", (value) =>
            {
                M4_GoldGun = value;
            });
            new ToggleButton(Murder4, "No Gun CoolDown", "Turns Off Gun CoolDown Only For You", "Turns On Gun CoolDown", (value) =>
            {
                M4_NoCoolDown = value;                
            });
            new SingleButton(Murder4, "Force Start", "Force Starts The Match Countdown", () =>
            {
                GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Btn_Start");                
            });
            new SingleButton(Murder4, "Bring Luger", "Brings The Luger", () =>
            {
                CLog.L("Teleported Luger");
                GameObject flag2 = GameObject.Find("Game Logic/Weapons/Unlockables/Luger (0)");
                if (flag2)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag2);
                    flag2.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
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
                SendUdonEventsWithName("SyncOpen");
                SendUdonEventsWithName("SyncUnlockR");
                SendUdonEventsWithName("SyncOpenR");
                SendUdonEventsWithName("SyncBreakR");
            });
            new SingleButton(Murder4, "Close Everything", "Opens Everything That Can Be Closed", () =>
            {
                SendUdonEventsWithName("SyncClose");
                SendUdonEventsWithName("SyncLock");
            });
            new SingleButton(Murder4, "Kill All", "Kills Everybody", () =>
            {
                GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "KillLocalPlayer");                                               
            });
            new ToggleButton(Murder4, "Kill Spam All", "Get Fucked Monkies", "No More Death", (value) =>
            {
                KillAllStateM = value;
                if (value) MelonLoader.MelonCoroutines.Start(KillLoopM());
            });
            new ToggleButton(Murder4, "Spam Car Doors", "Spams The Car Doors", "No More Funny Sounds", (value) =>
            {
                CarDoorSpam = value;
                if (value) MelonLoader.MelonCoroutines.Start(CarDoorLoop());
            });
        }        
        internal static bool KillAllStateM;
        internal static bool CarDoorSpam;
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
        private static IEnumerator CarDoorLoop()
        {
            for (; ; )
            {
                GameObject.Find("Environment/Garage/Car/Car Door (driver)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncOpen");
                GameObject.Find("Environment/Garage/Car/Car Door (passenger)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncOpen");
                GameObject.Find("Environment/Garage/Car/Car Door (backleft)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncOpen");
                GameObject.Find("Environment/Garage/Car/Car Door (backright)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncOpen");
                yield return new WaitForSeconds(0.05f);
                GameObject.Find("Environment/Garage/Car/Car Door (driver)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncClose");
                GameObject.Find("Environment/Garage/Car/Car Door (passenger)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncClose");
                GameObject.Find("Environment/Garage/Car/Car Door (backleft)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncClose");
                GameObject.Find("Environment/Garage/Car/Car Door (backright)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncClose");
                yield return new WaitForSeconds(0.05f);
                if (!CarDoorSpam)
                    yield break;
            }
        }
    }
}