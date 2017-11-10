using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float aliveDistance = 5f;

    public Rigidbody rigid;
    public Vector3 firePos;
    
    protected virtual void Awake()
    {
        //Get the rigidbody component in this game object
        rigid = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        //Record starting position from the first frame
        firePos = transform.position;
        Destroy(gameObject, aliveDistance);
    }

    public abstract void Use(Vector3 dir, float? speed = null);
}
