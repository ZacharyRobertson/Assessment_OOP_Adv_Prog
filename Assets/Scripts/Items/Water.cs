using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Preprocessor Directive
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Water : Item
{
    public float spreadAngle = 45;
    public float spreadRadius = 1.5f;
    public int drops = 5;
    
    //Get the direction to fire in
    public Vector3 GetDir(float angleD)
    {
        //Set angleR to angleD by the Degree to Radian conversion constant
        float angleR = angleD * Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Sin(angleR), 0, Mathf.Cos(angleR));
        //return the transform.rotation by dir
        //Direction is set to dir, off current rotation
        return transform.rotation * dir;
    }
    public override void Use()
    {
        //Loop through the amount of items we can spread
        for (int i = 0; i < drops; i++)
        {
            // Set a new variable that spawns a projectile
            Projectile p = SpawnProjectile(transform.position, transform.rotation);
            // Set a random angle between -spreadAngle and spreadAngle
            float randomAngle = Random.Range(-spreadAngle, spreadAngle);
            // Set direction to GetDir and pass randomAngle
            Vector3 direction = GetDir(randomAngle);
            // Set p's aliveDistance to spreadRadius
            p.aliveDistance = spreadRadius;
            // Call the Use method from p and Pass Direction
            p.Use(direction);
        }
    }
}
//Allows us to view the spread elements in the editor
#if UNITY_EDITOR
[CustomEditor(typeof(Water))]
public class WaterEditor : Editor
{
    void OnSceneGUI()
    {
        //reference the script of the current object being inspected
        Water water = (Water)target;
        // Set our transforma and position
        Transform transform = water.transform;
        Vector3 pos = transform.position;
        //Set our angle and radius
        float angle = water.spreadAngle;
        float radius = water.spreadRadius;
        //Set the angle to display our spread
        Vector3 leftDir = water.GetDir(angle);
        Vector3 rightDir = water.GetDir(-angle);

        //Draw the lines and arc that will display the spread area
        Handles.color = Color.blue;
        Handles.DrawLine(pos, pos + leftDir * radius);
        Handles.DrawLine(pos, pos + rightDir * radius);
        Handles.DrawWireArc(pos, Vector3.forward, rightDir, angle * 2, radius);
    }
}
#endif
