using UnityEngine;

public class ArrowToNextCheckpoint : MonoBehaviour
{
    public float rotateSpeed = 6f;

    void Update()
    {
        Transform target = CheckpointManager.Instance.CurrentTarget;
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.01f) return;

        Quaternion targetRot = Quaternion.LookRotation(dir) * Quaternion.Euler(90f, 0f, 90f);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            Time.deltaTime * rotateSpeed
        );
    }
}
