using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 30;
    public int current_health = 30;

    private PlayerController pc;
    private TeleportTrigger tpTrigger;

    public float moveSpeed = 2f;
    public float attackRange = 1.5f;

    private Transform playerTransform;
    private NavMeshAgent agent;
    [SerializeField] Transform goal;
    private GameObject[] enemies;
    [SerializeField] GameObject[] items;

    void Update()
    {
        if (current_health <= 0) {
            int rand = Random.Range(0, enemies.Length);
            GameObject item = items[rand];
            Destroy(gameObject);
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Sword") {
            current_health -= 10;
            Debug.Log("Enemy Health: " + current_health);
        }
    }

}
