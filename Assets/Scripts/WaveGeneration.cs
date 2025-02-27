using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveGeneration : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 5 different enemy types
    public Transform[] spawnPoints;   // Define multiple spawn locations
    public int totalWaves = 10;       // Total wave count per level
    public float baseTimeBetweenWaves = 5f; // Base time interval between waves
    public float baseTimeBetweenEnemies = 0.5f; // Base time interval between each enemy spawn

    public int currentWave = 1;
    private int enemyCount;
    public int enemiesRemaining;

    public TextMeshProUGUI WaveCount; // UI Reference for Wave Display

    private int currentLevel; // Default starting level
    private int maxLevel; // Max Level
    private float difficultyMultiplier = 1.0f; // Difficulty scaling factor


    void Start()
    {
        GameLogic Gm = FindObjectOfType<GameLogic>(); // Get reference from the scene

        if (Gm != null)
        {
            maxLevel = Gm.maxLevel;
            currentLevel = Gm.CurrentLevel;
        }

        LoadProgress(); // Load current level progress
        AdjustDifficulty(); // Adjust difficulty based on level
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        UpdateWaveIndicator();
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave <= totalWaves)
        {
            yield return StartCoroutine(SpawnWave(currentWave));
            yield return new WaitForSeconds(baseTimeBetweenWaves / difficultyMultiplier); // Decrease wave interval with difficulty
            currentWave++;
        }
    }

    IEnumerator SpawnWave(int waveNumber)
    {
        enemyCount = Mathf.RoundToInt((3 + (waveNumber - 1) * 3) * difficultyMultiplier); // More enemies per wave
        enemiesRemaining = enemyCount;
        int enemyTypeCount = Mathf.Min(2 + waveNumber / 2, enemyPrefabs.Length); // Gradually unlock enemy types

        for (int i = 0; i < enemyCount; i++)
        {
            int enemyIndex = Random.Range(0, enemyTypeCount); // Randomly select enemy
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Random spawn location
            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, Quaternion.Euler(0, 0, 180));

            // Scale enemy difficulty
            ScaleEnemy(enemy);

            yield return new WaitForSeconds(baseTimeBetweenEnemies / difficultyMultiplier); // Faster spawn rate
        }

        Debug.Log($"Wave {waveNumber} spawned with {enemyCount} enemies of {enemyTypeCount} types.");
    }

    void ScaleEnemy(GameObject enemy)
    {
        Enemy stats = enemy.GetComponent<Enemy>(); // Assuming enemy has a stats script
        Health hpstats = enemy.GetComponent<Health>();

        if (stats != null && hpstats != null)
        {
            hpstats.MaxHp *= difficultyMultiplier;
            stats.speed *= 1 + (0.05f * currentLevel); // Increase speed slightly each level
        }
    }

    void UpdateWaveIndicator()
    {
        if (WaveCount != null)
        {
            WaveCount.text = "Wave: " + currentWave.ToString("0") + " / Level: " + currentLevel;
        }
    }

    void AdjustDifficulty()
    {
        difficultyMultiplier = 1.0f + (currentLevel - 1) * 0.2f; // Difficulty scales up every level
        baseTimeBetweenEnemies = Mathf.Max(0.2f, baseTimeBetweenEnemies - (0.05f * currentLevel)); // Reduce spawn time
    }

    void SaveProgress()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.Save();
    }

    void LoadProgress()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
        else
        {
            currentLevel = 1;
        }
    }
}