using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public Camera cam;
    [SyncVar]
    public float score = 0;
    public Rigidbody rigid;

    public Vector3 spawnPos;
    private Health health;
    // Use this for initialization
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        health = GetComponent<Health>();
        
    }
    void Start()
    {
        spawnPos = transform.position;
    }

    void Update()
    {
        health.CheckHealth();
    }
}
