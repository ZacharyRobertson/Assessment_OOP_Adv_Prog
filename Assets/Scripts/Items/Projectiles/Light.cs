using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : Projectile
{
    // Use this for initialization
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
}
