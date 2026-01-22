using UnityEngine;

public class PauseMenuPositioner : MonoBehaviour
{
    public Transform xrCamera;

    public float distance = 1.5f;
    public float heightOffset = -0.1f;

    public void PlaceInFrontOfPlayer()
    {
        Vector3 forward = new Vector3(
            xrCamera.forward.x,
            0,
            xrCamera.forward.z
        ).normalized;

        Vector3 targetPosition =
            xrCamera.position +
            forward * distance +
            Vector3.up * heightOffset;

        transform.position = targetPosition;

        transform.rotation = Quaternion.LookRotation(forward);
    }
}