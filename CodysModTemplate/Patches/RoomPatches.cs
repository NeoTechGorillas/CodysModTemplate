using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using System;
using UnityEngine;

namespace CodysModTemplate.Patches
{
    // Static class that handles room-related logic and patches
    public static class RoomPatches
    {
        // Checks if the current room is a modded lobby by looking for a "gameMode" property
        public static bool IsModdedLobby()
        {
            try
            {
                // Check if we're in a room and the "gameMode" property exists
                if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("gameMode"))
                {
                    var gameMode = PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString();
                    return gameMode.Contains("MODDED", StringComparison.OrdinalIgnoreCase); 
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error checking modded lobby status: {ex.Message}");
            }
            return false;
        }

        // Attempts to join a room using a code
        public static void JoinRoomWithCode(string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    Debug.LogWarning("JoinRoomWithCode was called with an empty or null code.");
                    return;
                }

                PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(code, JoinType.Solo);
                Debug.Log($"Attempting to join room with code: {code}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error attempting to join room with code {code}: {ex.Message}");
            }
        }

        // Disconnects from the current room and returns to single-player mode
        public static void Disconnect()
        {
            try
            {
                NetworkSystem.Instance.ReturnToSinglePlayer();
                Debug.Log("Disconnected from room and returned to single-player mode.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error while disconnecting: {ex.Message}");
            }
        }
    }

    // Harmony patch for MonoBehaviourPunCallbacks.OnJoinedRoom
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnJoinedRoom")]
    public class RoomJoinPatch
    {
        // Set IsModdedRoom flag to true if the current room is a modded lobby
        public static void Postfix()
        {
            Main.IsModdedRoom = RoomPatches.IsModdedLobby();
            Debug.Log($"Player has joined a {(Main.IsModdedRoom ? "modded" : "standard")} room.");
        }
    }

    // Harmony patch for MonoBehaviourPunCallbacks.OnLeftRoom
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnLeftRoom")]
    public class RoomLeftPatch
    {
        // Set IsModdedRoom flag to false when leaving the room
        public static void Postfix()
        {
            Main.IsModdedRoom = false;
            Debug.Log("Player has left the room.");
        }
    }
}