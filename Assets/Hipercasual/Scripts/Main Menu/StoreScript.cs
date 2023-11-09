using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
    public CharacterInfo characterInfo;

    public Text imprimirDinero;

    public void BuyInStore(string name)
    {
        //Ocultar todos los paneles menos el seleccionado
        switch (name)
        {
            case "Speed":
                if(PlayerPrefs.GetInt("savedScore") >= 500)
                {
                    characterInfo.baseMovementSpeed++;
                    PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 500);
                    Debug.Log("Compraste Velocidad");
                }
                else
                {
                    Debug.Log("Dinero Insuficiente");
                }
                break;
            case "Shield":
                if (PlayerPrefs.GetInt("savedScore") >= 500)
                {
                    characterInfo.baseHealth++;
                    PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 500);
                    Debug.Log("Compraste Vida");
                }
                else
                {
                    Debug.Log("Dinero Insuficiente");
                }
                break;
            case "Damage":
                if (PlayerPrefs.GetInt("savedScore") >= 500)
                {
                    characterInfo.baseAttackSpeed++;
                    PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 500);
                    Debug.Log("Compraste Daño");
                }
                else
                {
                    Debug.Log("Dinero Insuficiente");
                }
                break;
            default:
                Debug.Log("Opcion Invalida");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        imprimirDinero.text = "Your Money: $" + PlayerPrefs.GetInt("savedScore");
    }
}
