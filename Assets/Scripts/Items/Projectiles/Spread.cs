using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spread : Projectile
{
    public int spreadDamage = 10;
    public float spreadRadius = 1.5f;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Use(Vector3 direction, float? speed = null)
    {
        float currentSpeed = this.speed;
        // Check if speed has been used
        if (speed != null)
        {
            currentSpeed = speed.Value;
        }
        // Fire in that direction
        rigid.AddForce(direction * currentSpeed, ForceMode.Impulse);
    }

    void AreaDamage()
    {
       
        //Destroy this gameObject
        Destroy(gameObject);

        //Damage enemies in range
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, spreadRadius);

        foreach (Collider col in objectsInRange)
        {
            //Create a reference to an enemy script
            Enemy enemy = col.GetComponent<Enemy>();

            if(enemy != null)
            {
                //  Deal Damage
                enemy.health -= spreadDamage;
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Enemy")
        {
            AreaDamage();
        }
    }
}

