using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CycleMaterialAlbedo : MonoBehaviour
{
    public List<Texture> albedoTextures = new List<Texture>();
    public List<Texture> emissionTextures = new List<Texture>();
    public Material toSwap;
    public int index = -1;

    [Button]
    private void DoOnSwap() { OnSwap(); }

    // Start is called before the first frame update
    void Start()
    {
        if (albedoTextures.Count == 0 || emissionTextures.Count == 0 || toSwap == null || albedoTextures.Count != emissionTextures.Count)
        {
            Debug.LogError("Insufficient textures or missing material");
            Destroy(this);
        }
        OnSwap();
    }

    public void OnSwap(bool moveForwards = true)
    {
        if (moveForwards)
            index = (index + 1) % (albedoTextures.Count);
        else
            index = (index - 1) >= 0 ? index - 1 : albedoTextures.Count;

        toSwap.SetTexture("_MainTex", albedoTextures[index]);
        toSwap.SetTexture("_EmissionMap", emissionTextures[index]);
    }
}
