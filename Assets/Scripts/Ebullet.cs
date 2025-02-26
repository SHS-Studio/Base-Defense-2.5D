using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ebullet : MonoBehaviour
{
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindObjectOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Baricate")
        {
            coll.gameObject.GetComponent<Health>().TakeDamage(enemy.damage);
            Destroy(gameObject);
        }

    }

      
}
