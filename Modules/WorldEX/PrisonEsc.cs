using xButtonAPI.Controls;
using xButtonAPI.Controls.Grouping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using xButtonAPI;
using xButtonAPI.Misc;
using xButtonAPI.Pages;
using UnityEngine.UI;
using VRC.SDKBase;
using ConsoleLogger;
using MelonLoader;
using VRC;
using Object = UnityEngine.Object;
using System.Collections;
using Wrapper.WorldWrapper;
using System.Diagnostics;

namespace EXO.Modules
{
    internal class PrisonEsc : BaseModule
    {
        internal static bool GoldLooper;
        public override void OnQuickMenuInit()
        {
            var Prison = new CollapsibleButtonGroup(MainModule.WorldEX, "<color=#9b0000>Prison Escape</color>");

            new ToggleButton(Prison, "Gold Guns", "Make all guns Gold", "Disable", (value) =>
            {
                GoldLooper = value;
                if (value)
                    MelonCoroutines.Start(GoldLoop());
            }).SetToggleState(false, true);
            new SingleButton(Prison, "Start Game", "Force Start The Game", () =>
            {
                GameObject.Find("Scripts/Game Manager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "StartGameCountdown");
            });
            new SingleButton(Prison, "Kill All", "Kills Everyone", () =>
            {
                GameObject.Find("Scripts/Player Object Pool/PlayerData").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Damage250");
                for (int i = 0; i < 34; i++)
                {
                    GameObject Kill = GameObject.Find("Scripts/Player Object Pool/PlayerData (" + i.ToString() + ")");
                    if (Kill)
                    {
                        Kill.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Damage250");
                    }
                }                                
            });            
            new SingleButton(Prison, "Bring All Misc Items", "Brings All Misc Items", () =>
            {
                CLog.L("Teleported Ducks");
                GameObject Obj = GameObject.Find("Items/Misc Items/Rubber Duck Spawn");
                System.Collections.Generic.List<GameObject> Ducks = new System.Collections.Generic.List<GameObject>
                {
                    GameObject.Find("Items/Misc Items/Basketball").gameObject,
                    GameObject.Find("Items/Misc Items/Basketball Spawn").gameObject,
                    GameObject.Find("Items/Misc Items/Soap").gameObject,
                    GameObject.Find("Items/Misc Items/Soap Spawn").gameObject,
                    GameObject.Find("Items/Misc Items/Fork").gameObject,
                    GameObject.Find("Items/Misc Items/Fork Spawn").gameObject,                    
                    GameObject.Find("Items/Misc Items/Rubber Duck Spawn").gameObject,
                    GameObject.Find("Items/Misc Items/Rubber Duck").gameObject,
                    GameObject.Find("Items/Misc Items/Rubber Duck/rubberduck").gameObject,
                    GameObject.Find("Items/Misc Items/Food Trays/FoodTray_Full Pickup").gameObject,
                    GameObject.Find("Items/Misc Items/Food Trays/FoodTray Full Spawn").gameObject,
                    GameObject.Find("Items/Misc Items/Food Trays/FoodTray_Full Pickup (1)").gameObject,
                    GameObject.Find("Items/Misc Items/Food Trays/FoodTray Full Spawn (1)").gameObject,
                    GameObject.Find("Items/Misc Items/Food Trays/FoodTray Empty Spawn").gameObject,
                    GameObject.Find("Items/Misc Items/Food Trays/FoodTray_Empty Pickup").gameObject,
                };
                foreach (GameObject gameObject in Ducks)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject);
                    gameObject.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
            });
            new SingleButton(Prison, "Bring M4", "Brings A M4", () =>
            {
                CLog.L("Teleported M4A1");
                GameObject Obj = GameObject.Find("Items/Static Guns/M4A1");
                if (Obj)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj);
                    Obj.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
            });
            new SingleButton(Prison, "Bring Sniper", "Brings A Sniper", () =>
            {
                CLog.L("Teleported Sniper");
                GameObject Obj = GameObject.Find("Items/Static Guns/Sniper");
                if (Obj)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj);
                    Obj.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
            });
            new SingleButton(Prison, "Bring Shotgun", "Brings A Shotgun", () =>
            {
                CLog.L("Teleported Shotgun");
                GameObject Obj = GameObject.Find("Items/Static Guns/Shotgun");
                if (Obj)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj);
                    Obj.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
            });
            new SingleButton(Prison, "Bring SMG", "Brings A SMG", () =>
            {
                CLog.L("Teleported SMG");
                GameObject Obj = GameObject.Find("Items/Static Guns/SMG");
                if (Obj)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj);
                    Obj.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
            });
            new SingleButton(Prison, "Bring Magnum", "Brings A Magnum", () =>
            {
                CLog.L("Teleported Magnum");
                GameObject Obj = GameObject.Find("Items/Static Guns/Magnum");
                if (Obj)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj);
                    Obj.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
            });
            new SingleButton(Prison, "Bring Revolver", "Brings A Revolver", () =>
            {
                CLog.L("Teleported Revolver");
                GameObject Obj = GameObject.Find("Items/Random Guns/Revolver");
                if (Obj)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj);
                    Obj.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
            });
            new SingleButton(Prison, "Bring Pistol", "Brings A Pistol", () =>
            {
                CLog.L("Teleported Shotgun");
                GameObject Obj = GameObject.Find("Items/Static Guns/Pistol");
                if (Obj)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj);
                    Obj.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
            });
            new SingleButton(Prison, "Bring Knife", "Brings A Knife", () =>
            {
                CLog.L("Teleported Knife");
                GameObject Obj = GameObject.Find("Items/Knives/Knife");
                if (Obj)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj);
                    Obj.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
            });
            new SingleButton(Prison, "Bring Keycards", "Bring All Keycard", () =>
            {
                for (int i = 0; i < 32; i++)
                {
                    GameObject KeyCards = GameObject.Find("Items/Keycards/Keycard (" + i.ToString() + ")");
                    if (KeyCards)
                    {                       
                        Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, KeyCards);
                        KeyCards.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                    }
                }
                
            });
            new SingleButton(Prison, "Bring All Guns", "Bring All Guns", () =>
            {
                CLog.L("GUNNNNNNNSSSS");
                GameObject Obj = GameObject.Find("Items/Static Guns/Pistol");
                if (Obj)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj);
                    Obj.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
                for (int i = 0; i < 12; i++)
                {
                    GameObject Pistol = GameObject.Find("Items/Static Guns/Pistol (" + i.ToString() + ")");
                    if (Pistol)
                    {
                        Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Pistol);
                        Pistol.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                    }
                }
                GameObject Obj1 = GameObject.Find("Items/Static Guns/M4A1");
                if (Obj1)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj);
                    Obj1.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
                for (int i = 0; i < 4; i++)
                {
                    GameObject M4A1 = GameObject.Find("Items/Static Guns/M4A1 (" + i.ToString() + ")");
                    if (M4A1)
                    {
                        Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, M4A1);
                        M4A1.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                    }
                }
                GameObject Obj2 = GameObject.Find("Items/Static Guns/Shotgun");
                if (Obj2)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj2);
                    Obj2.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
                for (int i = 0; i < 4; i++)
                {
                    GameObject Shotgun = GameObject.Find("Items/Static Guns/Shotgun (" + i.ToString() + ")");
                    if (Shotgun)
                    {
                        Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Shotgun);
                        Shotgun.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                    }
                }
                GameObject Obj3 = GameObject.Find("Items/Static Guns/SMG");
                if (Obj3)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj3);
                    Obj3.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
                for (int i = 0; i < 4; i++)
                {
                    GameObject SMG = GameObject.Find("Items/Static Guns/SMG (" + i.ToString() + ")");
                    if (SMG)
                    {
                        Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, SMG);
                        SMG.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                    }
                }
                GameObject Obj4 = GameObject.Find("Items/Static Guns/Sniper");
                if (Obj4)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj4);
                    Obj4.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
                GameObject Obj5 = GameObject.Find("Items/Static Guns/Sniper (1)");
                if (Obj5)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj5);
                    Obj5.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
                GameObject Obj6 = GameObject.Find("Items/Static Guns/Magnum");
                if (Obj6)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj6);
                    Obj6.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
                GameObject Obj7 = GameObject.Find("Items/Random Guns/Revolver");
                if (Obj7)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj7);
                    Obj7.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }



            });
        }
        internal static IEnumerator GoldLoop()
        {
            List<GameObject> Objs = new List<GameObject>();
            for (int g = 0; g < WorldWrapper.udonBehaviours.Length; g++)
                foreach (string name in WorldWrapper.udonBehaviours[g]._eventTable.Keys)
                    if (name == "EnablePatronEffects" && !Objs.Contains(WorldWrapper.udonBehaviours[g].gameObject))
                        Objs.Add(WorldWrapper.udonBehaviours[g].gameObject);
            for (; ; )
            {
                if (!GoldLooper)
                {
                    foreach (var Obj in Objs)
                        Obj.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "DisablePatronEffects");
                    yield break;
                }
                foreach (var Obj in Objs)
                    Obj.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "EnablePatronEffects");
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
