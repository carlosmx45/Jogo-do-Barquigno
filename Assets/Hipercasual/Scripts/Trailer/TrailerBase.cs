using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerBase : MonoBehaviour
{
    [SerializeField] GameObject pubLv1;
    [SerializeField] GameObject pubLv2;
    [SerializeField] GameObject anchorLv1;
    [SerializeField] GameObject anchorLv2;
    [SerializeField] GameObject updatePubParticle;
    [SerializeField] GameObject updateAnchorParticle;
    [SerializeField] GameObject transitionSprite;
    [SerializeField] GameObject updateSound;
    [SerializeField] GameObject transitionP1Sound;

    [SerializeField] GameObject camera1;
    [SerializeField] GameObject camera2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdatePub();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            UpdateAnchor();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ActivateTransitionP1();
        }

        void UpdatePub()
        {
            pubLv1.SetActive(false);
            pubLv2.SetActive(true);
            updatePubParticle.SetActive(true);
            Instantiate(updateSound, this.transform.position, this.transform.rotation);
        }

        void UpdateAnchor()
        {
            anchorLv1.SetActive(false);
            anchorLv2.SetActive(true);
            updateAnchorParticle.SetActive(true);
            Instantiate(updateSound, this.transform.position, this.transform.rotation);
        }

        void ActivateTransitionP1()
        {
            Instantiate(transitionP1Sound, this.transform.position, this.transform.rotation);
            camera1.SetActive(false);
            camera2.SetActive(true);
            transitionSprite.SetActive(true);
        }
    }
}
