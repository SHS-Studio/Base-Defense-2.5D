using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float MaxHp = 100;
    public float CurntHp;
    public TextMeshProUGUI hpText; // Reference to TextMeshPro UI element

    void Start()
    {
        CurntHp = MaxHp;
        UpdateHPText();
    }

    void Update()
    {
        MaxHp = CurntHp;
        Die();
        UpdateHPText(); // Update HP text each frame
    }

    public void TakeDamage(float damage)
    {
        CurntHp -= damage;
        CurntHp = Mathf.Clamp(CurntHp, 0, MaxHp); // Prevent HP from going below 0 or above MaxHp
        UpdateHPText();
    }

    void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + CurntHp.ToString("0") + "/" + MaxHp.ToString("0");
        }
    }

    public void Die()
    {
        if (CurntHp <= 0)
        {
           Destroy(gameObject, 2f);
        }
    }
}
