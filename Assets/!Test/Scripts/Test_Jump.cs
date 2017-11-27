using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Jump : Test
{
    [Header("Test Parameters")]
    public float minHeight = 1f;

    private PlayerController player;
    private float originalY;
    private float jumpApex;
    void Start()
    {
        player = GetComponent<PlayerController>();
        //Set our original Y
        originalY = transform.position.y;
    }
    protected override void Simulate()
    {
        //simulate Jump
        player.Jump();
    }
    protected override void Check()
    {
        //Get the currnet player y position
        float playerY = transform.position.y;
        float height = playerY - originalY;
        //If the height is over the jumpApex
        if(height > jumpApex)
            jumpApex = height;
        //If the jumpApex is higher than minHeight
        if(jumpApex > minHeight)
            IntegrationTest.Pass(gameObject);
    }
    
}
