using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerMove : NetworkBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float jumpHeight = 15f;
    public Vector3 move;
    public bool isGrounded = false;

    //References
    [HideInInspector]
    private Player player;
    [HideInInspector]
    private Rigidbody rigid;
    // Use this for initialization
    void Awake()
    {
        player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody>();
        isGrounded = true;
    }
    public void Move(float h, float v)
    {
        // Set move based on Input
        move = new Vector3(h, 0, v);
        //make our move relative to current rotation
        move = player.rigid.rotation * move;
        //Normalize move and multiply by moveSpeed and deltaTime to make it proportional
        move = move.normalized * moveSpeed * Time.deltaTime;

        //Move the player using the rigidbody
        rigid.MovePosition(transform.position + move);
    }
    public void RotateToMouse(float x, float y)
    {
        //SET variables for the camera Transform
        Transform camTransform = player.cam.transform;
        //Set the variable that will clamp our camera so we do not jitter
        float xClamp = y;
        #region Camera Variables
        //SET variable for the camera and playerTransform to rotate
        Vector3 rot = camTransform.rotation.eulerAngles;
        Vector3 playerRot = rigid.rotation.eulerAngles;
        //Set the variables to rotate depending on Input
        rot.x -= y;
        playerRot.y += x;
        rot.z = 0;
        #endregion
        #region Clamp
        //Clamp our Camera
        if (xClamp > 90)
        {
            xClamp = 90;
            rot.x = 90;
        }
        else if (xClamp < -90)
        {
            xClamp = -90;
            rot.x = -90;
        }
        #endregion
        camTransform.rotation = Quaternion.Euler(rot);
        rigid.rotation = Quaternion.Euler(playerRot);
    }
    public void Jump()
    {
            if (isGrounded)
            {
                rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                isGrounded = false;
            }
    }
    void OnCollisonEnter(Collision other)
    {
        isGrounded = true;
    }
}

