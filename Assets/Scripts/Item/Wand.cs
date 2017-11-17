using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Item
{
    //The transform of the firePoint
    public Transform firePoint;
    //The ray we will fire to send our shot information
    private Ray ray;
    //The layer affected by our Ray
    public LayerMask itemMask;
    //The item range
    public float range = 100;
    //Item damage
    public int damage = 5;

    public override void UseItem()
    {
        //Send our ray forward from the firePoint
        ray = new Ray(firePoint.position, Vector3.forward);
        //Store the information our ray hits
        RaycastHit hit;
        //If we fire out a ray and check for itemMask
        if (Physics.Raycast(ray, out hit, range, itemMask))
        {
            //Check for a health component
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            //Make sure we have a health script to reference
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage, player);
            }
        Projectile p = SpawnProjectile(firePoint.position, firePoint.rotation);
        p.Fire(ray.direction);
        }
        else
        {
            Projectile p = SpawnProjectile(firePoint.position, firePoint.rotation);
            p.Fire(Vector3.forward,3);
        }
    }
}
