using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners; // Array of enemy spawners
    public float timeBetweenWaves; // Time between each wave
    public float timeBeforeBoss; // Time before the boss appears
    public Text waveTimerText; // Reference to the UI text element for the wave timer
    public Text bossTimerText; // Reference to the UI text element for the boss timer
    public GameObject bossPrefab; // Reference to the boss prefab

    private int currentWave = 0;
    private float waveTimer;
    private float bossTimer;
    private bool bossSpawned = false;

    void Start()
    {   
        StartCoroutine(StartWaveTimer());
        StartCoroutine(StartBossTimer());
    }

    IEnumerator StartWaveTimer()
    {
        waveTimer = timeBetweenWaves;
        while (true)
        {
            UpdateWaveTimerUI();
            yield return new WaitForSeconds(1f);
            waveTimer -= 1f;
            if (waveTimer <= 0f)
            {
                waveTimer = timeBetweenWaves;
                StartNextWave();
            }
        }
    }

    IEnumerator StartBossTimer()
    {
        bossTimer = timeBeforeBoss;
        while (true)
        {
            UpdateBossTimerUI();
            yield return new WaitForSeconds(1f);
            bossTimer -= 1f;
            if (bossTimer <= 0f)
            {
                bossTimer = timeBeforeBoss;
                bossSpawned = true;
                SpawnBoss();
            }
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

    void SpawnBoss()
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.SpawnBoss(bossPrefab);
        }
    }

    void UpdateWaveTimerUI()
    {
        if (waveTimerText != null)
        {
            waveTimerText.text = "Time Until Next Wave: " + Mathf.CeilToInt(waveTimer).ToString();
        }
    }

    void UpdateBossTimerUI()
    {
        if (bossTimerText != null)
        {
            bossTimerText.text = "Time Until Boss: " + Mathf.CeilToInt(bossTimer).ToString();
        }
    }
}
