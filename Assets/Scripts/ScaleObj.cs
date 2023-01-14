using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ScaleObj : MonoBehaviour
{
    public GameObject obj;
    [Range(0.000001f, 10.0f)]
    public float scale = 1f;

    private float minScale = 0.000001f;
    private float maxScale = 10f;

    public float debugScaleUpdate = 1f;
    [Button("Update Scale")]
    private void Method2() { UpdateScale(debugScaleUpdate); }

    // Update is called once per frame
    void Update()
    {
        if (obj)
            obj.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void UpdateScale(float dir)
    {
        float dist = 0.1f * dir;
        scale = Mathf.Clamp(scale + dist, scale - 0.1f, scale + 0.1f);
        if (scale <= minScale)
            scale = minScale;
        if (scale >= maxScale)
            scale = maxScale;
    }
}
