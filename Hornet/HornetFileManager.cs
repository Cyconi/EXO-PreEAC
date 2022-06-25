using xButtonAPI.Controls;
using xButtonAPI.Controls.Grouping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DruUdonStuff;

namespace EXO.Hornet
{
    class HornetFileManager : BaseModule
    {
        public static List<HornetButton> buttons;

        public static ButtonGroup HornetMenu;
        public override void OnQuickMenuInit()
        {
            HornetMenu = new ButtonGroup(MainModule.Hornet, "<color=#9b0000>Hornet Place</color>");
        }
        
        public static void CreateButtons()
        {
            GameObject.Destroy(HornetMenu.headerGameObject);
            GameObject.Destroy(HornetMenu.gameObject);
            HornetMenu.Destroy();
            HornetMenu = new ButtonGroup(MainModule.Hornet, "<color=#9b0000>Custom Buttons</color>");

            buttons = JsonConvert.DeserializeObject<List<HornetButton>>(File.ReadAllText("EXO\\Hornet\\Buttons.json"));

            foreach(HornetButton button in buttons)
            {
                new SingleButton(HornetMenu, button.name, button.desc, () =>
                {
                    UdonStuff.SendUdonEventAll(button.eventObject, button.udonEvent);
                }, false);
            }
        }
    }

    public class HornetButton
    {
        public string name { get; set; }
        public string desc { get; set; }
        public string eventObject { get; set; }
        public string udonEvent { get; set; }
    }
}