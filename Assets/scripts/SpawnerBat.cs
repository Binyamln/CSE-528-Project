using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBat : MonoBehaviour
{
    [SerializeField] private float interval = 1f;
    [SerializeField] private GameObject[] enemies;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(interval);

        while (true)
        {
            yield return wait;

            // Check the total number of enemies currently on the map using the EnemyController class
            int totalEnemies = EnemyController.totalEnemyCount;

            
            if (totalEnemies < 30)
            {
                int rand = Random.Range(0, enemies.Length);
                GameObject enemy = enemies[rand];
                Instantiate(enemy, transform.position, Quaternion.identity);
            }
        }
    }
}
