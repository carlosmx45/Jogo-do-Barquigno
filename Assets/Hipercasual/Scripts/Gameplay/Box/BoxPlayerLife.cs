using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlayerLife : MonoBehaviour
{
    [Header("Player references")]
    [Tooltip("Reference to the player's rigidbody")]
    [SerializeField] Rigidbody rb;
    [SerializeField] CharacterInfo playerStats;
    [SerializeField] BoxPlayerController boxPlayerController;

    [Header("Player Stats")]
    public int boxPlayerHealth;

    [Header("UI Reference")]
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject InGameUI;
    [SerializeField] GameObject PauseUI;
    [SerializeField] HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(boxPlayerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        MeMori();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                boxPlayerHealth--;
                healthBar.SetHealth(boxPlayerHealth);
                MeMori();
                break;
            default:
                break;
        }
    }

    private void MeMori()
    {
        if (boxPlayerHealth <= 0)
        {
            boxPlayerController.KO();
            Debug.Log("ded");
            InGameUI.SetActive(false);
            GameOverUI.SetActive(true);
        }
    }
}
