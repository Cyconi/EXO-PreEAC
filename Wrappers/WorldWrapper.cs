using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;
using VRC.Networking;
using VRC.Udon;
using VRC.Core;

namespace Wrapper.WorldWrapper
{
    internal static class WorldWrapper
    {
        internal static ApiWorld ApiWorld { get => RoomManager.field_Internal_Static_ApiWorld_0; }
        internal static ApiWorldInstance ApiWorldInstance { get => RoomManager.field_Internal_Static_ApiWorldInstance_0; }
        internal static string WorldID { get => ApiWorld.id; }
        internal static string InstanceID { get => ApiWorldInstance.instanceId; }
        public static string Current_World_ID { get { return $"{WorldID}:{RoomManager.field_Internal_Static_ApiWorldInstance_0.instanceId}"; } }
        public static bool In_World { get { return ApiWorld != null; } }

        internal static VRC_Pickup[] vrc_Pickups;
        internal static UdonBehaviour[] udonBehaviours;
        internal static VRC_Trigger[] vrc_Triggers;
        internal static VRC.SDK3.Components.VRCPickup[] AllUdonPickups;
        internal static VRCSDK2.VRC_ObjectSync[] AllSyncPickups;

        internal static void Init()
        {
            vrc_Pickups = UnityEngine.Object.FindObjectsOfType<VRC_Pickup>();
            udonBehaviours = UnityEngine.Object.FindObjectsOfType<UdonBehaviour>();
            vrc_Triggers = UnityEngine.Object.FindObjectsOfType<VRC_Trigger>();
            AllUdonPickups = UnityEngine.Object.FindObjectsOfType<VRC.SDK3.Components.VRCPickup>();
            AllSyncPickups = UnityEngine.Object.FindObjectsOfType<VRCSDK2.VRC_ObjectSync>();
        }
    }
}