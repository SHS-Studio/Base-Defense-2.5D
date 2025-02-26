using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public enum EnemyType
{
    A,
    b, c, d, e, f
} 
public class Enemy : MonoBehaviour
{ 
    public EnemyType type;
    public float damage;
    public float speed;


    public GameObject projectilePrefab; // The projectile to spawn
    public Transform target; // The player or target object
    public float attackRange = 10f; // Distance at which the enemy starts attacking
    public float spawnInterval = 2f; // Time between spawns
    public Transform spawnPoint; // Where the projectile spawns from

    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Baricate").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }


    public void Attack()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= attackRange && !isAttacking)
        {
            StartCoroutine(SpawnProjectiles());
            isAttacking = true;
        }
        else if (distance > attackRange && isAttacking)
        {
            StopCoroutine(SpawnProjectiles());
            isAttacking = false;
        }

    }
    IEnumerator SpawnProjectiles()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            // Add force or behavior to the projectile if needed
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
