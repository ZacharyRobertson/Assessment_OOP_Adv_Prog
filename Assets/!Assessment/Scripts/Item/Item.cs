using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    //The projectilePrefab we will fire
    public GameObject projectile;
    //The amount of uses before the weapon needs to reload
    public float maxUses = 50;
    public float currentUses;
    //The rate at which we can fire
    public float useRate = 1f;

    [HideInInspector]
    public Player player;

    public virtual void Reload()
    {
        currentUses = maxUses;
    }

    public abstract void UseItem();
    public Projectile SpawnProjectile(Vector3 pos, Quaternion rot)
    {
        
        GameObject clone = Instantiate(projectile, pos, rot);
        Projectile p = clone.GetComponent<SmallProjectile>();
        p.Fire(transform.forward);
        Debug.Log("attached the component");
        currentUses--;
        return p;
    }
}
