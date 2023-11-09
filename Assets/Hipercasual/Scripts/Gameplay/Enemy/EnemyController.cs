using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Efectos")]
    public GameObject Explosion;
    public GameObject debris;
    public GameObject Sfx;

    [Tooltip("Reference to the player's rigidbody")]
    [SerializeField] Rigidbody rb;

    [SerializeField] CharacterInfo enemyStats;

    [SerializeField] GameObject player;
    

    [SerializeField] int health;
    [SerializeField] int attackSpeed;
    [SerializeField] float movementSpeed;


    [SerializeField] bool TakingDamage_IsRunning = false;
         
    // Start is called before the first frame update
    void Start()
    {
        health = enemyStats.baseHealth;
        attackSpeed = enemyStats.baseAttackSpeed; 
        movementSpeed = enemyStats.baseMovementSpeed;
        //Set Force
        rb.AddRelativeForce(Vector3.forward * movementSpeed);
        player = GameObject.Find("_Player");
    }

    void Update()
    {
        if (health <= 0)
        {
            Instantiate(Explosion, this.transform.position, this.transform.rotation);
            Instantiate(Sfx, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
            PlayerPrefs.SetInt("enemiesDefeated", PlayerPrefs.GetInt("enemiesDefeated") + 1);
            PlayerPrefs.SetInt("gameScore", PlayerPrefs.GetInt("gameScore") + 25);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = Vector3.MoveTowards(this.transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        this.transform.position = movement;

        this.transform.LookAt(player.transform);
        
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        switch (collisionInfo.gameObject.tag)
        {
            case "Player": Destroy(this.gameObject);
                Instantiate(Explosion, this.transform.position, this.transform.rotation);
                Instantiate(Sfx, this.transform.position, this.transform.rotation);
                break;
            case "Bullet": health--;
                Instantiate(Explosion, this.transform.position, this.transform.rotation);
                break;
            default: //Debug.Log(collisionInfo.gameObject.tag);
                break;
        }
    }

    void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                if (TakingDamage_IsRunning == false)
                {
                    StartCoroutine(TakingDamage());
                }
                break;
             default:
                break;

        }
    }

    IEnumerator TakingDamage()
    {
        TakingDamage_IsRunning = true;
        health--;
        Instantiate(debris, this.transform.position, this.transform.rotation);
        //Debug.Log("Damage tick on" + this.gameObject.name);
        yield return new WaitForSeconds(1);
        TakingDamage_IsRunning = false;
    }

}
