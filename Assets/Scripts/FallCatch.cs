using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCatch : MonoBehaviour
{
    public GameObject player;
    public float minYVal = 0f;
    public Vector3 startPos;

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y <= minYVal)
        {
            CharacterController c = player.GetComponent<CharacterController>();
            Rigidbody r = player.GetComponent<Rigidbody>();
            if (c)
                c.enabled = false;
            if (r)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
                r.isKinematic = true;
            }
            player.transform.position = startPos;
            if (c)
                c.enabled = true;
            if (r)
                r.isKinematic = false;
        }
    }
}
