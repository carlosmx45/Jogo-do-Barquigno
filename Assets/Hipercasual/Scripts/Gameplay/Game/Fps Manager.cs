using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsManager : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 144;
    }
}
