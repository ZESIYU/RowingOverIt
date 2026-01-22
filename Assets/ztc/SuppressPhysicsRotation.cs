using UnityEngine;

public class SuppressPhysicsRotation : MonoBehaviour
{
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 只压制物理带来的旋转
        rb.angularVelocity = Vector3.zero;
    }
}