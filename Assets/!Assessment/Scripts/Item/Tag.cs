using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag : Item
{
    //The transform of the firePoint
    public Transform firePoint;
    //The ray we will fire to send our shot information
    private Ray ray;
    //The item range
    public float range = 30;
    //Item damage
    public int damage = 1;

    public override void UseItem()
    {
        //Send our ray forward from the firePoint
        ray = new Ray(firePoint.position, Vector3.forward);
        //Store the information our ray hits
        RaycastHit hit;
        //If we fire out a ray and check for itemMask
        if (Physics.Raycast(ray, out hit, range))
        {
            Projectile p = SpawnProjectile(firePoint.position, firePoint.rotation);
            p = p.GetComponent<SmallProjectile>();
            p.Fire(ray.direction);
            //Spawn the bullet on the client
            //Check for a health component
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            //Make sure we have a health script to reference
            if (enemyHealth != null)
            {
                Debug.Log("Hit an Enemy");
                enemyHealth.TakeDamage(damage, player);
            }
        }
    }
}
