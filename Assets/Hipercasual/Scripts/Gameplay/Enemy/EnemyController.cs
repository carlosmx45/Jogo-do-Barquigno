using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Tooltip("Reference to the player's rigidbody")]
    [SerializeField] Rigidbody rb;
    [SerializeField] CharacterInfo enemyStats;
    [SerializeField] float movementSpeed;

    void Start()
    {
        movementSpeed = enemyStats.baseMovementSpeed;
        rb.AddRelativeForce(Vector3.forward * movementSpeed);
    }
    void FixedUpdate()
    {
        rb.velocity = (transform.forward * movementSpeed);
    }
}
