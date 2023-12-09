using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpeditionsManager : MonoBehaviour
{
    [Header("Objetos de UI")]
    [SerializeField] GameObject expeditionButton1;
    [SerializeField] GameObject expeditionButton2;
    [SerializeField] GameObject expeditionButton3;

    [Header("Objetos de Timer")]
    public float timer1 = -10;
    public Text timer1Text;
    public float timer2 = -10;
    public Text timer2Text;
    public float timer3 = -10;
    public Text timer3Text;

    public bool expedition1Completed;
    public bool expedition2Completed;
    public bool expedition3Completed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer1 -= Time.deltaTime;
        timer1Text.text = "" + timer1.ToString("f0");
        timer2 -= Time.deltaTime;
        timer2Text.text = "" + timer2.ToString("f0");
        timer3 -= Time.deltaTime;
        timer3Text.text = "" + timer3.ToString("f0");

        if (timer1 <= 1 && timer1 >= -1)
        {
            expedition1Completed = true;
            if (expedition1Completed == true)
            {
                expeditionButton1.SetActive(true);
                PlayerPrefs.SetInt("savedRum", PlayerPrefs.GetInt("savedRum") + 10);
                timer1 = -5;
                expedition1Completed = false;
            }
        }
        if (timer2 <= 1 && timer2 >= -1)
        {
            expedition2Completed = true;
            if (expedition2Completed == true)
            {
                expeditionButton2.SetActive(true);
                PlayerPrefs.SetInt("savedRum", PlayerPrefs.GetInt("savedRum") + 10);
                timer2 = -5;
                expedition2Completed = false;
            }
        }
        if (timer3 <= 1 && timer3 >= -1)
        {
            expedition3Completed = true;
            if (expedition3Completed == true)
            {
                expeditionButton3.SetActive(true);
                PlayerPrefs.SetInt("savedRum", PlayerPrefs.GetInt("savedRum") + 10);
                timer3 = -5;
                expedition3Completed = false;
            }
        }
    }

    public void Expedition1()
    {
        if (PlayerPrefs.GetInt("savedScore") >= 300)
        {
            PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 300);
            expeditionButton1.SetActive(false);
            timer1 = 60;
        }
    }

    public void Expedition2()
    {
        if (PlayerPrefs.GetInt("savedScore") >= 300)
        {
            PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 300);
            expeditionButton2.SetActive(false);
            timer2 = 60;
        }
    }

    public void Expedition3()
    {
        if (PlayerPrefs.GetInt("savedScore") >= 300)
        {
            PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 300);
            expeditionButton3.SetActive(false);
            timer3 = 60;
        }
    }
}
