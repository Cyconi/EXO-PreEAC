using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Wrapper.PlayerWrapper;

namespace EXO
{
    internal class PlayerLineESP
    {
        public static GameObject Base;
        internal static List<VRC.Player> AllPlayers = new List<VRC.Player>();
        internal static bool State;
        internal static IEnumerator StartPlayerLine()
        {           
            foreach (var player in AllPlayers)
            {                ;
                if (player == null)
                        yield return null;
                if (!State)
                    yield return null;

                Base.GetComponent<LineRenderer>().SetPosition(1, Camera.main.transform.position);
                Base.GetComponent<LineRenderer>().SetPosition(0, player.transform.position);
                yield return new WaitForEndOfFrame();
            }
            yield break;
        }        
    }
    internal static class GetOrAdd
    {
        public static T GetOrAddComponents<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null)
                return gameObject.AddComponent<T>();

            return component;
        }
    }
}
