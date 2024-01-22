using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlayerController : MonoBehaviour
{
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            playerAnimator.SetBool("TurnRight", true);
        } else if (Input.GetKeyUp(KeyCode.D))
        {
            playerAnimator.SetBool("TurnRight", false);
        }

        
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerAnimator.SetBool("TurnLeft", true);
        } else if (Input.GetKeyUp(KeyCode.A))
        {
            playerAnimator.SetBool("TurnLeft", false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnimator.SetBool("Block", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnimator.SetBool("Block", false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerAnimator.SetBool("HitLeft", true);
            playerAnimator.SetBool("HitRight", true);
            playerAnimator.SetBool("NormalJabs", true);
        } else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            playerAnimator.SetBool("HitLeft", false);
            playerAnimator.SetBool("HitRight", false);
            playerAnimator.SetBool("NormalJabs", false);
            Debug.Log("Jabs OFF");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.SetBool("Eppercut", true);
        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            playerAnimator.SetBool("Eppercut", false);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ResetStates();
        }

    }

    public void KO()
    {
        playerAnimator.SetBool("KO", true);
        playerAnimator.SetBool("TurnRight", false);
        playerAnimator.SetBool("TurnLeft", false);
        playerAnimator.SetBool("HitLeft", false);
        playerAnimator.SetBool("HitRight", false);
        playerAnimator.SetBool("Eppercut", false);
        playerAnimator.SetBool("Block", false);
    }

    public void ResetStates()
    {
        playerAnimator.SetBool("TurnRight", false);
        playerAnimator.SetBool("TurnLeft", false);
        playerAnimator.SetBool("HitLeft", false);
        playerAnimator.SetBool("HitRight", false);
        playerAnimator.SetBool("Eppercut", false);
        playerAnimator.SetBool("Block", false);
        playerAnimator.SetBool("KO", false);
        Debug.Log("Estados Reiniciados");
    }
}
