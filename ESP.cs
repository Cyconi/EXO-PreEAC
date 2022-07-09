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
using UnhollowerBaseLib;
using Wrapper.PlayerWrapper;

namespace EXO
{
    internal class ESP
    {
        internal static bool ItemESP;
        internal static bool TriggerESP;
        internal static bool BoxColESP;
        internal static bool CapsuleESP;
        internal static bool RigidbodyESP;
        internal static bool UdonESP;
        internal static bool InterESP;
        internal static bool MeshESP;

        public static IEnumerator ItemHighlight()
        {                                                            
            Il2CppArrayBase<VRC.SDKBase.VRC_Pickup> AllBaseUdonPickups = Resources.FindObjectsOfTypeAll<VRC.SDKBase.VRC_Pickup>();
            Il2CppArrayBase<VRCSDK2.VRC_Pickup> array = Resources.FindObjectsOfTypeAll<VRCSDK2.VRC_Pickup>();
            Il2CppArrayBase<VRCPickup> AllUdonPickups = Resources.FindObjectsOfTypeAll<VRCPickup>();
            var AllSyncPickups = Resources.FindObjectsOfTypeAll<VRC_ObjectSync>();
            var AllSDK3SyncPickups = Resources.FindObjectsOfTypeAll<VRCObjectSync>();

            while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
                yield return null;
            
            for (; ; )
            {                
                foreach (VRC.SDKBase.VRC_Pickup vrc_Pickup in AllBaseUdonPickups)
                {
                    bool Object = !(vrc_Pickup == null) && !(vrc_Pickup.gameObject == null) && vrc_Pickup.gameObject.active && vrc_Pickup.enabled && vrc_Pickup.pickupable && !vrc_Pickup.name.Contains("ViewFinder") && !(HighlightsFX.prop_HighlightsFX_0 == null);
                    if (Object)
                    {
                        HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(vrc_Pickup.GetComponentInChildren<MeshRenderer>(), ItemESP);
                    }
                }
                foreach (VRCSDK2.VRC_Pickup vrc_Pickup in array)
                {
                    bool Object = !(vrc_Pickup == null) && !(vrc_Pickup.gameObject == null) && vrc_Pickup.gameObject.active && vrc_Pickup.enabled && vrc_Pickup.pickupable && !vrc_Pickup.name.Contains("ViewFinder") && !(HighlightsFX.prop_HighlightsFX_0 == null);
                    if (Object)
                    {
                        HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(vrc_Pickup.GetComponentInChildren<MeshRenderer>(), ItemESP);
                    }
                }
                foreach (VRCPickup vrc_Pickup in AllUdonPickups)
                {
                    bool Object = !(vrc_Pickup == null) && !(vrc_Pickup.gameObject == null) && vrc_Pickup.gameObject.active && vrc_Pickup.enabled && vrc_Pickup.pickupable && !vrc_Pickup.name.Contains("ViewFinder") && !(HighlightsFX.prop_HighlightsFX_0 == null);
                    if (Object)
                    {
                        HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(vrc_Pickup.GetComponentInChildren<MeshRenderer>(), ItemESP);
                    }
                }
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

                yield return new WaitForSeconds(0.1f);
            }
        }
        public static IEnumerator TriggerHighlight()
        {
            var AllTriggers = Resources.FindObjectsOfTypeAll<VRC.SDKBase.VRC_Trigger>();
            var AllSDK2Triggers = Resources.FindObjectsOfTypeAll<VRCSDK2.VRC_Trigger>();
            

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

                if (!TriggerESP)
                    yield break;

                yield return new WaitForSeconds(0.1f);
            }
        }

        public static IEnumerator BoxColliderHighlight()
        {
            var BoxCol = Resources.FindObjectsOfTypeAll<UnityEngine.BoxCollider>();
            var BoxCol2D = Resources.FindObjectsOfTypeAll<UnityEngine.BoxCollider2D>();

            while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
                yield return null;

            for (; ; )
            {
                for (int i = 0; i < BoxCol.Length; i++)
                    try
                    {
                        if (BoxCol[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (BoxCol[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = BoxCol[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, BoxColESP);
                            }
                    }
                    catch { }
                for (int i = 0; i < BoxCol2D.Length; i++)
                    try
                    {
                        if (BoxCol2D[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (BoxCol2D[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = BoxCol2D[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, BoxColESP);
                            }
                    }
                    catch { }

                if (!BoxColESP)
                    yield break;

                yield return new WaitForSeconds(0.1f);
            }
        }        
        public static void CapsuleHighlight(VRC.Player player, bool state)
        {
            Renderer renderer;
            if (player == null)
                renderer = null;

            else
            {
                Transform transform = player.transform.Find("SelectRegion");
                renderer = ((transform != null) ? transform.GetComponent<Renderer>() : null);
                Renderer renderer2 = renderer;
                if (renderer2)
                    HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer2, state);

            }
        }
        public static void MeshHighlight(VRC.Player player, bool State)
        {
            var id = player.prop_APIUser_0.id;
            if (id == null || id == PlayerWrapper.GetPlayer().prop_APIUser_0.id) return;
            foreach (Renderer renderer in player._vrcplayer.field_Internal_GameObject_0.GetComponentsInChildren<Renderer>())
            {
                HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer, State);
            }
        }
        internal static void PlayerMeshHighlight()
        {
            if (!MeshESP) return;                        
            try
            {
                List<VRC.Player>.Enumerator player = PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0.ToArray().ToList().GetEnumerator();
                if (player.Current.prop_APIUser_0 == null && player.Current) return;
                while (player.MoveNext())
                {
                    MeshHighlight(player.Current, false);
                }
            }
            catch { }
        }
        public void OnUpdate()
        {
            PlayerMeshHighlight();
        }
        public static IEnumerator RigidbodyHighlight()
        {
            var Rigidbody = Resources.FindObjectsOfTypeAll<UnityEngine.Rigidbody>();
            var Rigidbody2D = Resources.FindObjectsOfTypeAll<UnityEngine.Rigidbody2D>();

            while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
                yield return null;

            for (; ; )
            {
                for (int i = 0; i < Rigidbody.Length; i++)
                    try
                    {
                        if (Rigidbody[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (Rigidbody[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = Rigidbody[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, RigidbodyESP);
                            }
                    }
                    catch { }
                for (int i = 0; i < Rigidbody2D.Length; i++)
                    try
                    {
                        if (Rigidbody2D[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (Rigidbody2D[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = Rigidbody2D[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, RigidbodyESP);
                            }
                    }
                    catch { }

                if (!RigidbodyESP)
                    yield break;

                yield return new WaitForSeconds(0.1f);
            }
        }
        public static IEnumerator UdonHighlight()
        {
            var UdonBehaviours = Resources.FindObjectsOfTypeAll<VRC.Udon.UdonBehaviour>();
            var UdonSync = Resources.FindObjectsOfTypeAll<VRC.Networking.UdonSync>();
            var Photon = Resources.FindObjectsOfTypeAll<Photon.Pun.PhotonView>();
            var PhotonHandle = Resources.FindObjectsOfTypeAll<Photon.Pun.PhotonHandler>();

            while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
                yield return null;

            for (; ; )
            {
                for (int i = 0; i < UdonBehaviours.Length; i++)
                    try
                    {
                        if (UdonBehaviours[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (UdonBehaviours[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = UdonBehaviours[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, UdonESP);
                            }
                    }
                    catch { }
                for (int i = 0; i < UdonSync.Length; i++)
                    try
                    {
                        if (UdonSync[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (UdonSync[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = UdonSync[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, UdonESP);
                            }
                    }
                    catch { }
                for (int i = 0; i < Photon.Length; i++)
                    try
                    {
                        if (Photon[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (Photon[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = Photon[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, UdonESP);
                            }
                    }
                    catch { }
                for (int i = 0; i < PhotonHandle.Length; i++)
                    try
                    {
                        if (PhotonHandle[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (PhotonHandle[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = PhotonHandle[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, UdonESP);
                            }
                    }
                    catch { }

                if (!UdonESP)
                    yield break;

                yield return new WaitForSeconds(0.1f);
            }
        }
        public static IEnumerator InteractableHighlight()
        {
            var AllInteractable = Resources.FindObjectsOfTypeAll<VRCInteractable>();
            var AllBaseInteractable = Resources.FindObjectsOfTypeAll<VRC.SDKBase.VRC_Interactable>();
            var AllSDK2Interactable = Resources.FindObjectsOfTypeAll<VRCSDK2.VRC_Interactable>();

            while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
                yield return null;

            for (; ; )
            {
                for (int i = 0; i < AllInteractable.Length; i++)
                    try
                    {
                        if (AllInteractable[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null))
                            if (AllInteractable[i].GetComponent<MeshRenderer>() != null)
                            {
                                var render = AllInteractable[i].GetComponent<MeshRenderer>();
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, InterESP);
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
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, InterESP);
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
                                HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(render, InterESP);
                            }
                    }
                    catch { }

                if (!InterESP)
                    yield break;

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
