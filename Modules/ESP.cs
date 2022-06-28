using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wrapper.PlayerWrapper;

namespace EXO.Modules
{
    internal class ESP : BaseModule
    {
        public static bool EspState = false;
        public override void OnUpdate()
        {
            if (EspState)
            {
                List<VRC.Player>.Enumerator player = PlayerWrapper.AllPlayers2().Array.ToList().GetEnumerator();
                if (player.Current.prop_APIUser_0 == null && player.Current) return;
                while (player.MoveNext())
                {
                    PlayerWrapper.PlayerMeshEsp(player.Current, false);
                }
            }
        }
    }
}
