using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{

    public float destroyTime = 5f;
    // Update is called once per frame
    void Update()
    {
        Destroy(transform.gameObject, destroyTime);
    }
}
