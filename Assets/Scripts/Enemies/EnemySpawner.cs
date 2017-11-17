using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    delegate void EnemyTypeFunc(int amount);
    private List<EnemyTypeFunc> enemyTypeFuncs = new List<EnemyTypeFunc>();

    public Transform target;
    public GameObject ghostPrefab;
    public GameObject zombiePrefab;
    public int minAmount = 0, maxAmount = 2;
    public float spawnRate = 1f;

    private bool hasSpawned;
    int randomAmount;
    int enemyType;
    // Use this for initialization
    void Start()
    {
        enemyTypeFuncs.Add(SpawnGhost);
        enemyTypeFuncs.Add(SpawnZombie);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSpawned == false)
        {
            StartCoroutine(Spawn());
        }
    }

    void SpawnGhost(int amount)
    {
        Instantiate(ghostPrefab, transform.position, transform.rotation, this.transform);
    }
    void SpawnZombie(int amount)
    {
        Instantiate(zombiePrefab, transform.position, transform.rotation, this.transform);

    }
    IEnumerator Spawn()
    {
        hasSpawned = true;
        //Set our random amount once per frame
        randomAmount = Random.Range(minAmount, maxAmount);
        enemyType = Random.Range(0, enemyTypeFuncs.Count);
        //Call the delegate function
        enemyTypeFuncs[enemyType](randomAmount);
        yield return new WaitForSeconds(spawnRate);
        hasSpawned = false;
    }
}

