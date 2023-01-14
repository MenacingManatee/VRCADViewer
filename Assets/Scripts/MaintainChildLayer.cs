using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MaintainChildLayer : MonoBehaviour
{
    [Layer]
    public int layer;

    // Update is called once per frame
    void Update()
    {
        foreach (Transform t in transform)
            transform.gameObject.layer = layer;
    }
}
