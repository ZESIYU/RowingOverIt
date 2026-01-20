using UnityEngine;

public class WaterUVFlow : MonoBehaviour
{
    public float speed = 0.1f;
    private Renderer rend;

    void Start()
    {
        // 自动找 Renderer，支持父物体或子物体
        rend = GetComponent<Renderer>();
        if (rend == null) rend = GetComponentInChildren<Renderer>();
        if (rend == null) Debug.LogError("No Renderer found for WaterUVFlow!");
    }

    void Update()
    {
        if (rend == null) return;

        Vector2 offset = rend.material.mainTextureOffset;
        offset.y += speed * Time.deltaTime;
        rend.material.mainTextureOffset = offset;
    }
}
