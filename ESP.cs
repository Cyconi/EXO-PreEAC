using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC;
using Wrapper.WorldWrapper;
using VRC.SDKBase;
using VRC.SDK3.Components;
using VRCSDK2;
using VRC_Pickup = VRCSDK2.VRC_Pickup;

namespace EXO
{
    internal class ESP
    {
        internal static bool ItemESP;
        internal static float OnUpdateRoutineDelay = 0f;
        public static List<Renderer> PickupsRenderers = new List<Renderer>();
        public static List<Renderer> TriggersRenderers = new List<Renderer>();
        public static IEnumerator ItemHighlight()
        {
            var array = UnityEngine.Object.FindObjectsOfType<VRC_Pickup>();
            var AllUdonPickups = UnityEngine.Object.FindObjectsOfType<VRCPickup>();
            var AllSyncPickups = UnityEngine.Object.FindObjectsOfType<VRC_ObjectSync>();

            while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
                yield return null;

            for (; ; )
            {
                for (int i = 0; i < array.Length; i++)
                    try
                    {
                        if (array[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (array[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = array[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, ItemESP);
                            }
                    }
                    catch { }

                for (int i = 0; i < AllUdonPickups.Length; i++)
                    try
                    {
                        if (AllUdonPickups[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (AllUdonPickups[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = AllUdonPickups[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, ItemESP);
                            }
                    }
                    catch { }

                for (int i = 0; i < AllSyncPickups.Length; i++)
                    try
                    {
                        if (AllSyncPickups[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (AllSyncPickups[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = AllSyncPickups[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, ItemESP);
                            }
                    }
                    catch { }

                if (!ItemESP)
                    yield break;

                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
