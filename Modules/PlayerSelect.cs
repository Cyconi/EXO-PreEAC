using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using xButtonAPI.Controls;
using xButtonAPI.Controls.Grouping;
using ConsoleLogger;
using UnityEngine.UI;
using MelonLoader;
using VRC.UI.Elements.Menus;
using VRC.UI;
using VRC;
using Wrapper.PlayerWrapper;
using System.Collections;

namespace EXO.Modules
{
    
    public class PlayerSelect : BaseModule
    {
        internal static VRC.Player Target;
        internal static VRC.Player GetSelPlayer()
        {
            if (GameObject.Find("UserInterface").GetComponentInChildren<SelectedUserMenuQM>() == null)
                return null;
            return PlayerWrapper.GetByUsrID(GameObject.Find("UserInterface").gameObject.GetComponentInChildren<SelectedUserMenuQM>().field_Private_IUser_0.prop_String_0);
        }
        private static CollapsibleButtonGroup PlayerSel;
        public override void OnApplicationStart()
        {
            MelonCoroutines.Start(StartUpMenuLoad());            
        }

        private static IEnumerator StartUpMenuLoad()
        {
            while (GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)") == null) 
                yield return new WaitForEndOfFrame();

            OnceMenuLoad();
            yield break;
        }

        internal static void OnceMenuLoad()
        {        
            Transform Parent = GameObject.Find("Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup").transform;
            
            PlayerSel = new CollapsibleButtonGroup(Parent, "<color=#9b0000>[EXO]</color>");
            CLog.L("[ Player Select Menu Has Been Assembled! ]");

            new SingleButton(PlayerSel, "Set Target", "Sets User as Target", () =>
            {
                try
                {
                    Target = GetSelPlayer();
                }
                catch
                {
                    CLog.E("Was Unable to Set Target!");
                }                
                CLog.L($"Target Set to {Target.DisplayName()}");
            });
            new SingleButton(PlayerSel, "Explode", "BOOM!", () =>
            {
                CLog.L($"{Target.DisplayName()} Went BOOM!");
                GameObject flag1 = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)/Intact");
                if (flag1)
                {
                    flag1.active = true;
                }
                GameObject flag2 = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
                if (flag2)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag2);
                    flag2.transform.position = Target.transform.position + new Vector3(0f, 0.1f, 0f);
                }
                GameObject flag3 = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
                if (flag3)
                {
                    flag3.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Explode");
                }
                GameObject flag4 = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)/Intact");
                if (flag4)
                {
                    flag4.active = true;
                }
            }, true, null);
            new SingleButton(PlayerSel, "Kill", "Kill Selected Player", () =>
            {
                CLog.L($"{Target.DisplayName()} Died");
                VRCPlayer component = Target.gameObject.GetComponent<VRCPlayer>();
                string value = component._player.ToString();
                //M4 and Amongus
                for (int i = 0; i < 24; i++)
                {
                    string text = "Player Node (" + i.ToString() + ")";
                    string text2 = "Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text";
                    bool flag = GameObject.Find(text2).GetComponent<Text>().text.Equals(value);
                    if (flag)
                    {
                        CLog.L(text);
                        UdonBehaviour component2 = GameObject.Find(text).GetComponent<UdonBehaviour>();
                        component2.SendCustomNetworkEvent(0, "SyncKill");
                    }
                }
                //PrisonEsc it dont work idk lol
                for (int i = 0; i < 34; i++)
                {
                    string text = "Scripts/Player Object Pool/PlayerData (" + i.ToString() + ")";                    
                    bool flag = GameObject.Find(text).GetComponent<Text>().text.Equals(value);
                    if (flag)
                    {
                        CLog.L(text);
                        UdonBehaviour component2 = GameObject.Find(text).GetComponent<UdonBehaviour>();
                        component2.SendCustomNetworkEvent(0, "Damage250");
                    }
                }
                //Ghost it also dont work idk lol
                for (int i = 0; i < 21; i++)
                {
                    string text = "GameManager/PlayerObjectList/Human/PlayerCharacterObject  (" + i.ToString() + ")/DamageSync";                    
                    bool flag = GameObject.Find(text).GetComponent<Text>().text.Equals(value);
                    if (flag)
                    {
                        CLog.L(text);
                        UdonBehaviour component2 = GameObject.Find(text).GetComponent<UdonBehaviour>();
                        component2.SendCustomNetworkEvent(0, "HitDamage80");
                        component2.SendCustomNetworkEvent(0, "HitDamage80");
                        component2.SendCustomNetworkEvent(0, "HitDamage80");

                    }
                }
            }, true, null);
            new ToggleButton(PlayerSel, "Explode Spam", "GOO BOOOM", "No More Boom", (value) =>
            {
                CLog.L($"{Target.DisplayName()} Is In An Endless BOOM!");
                ExplodeState = value;
                if (value) MelonLoader.MelonCoroutines.Start(ExplodeLoop());
            });
            new ToggleButton(PlayerSel, "Kill Spam", "Get Fucked Monkie", "No More Death", (value) =>
            {
                CLog.L($"{Target.DisplayName()} Is In An Endless Death Loop!");
                KillState = value;
                if (value) MelonLoader.MelonCoroutines.Start(KillLoop());
            });
            new ToggleButton(PlayerSel, "Kill Screen", "Get Fucked No See", "Ok you can see now", (value) =>
            {
                CLog.L($"{Target.DisplayName()} Can Not See Or Play The Game");
                KillScreenState = value;
                if (value) MelonLoader.MelonCoroutines.Start(KillScreenLoop());
            });
        }
        internal static bool KillState;
        internal static bool KillScreenState;
        internal static bool ExplodeState;
        internal static IEnumerator ExplodeLoop()
        {
            for (; ; )
            {
                GameObject flag1 = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)/Intact");
                if (flag1)
                {
                    flag1.active = true;
                }
                GameObject flag2 = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
                if (flag2)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag2);
                    flag2.transform.position = Target.transform.position + new Vector3(0f, 0.1f, 0f);
                }
                GameObject flag3 = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
                if (flag3)
                {
                    flag3.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Explode");
                }
                GameObject flag4 = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)/Intact");
                if (flag4)
                {
                    flag4.active = true;
                }
                yield return new WaitForSeconds(0f);
                if (!ExplodeState)
                    yield break;
            }
        }
        internal static IEnumerator KillLoop() //M4 and AmongUs
        {
            for (; ; )
            {
                VRCPlayer component = Target.gameObject.GetComponent<VRCPlayer>();
                string value = component._player.ToString();
                for (int i = 0; i < 24; i++)
                {
                    string text = "Player Node (" + i.ToString() + ")";
                    string text2 = "Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text";
                    bool flag = GameObject.Find(text2).GetComponent<Text>().text.Equals(value);
                    if (flag)
                    {                        
                        UdonBehaviour component2 = GameObject.Find(text).GetComponent<UdonBehaviour>();
                        component2.SendCustomNetworkEvent(0, "SyncAssignB");
                    }
                }                
                for (int i = 0; i < 24; i++)
                {
                    string text = "Player Node (" + i.ToString() + ")";
                    string text2 = "Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text";
                    bool flag = GameObject.Find(text2).GetComponent<Text>().text.Equals(value);
                    if (flag)
                    {                        
                        UdonBehaviour component2 = GameObject.Find(text).GetComponent<UdonBehaviour>();
                        component2.SendCustomNetworkEvent(0, "SyncKill");
                    }
                }
                yield return new WaitForSeconds(0f);
                if (!KillState)
                    yield break;
            }
        }
        internal static IEnumerator KillScreenLoop() //M4 and AmongUs
        {
            for (; ; )
            {
                VRCPlayer component = Target.gameObject.GetComponent<VRCPlayer>();
                string value = component._player.ToString();                
                for (int i = 0; i < 24; i++)
                {
                    string text = "Player Node (" + i.ToString() + ")";
                    string text2 = "Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text";
                    bool flag = GameObject.Find(text2).GetComponent<Text>().text.Equals(value);
                    if (flag)
                    {                        
                        UdonBehaviour component2 = GameObject.Find(text).GetComponent<UdonBehaviour>();
                        component2.SendCustomNetworkEvent(0, "SyncKill");
                    }
                }
                yield return new WaitForSeconds(0.2f);
                if (!KillScreenState)
                    yield break;
            }
        }
        
    }
}
