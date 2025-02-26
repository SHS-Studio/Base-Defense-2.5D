using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSpawner : MonoBehaviour
{
    public GameObject[] cardPrefabs; // Array of possible card prefabs
    public Transform[] spawnPoints; // Empty bases where cards will spawn

    void Start()
    {
        string savedCards = PlayerPrefs.GetString("SelectedCards", "");
        if (!string.IsNullOrEmpty(savedCards))
        {
            string[] selectedCardNames = savedCards.Split(','); // Convert string back to array
            SpawnCards(selectedCardNames);
        }
    }

    void SpawnCards(string[] cardNames)
    {
        for (int i = 0; i < cardNames.Length && i < spawnPoints.Length; i++)
        {
            GameObject cardPrefab = FindCardPrefab(cardNames[i]);
            if (cardPrefab != null)
            {
                GameObject cardInstance = Instantiate(cardPrefab, spawnPoints[i].position , Quaternion.identity);

                // Set as child of spawn point
                cardInstance.transform.SetParent(spawnPoints[i], true);

            }
        }
    }

    GameObject FindCardPrefab(string cardName)
    {
        foreach (GameObject card in cardPrefabs)
        {
            if (card.name == cardName)
                return card;
        }
        return null;
    }
}