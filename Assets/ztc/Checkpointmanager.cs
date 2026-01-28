using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    [Header("Checkpoint Order")]
    public Transform[] checkpoints; // 按顺序拖

    int currentIndex = 0;

    // ===== 复活用 =====
    Vector3 checkpointPos;
    Quaternion checkpointRot;
    bool hasCheckpoint = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // =========================
    // 给箭头用：下一个目标
    // =========================
    public Transform CurrentTarget
    {
        get
        {
            if (currentIndex >= checkpoints.Length)
                return null;

            return checkpoints[currentIndex+1];
        }
    }

    // =========================
    // 被 Checkpoint 调用
    // =========================
    public void ReachCheckpoint(int index, Vector3 pos, Quaternion rot)
    {

        checkpointPos = pos;
        checkpointRot = rot;
        hasCheckpoint = true;

        Debug.Log($"Checkpoint {index} reached");

        currentIndex=index;

    }

    // =========================
    // 死亡 / 刺用
    // =========================
    public void Respawn(Rigidbody rb)
    {
        if (!hasCheckpoint) return;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.position = checkpointPos;
        rb.rotation = checkpointRot;
    }
}
