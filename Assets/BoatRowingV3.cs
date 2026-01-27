using UnityEngine;
using UnityEngine.XR;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BoatMovement_WithHaptic : MonoBehaviour
{
    [Header("Oar Pivots")]
    public Transform leftPivot;
    public Transform rightPivot;

    [Header("Speed Limit")]
    public float maxSpeed = 15f;

    [Header("Power")]
    public float forwardPower = 8f;     // 推进强度
    public float turnPower = 4f;        // 转向强度
    public float maxAngle = 65f;        // 有效划水角度

    [Header("Physics")]
    public float linearDamping = 1.5f;
    public float angularDamping = 3.0f;

    [Header("Haptics")]
    public XRNode leftHand = XRNode.LeftHand;
    public XRNode rightHand = XRNode.RightHand;

    [Header("State")]
    public bool isStunned = false;

    public float hapticAmplitude = 0.6f;
    public float hapticDuration = 0.1f;

    bool leftInWater = false;
    bool rightInWater = false;

    Rigidbody rb;

    float lastLeftAngle;
    float lastRightAngle;

    RigidbodyConstraints originalConstraints;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = linearDamping;
        rb.angularDamping = angularDamping;
        originalConstraints = rb.constraints;
    }

    void FixedUpdate()
    {
        if (isStunned)
        {
            rb.angularVelocity = Vector3.zero;
            return;
        }

        float leftStroke = SampleStroke(
            leftPivot,
            ref lastLeftAngle,
            leftHand,
            ref leftInWater
        );

        float rightStroke = SampleStroke(
            rightPivot,
            ref lastRightAngle,
            rightHand,
            ref rightInWater
        );


        ApplyMovement(leftStroke, rightStroke);
        LimitMaxSpeed(); 
    }

    public void Stun(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(StunRoutine(duration));
    }

    IEnumerator StunRoutine(float duration)
    {
        isStunned = true;

        rb.angularVelocity = Vector3.zero;

        rb.constraints = originalConstraints | RigidbodyConstraints.FreezeRotationY;

        yield return new WaitForSeconds(duration);

        rb.angularVelocity = Vector3.zero;

        rb.constraints = originalConstraints;

        isStunned = false;

    }

    // =========================
    // 采样单侧桨“这一帧的有效划水量”
    // =========================
    float SampleStroke(
        Transform pivot,
        ref float lastAngle,
        XRNode handNode,
        ref bool inWater
    )
    {   
        if (pivot == null) return 0f;

        float angle = pivot.localEulerAngles.z;
        if (angle > 180f) angle -= 360f;

        float delta = angle - lastAngle;
        lastAngle = angle;

        bool nowInWater = Mathf.Abs(angle) < maxAngle;

        //  刚刚入水 -> 震动
        if (nowInWater && !inWater)
        {
            SendHaptic(handNode);
        }

        inWater = nowInWater;

        
            Debug.Log(
                $"[STROKE] angle={angle:F3} delta={delta:F3} nowInWater={nowInWater}"
            );
        
        

        // 只有在水里 & 往后划 才推进
        if (!nowInWater || delta <= 0f)
            return 0f;

        return delta;
    }

    // =========================
    // 推进 + 转向（核心）
    // =========================
    void ApplyMovement(float leftStroke, float rightStroke)
    {
        // ---- 1 推进（两桨叠加）----
        float forwardStroke = leftStroke + rightStroke;

        if (forwardStroke > 0f)
        {
            //  你说过：船头在 -X
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

        // ---- 2️ 转向（左右差）----
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
    
    void LimitMaxSpeed()
    {
        Vector3 vel = rb.linearVelocity;

        // 只限制水平速度（不影响 Y）
        Vector3 horizontalVel = new Vector3(vel.x, 0f, vel.z);

        if (horizontalVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = horizontalVel.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(
                limitedVel.x,
                vel.y,
                limitedVel.z
            );
        }

        Debug.Log(
            $"[MAXSPEED] current={rb.linearVelocity} previous={vel}"
        );
    }

    void SendHaptic(XRNode node)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(node);

        if (device.isValid &&
            device.TryGetHapticCapabilities(out HapticCapabilities caps) &&
            caps.supportsImpulse)
        {
            device.SendHapticImpulse(
                0,
                hapticAmplitude,
                hapticDuration
            );
        }
    }
}
