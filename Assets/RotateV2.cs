using UnityEngine;

public class OarPivotFromHandV2 : MonoBehaviour
{
    public Transform hand;
    public Transform reference;   // 👈 BoatRoot
    public float rotateSpeed = 1f;
    public bool invert = false;

    Vector3 lastHandDir;

    void Start()
    {
        if (hand != null && reference != null)
            lastHandDir = GetHandDir();
    }

    void Update()
    {
        if (hand == null || reference == null)
            return;

        Vector3 currentDir = GetHandDir();

        if (currentDir.sqrMagnitude < 0.0001f || lastHandDir.sqrMagnitude < 0.0001f)
            return;

        float deltaAngle = Vector3.SignedAngle(
            lastHandDir,
            currentDir,
            Vector3.forward   // reference 本地 Z
        );

        if (invert)
            deltaAngle = -deltaAngle;

        transform.Rotate(0f, 0f, deltaAngle * rotateSpeed, Space.Self);

        lastHandDir = currentDir;
    }

    Vector3 GetHandDir()
    {
        Vector3 handLocal = reference.InverseTransformPoint(hand.position);
        Vector3 pivotLocal = reference.InverseTransformPoint(transform.position);

        Vector3 dir = handLocal - pivotLocal;
        dir.z = 0f;

        return dir.normalized;
    }
}
