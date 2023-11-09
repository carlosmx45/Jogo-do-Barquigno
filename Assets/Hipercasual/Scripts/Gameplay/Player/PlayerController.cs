using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player references")]
    [Tooltip("Reference to the player's rigidbody")]
    [SerializeField] Rigidbody rb;
    [SerializeField] CharacterInfo playerStats;
    private PlayerInput input;

    [Header("Player Stats")]
    [SerializeField] int health;
    [SerializeField] int attackSpeed;
    [SerializeField] float movementSpeed;

    [Header("UI Reference")]
    public GameObject GameOverUI;
    [SerializeField] PauseMenu PauseUI;
    [SerializeField] ScoreManager _ScoreManager;
    [SerializeField] HealthBar healthBar;
    
    [Header("FX")]
    public GameObject CoinParticle;
    public GameObject CoinSfx;
    public GameObject ExplosionSfx;

    [Header("ShootingSystem")]
    public GameObject bulletPrefab;
    public GameObject explosion;
    public Transform cannonPointR;
    public Transform cannonPointL;
    public Transform cannonPointF;
    public float bulletSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        health = playerStats.baseHealth; //+ shop upgrade health
        healthBar.SetMaxHealth(health);
        attackSpeed = playerStats.baseAttackSpeed; //+ shop upgrade
        movementSpeed = playerStats.baseMovementSpeed;//+ shop upgrade

        input = GetComponent<PlayerInput>();  
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
            //ForwardMovement
            rb.AddRelativeForce(Vector3.forward * movementSpeed);
            rb.velocity = (transform.forward * movementSpeed);
            rb.velocity.Normalize();

            //RotateMovement
            float value = input.actions["Move"].ReadValue<float>();

            transform.Rotate(0, value * 2, 0, Space.Self);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        switch (collisionInfo.gameObject.tag)
        {
            case "Enemy":
                health --;
                healthBar.SetHealth(health);
                if (health == 0)
                {
                    Destroy(this.gameObject);
                    //PauseUI.Pause();
                    GameOverUI.SetActive(true);
                    _ScoreManager.GameEndProcess();
                }
                break;

            case "Coin": _ScoreManager.IncreaseScore("gameScore", 100);
                health ++;
                Destroy(collisionInfo.gameObject);
                Instantiate(CoinParticle, this.transform.position, this.transform.rotation);
                Instantiate(CoinSfx, this.transform.position, this.transform.rotation);
                break;

            case "Bullet":
                health --;
                break;

            default: //Debug.Log(collisionInfo.gameObject.tag);
                break;
        }
    }

    public void ShootEnemy()
    {
        //Cannon R
        Instantiate(explosion, cannonPointR.position, cannonPointR.rotation);
        Instantiate(ExplosionSfx, cannonPointR.position, cannonPointR.rotation);
        var bulletR = Instantiate(bulletPrefab, cannonPointR.position, cannonPointR.rotation);
        bulletR.GetComponent<Rigidbody>().velocity = cannonPointR.forward * bulletSpeed;

        //Cannon L
        Instantiate(explosion, cannonPointL.position, cannonPointL.rotation);
        var bulletL = Instantiate(bulletPrefab, cannonPointL.position, cannonPointL.rotation);
        bulletL.GetComponent<Rigidbody>().velocity = cannonPointL.forward * bulletSpeed;

        //Cannon L
        Instantiate(explosion, cannonPointF.position, cannonPointF.rotation);
        var bulletF = Instantiate(bulletPrefab, cannonPointF.position, cannonPointF.rotation);
        bulletF.GetComponent<Rigidbody>().velocity = cannonPointF.forward * bulletSpeed;
        Debug.Log("Shoot");
    }

}
