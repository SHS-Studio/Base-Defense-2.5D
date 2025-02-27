using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public WaveGeneration waveGeneration;
    public GameObject Baricade;
    public int maxLevel = 10;
    public int CurrentLevel = 1;
    //public int totalEnemies;
    public int enemiesDefeated;

    void Start()
    {
        Baricade = GameObject.FindGameObjectWithTag("Baricate");
        waveGeneration = GameObject.FindObjectOfType<WaveGeneration>();
        LoadProgress(); // Load saved level progress
    }

    public void Update()
    {
        WiningLogic();
        GameOver();
    }
    public void WiningLogic()
    {

        Health hp = Baricade.GetComponent<Health>();
        int curntwave = waveGeneration.currentWave;
        int totalwave = waveGeneration.totalWaves;


        if (hp.MaxHp > 0 && curntwave == totalwave && waveGeneration.totalenemy == enemiesDefeated) // Ensuring player wins only when the last wave is cleared
        {
           // CurrentLevel++;
            SaveProgress();
            LoadNextLevel();
        }
    }
    public void GameOver()
    {
        Health hp = Baricade.GetComponent<Health>();
        int curntwave = waveGeneration.currentWave;

        if (hp.MaxHp <= 0 && curntwave >= 1)
        {
            // string currentScene = SceneManager.GetActiveScene().name; // Get the current scene name

            ResetGame();
            LoadProgress();

        }
    }
    void LoadNextLevel()
    {
        if (CurrentLevel > maxLevel)
        {
            SceneManager.LoadScene("Level" + (CurrentLevel + 1).ToString());
        }

    }
    void SaveProgress()
    {
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel +1);
        PlayerPrefs.Save();
    }

    void LoadProgress()
    {
        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
    }


    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("CardSelection");
    }
    [ContextMenu("resetsavedata")]
    private void resetsavedata()
    {
        PlayerPrefs.SetInt("CurrentLevel", 1);

    }
    
}