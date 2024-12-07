﻿using System;
using System.Reflection;
using HarmonyLib;
using BepInEx.Logging;
using UnityEngine;

namespace CodysModTemplate.Patches
{
    public class HarmonyPatches
    {
        private static readonly Harmony instance = new Harmony(ModInfo.GUID);

        public static bool IsPatched { get; private set; }

        // Apply all Harmony patches in the executing assembly
        internal static void ApplyHarmonyPatches()
        {
            if (IsPatched) return;

            try
            {
                Debug.Log($"{ModInfo.Name} is Applying Harmony patches...");
                instance.PatchAll(Assembly.GetExecutingAssembly());
                IsPatched = true;
                Debug.Log($"Harmony patches applied successfully, in {ModInfo.Name}.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error applying Harmony patches: {ex.Message}");
            }
        }

        // Remove all Harmony patches
        internal static void RemoveHarmonyPatches()
        {
            if (!IsPatched) return;

            try
            {
                Debug.Log("Removing Harmony patches...");
                instance.UnpatchSelf();
                IsPatched = false;
                Debug.Log("Harmony patches removed successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error removing Harmony patches: {ex.Message}");
            }
        }
    }
}