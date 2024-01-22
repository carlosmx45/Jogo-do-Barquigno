using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SmallMenuControllerScript : MonoBehaviour
{
    private Animator cameraAnimator;

    public GameObject uiUpMenu;
    public GameObject uiDownMenu;

    // Start is called before the first frame update
    void Start()
    {
        cameraAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Arriba()
    {
        cameraAnimator.SetBool("movedTo2", true);
        cameraAnimator.SetBool("movedTo1", false);
        uiDownMenu.SetActive(false);
        uiUpMenu.SetActive(true);
    }
    public void Abajo()
    {
        cameraAnimator.SetBool("movedTo2", false);
        cameraAnimator.SetBool("movedTo1", true);
        uiDownMenu.SetActive(true);
        uiUpMenu.SetActive(false);
    }

    public void GoToAdventure()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void GoToBase()
    {
        SceneManager.LoadScene("TrailerBase");
    }
}
