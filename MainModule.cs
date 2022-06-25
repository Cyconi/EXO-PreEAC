using xButtonAPI.Controls;
using xButtonAPI.Controls.Grouping;
using xButtonAPI.Pages;
using ConsoleLogger;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using EXO.Hornet;

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
                Process.Start(Directory.GetCurrentDirectory() + "\\VRChat.exe");
                Process.GetCurrentProcess().Kill();
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
    }
}
