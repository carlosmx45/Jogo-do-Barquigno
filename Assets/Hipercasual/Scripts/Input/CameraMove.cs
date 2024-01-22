using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 Origin;
    private Vector3 Difference;
    private Vector3 ResetCamera;

    private bool drag = false;

    void Start()
    {
        ResetCamera = Camera.main.transform.position;
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if(Input.GetMouseButton(0))
        {
            Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if(drag == false)
            {
                drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            drag = false;
        }

        if (drag)
        {
            Camera.main.transform.position = Origin - Difference;
            Camera.main.transform.position = new Vector3(
            Mathf.Clamp(Camera.main.transform.position.x, -138, -134),
            Mathf.Clamp(Camera.main.transform.position.y, 39, 39),
            Mathf.Clamp(Camera.main.transform.position.z, -29, -25));
        }
    }
}
