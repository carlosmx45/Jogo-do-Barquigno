using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAI : MonoBehaviour
{
    [SerializeField] GameObject victoryUI;
    [SerializeField] GameObject inGameUI;

    private byte attackDelay;
    public byte iaVelocity;

    private Animator aiAnimator;

    // Start is called before the first frame update
    void Start()
    {
        aiAnimator = GetComponent<Animator>();

        attackDelay = iaVelocity;
        StartCoroutine("AIAttack");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetStates();

            Debug.Log("IA Defiende");
            aiAnimator.SetBool("Block", true);
        }
    }

    IEnumerator AIAttack()
    {
        ResetStates();
        attackDelay = iaVelocity;

        int rand = Random.Range(1, 9);
        switch (rand)
        {
            case 1: Debug.Log("IA Ataca Hacia Adelante");
                aiAnimator.SetBool("NormalJabs", true);
                break;
            case 2: Debug.Log("IA Ataca Hacia la Izquierda");
                aiAnimator.SetBool("TurnLeft", true);
                aiAnimator.SetBool("HitLeft", true);
                break;
            case 3: Debug.Log("IA Ataca Hacia la Derecha");
                aiAnimator.SetBool("TurnRight", true);
                aiAnimator.SetBool("HitRight", true);
                break;
            case 4:
                Debug.Log("IA Defiende");
                aiAnimator.SetBool("Block", true);
                break;
            case 5:
                Debug.Log("IA Utiliza Uppercut");
                aiAnimator.SetBool("NormalJabs", true);
                aiAnimator.SetBool("Eppercut", true);
                attackDelay = 1;
                break;
            case 6:
                Debug.Log("IA No Hace Nada");
                break;
            case 7:
                Debug.Log("IA Se Mueve A La Derecha");
                aiAnimator.SetBool("TurnRight", true);
                attackDelay = 1;
                break;
            case 8:
                Debug.Log("IA Se Mueve A La Izquierda");
                aiAnimator.SetBool("TurnLeft", true);
                attackDelay = 1;
                break;
        }
        yield return new WaitForSeconds(attackDelay);
        StartCoroutine("AIAttack");
        yield return null;
    }

    public void KO()
    {
        aiAnimator.SetBool("KO", true);
        aiAnimator.SetBool("TurnRight", false);
        aiAnimator.SetBool("TurnLeft", false);
        aiAnimator.SetBool("HitLeft", false);
        aiAnimator.SetBool("HitRight", false);
        aiAnimator.SetBool("NormalJabs", false);
        aiAnimator.SetBool("Eppercut", false);
        aiAnimator.SetBool("Block", false);

        victoryUI.SetActive(true);
        inGameUI.SetActive(false);
    }

    public void ResetStates()
    {
        aiAnimator.SetBool("TurnRight", false);
        aiAnimator.SetBool("TurnLeft", false);
        aiAnimator.SetBool("HitLeft", false);
        aiAnimator.SetBool("HitRight", false);
        aiAnimator.SetBool("NormalJabs", false);
        aiAnimator.SetBool("Eppercut", false);
        aiAnimator.SetBool("Block", false);
        Debug.Log("Estados de la IA Reiniciados");
    }
}
