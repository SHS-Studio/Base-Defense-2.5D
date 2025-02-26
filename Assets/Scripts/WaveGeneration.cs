using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGeneration : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 5 different enemy types
    public Transform[] spawnPoints;   // Define multiple spawn locations
    public int totalWaves = 10;       // Total wave count
    public float timeBetweenWaves = 5f; // Time interval between waves
    private int currentWave = 1;
    int enemyCount;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }
    public void Update()
    {
        AllEnemyKilled();
    }
    IEnumerator SpawnWaves()
    {
        while (currentWave <= totalWaves)
        {
            SpawnWave(currentWave);
            yield return new WaitForSeconds(timeBetweenWaves);
            currentWave++;
        }
    }

    void SpawnWave(int waveNumber)
    {
         enemyCount = 3 + (waveNumber - 1) * 3; // Increase enemy count by 3 each wave
        int enemyTypeCount = Mathf.Min(2 + waveNumber / 2, enemyPrefabs.Length); // Increase enemy types gradually

        for (int i = 0; i < enemyCount; i++)
        {
            int enemyIndex = Random.Range(0, enemyTypeCount); // Select from available enemy types
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Random spawn location
            Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, Quaternion.Euler(0,0,90));
        }

        Debug.Log($"Wave {waveNumber} spawned with {enemyCount} enemies of {enemyTypeCount} types.");
    }

    void AllEnemyKilled()
    {
        if (enemyCount == 0)
        {
            StartCoroutine(SpawnWaves());
        }
    }
}
