using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 1;
    protected virtual void OnTriggerEnter(Collider other)
    {
        // If object has health
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            //Deal damage to the object

            health.TakeDamage(damage);
        }
    }
}
