using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab of the enemy to spawn
    public float spawnRadius = 5f; // Radius within which the enemy can spawn
    public float initialEnemyHealth = 100f; // Initial health of enemies
    public int healthIncreasePerWave = 5; // Health increase per wave
    public int NumberOfEnmeies;
    private bool spawningWave = false;
    private float currentEnemyHealth;

    void Start()
    {
        currentEnemyHealth = initialEnemyHealth;
        
    }

    public void StartWave(int waveNumber)
    {
        if (!spawningWave)
        {
            StartCoroutine(SpawnWave(waveNumber));
        }
    }

    IEnumerator SpawnWave(int waveNumber)
    {
        spawningWave = true;

        // Increase enemy health for this wave
        currentEnemyHealth += healthIncreasePerWave * waveNumber;

        // Spawn a wave of enemies
        for (int i = 0; i < NumberOfEnmeies; i++) // Adjust 5 to the number of enemies per wave
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }

        spawningWave = false;
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is not assigned!");
            return;
        }

        // Calculate a random angle within a full circle
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);

        // Calculate random coordinates within the spawn radius
        float randomX = spawnRadius * Mathf.Cos(randomAngle);
        float randomY = spawnRadius * Mathf.Sin(randomAngle);

        Vector3 spawnPosition = transform.position + new Vector3(randomX, randomY, 0);

        // Spawn the enemy at the calculated position with adjusted health
        GameObject enemyGameObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Get the Enemy component from the spawned enemy game object
        Enemy enemyComponent = enemyGameObject.GetComponent<Enemy>();

        // Check if the Enemy component exists
        if (enemyComponent != null)
        {
            // Set the health of the spawned enemy
            enemyComponent.maxHealth += healthIncreasePerWave;
        }
        else
        {
            Debug.LogWarning("Enemy component not found on the spawned enemy game object.");
        }
    }
}
