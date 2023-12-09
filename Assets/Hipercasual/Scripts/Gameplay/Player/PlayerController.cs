using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    public GameObject InGameUI;
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
    public float shootingCoolDown = 8f;
    public float shootingDamage;
    public bool shotOnCooldown = false;

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

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "KillingFog":
                Debug.Log("ded");
                health--;
                MeMori();
                break;
            case "LightHouse":
                SceneManager.LoadScene("BaseScene");
                //PauseUI.Pause();
                break;
            default:
                break;
        }
    }

    private void MeMori()
    {
        if (health == 0)
        {
            InGameUI.SetActive(false);
            Destroy(this.gameObject);
            //PauseUI.Pause();
            GameOverUI.SetActive(true);
            _ScoreManager.GameEndProcess();
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        switch (collisionInfo.gameObject.tag)
        {
            case "Enemy":
                health --;
                healthBar.SetHealth(health);
                MeMori();
                break;

            case "Coin": _ScoreManager.IncreaseScore("gameScore", 100);
                health ++;
                Destroy(collisionInfo.gameObject);
                Instantiate(CoinParticle, this.transform.position, this.transform.rotation);
                Instantiate(CoinSfx, this.transform.position, this.transform.rotation);
                break;

            case "Bullet":
                health --;
                MeMori();
                break;

            default: Debug.Log(collisionInfo.gameObject.tag);
                break;
        }
    }

    public void ShootEnemy()
    {
        if (shotOnCooldown == false)
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

            shotOnCooldown = true;

            StartCoroutine(WaitforCooldown());
        }
        
    }

    private IEnumerator WaitforCooldown()
    {
        yield return new WaitForSeconds(shootingCoolDown);
        shotOnCooldown = false;
    }
}
