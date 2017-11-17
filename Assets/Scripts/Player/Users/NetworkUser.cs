using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class NetworkUser : NetworkBehaviour
{
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public PlayerMove moveScript;
    [Range(0, 1)]
    public float lerpRate = 1;
    [SyncVar]
    Vector3 syncPosition;
    [SyncVar]
    Quaternion syncRotation;
    public Camera cam;
    public AudioListener audi;

    public EnemySpawner[] spawners;

    // Use this for initialization
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        audi = GetComponentInChildren<AudioListener>();
        player = GetComponent<Player>();
        moveScript = GetComponent<PlayerMove>();
        if (!isLocalPlayer)
        {
            cam.enabled = false;
            audi.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (player == null) return;
            #region movement
            #region Input
            //Store the axes we will use to move (uses the same axis for Console/PC)
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            #endregion            
            moveScript.Move(h, v);
            #endregion
            #region Rotation
            #region Inputs
            //SET variables for Mouse input
            float sensitivity = 0.2f;
            float rotX = 0;
            float rotY = 0;
            rotX += Input.GetAxis("Mouse X") * sensitivity;
            rotY += Input.GetAxis("Mouse Y") * sensitivity;

            //Set variables for controller Input(XBOX360 controller)
            rotX += Input.GetAxis("RJoystickH") * sensitivity;
            rotY += Input.GetAxis("RJoystickV") * sensitivity;
            //Set a deadzone so that we do not wander when not moving the mouse or joystick
            float inputDeadZone = 0.2f;
            //Make sure the dead zone is adhered to
            if (rotX <= inputDeadZone && rotX >= -inputDeadZone)
            {
                rotX = 0;
            }
            if (rotY <= inputDeadZone && rotY >= -inputDeadZone)
            {
                rotY = 0;
            }
            #endregion
            moveScript.RotateToMouse(rotX, rotY);
            #endregion
            #region Jump
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                moveScript.Jump();
            }
            #endregion
        }
        else
        {
            LerpPosition();
            lerpRotation();
        }
    }
        [Command]
    void Cmd_SendPositionToServer(Vector3 position)
    {
        syncPosition = position;
    }
    [Command]
    void Cmd_SendRotationToServer(Quaternion rotation)
    {
        syncRotation = rotation;
    }
    void LerpPosition()
    {
        Rigidbody rigid = player.rigid;
        rigid.position = Vector3.Lerp(rigid.position, syncPosition, lerpRate);
    }
    void lerpRotation()
    {
        Rigidbody rigid = player.rigid;
        rigid.rotation = Quaternion.Lerp(rigid.rotation, syncRotation, lerpRate);
    }

    void OnCollisionEnter(Collision other)
    {
        moveScript.isGrounded = true;
    }
}

