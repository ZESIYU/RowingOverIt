using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int index;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        CheckpointManager.Instance.ReachCheckpoint(
            index,
            other.transform.position,
            other.transform.rotation
        );
    }
}
