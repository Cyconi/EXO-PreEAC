﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;

namespace EXO.Patch
{
    internal class JoinLeave
    {
        internal static bool IsA;
        internal static bool IsB;

        public static void StartPatch()
        {
            CLog.L("[Patches] Patching NetworkManager...");
            Patches.instance.Patch(
                typeof(NetworkManager).GetMethod(nameof(NetworkManager.Method_Public_Void_Player_0)),
                typeof(JoinLeave).GetMethod(nameof(A), BindingFlags.NonPublic | BindingFlags.Static).ToNewHarmonyMethod()
            );

            Patches.instance.Patch(
                typeof(NetworkManager).GetMethod(nameof(NetworkManager.Method_Public_Void_Player_1)),
                typeof(JoinLeave).GetMethod(nameof(B), BindingFlags.NonPublic | BindingFlags.Static).ToNewHarmonyMethod()
            );
            CLog.L("[Patches] Patched NetworkManager!");
        }

        internal static bool A(VRC.Player __0) // Join
        {
            if (IsA == false && IsB)
                OnPlayerLeave(__0);
            else if (IsA && !IsB)
                OnPlayerJoin(__0);
            else if (!IsA && !IsB)
            {
                CLog.L("[A] = [Join]");
                CLog.L("[B] = [Leave]");
                IsA = true;
            }

            return true;
        }

        internal static bool B(VRC.Player __0) // Leave
        {
            if (IsB == false && IsA)
                OnPlayerLeave(__0);
            else if (IsB && !IsA)
                OnPlayerJoin(__0);
            else if (!IsA && !IsB)
            {
                CLog.L("B = Join");
                CLog.L("A = Leave");
                IsB = true;
            }

            return true;
        }

        internal static void OnPlayerJoin(VRC.Player __0)
        {
            if (ESP.CapsuleESP)
                ESP.HighlightPlayer(__0, ESP.CapsuleESP);                
            if (ESP.MeshESP)
                ESP.PlayerMeshEsp(__0, ESP.MeshESP);
        }

        internal static void OnPlayerLeave(VRC.Player __0)
        {

        }
    }
}
