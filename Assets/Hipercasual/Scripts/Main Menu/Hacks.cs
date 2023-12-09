using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            KonamiCode();
        }
    }

    public void KonamiCode()
    {
        PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") + 500);
    }
}
