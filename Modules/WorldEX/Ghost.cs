using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xButtonAPI.Controls;
using xButtonAPI.Controls.Grouping;
using ConsoleLogger;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRC;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace EXO.Modules
{
    internal class Ghost : BaseModule
    {
        internal static bool G_DeityMode;
        public override void OnQuickMenuInit()
        {
            var Ghost = new CollapsibleButtonGroup(MainModule.WorldEX, "<color=#9b0000>Ghost</color>");
            new ToggleButton(Ghost, "Deity Mode", "Turn On Deity Mode", "Turn Off Diety Mode", (value) =>
            {
                G_DeityMode = value;
            });
            new SingleButton(Ghost, "Bring Key", "Bring Me a Key", () =>
            {
                GameObject flag1 = GameObject.Find("PoliceStation_A/Functions/KeySpawn/Keys/Key");
                if (flag1)
                {
                    flag1.active = true;
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag1);
                    flag1.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
                GameObject flag2 = GameObject.Find("PoliceStation_A/Functions/KeySpawn/Keys/Key (1)");
                if (flag2)
                {
                    flag2.active = true;
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag2);
                    flag2.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.2f, 0f);
                }
                GameObject flag3 = GameObject.Find("PoliceStation_A/Functions/KeySpawn/Keys/Key (2)");
                if (flag3)
                {
                    flag3.active = true;
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag3);
                    flag3.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.3f, 0f);
                }
            });
            new SingleButton(Ghost, "Start Match", "Starts The Match", () =>
            {
                GameObject flag2 = GameObject.Find("LobbyManager");
                if (flag2)
                {
                    flag2.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_ReadyStartGame");
                }
            });
            new SingleButton(Ghost, "Craft All Guns", "GUNNNNSSS", () =>
            {
                GameObject flag1 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T1-M1911");
                if (flag1)
                {
                    flag1.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag2 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T1-M1911 (1)");
                if (flag2)
                {
                    flag2.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag3 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T1-M1911 (2)");
                if (flag3)
                {
                    flag3.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag4 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T1-M1911 (3)");
                if (flag4)
                {
                    flag4.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag5 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T1-M1911 (4)");
                if (flag5)
                {
                    flag5.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag6 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T1-M1911 (5)");
                if (flag6)
                {
                    flag6.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag7 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T1-M1911 (6)");
                if (flag7)
                {
                    flag7.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag8 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T1-M1911 (7)");
                if (flag8)
                {
                    flag8.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag9 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T1-M1911 (8)");
                if (flag9)
                {
                    flag9.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag10 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T2_MP7");
                if (flag10)
                {
                    flag10.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag11 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T2_MP7 (1)");
                if (flag11)
                {
                    flag11.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag12 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T2_MP7 (2)");
                if (flag12)
                {
                    flag12.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag13 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T2_MP7 (3)");
                if (flag13)
                {
                    flag13.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag15 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T2_MP7 (4)");
                if (flag15)
                {
                    flag15.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag16 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T2_MP7 (5)");
                if (flag16)
                {
                    flag16.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag14 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T3_Vector");
                if (flag14)
                {
                    flag14.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag17 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T3_Vector (1)");
                if (flag17)
                {
                    flag17.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag18 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T3_Vector (2)");
                if (flag18)
                {
                    flag18.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag19 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T2_MP7 (6)");
                if (flag19)
                {
                    flag19.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag20 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T1-M1911 (9)");
                if (flag20)
                {
                    flag20.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag21 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T3_Vector (3)");
                if (flag21)
                {
                    flag21.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
                GameObject flag22 = GameObject.Find("PoliceStation_A/Functions/WeaponWorkShops/T2_M500 (3)");
                if (flag22)
                {
                    flag22.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
                }
            });
            new SingleButton(Ghost, "Ghost Win", "Ghost Winnnnn!", () =>
            {
                GameObject flag2 = GameObject.Find("LobbyManager");
                if (flag2)
                {
                    flag2.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_GhostWin");
                }
            });
            new SingleButton(Ghost, "Humans Win", "Humans Winnnnn!", () =>
            {
                GameObject flag2 = GameObject.Find("LobbyManager");
                if (flag2)
                {
                    flag2.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_HumanWin");
                }
            });            
            new SingleButton(Ghost, "Add Less Money", "Gives You Money", () =>
            {
                GameObject.Find("GameManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.Owner, "AltCurrency");
            });
            new SingleButton(Ghost, "Add Money", "Gives You Money", () =>
            {
                GameObject.Find("GameManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.Owner, "OnSuspiciousKill");                
            });
            new SingleButton(Ghost, "Sub Money", "Deletes Your Money", () =>
            {
                GameObject.Find("GameManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.Owner, "OnInnocentKill");                                 
            });
            new SingleButton(Ghost, "Fuck Ghosts Money", "Makes The Ghost Lose Money", () =>
            {
                GameObject.Find("GameManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.Owner, "OnGhostKill");
            });
        }
    }
}
