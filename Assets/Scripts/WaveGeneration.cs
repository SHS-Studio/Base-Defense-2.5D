using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGeneration : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 5 different enemy types
    public Transform[] spawnPoints;   // Define multiple spawn locations
    public int totalWaves = 10;       // Total wave count
    public float timeBetweenWaves = 5f; // Time interval between waves
    public float timeBetweenEnemies = 0.5f; // Time interval between each enemy spawn
    private int currentWave = 1;
    private int enemyCount;
    private int enemiesRemaining;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        AllEnemiesKilled();
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave <= totalWaves)
        {
            yield return StartCoroutine(SpawnWave(currentWave));
            yield return new WaitForSeconds(timeBetweenWaves);
            currentWave++;
        }
    }

    IEnumerator SpawnWave(int waveNumber)
    {
        enemyCount = 3 + (waveNumber - 1) * 3; // Increase enemy count by 3 each wave
        enemiesRemaining = enemyCount;
        int enemyTypeCount = Mathf.Min(2 + waveNumber / 2, enemyPrefabs.Length); // Increase enemy types gradually

        for (int i = 0; i < enemyCount; i++)
        {
            int enemyIndex = Random.Range(0, enemyTypeCount); // Select from available enemy types
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Random spawn location
            Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, Quaternion.Euler(0, 0, 180));
            yield return new WaitForSeconds(timeBetweenEnemies); // Time interval between spawning each enemy
        }

        Debug.Log($"Wave {waveNumber} spawned with {enemyCount} enemies of {enemyTypeCount} types.");
    }

    public void EnemyKilled()
    {
        enemiesRemaining--;
    }

    void AllEnemiesKilled()
    {
        if (enemiesRemaining <= 0 && currentWave <= totalWaves)
        {
            StopAllCoroutines();
            StartCoroutine(SpawnWaves());
        }
    }
}
