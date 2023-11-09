using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementProcess : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Initialize PlayerPref Data Keys
            //Minute Data
        if (PlayerPrefs.HasKey("MinutesInGame") == false)
        {
            PlayerPrefs.SetInt("MinutesInGame", 0);
        }

        //Invoke Repeating every minute to save MinuteData
        InvokeRepeating("SaveMinuteData", 60.0f, 60.0f);
        Debug.Log("Started Timer");


            //Enemies Defeated
        if (PlayerPrefs.HasKey("enemiesDefeated") == false)
        {
            PlayerPrefs.SetInt("enemiesDefeated", 0);
        }


    }

    //CancelInvokes  Call on GameOver
    public void StopInvokeMinuteData(){
        CancelInvoke("SaveMinuteData");
    }

    //Save Minute Data
    void SaveMinuteData(){
        PlayerPrefs.SetInt("MinutesInGame", PlayerPrefs.GetInt("MinutesInGame") + 1);
        Debug.Log("Saved Data!" + PlayerPrefs.GetInt("MinutesInGame"));
        
    }
}