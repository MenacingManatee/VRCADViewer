using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterEvent : MonoBehaviour
{
    public UnityEvent triggerEvent;
    public UnityEvent triggerStayEvent;
    public float minStayTime = 1f;

    private bool stayEventCalled = false;
    private float stayTime = 0f;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
            triggerEvent.Invoke();
    }

    void OnTriggerStay(Collider col)
    {
        if (stayEventCalled)
            return;
        if (stayTime < minStayTime)
            stayTime += Time.deltaTime;
        else
        {
            stayEventCalled = true;
            triggerStayEvent.Invoke();
        }
    }
}
