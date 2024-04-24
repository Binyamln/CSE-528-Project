using UnityEngine;
using System.Collections.Generic;

public class TeleportTrigger : MonoBehaviour
{
    private Transform[] spawnpoints; // Array to hold references to the Spawnpoint objects
    private List<Transform> visitedRooms = new List<Transform>(); // Store the previously visited rooms

    private void Start()
    {
        GameObject spawnpointManager = GameObject.Find("SpawnPointsManager");

        if (spawnpointManager != null)
        {
            spawnpoints = new Transform[spawnpointManager.transform.childCount];
            for (int i = 0; i < spawnpointManager.transform.childCount; i++)
            {
                spawnpoints[i] = spawnpointManager.transform.GetChild(i);
            }
        }
        else
        {
            Debug.LogError("SpawnpointManager not found!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.transform);
        }
    }

    private void TeleportPlayer(Transform playerTransform)
    {
        if (spawnpoints == null || spawnpoints.Length == 0)
        {
            Debug.LogError("Spawnpoints not initialized!");
            return;
        }

        // Determine available spawn points excluding the current room
        List<Transform> availableSpawnpoints = new List<Transform>(spawnpoints);
        if (visitedRooms.Count > 0)
        {
            // Exclude the most recently visited rooms to prevent immediate backtracking
            foreach (Transform visitedRoom in visitedRooms)
            {
                availableSpawnpoints.Remove(visitedRoom);
            }
        }

        // Select a new random spawnpoint from the available ones
        if (availableSpawnpoints.Count > 0)
        {
            Transform newRoom = availableSpawnpoints[Random.Range(0, availableSpawnpoints.Count)];
            playerTransform.position = newRoom.position;

            // Manage visited rooms history
            visitedRooms.Add(newRoom);
            // Optionally limit the memory of visited rooms, for example remember only the last 2 rooms
            if (visitedRooms.Count > 2) // Adjust this number based on your game's requirements
            {
                visitedRooms.RemoveAt(0); // Removes the oldest room from the list
            }
        }
        else
        {
            Debug.LogWarning("No available spawnpoints found, possibly all have been visited.");
        }
    }
}
