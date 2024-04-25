using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    public static int totalEnemyCount = 0; // Static variable to keep track of total enemy count
    public int maxHealth = 10;
    public int current_health = 10;
    private GameObject[] items;
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;
    public float wanderingRadius = 5f;  // Radius for random wandering
    public float updateInterval = 1f;   // How often to update the destination

    private Transform playerTransform;
    private Vector3 destination;
    private float timer;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        timer = updateInterval;

        // Increase total enemy count when enemy is spawned
        totalEnemyCount++;
    }

    void Update()
    {
        animator.SetFloat("EnemyX", destination.x);
        animator.SetFloat("EnemyY", destination.y);
        if (current_health <= 0)
        {
            DestroyEnemyAndDropItem();
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                UpdateDestination();
                timer = updateInterval;
            }

            MoveTowardsDestination();
        }
    }

    private void OnDestroy()
    {
        // Decrease total enemy count when enemy is destroyed
        totalEnemyCount--;
    }

    private void DestroyEnemyAndDropItem()
    {
        //  int rand = Random.Range(0, items.Length);  // Ensure using items length here
        // GameObject item = items[rand];
        // Instantiate(item, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            current_health -= 10;
            Debug.Log("Enemy Health: " + current_health);
        }
    }

    private void UpdateDestination()
    {
        destination = Random.insideUnitSphere * wanderingRadius + playerTransform.position;
    }

    private void MoveTowardsDestination()
    {
        Vector3 direction = (destination - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
