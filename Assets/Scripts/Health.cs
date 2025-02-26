using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHp = 100;
    public float CurntHp = 100;
    // Start is called before the first frame update
    void Start()
    {
        CurntHp = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        MaxHp = CurntHp;
        Die();
    }

    public void TakeDamage(float damage)
    {
        CurntHp -= damage;
    }

    public void Die()
    {
        if (MaxHp <= 0)
        {
            Destroy(gameObject,2f);
        }
    }
}
