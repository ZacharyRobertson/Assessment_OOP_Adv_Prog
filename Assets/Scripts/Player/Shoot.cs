using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shoot : NetworkBehaviour
{
    //The transform of the firePoint
    public Transform firePoint;
    //The ray we will fire to send our shot information
    private Ray ray;

    public Item item;
    private Player player;
    // Use this for initialization
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //If we are not the local client
        if(!isLocalPlayer)
        {
            //Do nothing and return
            return;
        }
        //Send our ray forward from the firePoint
        ray = new Ray(firePoint.position, Vector3.forward);


    }
}
