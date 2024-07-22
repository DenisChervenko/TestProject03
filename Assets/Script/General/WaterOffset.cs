using UnityEngine;

public class WaterOffset : MonoBehaviour
{
    public Material waterMaterial; 
    public float waveSpeed = 1.0f; 
    public float waveScale = 1.0f; 

    private Vector2 _offset;

    void Update()
    {
        _offset.x += waveSpeed * Time.deltaTime;
        _offset.y += waveSpeed * Time.deltaTime;

        if (waterMaterial != null)
            waterMaterial.SetTextureOffset("_MainTex", _offset * waveScale);
    }
}
