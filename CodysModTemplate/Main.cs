using System;
using BepInEx;
using CodysModTemplate.Patches;
using UnityEngine;

namespace CodysModTemplate
{
    [BepInPlugin(ModInfo.GUID, ModInfo.Name, ModInfo.Version)]
    public class Main : BaseUnityPlugin
    {
        public static bool IsModdedRoom;

        // Called once when the mod is loaded
        void Start()
        {
            try
            {
                // Ensure GorillaTagger.OnPlayerSpawned is an event and subscribe to it correctly
                GorillaTagger.OnPlayerSpawned(Initialized);
                Debug.Log($"{ModInfo.Name}, v{ModInfo.Version} has been Initialized");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during mod initialization: {ex.Message}");
            }
        }

        // Method that gets called once a player is spawned
        void Initialized()
        {
            Debug.Log("Game Initialized and Player Spawned");
        }

        // The stuff in here happens when the mods is Enabled
        void OnEnable()
        {
            try
            {
                HarmonyPatches.ApplyHarmonyPatches();
                Debug.Log("Harmony patches applied.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error applying Harmony patches: {ex.Message}");
            }
        }

        // The stuff in here happens when the mods is Disabled
        void OnDisable()
        {
            try
            {
                HarmonyPatches.RemoveHarmonyPatches();
                Debug.Log("Harmony patches removed.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error removing Harmony patches: {ex.Message}");
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
                    Debug.Log("Player has entered a modded lobby.");
                }
                else
                {
                    Debug.Log("Player is in a standard room.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during Update: {ex.Message}");
            }
        }
    }
}