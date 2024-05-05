using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners; // Array of enemy spawners
    public float timeBetweenWaves = 10f; // Time between each wave

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(StartWaves());
    }

    IEnumerator StartWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            StartNextWave();
        }
    }

    void StartNextWave()
    {
        currentWave++;
        Debug.Log("Starting Wave " + currentWave);
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.StartWave(currentWave);
        }
    }
}
