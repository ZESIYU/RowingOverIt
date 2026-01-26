using UnityEngine;

public class OarWaterSound : MonoBehaviour
{
    [Header("Oar")]
    public Transform pivot;
    public float maxAngle = 90f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] splashClips;   // 改成数组

    bool isInWater = false;

    void Update()
    {
        if (pivot == null || audioSource == null || splashClips == null || splashClips.Length == 0)
            return;

        float angle = pivot.localEulerAngles.z;
        if (angle > 180f) angle -= 360f;

        bool nowInWater = Mathf.Abs(angle) < maxAngle;

        // 入水瞬间
        if (nowInWater && !isInWater)
        {
            PlayRandomSplash();
            //Debug.Log(
            //    $"[SOUND] Sound Played Successfully."
            //);
        }

        isInWater = nowInWater;
    }

    void PlayRandomSplash()
    {
        int index = Random.Range(0, splashClips.Length);
        audioSource.PlayOneShot(splashClips[index]);
    }
}

