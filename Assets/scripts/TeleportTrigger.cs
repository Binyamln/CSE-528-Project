using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    private static GameObject[] roomPrefabs; 
    public static GameObject currentRoom; 
    private GameObject[] itemPrefabs; 

    void Start()
    {
        if (roomPrefabs == null || roomPrefabs.Length == 0)
        {
            roomPrefabs = Resources.LoadAll<GameObject>("roomPrefabs");
        }


        itemPrefabs = Resources.LoadAll<GameObject>("Items");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
            TeleportPlayer();
        }
    }

    void TeleportPlayer()
    {
        if (roomPrefabs.Length == 0)
        {
            Debug.LogError("No room prefabs found. Please check your Resources/roomPrefabs directory.");
            return;
        }

        if (currentRoom != null)
        {
            Destroy(currentRoom);
        }

        GameObject roomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Length)];
        currentRoom = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity);

        SpawnItemsInRoom(currentRoom);

        Transform spawnPoint = currentRoom.transform.Find("SpawnPoint");
        if (spawnPoint != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = spawnPoint.position; // Teleport the player to the spawn point
        }
        else
        {
            Debug.LogError("No spawn point found in the new room.");
        }
    }

    void SpawnItemsInRoom(GameObject room)
    {
        if (itemPrefabs.Length > 0)
        {
            GameObject itemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            Vector3 spawnPosition = room.transform.position; // Modify this as needed
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity, room.transform);
        }
    }
}
