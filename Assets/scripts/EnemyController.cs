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
    public GameObject speedItem;
    public GameObject healthItem;
    public GameObject damageItem;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        pc = playerTransform.GetComponent<PlayerController>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Spawn();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= attackRange)
        {
            // attack the player
            //pc.TakeDamage(10);
        }
        else
        {
            agent.SetDestination(goal.position);
        }
        if (current_health <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Sword") {
            current_health -= 10;
            Debug.Log("Enemy Health: " + current_health);
        }
    }

    private void Spawn()
    {
        /* Spawn enemies in random positions */

        float randomX = Random.Range(-5f, 5f);
        float randomY = Random.Range(-5f,5f);
        Vector2 spawnPosition = new Vector2(randomX, randomY);
        /* Spawn up to 5 enemies */
        GameObject enemy = enemies[Random.Range(0,5)];
 
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

}
