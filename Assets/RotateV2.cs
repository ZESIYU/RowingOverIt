using UnityEngine;

public class OarPivotFromHandV2 : MonoBehaviour
{
    public Transform hand;
    public float rotateSpeed = 1f;
    public bool invert = false;

    Vector3 lastHandDir;

    void Start()
    {
        if (hand != null)
            lastHandDir = GetHandDir();
    }

    void Update()
    {
        if (hand == null)
            return;

        Vector3 currentDir = GetHandDir();

        if (currentDir.sqrMagnitude < 0.0001f || lastHandDir.sqrMagnitude < 0.0001f)
            return;

        // 计算“这一帧手柄绕 Z 轴转了多少角度”
        float deltaAngle = Vector3.SignedAngle(
            lastHandDir,
            currentDir,
            Vector3.forward
        );

        if (invert)
            deltaAngle = -deltaAngle;

        // 按手柄挥动速度旋转 pivot
        transform.Rotate(0f, 0f, deltaAngle * rotateSpeed, Space.Self);

        lastHandDir = currentDir;
    }

    Vector3 GetHandDir()
    {
        Vector3 dir = hand.position - transform.position;
        dir.z = 0f;      // 只绕 Z 轴
        return dir.normalized;
    }
}
