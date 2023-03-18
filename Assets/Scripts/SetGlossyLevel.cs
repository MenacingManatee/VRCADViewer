using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGlossyLevel : MonoBehaviour
{
    public List<Material> mats = new List<Material>();

    void Start()
    {
        OnSetGlossy(0.5f);
    }
    
    public void OnSetGlossy(float level)
    {
        foreach (var m in mats)
        {
            m.SetFloat("_Glossiness", level);
        }
    }
}
