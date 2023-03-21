using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Autohand;

public class CustomMovementOverride : MonoBehaviour
{
    public float moveSpeed = 1f;
    public SteamVR_Input_Sources moveController;
    public SteamVR_Action_Vector2 moveAxis;
    public Transform steamObjectContainer;
    public Transform forwardFollow;
    public Rigidbody rb;
    public AutoHandPlayer player;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveDir = Vector3.zero;
        if (moveAxis.active)
        {
            var v2Dir = moveAxis.axis;
            moveDir.x = v2Dir.x;
            moveDir.z = v2Dir.y;

            moveDir = AlterDirection(moveDir);
            Debug.Log(moveDir);
            //player.SyncBodyHead();

            rb.MovePosition(transform.position + moveDir * Time.deltaTime * moveSpeed);
            steamObjectContainer.position = transform.position;
        }
    }

    Vector3 AlterDirection(Vector3 moveAxis)
    {
        return Quaternion.AngleAxis(forwardFollow.eulerAngles.y, Vector3.up) * (new Vector3(moveAxis.x, moveAxis.y, moveAxis.z));
    }
}
