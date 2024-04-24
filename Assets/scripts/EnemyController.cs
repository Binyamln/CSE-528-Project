using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private PlayerController pc;

    public float moveSpeed = 2f;
    public float attackRange = 1.5f;

    private Transform playerTransform;
    private NavMeshAgent agent;

void Start()
{
    currentHealth = maxHealth;
    agent = GetComponent<NavMeshAgent>();
    playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    pc = playerTransform.GetComponent<PlayerController>();
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
}
