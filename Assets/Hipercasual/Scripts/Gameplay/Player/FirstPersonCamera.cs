using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public FixedTouchField TouchField;
    public PlayerController playerController;

    protected Rigidbody Rigidbody;

    protected float CameraAngleY;
    protected float CameraAngleSpeed = 0.1f;
    protected float CameraPosY;
    protected float CameraPosSpeed = 0.1f;


    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (playerController.isAiming == true)
        {
            var vel = Quaternion.AngleAxis(CameraAngleY + 180, Vector3.up);

            Rigidbody.velocity = new Vector3(vel.x, Rigidbody.velocity.y, vel.z);

            CameraAngleY += TouchField.TouchDist.x * CameraAngleSpeed;

            Camera.main.transform.rotation = Quaternion.AngleAxis(CameraAngleY, Vector3.up);
        }
    }
}
