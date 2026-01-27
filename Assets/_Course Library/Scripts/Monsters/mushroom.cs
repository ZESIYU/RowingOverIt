using UnityEngine;

public class VerticalOscillator : MonoBehaviour
{
    [Header("Motion")]
    public float amplitude = 0.5f;   // 上下移动的高度
    public float frequency = 1f;     // 往返频率（次/秒）

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * Mathf.PI * 2f * frequency) * amplitude;
        transform.position = startPos + Vector3.up * yOffset;
    }
}
