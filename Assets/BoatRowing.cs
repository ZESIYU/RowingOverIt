using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatMovement : MonoBehaviour
{
    public Transform leftPivot;
    public Transform rightPivot;

    public float power = 5f;        // 推进力大小
    public float maxAngle = 60f;    // 多大角度算在“划水”

    Rigidbody rb;

    float lastLeftAngle=0;
    float lastRightAngle=0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 0.5f;            // 防止无限加速
        rb.angularDamping = 2f;
    }

    void FixedUpdate()
    {
        float leftDelta = GetStrokeDelta(leftPivot, ref lastLeftAngle);
        float rightDelta = GetStrokeDelta(rightPivot, ref lastRightAngle);

        float stroke = leftDelta + rightDelta;

        if (stroke > 0f)
        {
            Vector3 force = -transform.right * stroke * power;
            rb.AddForce(force, ForceMode.Force);
        }
        Debug.Log($"leftDelta={leftDelta}, rightDelta={rightDelta}");
    }

    float GetStrokeDelta(Transform pivot, ref float lastAngle)
    {
        if (pivot == null)
            return 0f;

        float angle = pivot.localEulerAngles.z;
        if (angle > 180f) angle -= 360f;

        float delta = angle - lastAngle;
        lastAngle = angle;

        // 只在“划水角度区间”内才算推进
        if (Mathf.Abs(angle) > maxAngle)
            return 0f;

        return delta;
    }
}
