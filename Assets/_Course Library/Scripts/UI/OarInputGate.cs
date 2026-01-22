using UnityEngine;

public class OarInputGate : MonoBehaviour
{
    public Transform pivot; // 对应 leftPivot / rightPivot

    private Vector3 cachedLocalEuler;

    void Start()
    {
        if (pivot == null)
            pivot = transform;

        cachedLocalEuler = pivot.localEulerAngles;
    }

    void LateUpdate()
    {
        if (!InputGate.InputEnabled)
        {
            // 冻结桨角度，保证 delta = 0
            pivot.localEulerAngles = cachedLocalEuler;
        }
        else
        {
            // 游戏开始后更新缓存
            cachedLocalEuler = pivot.localEulerAngles;
        }
    }
}
