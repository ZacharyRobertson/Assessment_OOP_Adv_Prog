using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public Camera cam;
    public GameObject scoreText;

    private Rigidbody rigid;
    // Use this for initialization
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
        //Spawn the text that will display the players score
        GameObject clone = Instantiate(scoreText);
        Text score = clone.GetComponent<Text>();

        //if we are not the localPlayer
        if(!isLocalPlayer)
        {
            //Set a new Color variable
            Color color = score.color;
            //Make the alpha of the color transparent so that we do not see any other player's score overlapping our
            color.a = 0.1f;
            score.color = color;
        }
    }
}
