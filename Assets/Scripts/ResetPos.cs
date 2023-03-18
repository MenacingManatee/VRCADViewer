using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPos : MonoBehaviour
{
    public GameObject toReset;
    void Start()
    {
        toReset = GameObject.Find("Head Follower");
    }
    public void ResetPosition(GameObject toReset)
    {
        toReset.transform.localPosition = Vector3.zero;
    }

    public void ResetToCopiedY(Transform toCopyFrom)
    {
        Vector3 newPos = toReset.transform.position;
        newPos.y = toCopyFrom.position.y;
        toReset.transform.position = newPos;
    }
}
