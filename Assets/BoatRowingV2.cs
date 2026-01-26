using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatMovement_VRRowing : MonoBehaviour
{
    [Header("Oar Pivots")]
    public Transform leftPivot;
    public Transform rightPivot;

    [Header("Power")]
    public float forwardPower = 8f;     // 推进强度
    public float turnPower = 4f;        // 转向强度
    public float maxAngle = 65f;        // 有效划水角度

    [Header("Physics")]
    public float linearDamping = 1.5f;
    public float angularDamping = 3.0f;

    [Header("State")]
    public bool isStunned = false;
    Rigidbody rb;

    float lastLeftAngle;
    float lastRightAngle;

    RigidbodyConstraints originalConstraints;

    void Start()
{
    rb = GetComponent<Rigidbody>();

    rb.linearDamping = linearDamping;
    rb.angularDamping = angularDamping;

    // 记录初始约束（非常重要）
    originalConstraints = rb.constraints;
}



    void FixedUpdate()
    {
        if (isStunned)
        {
            rb.angularVelocity = Vector3.zero;
            return;
        }

        float leftStroke  = SampleStroke(leftPivot, ref lastLeftAngle);
        float rightStroke = SampleStroke(rightPivot, ref lastRightAngle);

        ApplyMovement(leftStroke, rightStroke);
    }

public void Stun(float duration)
{
    StopAllCoroutines();
    StartCoroutine(StunRoutine(duration));
}

IEnumerator StunRoutine(float duration)
{
    isStunned = true;

    // ⭐ 立刻清掉旋转
    rb.angularVelocity = Vector3.zero;

    // ⭐ 只在 stun 期间锁 Y 轴旋转
    rb.constraints = originalConstraints | RigidbodyConstraints.FreezeRotationY;

    yield return new WaitForSeconds(duration);

    // ⭐ 恢复原本约束
    rb.constraints = originalConstraints;

    isStunned = false;
}


    // =========================
    // 采样单侧桨“这一帧的有效划水量”
    // =========================
    float SampleStroke(Transform pivot, ref float lastAngle)
    {
        if (pivot == null) return 0f;

        float angle = pivot.localEulerAngles.z;
        if (angle > 180f) angle -= 360f;

        float delta = angle - lastAngle;
        lastAngle = angle;

        // 不在划水区间
        if (Mathf.Abs(angle) > maxAngle)
            return 0f;

        // 只认“往后划”
        if (delta <= 0f)
            return 0f;

        return delta;
    }

    // =========================
    // 推进 + 转向（核心）
    // =========================
    void ApplyMovement(float leftStroke, float rightStroke)
    {
        // ---- 1️⃣ 推进（两桨叠加）----
        float forwardStroke = leftStroke + rightStroke;

        if (forwardStroke > 0f)
        {
            // ⚠️ 你说过：船头在 -X
            Vector3 forward = -transform.right;
            forward.y = 0f;
            forward.Normalize();
            Vector3 force = forward * forwardStroke * forwardPower;

            rb.AddForce(
                force,
                ForceMode.Force
            );

            Debug.Log(
                $"[FORWARD] stroke={forwardStroke:F3} " +
                $"force={force} mag={force.magnitude:F2} " +
                $"vel={rb.linearVelocity}"
            );
        }

        // ---- 2️⃣ 转向（左右差）----
        float turn = rightStroke - leftStroke;
        Vector3 torque = Vector3.up * turn * turnPower;

        if (Mathf.Abs(turn) > 0.0001f)
        {
            rb.AddTorque(
                -torque,
                ForceMode.Force
            );
        }
            Debug.Log(
                $"[TURN] turn={turn:F3} torque={torque.y:F2}"
            );
    }
}
