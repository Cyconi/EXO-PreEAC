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
using static EXO_Udon.UdonStuff;

namespace EXO.Modules
{
    internal class AmongUs : BaseModule
    {
        public override void OnQuickMenuInit()
        {
            var AmongUs = new CollapsibleButtonGroup(MainModule.WorldEX, "<color=#9b0000>Among Us</color>");
            new ToggleButton(AmongUs, "Deity Mode", "Turn On Deity Mode", "Turn Off Diety Mode", (value) =>
            {
                Murder4.M4_DeityMode = value;
            });
            new SingleButton(AmongUs, "Task EarRape", "Gah My Ears!!", () =>
            {
                SendUdonEventsWithName("CompleteTask");
            });
            new SingleButton(AmongUs, "Give All Tasks", "Dam Thats alot to do", () =>
            {
                SendUdonEventsWithName("AssignTask");
                SendUdonEventsWithName("AssignWiring");
            });
            new SingleButton(AmongUs, "Self Imposter", "Set Yourself as Imposter", () =>
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
            new SingleButton(AmongUs, "Self Crewmate", "Set Yourself as Crewmate", () =>
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
            new ToggleButton(AmongUs, "Kill Spam All", "Get Fucked Monkies", "No More Death", (value) =>
            {
                KillAllStateA = value;
                if (value) MelonLoader.MelonCoroutines.Start(KillLoopA());
            });
            new ToggleButton(AmongUs, "Kill Screen All", "No See Monkies", "ok you can see now", (value) =>
            {
                KillScreenStateA = value;
                if (value) MelonLoader.MelonCoroutines.Start(KillScreenLoopA());
            });
            new ToggleButton(AmongUs, "Skip Spam", "Skip Vote Spam", "Is it over?", (value) =>
            {
                SkipVoteState = value;
                if (value) MelonLoader.MelonCoroutines.Start(SkipVoteLoop());
            });
            new ToggleButton(AmongUs, "EarRape Spam", "Spam Task EarRape", "Is it over?", (value) =>
            {
                EarRapeState = value;
                if (value) MelonLoader.MelonCoroutines.Start(EarRapeLoop());
            });
            new ToggleButton(AmongUs, "Perm Card Swipe", "Coping", "Is it over?", (value) =>
            {
                CardSwipeState = value;
                if (value) MelonLoader.MelonCoroutines.Start(CardSwipeLoop());
            });
            new SingleButton(AmongUs, "Vote Out All", "XD Goodbye", () =>
            {
                for (int i = 0; i < 25; i++)
                {
                    GameObject flag = GameObject.Find("Player Node (" + i.ToString() + ")");
                    if (flag)
                    {
                        flag.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncVotedOut");
                    }
                }
            });
        }
        internal static bool CardSwipeState;
        internal static bool KillAllStateA;
        internal static bool KillScreenStateA;
        internal static bool SkipVoteState;
        internal static bool EarRapeState;
        internal static IEnumerator KillLoopA()
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
                    GameObject flag = GameObject.Find("Player Node (" + i.ToString() + ")");
                    if (flag)
                    {
                        flag.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncAssignM");
                    }
                }
                yield return new WaitForSeconds(0f);
                if (!KillAllStateA)
                    yield break;
            }
        }
        internal static IEnumerator KillScreenLoopA() //M4 and AmongUs
        {
            for (; ; )
            {
                GameObject KA = GameObject.Find("Game Logic");
                if (KA)
                {
                    KA.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "KillLocalPlayer");
                }
                yield return new WaitForSeconds(0.2f);
                if (!KillScreenStateA)
                    yield break;
            }
        }
        internal static IEnumerator SkipVoteLoop()
        {
            for (; ; )
            {
                GameObject Skip = GameObject.Find("Game Logic");
                if (Skip)
                {
                    Skip.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Btn_SkipVoting");
                }
                yield return new WaitForSeconds(0f);
                if (!SkipVoteState)
                    yield break;
            }
        }
        internal static IEnumerator CardSwipeLoop()
        {
            for (; ; )
            {
                GameObject.Find("Game Logic/Tasks/Task Card Swipe").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "AssignTask");
                yield return new WaitForSeconds(0.1f);
                if (!CardSwipeState)
                    yield break;
            }
        }
        internal static IEnumerator EarRapeLoop()
        {
            for (; ; )
            {                
                SendUdonEventsWithName("CompleteTask");
                yield return new WaitForSeconds(0.1f);
                if (!EarRapeState)
                    yield break;
            }
        }
    }    
}
