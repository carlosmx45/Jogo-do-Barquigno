using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuRayCastController : MonoBehaviour
{
    public static MenuRayCastController instance;

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


    public bool apagalo;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(apagalo)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("raycast");

            // Check if the ray hits the player
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Ship"))
            {
                UI_Menu.SetActive(false);
                UI_Shop.SetActive(false);
                UI_Inventory.SetActive(false);
                UI_Achievements.SetActive(false);
                UI_Expeditions.SetActive(false);
                UI_Pub.SetActive(false);
                UI_ExitConfirm.SetActive(true);
                apagalo = true;
                Debug.Log("Active Exit Confirm UI Panel");
            }
            else if(Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Store"))
            {
                UI_Menu.SetActive(false);
                UI_Shop.SetActive(true);
                UI_Inventory.SetActive(false);
                UI_Achievements.SetActive(false);
                UI_Expeditions.SetActive(false);
                UI_Pub.SetActive(false);
                UI_ExitConfirm.SetActive(false);
                apagalo = true;
                Debug.Log("Active Shop UI Panel");
            }
            else if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("SafeBox"))
            {
                UI_Menu.SetActive(false);
                UI_Shop.SetActive(false);
                UI_Inventory.SetActive(true);
                UI_Achievements.SetActive(false);
                UI_Expeditions.SetActive(false);
                UI_Pub.SetActive(false);
                UI_ExitConfirm.SetActive(false);
                apagalo = true;
                Debug.Log("Active Inventory UI Panel");
            }
            else if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("CaptainHouse"))
            {
                UI_Menu.SetActive(false);
                UI_Shop.SetActive(false);
                UI_Inventory.SetActive(false);
                UI_Achievements.SetActive(true);
                UI_Expeditions.SetActive(false);
                UI_Pub.SetActive(false);
                UI_ExitConfirm.SetActive(false);
                apagalo = true;
                Debug.Log("Active Achievements UI Panel");
            }
            else if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("LightHouse"))
            {
                UI_Menu.SetActive(false);
                UI_Shop.SetActive(false);
                UI_Inventory.SetActive(false);
                UI_Achievements.SetActive(false);
                UI_Expeditions.SetActive(true);
                UI_Pub.SetActive(false);
                UI_ExitConfirm.SetActive(false);
                apagalo = true;
                Debug.Log("Active Expeditions UI Panel");
            }
            else if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Pub"))
            {
                UI_Menu.SetActive(false);
                UI_Shop.SetActive(false);
                UI_Inventory.SetActive(false);
                UI_Achievements.SetActive(false);
                UI_Expeditions.SetActive(false);
                UI_Pub.SetActive(true);
                UI_ExitConfirm.SetActive(false);
                apagalo = true;
                Debug.Log("Active Expeditions UI Panel");
            }
        }
    }
}
