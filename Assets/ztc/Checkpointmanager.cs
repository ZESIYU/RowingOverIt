using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private Vector3 checkpointPos;
    private Quaternion checkpointRot;
    private bool hasCheckpoint = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetCheckpoint(Vector3 pos, Quaternion rot)
    {
        checkpointPos = pos;
        checkpointRot = rot;
        hasCheckpoint = true;

        Debug.Log("Checkpoint saved");
    }

    public void Respawn(Rigidbody rb)
    {
        if (!hasCheckpoint) return;
        Debug.Log("Respawn!");
        //rb.linearVelocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;

        rb.position = checkpointPos;
        rb.rotation = checkpointRot;
    }
}
