using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallProjectile : Projectile
{

    protected override void Start()
    {
        base.Start();
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
        transform.position += direction;
    }
}
