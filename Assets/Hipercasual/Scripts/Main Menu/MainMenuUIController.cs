using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuUIController : MonoBehaviour
{
    //Referencias a Prefabs de UI
    [Tooltip("Referencia a interfaz de menu de inicio")]
    [SerializeField] GameObject UI_Menu;

    [Tooltip("Referencia a interfaz de tienda")]
    [SerializeField] GameObject UI_Shop;

    [Tooltip("Referencia a interfaz de Inventario")]
    [SerializeField] GameObject UI_Inventory;

    [Tooltip("Referencia a interfaz de los logros")]
    [SerializeField] GameObject UI_Achievements;

    [Tooltip("Referencia a interfaz de las Expediciones")]
    [SerializeField] GameObject UI_Expeditions;

    [Tooltip("Referencia a interfaz del PUB")]
    [SerializeField] GameObject UI_Pub;

    [Tooltip("Referencia a interfaz de confirmacion de salida")]
    [SerializeField] GameObject UI_ExitConfirm;

    //func//Activar paneles UI
    public void GoToUIPanel(string name){
        //Ocultar todos los paneles menos el seleccionado
        switch (name)
        {
            case "Menu":
                UI_Menu.SetActive(true);
                UI_Shop.SetActive(false);
                UI_Inventory.SetActive(false);
                UI_Achievements.SetActive(false);
                UI_Expeditions.SetActive(false);
                UI_Pub.SetActive(false);
                UI_ExitConfirm.SetActive(false);
                MenuRayCastController.instance.apagalo = false;
                Debug.Log("Active Menu Panel");
                break;
            case "Shop":
                UI_Menu.SetActive(false);
                UI_Shop.SetActive(true);
                UI_Inventory.SetActive(false);
                UI_Achievements.SetActive(false);
                UI_Expeditions.SetActive(false);
                UI_Pub.SetActive(false);
                UI_ExitConfirm.SetActive(false);
                Debug.Log("Active Shop UI Panel");
                break;
            case "Inventory":
                UI_Menu.SetActive(false);
                UI_Shop.SetActive(false);
                UI_Inventory.SetActive(true);
                UI_Achievements.SetActive(false);
                UI_Expeditions.SetActive(false);
                UI_Pub.SetActive(false);
                UI_ExitConfirm.SetActive(false);
                Debug.Log("Active Inventory UI Panel");
                break;
            case "Achievements":
                UI_Menu.SetActive(false);
                UI_Shop.SetActive(false);
                UI_Inventory.SetActive(false);
                UI_Achievements.SetActive(true);
                UI_Expeditions.SetActive(false);
                UI_Pub.SetActive(false);
                UI_ExitConfirm.SetActive(false);
                Debug.Log("Active Achievements UI Panel");
                break;
            case "Expeditions":
                UI_Menu.SetActive(false);
                UI_Shop.SetActive(false);
                UI_Inventory.SetActive(false);
                UI_Achievements.SetActive(false);
                UI_Expeditions.SetActive(true);
                UI_Pub.SetActive(false);
                UI_ExitConfirm.SetActive(false);
                Debug.Log("Active Expeditions UI Panel");
                break;
            case "Pub":
                UI_Menu.SetActive(false);
                UI_Shop.SetActive(false);
                UI_Inventory.SetActive(false);
                UI_Achievements.SetActive(false);
                UI_Expeditions.SetActive(false);
                UI_Pub.SetActive(true);
                UI_ExitConfirm.SetActive(false);
                Debug.Log("Active Expeditions UI Panel");
                break;
            case "ExitConfirm":
                UI_Menu.SetActive(false);
                UI_Shop.SetActive(false);
                UI_Inventory.SetActive(false);
                UI_Achievements.SetActive(false);
                UI_Expeditions.SetActive(false);
                UI_Pub.SetActive(false);
                UI_ExitConfirm.SetActive(true);
                Debug.Log("Active Exit Confirm UI Panel");
                break;
            default:
                Debug.Log("Invalid UI panel");
                break;
        }
    }

    //func//Carga de escenas
    public void LoadScene(string name){
        SceneManager.LoadScene(name);
    }
    
}
