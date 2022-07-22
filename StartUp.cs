using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Wrapper.WorldWrapper;

namespace EXO
{
    internal class StartUp : BaseModule
    {
        public override void OnApplicationStart()
        {
            MelonCoroutines.Start(StartUpMenuLoad());
            CLog.L("[ EXO Is Booting... ]");

            if (!Directory.Exists("EXO\\Hornet"))
            {
                Directory.CreateDirectory("EXO\\Hornet");
                CLog.L("[ Hornet Custom Buttons Have Been Configured! ]");
            }
        }
        
        private static IEnumerator StartUpMenuLoad()
        {
            while (GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)") == null)
                yield return new WaitForEndOfFrame();

            OnceMenuLoad();
            yield break;
        }        
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            WorldWrapper.Init();
        }
        internal static void OnceMenuLoad()        
        {
            GUI.Form1 ua = new GUI.Form1();
            ua.Show();
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                CLog.L("[============================================================]");
                CLog.S("");
                CLog.S("");
                CLog.S("        [-] ████╗  ███████╗██╗░░██╗░█████╗░  ████╗ [-]");
                CLog.S("        [-] ██╔═╝  ██╔════╝╚██╗██╔╝██╔══██╗  ╚═██║ [-]");
                CLog.S("        [-] ██║░░  █████╗░░░╚███╔╝░██║░░██║  ░░██║ [-]");
                CLog.S("        [-] ██║░░  ██╔══╝░░░██╔██╗░██║░░██║  ░░██║ [-]");
                CLog.S("        [-] ████╗  ███████╗██╔╝╚██╗╚█████╔╝  ████║ [-]");
                CLog.S("        [-] ╚═══╝  ╚══════╝╚═╝░░╚═╝░╚════╝░  ╚═══╝ [-]");
                CLog.S("");
                CLog.S("");
                CLog.L("[============================================================]");
                Console.WriteLine("");
                Console.WriteLine("");
                CLog.L("[========================= KeyBinds =========================]");
                CLog.L("[                                                            ]");
                CLog.L("[    2    =  Hide GUI                                        ]");
                CLog.L("[    3    =  Show GUI                                        ]");
                CLog.L("[                                                            ]");
                CLog.L("[========================= KeyBinds =========================]");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }
    }

}

