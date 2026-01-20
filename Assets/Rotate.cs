using UnityEngine;

public class OarPivotFromHand : MonoBehaviour
{
    public Transform rightHand;   // Right Hand Controller
    public Transform pivot;       // Pivot-right
    public float rotateSpeed = 10f;
    public float minAngle = -60f;
    public float maxAngle = 60f;

    void Update()
    {
 
        Vector3 dir = rightHand.position - pivot.position;

        dir.y = 0f;

        if (dir.sqrMagnitude < 0.0001f)
            return;

        float angle = Vector3.SignedAngle(
            pivot.forward,
            dir.normalized,
            Vector3.up
        );


        angle = Mathf.Clamp(angle, minAngle, maxAngle);


        pivot.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
