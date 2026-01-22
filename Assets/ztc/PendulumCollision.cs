using UnityEngine;

public class PendulumPushNoRotate : MonoBehaviour
{
    public float pushSpeed = 5f;

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        // 计算水平方向
        Vector3 dir = other.transform.position - transform.position;
        dir.y = 0f;
        dir.Normalize();

        // 直接移动 Transform，不用物理旋转
        Vector3 newPos = other.transform.position + dir * pushSpeed;
        other.transform.position = newPos;

        // 彻底清掉角速度（保险）
        if (!rb.isKinematic)
        {
            rb.angularVelocity = Vector3.zero;
        }

        // 可选：禁用 Rigidbody 旋转约束（保留划船 AddTorque）
        // rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
