using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject rootGmObj;
    [SerializeField] Transform[] enemySpawnPoints;

    [SerializeField] GameObject islandPrefab;
    [SerializeField] GameObject coinPrefab;

    [SerializeField] GameObject enemyPrefab;

    [SerializeField] byte spawnDelay;
    [SerializeField] GameObject player;

    public int enemyInGame;
    public int maxEnemyInGame = 10;

    // Start is called before the first frame update
    void Start()
    {
        enemyInGame = 1;
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
                    int rand = Random.Range(1,6);
                    //Debug.Log(rand);
                    switch (rand)
                    {
                        case 1: Debug.Log("Nothing Happened"); break;//SInstantiate(islandPrefab, child.transform.position, child.transform.rotation); /*Debug.Log("SpawnIsland");*/ break;
                        case 2: Instantiate(coinPrefab, child.transform.position, child.transform.rotation); Debug.Log("SpawnCoin"); break;
                        case 3: Debug.Log("Nothing Happened"); break;
                        case 4:
                            if (enemyInGame < maxEnemyInGame)
                            {
                                Instantiate(enemyPrefab, child.transform.position, child.transform.rotation);
                                enemyInGame++;
                                Debug.Log("EnemySpawned");
                            }
                            else Debug.Log("MaxEnemyInGame");
                            break;
                        case 5: Debug.Log("Nothing Happened"); break;
                    }
                    break;
            }
            yield return new WaitForSeconds(spawnDelay);
        }
        StartCoroutine("SpawnObject");
        yield return null;
    }
}
