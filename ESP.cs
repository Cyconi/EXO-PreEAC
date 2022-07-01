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

namespace EXO
{
    internal class ESP
    {
        internal static bool ItemESP;
        internal static bool TriggerESP;
        public static IEnumerator ItemHighlight()
        {
            var array = UnityEngine.Object.FindObjectsOfType<VRCSDK2.VRC_Pickup>();
            var AllUdonPickups = UnityEngine.Object.FindObjectsOfType<VRCPickup>();
            var AllBaseUdonPickups = UnityEngine.Object.FindObjectsOfType<VRC.SDKBase.VRC_Pickup>();
            var AllSyncPickups = UnityEngine.Object.FindObjectsOfType<VRC_ObjectSync>();
            var AllSDK3SyncPickups = UnityEngine.Object.FindObjectsOfType <VRCObjectSync>();

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
                for (int i = 0; i < AllBaseUdonPickups.Length; i++)
                    try
                    {
                        if (AllBaseUdonPickups[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (AllBaseUdonPickups[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = AllBaseUdonPickups[i].GetComponent<MeshRenderer>();
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
                for (int i = 0; i < AllSDK3SyncPickups.Length; i++)
                    try
                    {
                        if (AllSDK3SyncPickups[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (AllSDK3SyncPickups[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = AllSDK3SyncPickups[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, ItemESP);
                            }
                    }
                    catch { }

                if (!ItemESP)
                    yield break;

                yield return new WaitForSeconds(0.01f);
            }
        }
        public static IEnumerator TriggerHighlight()
        {
            var AllTriggers = UnityEngine.Object.FindObjectsOfType<VRC.SDKBase.VRC_Trigger>();
            var AllSDK2Triggers = UnityEngine.Object.FindObjectsOfType<VRCSDK2.VRC_Trigger>();
            var AllInteractable = UnityEngine.Object.FindObjectsOfType<VRCInteractable>();
            var AllBaseInteractable = UnityEngine.Object.FindObjectsOfType<VRC.SDKBase.VRC_Interactable>();
            var AllSDK2Interactable = UnityEngine.Object.FindObjectsOfType<VRCSDK2.VRC_Interactable>();

            while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
                yield return null;

            for (; ; )
            {
                for (int i = 0; i < AllTriggers.Length; i++)
                    try
                    {
                        if (AllTriggers[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (AllTriggers[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = AllTriggers[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, TriggerESP);
                            }
                    }
                    catch { }
                for (int i = 0; i < AllSDK2Triggers.Length; i++)
                    try
                    {
                        if (AllSDK2Triggers[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (AllSDK2Triggers[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = AllSDK2Triggers[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, TriggerESP);
                            }
                    }
                    catch { }
                for (int i = 0; i < AllInteractable.Length; i++)
                    try
                    {
                        if (AllInteractable[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (AllInteractable[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = AllInteractable[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, TriggerESP);
                            }
                    }
                    catch { }
                for (int i = 0; i < AllBaseInteractable.Length; i++)
                    try
                    {
                        if (AllBaseInteractable[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (AllBaseInteractable[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = AllBaseInteractable[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, TriggerESP);
                            }
                    }
                    catch { }
                for (int i = 0; i < AllSDK2Interactable.Length; i++)
                    try
                    {
                        if (AllSDK2Interactable[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (AllSDK2Interactable[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = AllSDK2Interactable[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, TriggerESP);
                            }
                    }
                    catch { }

                if (!TriggerESP)
                    yield break;

                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
