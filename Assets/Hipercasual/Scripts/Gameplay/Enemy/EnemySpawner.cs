using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject rootGmObj;
    [SerializeField] Transform[] enemySpawnPoints;

    [SerializeField] GameObject enemyPrefab;

    [SerializeField] byte enemyDelay;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnPoints = rootGmObj.GetComponentsInChildren<Transform>();

        enemyDelay = 5;
        InvokeRepeating("DecreaseTimer", 0.0f, 60.0f);
        StartCoroutine("SpawnEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
    }

    void DecreaseTimer(){
        enemyDelay--;
    }

    IEnumerator SpawnEnemy()
    {
        foreach (Transform child in enemySpawnPoints){
            switch (child.name)
            {
                case "EnemySpawnPoints": 
                    break;
                default:
                Instantiate(enemyPrefab, child.transform.position, child.transform.rotation);
                    break;
            }
            yield return new WaitForSeconds(enemyDelay);
        }
        StartCoroutine("SpawnEnemy");
        yield return null;
    }
}
