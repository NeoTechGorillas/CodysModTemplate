using System;
using System.Reflection;
using HarmonyLib;
using BepInEx.Logging;

namespace CodysModTemplate.Patches
{
    public class HarmonyPatches
    {
        private static readonly Harmony instance = new Harmony(ModInfo.GUID);
        private static readonly ManualLogSource logger = new ManualLogSource("HarmonyPatches");

        public static bool IsPatched { get; private set; }

        // Apply all Harmony patches in the executing assembly
        internal static void ApplyHarmonyPatches()
        {
            if (IsPatched) return;

            try
            {
                logger.LogInfo("Applying Harmony patches...");
                instance.PatchAll(Assembly.GetExecutingAssembly());
                IsPatched = true;
                logger.LogInfo("Harmony patches applied successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Error applying Harmony patches: {ex.Message}");
            }
        }

        // Remove all Harmony patches
        internal static void RemoveHarmonyPatches()
        {
            if (!IsPatched) return;

            try
            {
                logger.LogInfo("Removing Harmony patches...");
                instance.UnpatchSelf();
                IsPatched = false;
                logger.LogInfo("Harmony patches removed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Error removing Harmony patches: {ex.Message}");
            }
        }
    }
}