using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //Nota de desarrollador: Usar PlayerPrefs no es nada eficiente ya que puede ser modificado por el jugador en los registros y no es el uso correcto que se le debe dar
    //sin embargo, por la fecha de entrega y nuestro progreso actual, preferimos hacerlo de esta manera, en teoria seria mejor usando variables publicas dentro de este script 
    //o un valor guardado en el jugador.

    Text Score;
    public Text gameOverMoney;

    //Agrega valor a un PlayerPref
    public void IncreaseScore(string key, int _scoreToAdd)
    {
        if (PlayerPrefs.HasKey(key) == true){
            PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + _scoreToAdd);
            Debug.Log("Added to PlayerPrefs: " + key);
        }
            

        else {
            PlayerPrefs.SetInt(key, 0);
            PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + _scoreToAdd);
            Debug.Log("PlayerPrefs key created: " + key);
        }
        Debug.Log("Increased Score");
    }

    //Regresa el valor int de una PlayerPref
    public int GetScore(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }

    void Start()
    {
        //Resetea el score
        PlayerPrefs.SetInt("gameScore", 0);

        //Redundante, borra el score de juego.
        Score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Money: " + GetScore("gameScore");
        gameOverMoney.text = "Money: " + PlayerPrefs.GetInt("savedScore");
    }

    public void GameEndProcess(){ //Esta mamada no jala, especificamente no guarda los valores en el otro playerprefs, suerte Carlos.
    
        //Guarda el score actual en otra PlayerPref para acceder a ella desde la tienda
        IncreaseScore("savedScore", GetScore("gameScore"));
        Debug.Log(PlayerPrefs.GetInt("savedScore"));
        PlayerPrefs.Save();
        //Borra el score actual
    }
}
