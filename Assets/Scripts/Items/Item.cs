using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int damage = 10;
    public int uses = 0;
    public int maxUses = 30;
    public float fireInterval = .2f;
    public GameObject projectilePrefab;

    //Creates a method we can call on another script
    public abstract void Use();
    //Creates the base method we can use to reload
    public virtual void Reload()
    {
        uses = maxUses;
    }
    //Creates the base method to instantiate a projectile
    public Projectile SpawnProjectile(Vector3 pos, Quaternion rot)
    {
        //Instantiate the new projectile
        GameObject clone = Instantiate(projectilePrefab, pos, rot);
        Projectile p = clone.GetComponent<Projectile>();

        //reduce uses by one
        uses--;
        //return the new Projectile
        return p;

    }
}
