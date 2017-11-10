using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag : Item {

    public override void Use()
    {
        // Set a new variable that spawns a projectile
        Projectile p = SpawnProjectile(transform.position, transform.rotation);
        //Set a direction to fire
        Vector3 direction = (Vector3.forward);
        // Call the Use method from p and Pass Direction
        p.Use(direction);
    }

    //public Vector3 GetDir(float angle)
    //{
    //    Vector3 dir = Vector3.
    //}
}
