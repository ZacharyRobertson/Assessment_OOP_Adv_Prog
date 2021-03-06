﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : Projectile
{
    public int explosionDamage = 10;
    public float explosionRadius = 5f;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Fire(Vector3 direction, float? speed = null)
    {
        float currentSpeed = this.speed;
        // Check if speed has been used
        if (speed != null)
        {
            currentSpeed = speed.Value;
        }
        direction = transform.rotation * direction;
        direction = direction.normalized * currentSpeed * Time.deltaTime;
        // Fire in that direction
        rigid.MovePosition(transform.position + direction);
    }

    void Explode()
    {
        //Destroy this gameObject
        Destroy(gameObject);

        //Damage enemies in range
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider col in objectsInRange)
        {
            //Create a reference to an enemy script
            EnemyHealth enemy = col.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(explosionDamage, null);
            }
        }
    }
    protected override void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            Explode();
            Destroy(gameObject);
        }
    }
}
