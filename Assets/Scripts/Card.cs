using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Card : MonoBehaviour
{
    public Draggable dragCard;
    public Detection[] ray;

    public float elixercost;
    public float speed;

    public GameObject[] DetectionRay;
    public Transform[] BulletSpawnPoints;

    public GameObject Bullet;
    public int refillbulletamnt;
    public int BulletCount;
    public float Bulletforce;
    public float Damage;

    public float firingtime; // Time interval between shots (editable in Inspector)
    public float shootTimer;

    public SpriteRenderer render;


    

    
     
    // Start is called before the first frame update
    void Start()
    {
        dragCard = GetComponent<Draggable>();
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        InBase();
        ReturnToBase();
        StartShooting();
        Attack();
    }

    public void StartShooting()
    {
        if(dragCard.isPlaced)
        {
            foreach (GameObject ray in DetectionRay)
            {
                if (BulletCount > 0)
                {
                    ray.SetActive(true);
                }
                else
                {
                    ray.SetActive(false);
                }
            }
            
        }

    }
    
    public void ReturnToBase()
    {
        if(BulletCount == 0 )
        {
            transform.position = Vector3.MoveTowards(transform.position, dragCard.startPosition, 
                speed * Time.deltaTime);
           render.flipY = true;
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    public void InBase()
    {
        if (transform.position == GetComponent<Draggable>().startPosition)
        {
            dragCard.isPlaced = false;
            BulletCount = refillbulletamnt;
            GetComponent<PolygonCollider2D>().enabled = true;
            render = GetComponent<SpriteRenderer>();
            render.flipY = false;
        }
    }

    public void Attack()
    {
        bool enemyDetected = false;

        foreach (Detection locater in ray)
        {
            if (locater != null && locater.IsEnemyDetected)
            {
                enemyDetected = true;
                break; // Exit loop if any detector detects an enemy
            }
        }

        if (enemyDetected && BulletCount > 0 && shootTimer >= firingtime)
        {
            foreach (Transform spawnpoint in BulletSpawnPoints)
            {
                if (BulletCount > 0) // Ensure bullets only spawn if available
                {
                    GameObject BulletClone = Instantiate(Bullet, spawnpoint.position, Quaternion.identity);
                    BulletClone.GetComponent<Rigidbody2D>().AddForce(spawnpoint.up * Bulletforce, ForceMode2D.Impulse);
                    BulletCount--;
                }
            }

            shootTimer = 0; // Reset timer only after all bullets are fired
        }
    }

    

}
