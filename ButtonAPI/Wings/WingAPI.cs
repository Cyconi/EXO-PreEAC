using MelonLoader;
using System;
using System.Collections;
using UnityEngine;
using static xButtonAPI.WingAPI.Wing;

namespace xButtonAPI.WingAPI
{
    public class WingAPI
    {
        public static Action<BaseWing> OnWingInit = new Action<BaseWing>(_ => { });


        private static bool hasInitialized = false;
        public static void Initialize()
        {
            if (hasInitialized) return;
            hasInitialized = true;

            MelonCoroutines.Start(FindUI());
        }

        private static IEnumerator FindUI()
        {
            while ((Transforms.UserInterface = GameObject.Find("UserInterface")?.transform) is null)
                yield return null;

            while ((Transforms.QuickMenu = Transforms.UserInterface.Find("Canvas_QuickMenu(Clone)")) is null)
                yield return null;

            Left.Setup(Transforms.QuickMenu.Find("Container/Window/Wing_Left"));
            Right.Setup(Transforms.QuickMenu.Find("Container/Window/Wing_Right"));

            Left.WingOpen.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(new Action(() => Init_L()));
            Right.WingOpen.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(new Action(() => Init_R()));
        }

        private static Action Init_L = new Action(() => 
        {
            Init_L = new Action(() => { });
            MelonLogger.Msg("Creating Left Wing UI");
            OnWingInit(Left);
        });

        private static Action Init_R = new Action(() =>
        {
            Init_R = new Action(() => { });
            MelonLogger.Msg("Creating Right Wing UI");
            OnWingInit(Right);
        });
    }
}
