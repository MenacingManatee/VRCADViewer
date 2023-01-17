using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRController : MonoBehaviour
{
    [Range(-1, 1)]
    public float sensitivity = 0.1f;
    public float maxSpeed = 1f;

    public SteamVR_Action_Boolean movePress = SteamVR_Input.GetBooleanAction("Teleport");
    // y = up/down, x = left/right
    public SteamVR_Action_Vector2 moveValue = SteamVR_Input.GetVector2Action("TurnValue");

    public bool isFlying = false;

    private float speed = 0f;

    public CharacterController player;
    private Transform cameraRig;
    private Transform head;


    void Awake()
    {
        if (player == null)
            player = GetComponent<CharacterController>();
    }

    void Start()
    {
        cameraRig = SteamVR_Render.Top().origin;
        head = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        HandleHead();
        HandleHeight();
    }

    private void HandleHead()
    {
        if (!cameraRig)
            return;
        Vector3 oldPos = cameraRig.position;
        Quaternion oldRot = cameraRig.rotation;

        transform.eulerAngles = new Vector3(0f, head.rotation.eulerAngles.y, 0f);

        cameraRig.position = oldPos;
        cameraRig.rotation = oldRot;
    }

    private void CalculateMovement()
    {
        //Vector3 orientationEuler = new Vector3(0f, transform.eulerAngles.y, 0f);
        //Quaternion orientation = Quaternion.Euler(orientationEuler);
        //Vector3 movement = Vector3.zero;

        //if (movePress.GetStateUp(SteamVR_Input_Sources.Any))
        //{
        //   speed = 0f;
        //}

        //if (movePress.state)
        //{

        if ((moveValue.axis.y <= 0.05f && moveValue.axis.y >= -0.05f) && (moveValue.axis.x <= 0.05f && moveValue.axis.x >= -0.05f))
            speed = 0f;
        //speed += moveValue.axis.y * sensitivity;
        //speed = Mathf.Clamp(speed, -maxSpeed * 0.5f, maxSpeed);

        //movement += orientation * (speed * Vector3.forward) * Time.deltaTime;
        //}

        //player.Move(movement);
        //if (movement != Vector3.zero)
        //    Debug.Log(movement);

        speed += moveValue.axis.magnitude * sensitivity;
        speed = Mathf.Clamp(speed, -maxSpeed * 0.5f, maxSpeed);
        Vector3 direction;
        if (!isFlying)
        {
            direction = Player.instance.hmdTransform.TransformDirection(new Vector3(moveValue.axis.x, 0f, moveValue.axis.y));
            player.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up));
        }
        else
        {
            direction = Camera.main.transform.forward;
            player.Move(speed * Time.deltaTime * direction);
        }
    }

    private void HandleHeight()
    {
        if (!head)
            return;
        float headHeight = Mathf.Clamp(head.localPosition.y, 0.5f, 2f);
        player.height = headHeight;

        Vector3 newCenter = Vector3.zero;
        newCenter.y = player.height / 2f;
        newCenter.y += player.skinWidth;

        newCenter.x = head.localPosition.x;
        newCenter.z = head.localPosition.z;

        newCenter = Quaternion.Euler(0f, -transform.eulerAngles.y, 0f) * newCenter;

        player.center = newCenter;
    }
}
