using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatsIsOcean, whatIsPlayer;

    [Header("Efectos")]
    public GameObject explosion;
    public GameObject debris;
    public GameObject explosionSfx;

    [SerializeField] CharacterInfo enemyStats;

    public GameObject bulletPrefab;
    public Transform aiSpawnPoint;
    public float bulletSpeed = 50;
    
    [SerializeField] int health;

    public AroundSpawner aroundSpawner;
    
    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Start()
    {
        aroundSpawner = GameObject.Find("EnemySpawnPoints").GetComponent<AroundSpawner>();
        health = enemyStats.baseHealth;
    }

    private void Awake()
    {
        player = GameObject.Find("_Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

        if (health <= 0)
        {
            //Instantiate(explosion, this.transform.position, this.transform.rotation);
            //Instantiate(explosionSfx, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
            PlayerPrefs.SetInt("enemiesDefeated", PlayerPrefs.GetInt("enemiesDefeated") + 1);
            PlayerPrefs.SetInt("gameScore", PlayerPrefs.GetInt("gameScore") + 25);
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        switch (collisionInfo.gameObject.tag)
        {
            case "Bullet":
                health--;
                Instantiate(explosion, this.transform.position, this.transform.rotation);
                Instantiate(explosionSfx, this.transform.position, this.transform.rotation);
                break;
            default: //Debug.Log(collisionInfo.gameObject.tag);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Bullet":
                health--;
                Instantiate(explosion, this.transform.position, this.transform.rotation);
                Instantiate(explosionSfx, this.transform.position, this.transform.rotation);
                break;
            default:
                break;
        }
    }

    private void Patrolling()
    {
        Debug.Log("Patrolling");
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walckpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    void SearchWalkPoint()
    {
        Debug.Log("SearchWalkPoint");
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        Debug.Log("Cordenada X: " + randomX + "cordenadaZ: " + randomZ);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatsIsOcean))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        Debug.Log("Chase Player");
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        Debug.Log("Attack");
        //make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Instantiate(explosion, aiSpawnPoint.position, aiSpawnPoint.rotation);
            Instantiate(explosionSfx, aiSpawnPoint.position, aiSpawnPoint.rotation);
            var aimBullet = Instantiate(bulletPrefab, aiSpawnPoint.position, aiSpawnPoint.rotation);
            aimBullet.GetComponent<Rigidbody>().velocity = aiSpawnPoint.forward * bulletSpeed;
            Debug.Log("enemy Shooted");
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
