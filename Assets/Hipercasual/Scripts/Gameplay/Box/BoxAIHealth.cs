using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAIHealth : MonoBehaviour
{
    [Header("Box AI references")]
    [Tooltip("Reference to the player's rigidbody")]
    [SerializeField] Rigidbody rb;
    [SerializeField] BoxAI boxAI;
    [SerializeField] EnemyHealthBar healthBar;

    public int aiHealth;

    private void Start()
    {
        healthBar.SetMaxHealth(aiHealth);
    }

    private void Update()
    {
        AIDied();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                aiHealth--;
                healthBar.SetHealth(aiHealth);
                AIDied();
                break;
            default:
                break;
        }
    }

    private void AIDied()
    {
        if (aiHealth <= 0)
        {
            boxAI.KO();
        }
    }
}
