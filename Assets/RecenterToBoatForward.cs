using UnityEngine;
using UnityEngine.XR.Management;

public class RecenterToBoatForward : MonoBehaviour
{
    public Transform boat;

    void Start()
    {
        Invoke(nameof(Recenter), 0.1f);
    }

    void Recenter()
    {
        if (boat == null) return;

        // 让 XR 以当前姿态为原点
        var xrInput = XRGeneralSettings.Instance?.Manager?.activeLoader;
        if (xrInput != null)
        {
            UnityEngine.XR.InputTracking.Recenter();
        }

        // 然后对齐 XR Origin 到船头
        Vector3 forward = -boat.right;
        forward.y = 0f;

        transform.rotation = Quaternion.LookRotation(forward);
    }
}
