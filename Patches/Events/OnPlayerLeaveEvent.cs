using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXO.Patches.Events
{
    public interface OnPlayerLeaveEvent
    {
        void PlayerLeave(VRC.Player player);
    }
}
