using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPos : MonoBehaviour
{
    public void ResetPosition(GameObject toReset)
    {
        toReset.transform.localPosition = Vector3.zero;
    }
}
