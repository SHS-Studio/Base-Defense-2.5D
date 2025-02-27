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

    public LayerMask targetLayer;

    public GameObject projectilePrefab; // The projectile to spawn
    public Transform target; // The player or target object
    public float attackRange = 10f; // Distance at which the enemy starts attacking
    public float spawnInterval = 2f; // Time between spawns
    public Transform spawnPoint; // Where the projectile spawns from
    public float Bulletforce; // Where the projectile spawns from

    private Coroutine attackCoroutine;

    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Baricate").transform;
    }

    // Update is called once per frame
    void Update()
    {
        DetectTargetAndAttack();
    }
    void DetectTargetAndAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange, targetLayer);
        bool targetInRange = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.transform == target)
            {
                targetInRange = true;
                break;
            }
        }

        if (targetInRange && !isAttacking)
        {
            attackCoroutine = StartCoroutine(SpawnProjectiles());
            isAttacking = true;
        }
        else if (!targetInRange && isAttacking)
        {
            StopCoroutine(attackCoroutine);
            isAttacking = false;
        }
    }

    IEnumerator SpawnProjectiles()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(spawnPoint.up * Bulletforce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void OnDisable()
    {
       GameLogic Logic =  GameObject.FindObjectOfType<GameLogic>();
        Logic.enemiesDefeated++;
    }
}
 
