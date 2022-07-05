using ConsoleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.DataModel;
using VRC.SDKBase;
using VRC.UI;
using VRC.UI.Elements.Menus;
using EXO;

namespace Wrapper.PlayerWrapper
{
    public static class PlayerWrapper
    {
        //This was bc i wanted an avi reload lmao
        public static Dictionary<int, VRC.Player> PlayersActorID = new Dictionary<int, VRC.Player>();
        private static Collider LocalPlayerCollider;
        private static VRC_EventHandler handler;
        internal static List<string> ClientUsers = new List<string>();        
        public static Player GetPlayer() => Player.prop_Player_0;
        public static Player[] GetAllPlayers() => PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray();
        //new
        public static VRC.Core.Pool.PooledArray<Player> AllPlayers2() => PlayerManager.prop_PlayerManager_0.prop_PooledArray_1_Player_0;
        public static Player GetByUsrID(string usrID) => GetAllPlayers().First(x => x.prop_APIUser_0.id == usrID);
        public static void Teleport(this Player player) => LocalVRCPlayer.transform.position = player.prop_VRCPlayer_0.transform.position;
        public static Player LocalPlayer() => Player.prop_Player_0;
        public static VRCPlayer LocalVRCPlayer => VRCPlayer.field_Internal_Static_VRCPlayer_0;
        public static APIUser GetAPIUser(this VRC.Player player) => player.prop_APIUser_0;
        public static float GetFrames(this Player player) => (player._playerNet.prop_Byte_0 != 0) ? Mathf.Floor(1000f / (float)player._playerNet.prop_Byte_0) : -1f;
        public static short GetPing(this Player player) => player._playerNet.field_Private_Int16_0;
        public static bool IsBot(this Player player) => player.GetPing() <= 0 && player.GetFrames() <= 0 && player.UserID() != APIUser.CurrentUser.id || player.transform.position == Vector3.zero;
        public static VRC.Player GetSelectedUser() => GetByUsrID(GameObject.Find("UserInterface").gameObject.GetComponentInChildren<SelectedUserMenuQM>().field_Private_IUser_0.prop_String_0);
        public static Player GetPlayer(this VRCPlayer player) => player.prop_Player_0;
        public static VRCPlayer GetVRCPlayer(this Player player) => player._vrcplayer;        
        public static Color GetTrustColor(this VRC.Player player) => VRCPlayer.Method_Public_Static_Color_APIUser_0(player.GetAPIUser());
        public static APIUser GetAPIUser(this VRCPlayer Instance) => Instance.GetPlayer().GetAPIUser();
        public static VRCPlayerApi GetVRCPlayerApi(this Player Instance) => Instance?.prop_VRCPlayerApi_0;
        public static bool GetIsMaster(this Player Instance) => Instance.GetVRCPlayerApi().isMaster;
        public static bool GetIsVRCDev(this Player Instance) => Instance.GetVRCPlayerApi().isModerator;
        public static int GetActorNumber(this Player player) => player.GetVRCPlayerApi() != null ? player.GetVRCPlayerApi().playerId : -1;
        public static USpeaker GetUspeaker(this Player player) => player.prop_USpeaker_0;
        public static Photon.Realtime.Player GetPhotonPlayer(this Player player) => player.prop_Player_1;
        public static bool ClientDetect(this Player player) => player.GetFrames() > 111 || player.GetFrames() < 10 || player.GetPing() > 5400 || player.GetPing() < 10 || ClientUsers.Contains(player.UserID());
        public static ApiAvatar GetAPIAvatar(this VRCPlayer vrcPlayer) => vrcPlayer.prop_ApiAvatar_0;
        public static ApiAvatar GetAPIAvatar(this Player player) => player.GetVRCPlayer().GetAPIAvatar();        
        public static Animator GetAnimator(this VRCPlayer player) => player.field_Internal_Animator_0;
        public static string UserID(this Player Instance) => Instance.GetAPIUser().id;
        public static void ReloadAvatar(this Player Instance) => VRCPlayer.Method_Public_Static_Void_APIUser_0(Instance.GetAPIUser());
        public static bool IsFriend(this Player player) => APIUser.CurrentUser.friendIDs.Contains(player.field_Private_APIUser_0.id);
        public static int GetPlayerCount() => VRCPlayerApi.GetPlayerCount();
        public static string DisplayName(this Player Instance) => Instance.GetAPIUser().displayName;
        public static string DisplayName(this APIUser Instance) => Instance.displayName;
        public static VRCPlayerApi GetVRCPlayerApi(this VRCPlayer Instance) => Instance.prop_VRCPlayerApi_0;
        public static bool GetIsInVR(this VRCPlayer Instance) => Instance.GetVRCPlayerApi().IsUserInVR();
        public static bool IsQuest(this Player player) => player.GetAPIUser().IsOnMobile;
        public static APIUser LocalAPIUser => APIUser.CurrentUser;
        public static USpeaker LocalUSpeaker => LocalVRCPlayer.prop_USpeaker_0;
        public static VRCPlayerApi LocalVRCPlayerAPI => LocalVRCPlayer.field_Private_VRCPlayerApi_0;
        public static PlayerManager PManager => PlayerManager.field_Private_Static_PlayerManager_0;
        public static void SetVolume(this Player player, float vol) =>
            player.GetUspeaker().field_Private_SimpleAudioGain_0.field_Public_Single_0 = vol;
        public static float DefaultGain => 1f;
        public static float MaxGain => float.MaxValue;
        public static void SetGain(float Gain) =>
            LocalGain = Gain;
        public static void ResetGain() =>
            USpeaker.field_Internal_Static_Single_1 = DefaultGain;
        public static float LocalGain
        {
            get { return USpeaker.field_Internal_Static_Single_1; }
            set { USpeaker.field_Internal_Static_Single_1 = value; }
        }

        public static float AllGain
        {
            get { return USpeaker.field_Internal_Static_Single_0; }
            set { USpeaker.field_Internal_Static_Single_0 = value; }
        }

        public static void SendVRCEvent(VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType type, GameObject instagator) // Why in the player wrapper? i dunno loll
        {
            if (handler == null)
                handler = Resources.FindObjectsOfTypeAll<VRC_EventHandler>().FirstOrDefault<VRC_EventHandler>();

            vrcEvent.ParameterObject = Player.prop_Player_0.prop_USpeaker_0.gameObject;
            handler.TriggerEvent(vrcEvent, type, instagator, 0f);
        }

        internal static Player GetNetworkOwner(GameObject gameObject)
        {
            List<Player> all_player = GetAllPlayers().ToList();

            foreach (var player in all_player)
                if (player != null && player.prop_APIUser_0 != null)
                    if (Networking.GetOwner(gameObject) == player.field_Private_VRCPlayerApi_0)
                        return player;

            return null;
        }

        public static Player GetPlayerByUserID(string UserID)
        {
            return (from p in GetAllPlayers().ToList()
                    where p.GetAPIUser().id == UserID
                    select p).FirstOrDefault<Player>();
        }

        public static Player GetPlayerWithPlayerID(int playerID)
        {
            List<Player> list = GetAllPlayers().ToList();
            foreach (Player player in list.ToArray())
                if (player.GetVRCPlayerApi().playerId == playerID)
                    return player;

            return null;
        }

        public static void FreezeLocalPlayer(bool Enabled)
        {
            if (LocalPlayerCollider == null)
                LocalPlayerCollider = LocalVRCPlayer.GetComponent<Collider>();

            LocalPlayerCollider.enabled = Enabled;
        }

        public static void Tele2MousePos()
        {
            Ray posF = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //pos, directon 
            RaycastHit[] PosData = Physics.RaycastAll(posF);
            if (PosData.Length > 0) { RaycastHit pos = PosData[0]; VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position = pos.point; }
        }

        public static string GetFramesColord(this Player player)
        {
            float fps = player.GetFrames();
            if (fps > 60)
                return "<color=green>" + fps + "</color>";
            else if (fps > 25)
                return "<color=yellow>" + fps + "</color>";
            else
                return "<color=red>" + fps + "</color>";
        }

        public static string GetFramesTextColord(this Player player)
        {
            float fps = player.GetFrames();
            if (fps > 60)
                return "<color=green>FPS</color>";
            else if (fps > 25)
                return "<color=yellow>FPS</color>";
            else
                return "<color=red>FPS</color>";
        }

        public static string GetRankColord(this Player player)
        {
            bool MOD = player.prop_APIUser_0.hasModerationPowers || player.prop_APIUser_0.tags.Contains("admin_moderator");
            bool ADMIN = player.prop_APIUser_0.hasSuperPowers || player.prop_APIUser_0.tags.Contains("admin_");
            if (ADMIN)
                return "<color=#ff0000>[Admin User]</color>";
            else if (MOD)
                return "<color=red>[Moderation User]</color>";
            else if (player.prop_APIUser_0.hasVeteranTrustLevel)
                return "<color=#864EDD>Trusted</color>";
            else if (player.prop_APIUser_0.hasTrustedTrustLevel)
                return "<color=yellow>Known</color>";
            else if (player.prop_APIUser_0.hasKnownTrustLevel)
                return "<color=green>User</color>";
            else if (player.prop_APIUser_0.hasBasicTrustLevel)
                return "<color=blue>New</color>";
            else
                return "<color=white>Vistor</color>";
        }

        public static string GetPingColord(this Player player)
        {
            short Ping = player.GetPing();
            if (Ping > 150)
                return "<color=red>" + Ping + "</color>";
            else if (Ping > 75)
                return "<color=yellow>" + Ping + "</color>";
            else
                return "<color=green>" + Ping + "</color>";
        }

        public static string GetAviColord(this Player player)
        {
            string ApiAvi = player.prop_ApiAvatar_0.releaseStatus;

            if (ApiAvi == "public")
                return " | [<color=green>Public</color>]";
            else
                return " | [<color=red>Private</color>]";

        }

        public static string GetPingTextColord(this Player player)
        {
            short Ping = player.GetPing();
            if (Ping > 150)
                return "<color=red>Ping</color>";
            else if (Ping > 75)
                return "<color=yellow>Ping</color>";
            else
                return "<color=green>Ping</color>";
        }

        public static string GetPlatform(this Player player, bool Short = false)
        {
            if (player.GetAPIUser().IsOnMobile)
                return Short ? "<color=green>Q</color>" : "<color=green>Quest</color>";
            else if (player.GetVRCPlayerApi().IsUserInVR())
                return Short ? "<color=#CE00D5>V</color>" : "<color=#CE00D5>Vr</color>";
            else
                return Short ? "<color=grey>P</color>" : "<color=grey>PC</color>";
        }

        public static void DelegateSafeInvoke(this Delegate @delegate, params object[] args)
        {
            Delegate[] invocationList = @delegate.GetInvocationList();
            for (int i = 0; i < invocationList.Length; i++)
                try
                {
                    invocationList[i].DynamicInvoke(args);
                }
                catch (Exception ex)
                {
                    CLog.E("Error while executing delegate:\n" + ex.ToString(), ConsoleColor.Red);
                }

        }
        //added
        public static void PlayerMeshEsp(VRC.Player player, bool State)
        {
            var id = player.prop_APIUser_0.id;
            if (id == null || id == GetPlayer().prop_APIUser_0.id) return;
            var Renderer = player._vrcplayer.field_Internal_GameObject_0.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in player._vrcplayer.field_Internal_GameObject_0.GetComponentsInChildren<Renderer>())
            {
                HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer, State);
            }
        }
        //
        public static Player GetPlayerByActorID(int actorId)
        {
            Player player = null;
            PlayersActorID.TryGetValue(actorId, out player);
            return player;
        }
    }
}
