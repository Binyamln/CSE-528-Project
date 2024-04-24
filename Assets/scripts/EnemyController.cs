using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private PlayerController pc;
    private TeleportTrigger tpTrigger;

    public float moveSpeed = 2f;
    public float attackRange = 1.5f;

    private Transform playerTransform;
    private NavMeshAgent agent;

    private GameObject[] enemies;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        pc = playerTransform.GetComponent<PlayerController>();

        Spawn();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= attackRange)
        {
            // attack the player
            pc.TakeDamage(10);
        }
        else
        {
            agent.SetDestination(playerTransform.position);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
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
