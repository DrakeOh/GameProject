using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab of the enemy to spawn
    public GameObject bossPrefab; // Prefab of the boss to spawn
    public float spawnRadius = 5f; // Radius within which the enemy can spawn
    public float initialEnemyHealth = 100f; // Initial health of enemies
    public int healthIncreasePerWave = 5; // Health increase per wave
    public int numberOfEnemies; // Number of normal enemies per wave
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

    public void SpawnBoss(GameObject bossPrefab)
    {
        StartCoroutine(SpawnBossCoroutine());
    }

    IEnumerator SpawnWave(int waveNumber)
    {
        spawningWave = true;

        // Increase enemy health for this wave
        currentEnemyHealth += healthIncreasePerWave * waveNumber;

        // Spawn normal enemies
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy(enemyPrefab);
            yield return new WaitForSeconds(1);
        }

        spawningWave = false;
    }

    IEnumerator SpawnBossCoroutine()
    {
        yield return new WaitForSeconds(1f); // Wait a little before spawning boss

        // Calculate a random angle within a full circle
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);

        // Calculate random coordinates within the spawn radius
        float randomX = spawnRadius * Mathf.Cos(randomAngle);
        float randomY = spawnRadius * Mathf.Sin(randomAngle);

        Vector3 spawnPosition = transform.position + new Vector3(randomX, randomY, 0);

        // Spawn the boss at the calculated position
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }

    void SpawnEnemy(GameObject enemy)
    {
        if (enemy == null)
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
        GameObject enemyGameObject = Instantiate(enemy, spawnPosition, Quaternion.identity);

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
