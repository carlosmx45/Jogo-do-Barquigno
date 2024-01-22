using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public FixedTouchField TouchField;

    protected Rigidbody Rigidbody;

    protected float CameraAngleY;
    protected float CameraAngleSpeed = 0.1f;
    protected float CameraPosY;
    protected float CameraPosSpeed = 0.1f;

    public PlayerController playerController;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(playerController.isAiming == false)
        {
            var vel = Quaternion.AngleAxis(CameraAngleY + 180, Vector3.up);

            Rigidbody.velocity = new Vector3(vel.x, Rigidbody.velocity.y, vel.z);
            //transform.rotation = Quaternion.AngleAxis(CameraAngleY + Vector3.SignedAngle(Vector3.forward, input.normalized, Vector3.up), Vector3.up);

            CameraAngleY += TouchField.TouchDist.x * CameraAngleSpeed;

            Camera.main.transform.position = transform.position + Quaternion.AngleAxis(CameraAngleY, Vector3.up) * new Vector3(0, 3, 8);
            Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - Camera.main.transform.position, Vector3.up);
        }
    }
}
