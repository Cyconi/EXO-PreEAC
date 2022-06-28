using System; 
using System.Linq; 
using VRC.Core;  
using VRC;
using ConsoleLogger;
using Wrapper.PlayerWrapper;
using Wrapper.WorldWrapper;
using EXO.Modules;

namespace EXO.Patches
{
    internal class JoinLeavePatch
    {
        
        public static void Init()
        {
            try
            {
                VRCEventDelegate<Player> field_Internal_VRCEventDelegate_1_Player_ = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_0;
                VRCEventDelegate<Player> field_Internal_VRCEventDelegate_1_Player_2 = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_1;
                field_Internal_VRCEventDelegate_1_Player_.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<Player>(JoinLeavePatch.OnPlayerJoin));
                field_Internal_VRCEventDelegate_1_Player_2.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<Player>(JoinLeavePatch.OnPlayerLeave));
                CLog.L("[Patches] Join & Leave");
            }
            catch (Exception ex)
            {
                CLog.E(ex);

            }
        }
        private static void OnPlayerJoin(VRC.Player player)
        {
            if (player == PlayerWrapper.GetPlayer()) { WorldWrapper.Init(); }
            if (ESP.EspState == true)
                PlayerWrapper.PlayerMeshEsp(player, true);

            if (PlayerWrapper.PlayersActorID.ContainsKey(player.GetActorNumber()))
            {
                PlayerWrapper.PlayersActorID.Remove(player.GetActorNumber());
                PlayerWrapper.PlayersActorID.Add(player.GetActorNumber(), player);
                return;
            }
            PlayerWrapper.PlayersActorID.Add(player.GetActorNumber(), player);
        }

        private static void OnPlayerLeave(VRC.Player player)
        {
            if (player == null) return;
            for (int i = 0; i < EXOBase.Instance.OnPlayerLeaveEvents.Count; i++)
                EXOBase.Instance.OnPlayerLeaveEvents[i].PlayerLeave(player);
            PlayerWrapper.PlayersActorID.Remove(player.GetActorNumber());
        }
    }
} 
