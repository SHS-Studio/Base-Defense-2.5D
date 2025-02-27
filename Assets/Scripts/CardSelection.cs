using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardSelection : MonoBehaviour
{
    public List<string> selectedCards = new List<string>(); // List of selected cards
    public List<Button> btnlist = new List<Button>();

    public Color selectedColor = Color.green; // Selected card color
    public Color defaultColor = Color.white;  // Default color

    // Function to toggle TMP button selection
    public void ToggleCardSelection(string cardName)
    {
      
        if (selectedCards.Contains(cardName) )
        {
            selectedCards.Remove(cardName);
        }
        else
        {
            selectedCards.Add(cardName);
        }
    }
    // Function to store selected TMP buttons and enter the game
    public void EnterGame()
    {
        if (selectedCards.Count > 0)
        {
            string selectedCardsString = string.Join(",", selectedCards);
            PlayerPrefs.SetString("SelectedCards", selectedCardsString);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Level1");
        }
    }

    public void selectedbutton(Button btn)
    {
        if (btnlist.Contains(btn))
        {
            ColorBlock cb = btn.colors;
            cb.normalColor = defaultColor;
            cb.selectedColor = defaultColor;
            btn.colors = cb;
            btnlist.Remove(btn);
            
        }
        else
        {
            btnlist.Add(btn);
            ColorBlock cb = btn.colors;
            cb.normalColor = selectedColor;
            cb.selectedColor = selectedColor;
            btn.colors = cb;
        }
       
    }
}