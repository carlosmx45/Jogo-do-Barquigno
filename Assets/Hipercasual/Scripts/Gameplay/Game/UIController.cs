using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject joystickUI;

    public GameObject PauseButtonUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            
        { Debug.Log("the game is paused");
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        joystickUI.SetActive(true);
        PauseButtonUI.SetActive(true);
        Time.timeScale = 1f;;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        joystickUI.SetActive(false);
        PauseButtonUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Quitting Game...");
        SceneManager.LoadScene("Main");
    }

    public void PlayButtonSound()
    {
        //Aqui on que se reproduzca un sonido
    }

    public void UpdatePlayerHealth()
    {
        //Cada que el jugador sea dañado, llamar esta funcion para actualizar su barra de vida
    }

    public void UpdateEnemyHealth(/*Aqui podrias meter algo para referenciar al enemigo, como un Gameobject y ingresas 'this.gameObject*/)
    {
        //Cada que el enemigo sea dañado, llamar esta funcion para actualizar su barra de vida
        //Necesita referencias individuales, suerte
    }

    public void EndGame()
    {
        //Time scale set to 0
        //Display GameOver Screen
    }

    public void RestartScene()
    {
        //Restart current scene by name
    }
}

