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
    public string remoteLayerName = "RemotePlayer";
    public Vector3 spawnPos;
    private Health health;
    public GameManager gManager;
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
        if (!isLocalPlayer)
        {
            AssignRemoteLayer();
        }

        //Register player on the network
        Registerplayer();
    }

    void Update()
    {
        health.CheckHealth();
    }

    // Register the player's id on the network
    void Registerplayer()
    {
        //Get the id from the network identity component
        string ID = "Player " + GetComponent<NetworkIdentity>().netId;
        this.name = ID; // assign new ID to name
        gManager.playerList.Add(this); //Assign the player to the gManager list to record score
    }
    //Assign remote layer to current GameObject (if it is not a local player)
    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }
}
