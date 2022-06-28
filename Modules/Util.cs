using System;
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
                foreach (var Player in GetAllPlayers())
                    PlayerWrapper.ReloadAvatar(Player);
            });
            new ToggleButton(Util, "ESP [Not Working]", "ESP On", "ESP Off", (value) =>
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
        internal static List<Player> GetAllPlayers()
        {
            return PlayerManager.field_Private_Static_PlayerManager_0 == null ? null : PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0.ToArray().ToList();
        }                        
    }
}
