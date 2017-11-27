using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Test : MonoBehaviour
{
    public float checkDelay = 0f;
    private float checkTimer = 0f;

    protected virtual void Simulate() {}
    protected virtual void Debug() {}
    protected abstract void Check();

    // Update is called once per frame
    void Update()
    {
        //Call simulate
        Simulate();
        //Check on interval (0 if once per frame)
        checkTimer += Time.deltaTime;
        if(checkTimer >= checkDelay)
        {
            Check();
        }
        //Perform debugging
        Debug();
    }
}
