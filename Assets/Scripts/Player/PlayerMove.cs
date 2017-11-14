using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerMove : NetworkBehaviour
{
    public float moveSpeed = 8f;
    public LayerMask floor;

    private Vector3 move;
    private float camRay = 50f;

    private Player player;
    private Rigidbody rigid;

    // Use this for initialization
    void Awake()
    {
        player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player == null)
        {
            return;
        }
        if(isLocalPlayer)
        {
            //Store the axes we will use to move
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // Set move based on Input
            move = new Vector3(h, 0, v);

            //Rotate to Mouse pos
            RotateToMouse();
        }

        //Call the move variable that will take local variables
        Move();
    }
    void Move()
    {
        //Normalize move and multiply by moveSpeed and deltaTime to make it proportional
        move = move.normalized * moveSpeed * Time.deltaTime;

        //Move the player using the rigidbody
        rigid.MovePosition(transform.position + move);
    }
    void RotateToMouse()
    {
        // Create a ray that returns from the camera to mouse position
        Ray ray = player.cam.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable that stores information of what we hit
        RaycastHit hit;

        // Perform the raycast and check if it hits the floor layer
        if (Physics.Raycast(ray, out hit, camRay, floor))
        {
            // Create a vector that points to where we hit from our current position
            Vector3 dir = hit.point - transform.position;

            // Ensure the vector is not rotating us upwards
            dir.y = 0f;

            // Set a new quaternion based on the new direction
            Quaternion rot = Quaternion.LookRotation(dir);

            // Rotate the player towards the new direction
            rigid.transform.rotation = rot;
        }
    }
}
