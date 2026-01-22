using UnityEngine;
using UnityEngine.XR;

public class FaceWorldMinusXOnStart : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(RecenterAndAlign), 0.2f);
    }

    void RecenterAndAlign()
    {
        // 1. 重置 XR，把当前头显方向当作 forward
        InputTracking.Recenter();

        // 2. 旋转 XR Origin，使 forward 指向世界 -X
        transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
    }
}
