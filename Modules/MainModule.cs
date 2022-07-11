using xButtonAPI.Controls;
using xButtonAPI.Controls.Grouping;
using xButtonAPI.Pages;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using EXO.Hornet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using xButtonAPI.Misc;
using Wrapper.WorldWrapper;

namespace EXO
{
    public partial class MainModule : BaseModule
    {
        
        public static ButtonGroup MainMenu;
        public static MenuPage WorldEX;
        public static MenuPage Movement;
        public static MenuPage WayPoint;
        public static MenuPage Util;
        public static MenuPage Hornet;
        public override void OnQuickMenuInit()
        {
            CLog.L("[ Loading Menus... ]");            
            var Page = new MenuPage("<color=#9b0000>EXO</color>", "<color=#9b0000>[EXO]</color>", true);
            new Tab(Page, "<color=#9b0000>EXO</color>", FromBase(IconEXO));            
            //Main Menu Button Group
            MainMenu = new ButtonGroup(Page, "<color=#9b0000>EXO</color>");

            //MenuPages
            WorldEX = new MenuPage("<color=#9b0000>World Exploits</color>", "<color=#9b0000>[EXO World Exploits]</color>");
            Movement = new MenuPage("<color=#9b0000>Movement</color>", "<color=#9b0000>[EXO Movement]</color>");
            Util = new MenuPage("<color=#9b0000>Utilities</color>", "<color=#9b0000>[EXO Utilities]</color>");
            WayPoint = new MenuPage("<color=#9b0000>WayPoints</color>", "<color=#9b0000>[EXO WayPoints]</color>");
            Hornet = new MenuPage("<color=#9b0000>Hornet</color>", "<color=#9b0000>[EXO Hornet]</color>");

            //WC top buttons
            HalfButton.SingleHalfButton(Page.page.gameObject, "<color=#9b0000>Quit</color>", "Closes You Game", delegate 
            {
                Process.GetCurrentProcess().Kill();
            }, new Vector3(136.764f, 439.54f, 0));
            HalfButton.SingleHalfButton(Page.page.gameObject, "<color=#9b0000>Restart</color>", "Restarts Your Game", delegate 
            {
                Restart(true);
            }, new Vector3(357.9432f, 439.54f, 0));

            //MainButtons
            new SingleButton(MainMenu, "<color=#9b0000>World Exploits</color>", "Opens World Exploits Menu", () =>
            {
                WorldEX.OpenMenu();
            });            
            new SingleButton(MainMenu, "<color=#9b0000>Movement</color>", "Opens Movment Menu", () =>
            {
                Movement.OpenMenu();
            });
            new SingleButton(MainMenu, "<color=#9b0000>Utility</color>", "Open Utilities", () =>
            {
                Util.OpenMenu();
            });
            new SingleButton(MainMenu, "<color=#9b0000>WayPoints</color>", "Opens WayPoint Menu", () => 
            {
                WayPoint.OpenMenu();
            });
            new SingleButton(MainMenu, "<color=#9b0000>Hornet</color>", "Opens Hornet Custom Button Menu", () =>
            {
                HornetFileManager.CreateButtons();
                Hornet.OpenMenu();
            });
            EXO.Modules.WorldExploits.RunFirst();
            EXO.Modules.Murder4.RunFirst();
        }
        internal static void Restart(bool Rejoin = false, bool WithArgs = true)
        {
            MelonLoader.MelonCoroutines.Start(RestartRun(Rejoin, WithArgs));
        }
        public static IEnumerator RestartRun(bool Rejoin, bool WithArgs)
        {
            string Args = "";
            List<string> ts = new List<string>();
            ts = Environment.GetCommandLineArgs().ToList();
            string Path = Environment.GetCommandLineArgs().ToList().First(); // Grab the Path
            ts.Remove(Path); // Remove From Args
            foreach (var Arg in ts) // Grab all and Format Right
                if (!Arg.Contains("vrchat:"))
                    Args = Args + $" {Arg}";

            bool Wait;
            if (Rejoin)
            {
                Wait = true;
                xButtonAPI.xButtonAPI.GetQuickMenuInstance().ShowConfirmDialog("<color=#9b0000>EXO</color>", "<color=#9b0000>Would You Like To Rejoin The World?</color>", () =>
                {
                    Rejoin = true;
                    Wait = false;
                }, () =>
                {
                    Rejoin = false;
                    Wait = false;
                }, false, "<color=#9b0000>Yes</color>", "<color=#9b0000>No</color>");
                while (Wait)
                    yield return new WaitForSeconds(0.3f);
            }
            Process.Start(Directory.GetCurrentDirectory() + "\\VRChat.exe", (WithArgs ? Args : "") + (Rejoin ? $" vrchat://launch?id={WorldWrapper.Current_World_ID}" : ""));
            Process.GetCurrentProcess().Kill();
        }
    }
}
