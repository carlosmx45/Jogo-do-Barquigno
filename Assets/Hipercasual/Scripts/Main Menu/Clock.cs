using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public GameObject theDisplay;
    public int hour;
    public int minutes;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hour = System.DateTime.Now.Hour;
        minutes = System.DateTime.Now.Minute;
        theDisplay.GetComponent<Text>().text = "" + hour + ":" + minutes;
    }
}
