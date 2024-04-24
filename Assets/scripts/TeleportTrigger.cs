using UnityEngine;
using System.Collections.Generic;

public class TeleportTrigger : MonoBehaviour
{
    private Transform[] spawnpoints; // Array to hold references to the Spawnpoint objects
    private Transform previousRoom; // Store the previously visited room
    private Transform currentRoom; // Store the current room

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
            
            previousRoom = currentRoom;
            currentRoom = transform.parent;

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

        List<Transform> availableSpawnpoints = new List<Transform>();
        foreach (Transform spawnpoint in spawnpoints)
        {
            if (spawnpoint != currentRoom && spawnpoint != previousRoom)
            {
                availableSpawnpoints.Add(spawnpoint);
            }
        }

        if (availableSpawnpoints.Count == 0)
        {
            Debug.LogWarning("No available spawnpoints found!");
            return;
        }

        int randomIndex = Random.Range(0, availableSpawnpoints.Count);
        Transform randomSpawnpoint = availableSpawnpoints[randomIndex];

        playerTransform.position = randomSpawnpoint.position;
    }
}
