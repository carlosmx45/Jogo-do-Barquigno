using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintMoneyScript : MonoBehaviour
{
    [Header("Invetnarios")]
    [Tooltip("Referencia a de texto de Dinero")]
    [SerializeField] Text textDinero;
    [Tooltip("Referencia a de texto de Ron")]
    [SerializeField] Text textRon;
    [Tooltip("Referencia a de texto de Gente")]
    [SerializeField] Text textGente;

    // Start is called before the first frame update
    void Start()
    {
        //DeveloperOptions();
    }

    // Update is called once per frame
    void Update()
    {
        textDinero.text = "$" + PlayerPrefs.GetInt("savedScore");
        textRon.text = "" + PlayerPrefs.GetInt("savedRum");
        textGente.text = "" + PlayerPrefs.GetInt("savedPeople");
    }

    //developer only
    void DeveloperOptions()
    {
        //PlayerPrefs.SetInt("savedRum", 0);
        //PlayerPrefs.SetInt("savedPeople", 10);
    }
}
