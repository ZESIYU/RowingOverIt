using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager Instance;

    public AudioClip hoverClip;
    public AudioClip clickClip;

    AudioSource audioSource;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHover()
    {
        if (hoverClip != null)
            audioSource.PlayOneShot(hoverClip);
    }

    public void PlayClick()
    {
        if (clickClip != null)
            audioSource.PlayOneShot(clickClip);
    }
}
