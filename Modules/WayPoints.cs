using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using xButtonAPI.Controls;
using xButtonAPI.Controls.Grouping;
using xButtonAPI.Pages;
using xButtonAPI;
using xButtonAPI.Misc;
using ConsoleLogger;
using System.IO;
using System.Net;
using System.Net.Http;
using UnityEngine.UI;
using VRC;
using UnityEngine.SceneManagement;
//WC Waypoints
namespace EXO.Modules
{
    internal class WayPoints : BaseModule
    {
        private static Vector3 Pos1, Pos2, Pos3, Pos4;
        private static Quaternion Rot1, Rot2, Rot3, Rot4;                
        public override void OnQuickMenuInit()
        {

            var WayPoint = new ButtonGroup(MainModule.WayPoint, "<color=#9b0000>WayPoints</color>");                       

            new SingleButton(WayPoint, "Save WP 1", "Save Your Current Poz to Poz 1", () =>
            {
                SavePoz(1);
            });
            new SingleButton(WayPoint, "Save WP 2", "Save Your Current Poz to Poz 2", () =>
            {
                SavePoz(2);
            });
            new SingleButton(WayPoint, "Save WP 3", "Save Your Current Poz to Poz 3", () =>
            {
                SavePoz(3);
            });
            new SingleButton(WayPoint, "Save WP 4", "Save Your Current Poz to Poz 4", () =>
            {
                SavePoz(4);
            });
            new SingleButton(WayPoint, "Load WP 1", "Load Your Current Poz to Poz 1", () =>
            {
                LoadPoz(1);
            });
            new SingleButton(WayPoint, "Load WP 2", "Load Your Current Poz to Poz 2", () =>
            {
                LoadPoz(2);
            });
            new SingleButton(WayPoint, "Load WP 3", "Load Your Current Poz to Poz 3", () =>
            {
                LoadPoz(3);
            });
            new SingleButton(WayPoint, "Load WP 4", "Load Your Current Poz to Poz 4", () =>
            {
                LoadPoz(4);
            });
        }
        internal static void SavePoz(int slot)
        {
            switch (slot)
            {
                case 1:
                    Pos1 = LocalDownload().gameObject.transform.position;
                    ConsoleLogger.CLog.L($"Save Position {LocalDownload().gameObject.transform.position.ToString()} To Slot {slot}", ConsoleColor.Green);
                    Rot1 = LocalDownload().gameObject.transform.rotation;
                    break;
                case 2:
                    Pos2 = LocalDownload().gameObject.transform.position;
                    ConsoleLogger.CLog.L($"Save Position {LocalDownload().gameObject.transform.position.ToString()} To Slot {slot}", ConsoleColor.Green);
                    Rot2 = LocalDownload().gameObject.transform.rotation;
                    break;
                case 3:
                    Pos3 = LocalDownload().gameObject.transform.position;
                    ConsoleLogger.CLog.L($"Save Position {LocalDownload().gameObject.transform.position.ToString()} To Slot {slot}", ConsoleColor.Green);
                    Rot3 = LocalDownload().gameObject.transform.rotation;
                    break;
                case 4:
                    Pos4 = LocalDownload().gameObject.transform.position;
                    ConsoleLogger.CLog.L($"Save Position {LocalDownload().gameObject.transform.position.ToString()} To Slot {slot}", ConsoleColor.Green);
                    Rot4 = LocalDownload().gameObject.transform.rotation;
                    break;
            }
        }

        internal static void LoadPoz(int slot)
        {
            switch (slot)
            {
                case 1:
                    if (Pos1 == null || Rot1 == null) return;
                    LocalDownload().gameObject.transform.position = Pos1;
                    LocalDownload().gameObject.transform.rotation = Rot1;
                    break;
                case 2:
                    if (Pos2 == null || Rot2 == null) return;
                    LocalDownload().gameObject.transform.position = Pos2;
                    LocalDownload().gameObject.transform.rotation = Rot2;
                    break;
                case 3:
                    if (Pos3 == null || Rot3 == null) return;
                    LocalDownload().gameObject.transform.position = Pos3;
                    LocalDownload().gameObject.transform.rotation = Rot3;
                    break;
                case 4:
                    if (Pos4 == null || Rot4 == null) return;
                    LocalDownload().gameObject.transform.position = Pos4;
                    LocalDownload().gameObject.transform.rotation = Rot4;
                    break;
            }
        }

        internal static Player LocalDownload()
        {
            foreach (GameObject gameObject in GetAllGameObjects())
            {
                bool flag = gameObject.name.StartsWith("VRCPlayer[Local]");
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
    }
}
