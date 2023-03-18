using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpUpPosition : MonoBehaviour
{
    public void OnBumpUp()
    {
        Debug.Log("Bump");
        Rigidbody r = GetComponent<Rigidbody>();
        r.AddForce(transform.up * 30, ForceMode.Impulse);
    }
}
