using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    delegate void EnemyTypeFunc(int amount);
    private List<EnemyTypeFunc> enemyTypeFuncs = new List<EnemyTypeFunc>();
    public GameManager gManager;
    public Transform target, oldTarget;
    public GameObject ghostPrefab;
    public GameObject zombiePrefab;
    public List<Player> players = new List<Player>();
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
        gManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // If we have a target
        if (target != null)
        {
            //IF We haven't spawned recently
            if (hasSpawned == false)
            {
                //Spawn an enemy
                StartCoroutine(Spawn());
            }
        }
        else
        {
            //Otherwise Set a new target
            SetTarget();
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
        oldTarget = target;
        yield return new WaitForSeconds(spawnRate);
        hasSpawned = false;
        target = null;
    }


    void SetTarget()
    {
        //Make sure we access the current list of players based on the Game Manager
        players = gManager.playerList;
        //IF we have no target
        if (target == null)
        {
            int targetValue = Random.Range(0, players.Count);

            if (players[targetValue].transform == oldTarget)
            {
                //Set a case that we reset in case there is only one active player
                oldTarget = null;
                //try again
                SetTarget();
            }
            else
            {
                //Set the new Target
                target = players[targetValue].transform;
            }
        }
    }
}


