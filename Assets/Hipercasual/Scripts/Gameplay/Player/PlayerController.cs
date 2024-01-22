using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] float maxSpeed;
    [SerializeField] float rotationSpeed;

    [Header("UI Reference")]
    public GameObject GameOverUI;
    public GameObject InGameUI;
    [SerializeField] PauseMenu PauseUI;
    [SerializeField] ScoreManager _ScoreManager;
    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject aimButtonYes;
    [SerializeField] GameObject aimButtonNo;
    [SerializeField] GameObject normalShootingButton;
    [SerializeField] GameObject aimingShootingButton;
    [SerializeField] GameObject repairButton;
    
    [Header("FX")]
    public GameObject CoinParticle;
    public GameObject CoinSfx;
    public GameObject ExplosionSfx;
    public GameObject RepairdedSfx;

    [Header("ShootingSystem")]
    public GameObject bulletPrefab;
    public GameObject explosion;
    public Transform cannonPointR;
    public Transform cannonPointL;
    public Transform cannonPointF;
    public Transform aimSpawnPoint;
    public float bulletSpeed = 10;
    public float shootingCoolDown;
    public float shootingDamage;
    public bool shotOnCooldown = false;

    [Header("RepairSystem")]
    public float repairingCooldown;
    public bool isRepairing;
    public int personel;

    public bool isTurningRight;
    public bool isTurningLeft;

    public bool isAiming;
    public GameObject aimCamera;
    public GameObject freeCamera;

    // Start is called before the first frame update
    void Start()
    {
        health = playerStats.baseHealth; //+ shop upgrade health
        healthBar.SetMaxHealth(health);
        attackSpeed = playerStats.baseAttackSpeed; //+ shop upgrade
        movementSpeed = playerStats.baseMovementSpeed;//+ shop upgrade
        rotationSpeed = playerStats.baseRotationSpeed;

        maxSpeed = movementSpeed + 1;

        input = GetComponent<PlayerInput>();

        isAiming = false;
        isRepairing = false;

        shootingCoolDown = 16 - (PlayerPrefs.GetInt("savedPeople") / 2);

        personel = PlayerPrefs.GetInt("savedPeople");
        repairingCooldown = 45 - personel;

        //rb.AddRelativeForce(Vector3.forward * movementSpeed);
    }

    private void Update()
    {
        //KeyboardAndGamepad();
        int randomPeopleLost = Random.Range(0, 5);

        if (isTurningLeft)
        {
            transform.Rotate(0f, rotationSpeed * 0 - rotationSpeed * Time.deltaTime, 0f, Space.Self);
        }
        if (isTurningRight)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.Self);
        }

        Repairing();
    }

    void FixedUpdate()
    {
            //ForwardMovement
            rb.velocity = (transform.forward * movementSpeed);
            rb.velocity.Normalize();

            //RotateMovement
            //float value = input.actions["Move"].ReadValue<float>();
            //transform.Rotate(0, value * 2, 0, Space.Self);
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
                SceneManager.LoadScene("TrailerBase");
                //PauseUI.Pause();
                break;
            case "Coin":
                _ScoreManager.IncreaseScore("gameScore", 100);
                health++;
                Destroy(other.gameObject);
                Instantiate(CoinParticle, this.transform.position, this.transform.rotation);
                Instantiate(CoinSfx, this.transform.position, this.transform.rotation);
                break;
            case "EnemyBullet":
                health--;
                Instantiate(explosion, this.transform.position, this.transform.rotation);
                Instantiate(ExplosionSfx, this.transform.position, this.transform.rotation);
                MeMori();
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
            case "Coin": _ScoreManager.IncreaseScore("gameScore", 100);
                health ++;
                Destroy(collisionInfo.gameObject);
                Instantiate(CoinParticle, this.transform.position, this.transform.rotation);
                Instantiate(CoinSfx, this.transform.position, this.transform.rotation);
                break;

            case "EnemyBullet":
                health--;
                Instantiate(explosion, this.transform.position, this.transform.rotation);
                Instantiate(ExplosionSfx, this.transform.position, this.transform.rotation);
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

    public void Acelerar()
    {
        if (movementSpeed != maxSpeed)
        {
            movementSpeed ++;
        }
    }

    public void Frenar()
    {
        if (movementSpeed > 0)
        {
            movementSpeed --;
        }
    }

    public void RightDown()
    {
        isTurningRight = true;
    }
    public void RightUp()
    {
        isTurningRight = false;
    }
    public void LeftDown()
    {
        isTurningLeft = true;
    }
    public void LeftUp()
    {
        isTurningLeft = false;
    }

    public void Repair()
    {
        if (isRepairing == false)
        {
            isRepairing = true;
        }
    }

    public void Repairing()
    {
        if(isRepairing == true)
        {
            isAiming = false;
            freeCamera.SetActive(true);
            aimCamera.SetActive(false);
            aimButtonNo.SetActive(false);
            aimButtonYes.SetActive(false);
            normalShootingButton.SetActive(false);
            aimingShootingButton.SetActive(false);
            repairButton.SetActive(false);

            if (repairingCooldown > 1)
            {
                repairingCooldown -= Time.deltaTime;
                health++;
            }
            else if(repairingCooldown < 1)
            {
                isRepairing = false;
                freeCamera.SetActive(true);
                aimCamera.SetActive(false);
                aimButtonNo.SetActive(false);
                aimButtonYes.SetActive(true);
                normalShootingButton.SetActive(true);
                aimingShootingButton.SetActive(false);
                Instantiate(RepairdedSfx, this.transform.position, this.transform.rotation);
            }
        }
    }

    public void AimYes()
    {
        Debug.Log("apuntado");
        isAiming = true;
        freeCamera.SetActive(false);
        aimCamera.SetActive(true);
        aimButtonYes.SetActive(false);
        aimButtonNo.SetActive(true);
        normalShootingButton.SetActive(false);
        aimingShootingButton.SetActive(true);
    }

    public void AimNo()
    {
        Debug.Log("desapuntado");
        isAiming = false;
        freeCamera.SetActive(true);
        aimCamera.SetActive(false);
        aimButtonNo.SetActive(false);
        aimButtonYes.SetActive(true);
        normalShootingButton.SetActive(true);
        aimingShootingButton.SetActive(false);
    }

    public void AimShoot()
    {
        if (shotOnCooldown == false)
        {
            Instantiate(explosion, aimSpawnPoint.position, aimSpawnPoint.rotation);
            Instantiate(ExplosionSfx, aimSpawnPoint.position, aimSpawnPoint.rotation);
            var aimBullet = Instantiate(bulletPrefab, aimSpawnPoint.position, aimSpawnPoint.rotation);
            aimBullet.GetComponent<Rigidbody>().velocity = aimSpawnPoint.forward * bulletSpeed;
            Debug.Log("Aimed Shoot");

            shotOnCooldown = true;
            StartCoroutine(WaitforCooldown());
        }
    }

    /*public void KeyboardAndGamepad()
    {
        if (Input.GetKey(KeyCode.A))
        {
            LeftDown();
        }
        else LeftUp();

        if (Input.GetKey(KeyCode.D))
        {
            RightDown();
        }
        else RightUp();

        if (Input.GetKeyDown(KeyCode.W))
        {
            Acelerar();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Frenar();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) & isAiming == false)
        {
            AimYes();
        }
        else if(Input.GetKeyDown(KeyCode.LeftShift) & isAiming == true)
        {
            AimNo();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isAiming == true)
        {
            AimShoot();
        }
    }*/

    private IEnumerator WaitforCooldown()
    {
        yield return new WaitForSeconds(shootingCoolDown);
        shotOnCooldown = false;
    }
}
