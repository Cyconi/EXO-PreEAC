﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;
using xButtonAPI.Controls;
using xButtonAPI.Controls.Grouping;
using Wrapper.PlayerWrapper;
using ConsoleLogger;
using UnityEngine;
using System.Collections;
using MelonLoader;
using UnityEngine.SceneManagement;

namespace EXO.Modules
{
    internal class Util : BaseModule
    {
        internal static ToggleButton ToggleBtn;        
        public override void OnQuickMenuInit()
        {
            var Util = new ButtonGroup(MainModule.Util, "<color=#9b0000>Utilities</color>");

            new SingleButton(Util, "Reload All Avi", "Reloads all avatars", () =>
            {
                foreach (var Player in UserUtils.GetAllPlayers())
                    PlayerWrapper.ReloadAvatar(Player);
            });
            new ToggleButton(Util, "Item ESP", "Item ESP On", "Item ESP Off", (value) =>
            {
                ESP.ItemESP = value;
                GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.HSVToRGB(0f, 1f, 1f);
                MelonCoroutines.Start(ESP.ItemHighlight());
            });
            new ToggleButton(Util, "Trigger ESP", "Trigger ESP On", "Trigger ESP Off", (value) =>
            {
                ESP.TriggerESP = value;
                GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.HSVToRGB(0f, 1f, 1f);
                MelonCoroutines.Start(ESP.TriggerHighlight());
            });
            new ToggleButton(Util, "Box Collider ESP", "Box Collider ESP On", "Box Collider ESP Off", (value) =>
            {
                ESP.BoxColESP = value;
                GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.HSVToRGB(0f, 1f, 1f);
                MelonCoroutines.Start(ESP.BoxColliderHighlight());
            });
            new ToggleButton(Util, "Player ESP", "Player ESP On", "Player ESP Off", (value) =>
            {
                ESP.CapsuleColESP = value;
                GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.HSVToRGB(0f, 1f, 1f);
                MelonCoroutines.Start(ESP.CapsuleColliderHighlight());
            });
            new ToggleButton(Util, "Udon ESP", "Udon ESP On", "Udon ESP Off", (value) =>
            {
                ESP.UdonESP = value;
                GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.HSVToRGB(0f, 1f, 1f);
                MelonCoroutines.Start(ESP.UdonHighlight());
            });
            new ToggleButton(Util, "Interactable ESP", "Interactable ESP On", "Interactable ESP Off", (value) =>
            {
                ESP.InterESP = value;
                GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.HSVToRGB(0f, 1f, 1f);
                MelonCoroutines.Start(ESP.InteractableHighlight());
            });
            new ToggleButton(Util, "Rigidbody ESP", "Rigidbody ESP On", "Rigidbody ESP Off", (value) =>
            {
                ESP.RigidbodyESP = value;
                GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.HSVToRGB(0f, 1f, 1f);
                MelonCoroutines.Start(ESP.RigidbodyHighlight());
            });
            new ToggleButton(Util, "Mesh ESP [Broken]", "Mesh ESP On", "Mesh ESP Off", (value) =>
            {
                if (value)
                {
                    {
                        foreach (VRC.Player player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
                        {
                            PlayerWrapper.PlayerMeshEsp(player, true);
                        }
                        //ESP.EspState = true;
                    }
                }
                if (!value)
                {
                    foreach (VRC.Player player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
                    {
                        PlayerWrapper.PlayerMeshEsp(player, false);
                    }
                    //ESP.EspState = false;
                }
            });
            new ToggleButton(Util, "Toggle Maker", "Make New Toggle", "Get rid Of Toggle", (value) =>
            {
                    ToggleBtn.SetActive(value);                
            });
            ToggleBtn = new ToggleButton(Util, "Toggle Button test1", "toggle buttons1", "toggle Button1", (value) =>
            {
                if (value)
                {
                    CLog.L("it worked");
                }
            });
            ToggleBtn.SetActive(false);
        }
        
        internal class UserUtils
        {
            internal static GameObject GetLocalPlayer()
            {
                foreach (GameObject gameObject in UserUtils.GetAllGameObjects())
                {
                    bool flag = gameObject.name.StartsWith("VRCPlayer[Local]");
                    if (flag)
                    {
                        return gameObject;
                    }
                }
                return new GameObject();
            }
            public static VRCPlayer CurrentUser
            {
                get
                {
                    return VRCPlayer.field_Internal_Static_VRCPlayer_0;
                }
                set
                {
                    CurrentUser = CurrentUser;
                }
            }
            internal static Player LocalDownload()
            {
                foreach (GameObject gameObject in UserUtils.GetAllGameObjects())
                {
                    bool flag = gameObject.name.StartsWith("VRCPlayer[Local]");
                    if (flag)
                    {
                        return gameObject.GetComponent<VRCPlayer>().prop_Player_0;

                    }
                }
                return new Player();
            }
            internal static Player GetPlayerTest()
            {
                foreach (GameObject gameObject in UserUtils.GetAllGameObjects())
                {
                    bool flag = !GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local").active;
                    if (flag)
                    {
                        return gameObject.GetComponent<VRCPlayer>().prop_Player_0;
                    }
                }
                return new Player();
            }
            internal static GameObject[] GetAllGameObjects()
            {
                return SceneManager.GetActiveScene().GetRootGameObjects();
            }
            internal static List<Player> GetAllPlayers()
            {
                return PlayerManager.field_Private_Static_PlayerManager_0 == null ? null : PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0.ToArray().ToList();
            }
        }
    }
}
