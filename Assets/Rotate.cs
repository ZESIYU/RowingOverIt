using UnityEngine;

public class OarPivotFromHand : MonoBehaviour
{
    public Transform hand;      // Right or Left Hand Controller
    public float rotateSpeed = 10f;
    public bool invertAngle = false;

    void Update()
    {
        if (hand == null)
            return;

        // 1. Pivot -> Hand 方向
        Vector3 dir = hand.position - transform.position;

        // 2. 投影到 X-Y 平面（Z 轴旋转）
        dir.y = dir.y-0.1f;
        dir.z = 0f;

        if (dir.sqrMagnitude < 0.0001f)
            return;

        // 3. 计算绕 Z 轴的角度
        float angle = Vector3.SignedAngle(
            transform.up,        // 基准方向（很重要）
            dir.normalized,
            Vector3.forward      // Z 轴
        );

        if (invertAngle)
            angle = -angle;

        // 4. 旋转 Pivot（绕 Z）
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            targetRotation,
            Time.deltaTime * rotateSpeed
        );
    }
}
