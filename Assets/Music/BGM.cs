using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    public AudioSource audioSource;

    [Header("BGM List")]
    public AudioClip bgmA;
    public AudioClip bgmB;

    int currentIndex = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayNext();
    }

    void Update()
    {
        // 当前曲子播放完 → 播放下一首
        if (!audioSource.isPlaying)
        {
            PlayNext();
        }
    }

    void PlayNext()
    {
        if (currentIndex == 0)
        {
            audioSource.clip = bgmA;
            currentIndex = 1;
        }
        else
        {
            audioSource.clip = bgmB;
            currentIndex = 0;
        }

        audioSource.Play();
    }
}
