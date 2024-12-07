using System;
using BepInEx;
using CodysModTemplate.Patches;
using UnityEngine;

namespace CodysModTemplate
{
    [BepInPlugin(ModInfo.GUID, ModInfo.Name, ModInfo.Version)]
    public class Main : BaseUnityPlugin
    {
        private static BepInEx.Logging.ManualLogSource _logger = new BepInEx.Logging.ManualLogSource(ModInfo.Name);
        public static bool IsModdedRoom;

        // Called once when the mod is loaded
        void Start()
        {
            try
            {
                // Ensure GorillaTagger.OnPlayerSpawned is an event and subscribe to it correctly
                GorillaTagger.OnPlayerSpawned(Initialized);
                _logger.LogInfo($"{ModInfo.Name}, v{ModInfo.Version} has been Initialized");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during mod initialization: {ex.Message}");
            }
        }

        // Method that gets called once a player is spawned
        void Initialized()
        {
            _logger.LogInfo("Game Initialized and Player Spawned");
        }

        // The stuff in here happens when the mods is Enabled
        void OnEnable()
        {
            try
            {
                HarmonyPatches.ApplyHarmonyPatches();
                _logger.LogInfo("Harmony patches applied.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error applying Harmony patches: {ex.Message}");
            }
        }

        // The stuff in here happens when the mods is Disabled
        void OnDisable()
        {
            try
            {
                HarmonyPatches.RemoveHarmonyPatches();
                _logger.LogInfo("Harmony patches removed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error removing Harmony patches: {ex.Message}");
            }
        }

        // Called once per frame
        void Update()
        {
            try
            {
                // Handle modded room state logic here
                if (IsModdedRoom)
                {
                    _logger.LogInfo("Player has entered a modded lobby.");
                }
                else
                {
                    _logger.LogInfo("Player is in a standard room.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during Update: {ex.Message}");
            }
        }

        // Property to access the logger from other classes
        public static new BepInEx.Logging.ManualLogSource Logger => _logger;
    }
}