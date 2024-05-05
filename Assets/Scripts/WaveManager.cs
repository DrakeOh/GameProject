using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners; // Array of enemy spawners
    public float timeBetweenWaves; // Time between each wave
    public Text timerText; // Reference to the UI text element for the timer

    private int currentWave = 0;
    private float waveTimer;

    void Start()
    {
        StartCoroutine(StartWaves());
    }

    IEnumerator StartWaves()
    {
        while (true)
        {
            waveTimer = timeBetweenWaves;
            while (waveTimer > 0)
            {
                yield return new WaitForSeconds(1f);
                waveTimer -= 1f;
                UpdateTimerUI();
            }
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

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time Until Next Wave: " + Mathf.CeilToInt(waveTimer).ToString();
        }
    }
}
