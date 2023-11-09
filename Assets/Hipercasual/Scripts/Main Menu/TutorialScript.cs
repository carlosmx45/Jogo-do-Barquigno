using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    //Referencias a Prefabs de UI
    [Tooltip("Referencia a interfaz de Paginas")]
    [SerializeField] GameObject UI_HowToPlay1;
    [SerializeField] GameObject UI_HowToPlay2;
    [SerializeField] GameObject UI_HowToPlay3;
    [SerializeField] GameObject UI_HowToPlay4;


    public void Tutorial(string name)
    {
        //Ocultar todos los paneles menos el seleccionado
        switch (name)
        {
            case "1":
                UI_HowToPlay1.SetActive(true);
                UI_HowToPlay2.SetActive(false);
                UI_HowToPlay3.SetActive(false);
                UI_HowToPlay4.SetActive(false);
                Debug.Log("Pagina 1");
                break;
            case "2":
                UI_HowToPlay1.SetActive(false);
                UI_HowToPlay2.SetActive(true);
                UI_HowToPlay3.SetActive(false);
                UI_HowToPlay4.SetActive(false);
                Debug.Log("Pagina 2");
                break;
            case "3":
                UI_HowToPlay1.SetActive(false);
                UI_HowToPlay2.SetActive(true);
                UI_HowToPlay3.SetActive(false);
                UI_HowToPlay4.SetActive(false);
                Debug.Log("Pagina 3");
                break;
            case "4":
                UI_HowToPlay1.SetActive(false);
                UI_HowToPlay2.SetActive(false);
                UI_HowToPlay3.SetActive(false);
                UI_HowToPlay4.SetActive(true);
                Debug.Log("Pagina 4");
                break;
            default:
                Debug.Log("Invalid UI panel");
                break;
        }
    }
}
