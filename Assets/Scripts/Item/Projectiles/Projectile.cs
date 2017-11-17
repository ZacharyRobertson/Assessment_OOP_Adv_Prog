using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float distance = 5f;

    // Use this for initialization
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
        Destroy(gameObject, distance);
    }
    public abstract void Fire(Vector3 direction, float? speed = null);

    void OnTriggerEnter(Collider other)
    {
            Destroy(gameObject);
    }
}
