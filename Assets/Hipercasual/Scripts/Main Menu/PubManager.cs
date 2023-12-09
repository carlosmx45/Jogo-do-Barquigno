using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PubManager : MonoBehaviour
{
    [Header("Invetnarios")]
    [Tooltip("Referencia a de texto de Ron")]
    [SerializeField] Text textGente;
    [SerializeField] Text textScore;

    [Header("Referencias para UI del Pub")]
    [SerializeField] GameObject payForPeople;
    [SerializeField] GameObject adForPeople;
    [SerializeField] GameObject maxPeopleAnouncement;
    [SerializeField] GameObject referenceText;

    public static PubManager Instance;

    public bool isRewarded = false;

    // Update is called once per frame
    void Update()
    {
        if (isRewarded == true)
        {
            PlayerPrefs.SetInt("savedPeople", PlayerPrefs.GetInt("savedPeople") + 1);
            Debug.Log("anuncio dio persona");
            isRewarded = false;
        }

        textGente.text = "" + PlayerPrefs.GetInt("savedPeople");
        textGente.text = "" + PlayerPrefs.GetInt("savedPeople");
        MaxInventory();
    }

    void MaxInventory()
    {
        if (PlayerPrefs.GetInt("savedPeople") >= 30)
        {
            payForPeople.SetActive(false);
            adForPeople.SetActive(false);
            maxPeopleAnouncement.SetActive(true);
            referenceText.SetActive(false);
        }
        if (PlayerPrefs.GetInt("savedPeople") <= 30)
        {
            payForPeople.SetActive(true);
            adForPeople.SetActive(true);
            maxPeopleAnouncement.SetActive(false);
            referenceText.SetActive(true);
        }
    }

    public void BuyCrew()
    {
        if (PlayerPrefs.GetInt("savedScore") >= 100)
        {
            PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 100);
            PlayerPrefs.SetInt("savedPeople", PlayerPrefs.GetInt("savedPeople") + 1);
        }
    }

    public void AdForCrew()
    {
        AdsManager.Instance.rewardedAds.ShowRewardedAd();
        PlayerPrefs.SetInt("savedPeople", PlayerPrefs.GetInt("savedPeople") + 1);
    }
}
