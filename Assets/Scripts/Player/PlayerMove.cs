using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerMove : NetworkBehaviour
{
    public float moveSpeed = 8f;
    public float sensitivity = 2f;
    private Vector3 move;

    private Player player;
    private Rigidbody rigid;
    private Quaternion originalRotation;
    // Use this for initialization
    void Awake()
    {
        player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody>();
        if(rigid.freezeRotation)
        {
            originalRotation = transform.localRotation;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }
        if (isLocalPlayer)
        {
            //Store the axes we will use to move
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // Set move based on Input
            move = new Vector3(h, 0, v);
            move = transform.rotation * move;
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
        //SET variables for the camera Transform and for input
        Transform camTransform = player.cam.transform;
        float rotX = Input.GetAxis("Mouse X") * sensitivity;
        float rotY = Input.GetAxis("Mouse Y") * sensitivity;
        //Set the variable that will clamp our camera so we do not jitter
        float xClamp = rotY;
        //SET variable for the camera and playerTransform to rotate
        Vector3 rot = camTransform.rotation.eulerAngles;
        Vector3 playerRot = rigid.rotation.eulerAngles;
        //Set the variables to rotate depending on Input
        rot.x -= rotY;
        playerRot.y += rotX;
        rot.z = 0;

        //Clamp our Camera
        if(xClamp > 90)
        {
            xClamp = 90;
            rot.x = 90;
        }
        else if(xClamp < -90)
        {
            xClamp = -90;
            rot.x = -90;
        }

        camTransform.rotation = Quaternion.Euler(rot);
        rigid.rotation = Quaternion.Euler(playerRot);
    }
}

