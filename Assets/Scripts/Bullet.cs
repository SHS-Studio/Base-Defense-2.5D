using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   public Card  card;
    // Start is called before the first frame update
    void Start()
    {
        card = GameObject.FindObjectOfType<Card>();
    }

    // Update is called once per frame
    void Update()
    {
       // Destroy(gameObject,2f);
    }

  
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<Health>().TakeDamage(card.Damage);
            Destroy(gameObject);
        }
    }
}
