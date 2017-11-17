using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerMove : NetworkBehaviour
{
    //Movement Variables
    public float moveSpeed = 8f;
    private Vector3 move;
    //Rotation Variables
    public float rotX, rotY;
    public float sensitivity = 2f;
    
    //References
    private Player player;
    private Rigidbody rigid;
    // Use this for initialization
    void Awake()
    {
        player = GetComponent<Player>();
        rigid = player.rigid;
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
            #region Input
            //Store the axes we will use to move (uses the same axis for Console/PC)
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            #endregion
            // Set move based on Input
            move = new Vector3(h, 0, v);
            //make our move relative to current rotation
            move = rigid.rotation * move;
            //Rotate to Mouse pos if playing on PC
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
        //SET variables for the camera Transform
        Transform camTransform = player.cam.transform;
        #region Inputs
        //SET variables for Mouse input
        rotX += Input.GetAxis("Mouse X") * sensitivity;
        rotY += Input.GetAxis("Mouse Y") * sensitivity;

        //Set variables for controller Input(XBOX360 controller)
        rotX += Input.GetAxis("RJoystickH") * sensitivity;
        rotY += Input.GetAxis("RJoystickV") * sensitivity;
        //Set a deadzone so that we do not wander when not moving the mouse or joystick
        float inputDeadZone = 0.2f;
        //Make sure the dead zone is adhered to
        if(rotX <= inputDeadZone)
        {
            rotX = 0;
        }
        if(rotY <= inputDeadZone)
        {
            rotY = 0;
        }
        #endregion
        //Set the variable that will clamp our camera so we do not jitter
        float xClamp = rotY;
        #region Camera Variables
        //SET variable for the camera and playerTransform to rotate
        Vector3 rot = camTransform.rotation.eulerAngles;
        Vector3 playerRot = rigid.rotation.eulerAngles;
        //Set the variables to rotate depending on Input
        rot.x -= rotY;
        playerRot.y += rotX;
        rot.z = 0;
        #endregion
        #region Clamp
        //Clamp our Camera
        if (xClamp > 90)
        {
            xClamp = 90;
            rot.x = 90;
        }
        else if(xClamp < -90)
        {
            xClamp = -90;
            rot.x = -90;
        }
        #endregion
        camTransform.rotation = Quaternion.Euler(rot);
        rigid.rotation = Quaternion.Euler(playerRot);
    }
}

