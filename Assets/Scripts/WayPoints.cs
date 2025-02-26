using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class WayPoints : MonoBehaviour
{
    public Enemy m_Enemy;
    public Health hp;
    private float speed; // Movement speed of the enemy
    public float reachThreshold = 0.2f; // Distance threshold to consider target reached
    public string targetTag; // Tag of the target to find
    public LayerMask excludeLayer;

    private Transform target;

    void Start()
    {
        m_Enemy = GetComponent<Enemy>();
        hp = GetComponent<Health>();
        speed = m_Enemy.speed;


        // Find the target in the scene


        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }


    }

    void Update()
    {
        if (target == null) return;

        // Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        ModifyOverrideLayers();
    }

    void ModifyOverrideLayers()
    {
        float excludeLayerdistance =  Vector3.Distance(transform.position, target.position);
        if (excludeLayerdistance <= 1f)
        {
               Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            rb2d.excludeLayers = excludeLayer.value;

        }

    }
}


