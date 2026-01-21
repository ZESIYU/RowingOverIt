using UnityEngine;

public class PendulumCollision : MonoBehaviour
{
    [Header("击飞参数")]
    public float hitForce = 15f;       // 力量
    public float upwardModifier = 0.5f; // 提升 Y 方向力度

    void OnTriggerEnter(Collider other)
    {
        // 检测是否有 Rigidbody
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && !rb.isKinematic)
        {
            // 计算击飞方向：从锤子指向物体
            Vector3 dir = (other.transform.position - transform.position).normalized;

            // 可以加上向上的分量，让物体飞起
            dir.y += upwardModifier;

            // 施加冲量
            rb.AddForce(dir.normalized * hitForce, ForceMode.Impulse);
        }
    }
}