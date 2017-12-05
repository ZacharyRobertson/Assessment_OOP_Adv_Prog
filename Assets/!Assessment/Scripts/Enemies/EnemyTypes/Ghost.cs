﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{

    public float attackRange = .5f;
    public float meleeSpeed = 20f;
    public float meleeDelay = 0.6f;
    public GameObject attackBox;
    private bool isAttacking = false;

    public EnemySpawner spawner;
    // Use this for initialization
    void Start()
    {
        spawner = GetComponentInParent<EnemySpawner>();
        target = spawner.target;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //If target is within attackRange and we are not attacking
        if (IsCloseToTarget(attackRange) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
        FindNewTarget();
    }
    IEnumerator Attack()
    {
        //During
        isAttacking = true;
        attackBox.SetActive(true);
        behaviourIndex = Behaviour.IDLE;
        yield return new WaitForSeconds(meleeDelay);
        //After
        behaviourIndex = Behaviour.SEEK;
        attackBox.SetActive(false);
        isAttacking = false;

    }
    void FindNewTarget()
    {
        //SET a reference for all colliders within a 5 unit radius sphere
        Collider[] playerCol = Physics.OverlapSphere(transform.position, 5);
        //Loop through the colliders
        for (int i = 0; i < playerCol.Length; i++)
        {
            //Find the first Player
            if(playerCol[i].tag == "Player")
            {
                //Set the new target
                target = playerCol[i].transform;
            }
        }
    }
}
