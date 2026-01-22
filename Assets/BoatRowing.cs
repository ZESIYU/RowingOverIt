using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatMovement : MonoBehaviour
{
    public Transform leftPivot;
    public Transform rightPivot;

    public float power = 15f;          // 单侧桨推进力强度
    public float maxAngle = 60f;        // 划水有效角度

    Rigidbody rb;

    float lastLeftAngle=0f;
    float lastRightAngle=0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.linearDamping = 1.5f;
        rb.angularDamping = 2.5f;
    }

    void FixedUpdate()
    {
        ApplyOarForce(leftPivot, ref lastLeftAngle, isLeft: true);
        ApplyOarForce(rightPivot, ref lastRightAngle, isLeft: false);
    }

    void ApplyOarForce(Transform pivot, ref float lastAngle, bool isLeft)
    {
        if (pivot == null) return;

        // 1️⃣ 当前桨角度（Z 轴）
        float angle = pivot.localEulerAngles.z;
        if (angle > 180f) angle -= 360f;

        float delta = angle - lastAngle;
        lastAngle = angle;

        // 2️⃣ 只在划水区间 & 往后划
        if (Mathf.Abs(angle) > maxAngle) return;

        float stroke = delta;
        if (stroke <= 0f) return;

        // 3️⃣ 船头方向（⚠️ 你说过：船头是 X 轴）
        Vector3 forward = -transform.right;
        forward.y = 0f;
        forward.Normalize();

        // 4️⃣ 施力点 = 桨所在的世界位置
        Vector3 forcePos = pivot.position;
        forcePos.y = rb.worldCenterOfMass.y;

        // 5️⃣ 施加力（不是 AddForce，是 AddForceAtPosition）
        Vector3 force = forward * stroke * power;
        rb.AddForce(force, ForceMode.Force);


        Debug.Log(
        $"[{(isLeft ? "LEFT" : "RIGHT")}] " +
        $"stroke={stroke:F3}, " +
        $"force={force}, " +
        $"forceMag={force.magnitude:F3}, " +
        $"forcePos={forcePos}"
    );
    }
}
