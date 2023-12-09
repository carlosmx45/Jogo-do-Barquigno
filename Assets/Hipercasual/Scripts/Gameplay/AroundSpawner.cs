using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject rootGmObj;
    [SerializeField] Transform[] enemySpawnPoints;

    [SerializeField] GameObject islandPrefab;
    [SerializeField] GameObject coinPrefab;

    [SerializeField] byte spawnDelay;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnPoints = rootGmObj.GetComponentsInChildren<Transform>();

        spawnDelay = 5;
        StartCoroutine("SpawnObject");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
    }

    IEnumerator SpawnObject()
    {
        foreach (Transform child in enemySpawnPoints){
            switch (child.name)
            {
                case "EnemySpawnPoints": 
                    break;
                default:
                    int rand = Random.Range(1,4);
                    //Debug.Log(rand);
                    switch (rand)
                    {
                        case 1: Debug.Log("Nothing Happened"); break;//SInstantiate(islandPrefab, child.transform.position, child.transform.rotation); /*Debug.Log("SpawnIsland");*/ break;
                        case 2: Instantiate(coinPrefab, child.transform.position, child.transform.rotation); /*Debug.Log("SpawnCoin");*/ break;
                        case 3: Instantiate(coinPrefab, child.transform.position, child.transform.rotation); /*Debug.Log("SpawnCoin");*/ break;
                    }
                    break;
            }
            yield return new WaitForSeconds(spawnDelay);
        }
        StartCoroutine("SpawnObject");
        yield return null;
    }
}
