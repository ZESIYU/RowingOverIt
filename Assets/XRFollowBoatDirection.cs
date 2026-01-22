using UnityEngine;

public class XRFollowBoatDirection : MonoBehaviour
{
    public Transform boat;

    void LateUpdate()
    {
        if (boat == null) return;

        Vector3 forward = -boat.right;
        forward.y = 0f;

        if (forward.sqrMagnitude < 0.0001f)
            return;

        transform.rotation = Quaternion.LookRotation(forward);
    }
}
