using UnityEngine;

public class PendulumPushRB : MonoBehaviour
{
    public float pushForce = 50f;
    public float stunTime = 0.8f;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit!!!");
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        BoatMovement_WithHaptic boat =
            rb.GetComponent<BoatMovement_WithHaptic>();
        if (boat == null) return;

        Vector3 dir =  other.transform.position - transform.position;
        dir.y = 0f;
        dir.Normalize();

        rb.AddForce(dir * pushForce, ForceMode.VelocityChange);

        boat.Stun(stunTime);
    }
}
