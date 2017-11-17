using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Health : MonoBehaviour
{
    public int health = 3;
    public GameManager gameManager;
    private int respawnCount = 3;

    private Player player;
    // Use this for initialization
    void Awake()
    {
        player = GetComponent<Player>();
        gameManager = FindObjectOfType<GameManager>();
    }
    public void CheckHealth()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (respawnCount > 0)
        {
            Respawn();
        }
        else
        {
            gameManager.GameOver();
        }

    }
    void Respawn()
    {
        respawnCount--;
        transform.position = player.spawnPos;
    }
}
