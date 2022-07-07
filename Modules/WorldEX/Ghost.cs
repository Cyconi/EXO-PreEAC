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
using System.Collections;
using static EXO_Udon.UdonStuff;

namespace EXO.Modules
{
    internal class Ghost : BaseModule
    {
        internal static bool G_DeityMode;
        internal static bool G_NoCoolDown;
        public override void OnQuickMenuInit()
        {
            var Ghost = new CollapsibleButtonGroup(MainModule.WorldEX, "<color=#9b0000>Ghost</color>");
            new ToggleButton(Ghost, "Deity Mode", "Turn On Deity Mode", "Turn Off Diety Mode", (value) =>
            {
                G_DeityMode = value;
            });
            new ToggleButton(Ghost, "No Reload", "Click To Shoot And Reload Automaticaly", "Go Back To Being Basic", (value) =>
            {
                G_NoCoolDown = value;
            });                       
            new SingleButton(Ghost, "Bring Key", "Brings 3 Keys", () =>
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
            new SingleButton(Ghost, "Start Match", "Force Starts The Match", () =>
            {
                GameObject flag2 = GameObject.Find("LobbyManager");
                if (flag2)
                {
                    flag2.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_ReadyStartGame");
                }
            });
            new SingleButton(Ghost, "Craft All Guns", "Crafts All Guns", () =>
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
            new SingleButton(Ghost, "Ghost Win", "Makes Ghost Win", () =>
            {
                GameObject flag2 = GameObject.Find("LobbyManager");
                if (flag2)
                {
                    flag2.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_GhostWin");
                }
            });
            new SingleButton(Ghost, "Humans Win", "Makes Humans Win", () =>
            {
                GameObject flag2 = GameObject.Find("LobbyManager");
                if (flag2)
                {
                    flag2.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_HumanWin");
                }
            });            
            new SingleButton(Ghost, "+30 Money All", "Gives Everyone Money", () =>
            {
                GameObject.Find("GameManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "AltCurrency");
            });
            new SingleButton(Ghost, "+ Money All", "Gives Everyone Money", () =>
            {
                GameObject.Find("GameManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "OnSuspiciousKill");                
            });
            new SingleButton(Ghost, "- Money All", "Deletes Everyones Money", () =>
            {
                GameObject.Find("GameManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "OnInnocentKill");                                 
            });
            new SingleButton(Ghost, "Fuck Ghosts Money", "Makes The Ghost Lose Money", () =>
            {
                GameObject.Find("GameManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "OnGhostKill");
            });
            new SingleButton(Ghost, "Kill All", "Kills Everyone", () =>
            {
                GameObject.Find("DamageSync").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "BackStab");
                GameObject.Find("DamageSync").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "BackStabDamage");                
            });            
            new ToggleButton(Ghost, "Infinite Ammo All", "Gives Everyone Infinite Ammo", "Go Back To Normal", (value) =>
            {
                InvinateAmmoState = value;
                if (value) MelonLoader.MelonCoroutines.Start(InvinateAmmo());
            });
            new ToggleButton(Ghost, "Spam Shoot", "Spam Shoot All Weapons", "Stop", (value) =>
            {
                SpamShootState = value;
                if (value) MelonLoader.MelonCoroutines.Start(GhostSpamShoot());
                if (!value)
                {
                    SendUdonEventsWithName("Local_StartReloading");
                    SendUdonEventsWithName("Local_FinishReloading");
                }
            });
            new SingleButton(Ghost, "One Shot", "Forces All Guns To Shoot Once", () =>
            {
                SendUdonEventsWithName("Local_FireOneShot");
            });                        
            new SingleButton(Ghost, "Gun Delete", "Makes All Guns Invisible Other Than Newly Crafted Ones", () =>
            {
                SendUdonEventsWithName("Local_SetSpecialSkin");
            });
            new SingleButton(Ghost, "playerObj ", "Local_UpdateHumanMark", () =>
            {
                GameObject.Find("GameManager/PlayerObjectList/Human/PlayerCharacterObject").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_UpdateHumanMark");
                for (int i = 0; i < 21; i++)
                {
                    GameObject flag = GameObject.Find("GameManager/PlayerObjectList/Human/PlayerCharacterObject (" + i.ToString() + ")");
                    if (flag)
                    {
                        flag.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_UpdateHumanMark");
                    }
                }
            });
            new SingleButton(Ghost, "playerObj ", "Local_ActiveHuntMark", () =>
            {
                GameObject.Find("GameManager/PlayerObjectList/Human/PlayerCharacterObject").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_ActiveHuntMark");
                for (int i = 0; i < 21; i++)
                {
                    GameObject flag = GameObject.Find("GameManager/PlayerObjectList/Human/PlayerCharacterObject (" + i.ToString() + ")");
                    if (flag)
                    {
                        flag.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_ActiveHuntMark");
                    }
                }
            });
            new SingleButton(Ghost, "playerObj 2", "Initialize", () =>
            {
                GameObject.Find("GameManager/PlayerObjectList/Human/PlayerCharacterObject").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Initialize");
                for (int i = 0; i < 21; i++)
                {
                    GameObject flag = GameObject.Find("GameManager/PlayerObjectList/Human/PlayerCharacterObject (" + i.ToString() + ")");
                    if (flag)
                    {
                        flag.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Initialize");
                    }
                }
            });
            new SingleButton(Ghost, "playerObj 3", "Local_EnableHeroBuff", () =>
            {
                GameObject.Find("GameManager/PlayerObjectList/Human/PlayerCharacterObject").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_EnableHeroBuff");
                for (int i = 0; i < 21; i++)
                {
                    GameObject flag = GameObject.Find("GameManager/PlayerObjectList/Human/PlayerCharacterObject (" + i.ToString() + ")");
                    if (flag)
                    {
                        flag.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_EnableHeroBuff");
                    }
                }
            });
            new SingleButton(Ghost, "player Spectate", "RegisterPlayer", () =>
            {
                GameObject.Find("PoliceStation(Spectate)/Players/SpectatePlayer").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "RegisterPlayer");
                for (int i = 0; i < 21; i++)
                {
                    GameObject flag = GameObject.Find("PoliceStation(Spectate)/Players/SpectatePlayer (" + i.ToString() + ")");
                    if (flag)
                    {
                        flag.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "RegisterPlayer");
                    }
                }
            });
            new SingleButton(Ghost, "player Spectate 2", "UpdateIdentity", () =>
            {
                GameObject.Find("PoliceStation(Spectate)/Players/SpectatePlayer").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "UpdateIdentity");
                for (int i = 0; i < 21; i++)
                {
                    GameObject flag = GameObject.Find("PoliceStation(Spectate)/Players/SpectatePlayer (" + i.ToString() + ")");
                    if (flag)
                    {
                        flag.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "UpdateIdentity");
                    }
                }
            });
            new SingleButton(Ghost, "player Spectate 3", "SetHero", () =>
            {
                GameObject.Find("PoliceStation(Spectate)/Players/SpectatePlayer").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SetHero");
                for (int i = 0; i < 21; i++)
                {
                    GameObject flag = GameObject.Find("PoliceStation(Spectate)/Players/SpectatePlayer (" + i.ToString() + ")");
                    if (flag)
                    {
                        flag.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SetHero");
                    }
                }
            });
        }
        internal static bool SpamShootState;        
        internal static bool InvinateAmmoState;
        internal static IEnumerator GhostSpamShoot()
        {
            for (; ; )
            {                
                SendUdonEventsWithName("Local_FireOneShot");
                yield return new WaitForSeconds(0f);
                if (!SpamShootState)
                    yield break;
            }
        }
        internal static IEnumerator InvinateAmmo()
        {
            for (; ; )
            {                                
                SendUdonEventsWithName("InitializeWeapon");
                yield return new WaitForSeconds(1f);
                if (!InvinateAmmoState)
                    yield break;
            }
        }        
    }
}
