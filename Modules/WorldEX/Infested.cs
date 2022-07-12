using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using xButtonAPI.Controls;
using xButtonAPI.Controls.Grouping;
using static EXO_Udon.UdonStuff;

namespace EXO.Modules
{
    internal class Infested : BaseModule
    {
        internal static bool I_NoReload;
        public override void OnQuickMenuInit()
        {
            var Infested = new CollapsibleButtonGroup(MainModule.WorldEX, "<color=#9b0000>Infested</color>");

            new ToggleButton(Infested, "Deity Mode", "Turn On Deity Mode", "Turn Off Diety Mode", (value) =>
            {
                Ghost.G_NoReload = value;
            });
            new ToggleButton(Infested, "No Reload", "Click To Shoot And Reload Automaticaly", "Go Back To Being Basic", (value) =>
            {
                I_NoReload = value;
            });
            new SingleButton(Infested, "Start Match", "Force Starts The Match", () =>
            {
                GameObject.Find("LobbyManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_ReadyStartGame");
            });
            new SingleButton(Infested, "Kill All", "Kills Everyone", () =>
            {
                GameObject.Find("DamageSync").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "BackStab");
                GameObject.Find("DamageSync").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "BackStabDamage");
            });
            new SingleButton(Infested, "Shoot All", "Forces All Guns To Shoot Once", () =>
            {
                SendUdonEventsWithName("Local_FireOneShot");
            });
            new SingleButton(Infested, "Spin All", "Forces All Non Guns To Spin Once", () =>
            {
                SendUdonEventsWithName("Spin");
            });
            new ToggleButton(Infested, "Spam Shoot", "Spam Shoot All Weapons", "Stop", (value) =>
            {
                SpamShootState = value;
                if (value) MelonLoader.MelonCoroutines.Start(InfestedSpamShoot());
                if (!value)
                {
                    SendUdonEventsWithName("Local_EndFiring");
                    SendUdonEventsWithName("Local_StartReloading");
                    SendUdonEventsWithName("Local_FinishReloading");
                }
            });
            new ToggleButton(Infested, "Spam Traps", "Spam Arms The Traps", "Stop", (value) =>
            {
                SpamShootState = value;
                if (value) MelonLoader.MelonCoroutines.Start(TrapLoop());
                
            });
            new SingleButton(Infested, "Ghost Win", "Makes Ghost Win", () =>
            {
                GameObject.Find("LobbyManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_GhostWin");
            });
            new SingleButton(Infested, "Humans Win", "Makes Humans Win", () =>
            {
                GameObject.Find("LobbyManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_HumanWin");
            });
        }
        internal static bool SpamShootState;
        internal static bool TrapState;
        internal static IEnumerator InfestedSpamShoot()
        {
            for (; ; )
            {
                SendUdonEventsWithName("Local_FireOneShot");
                yield return new WaitForSeconds(0f);
                if (!SpamShootState)
                    yield break;
            }
        }
        internal static IEnumerator TrapLoop()
        {
            for (; ; )
            {
                SendUdonEventsWithName("ActiveTrap");
                SendUdonEventsWithName("DisarmTrap");
                yield return new WaitForSeconds(0f);
                if (!TrapState)
                    yield break;
            }
        }
    }
}
