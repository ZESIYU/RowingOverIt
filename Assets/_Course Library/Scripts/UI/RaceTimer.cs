using UnityEngine;

public class RaceTimer : MonoBehaviour
{
    public static RaceTimer Instance;

    public float ElapsedTime { get; private set; }
    public bool IsRunning { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (!IsRunning) return;

        ElapsedTime += Time.deltaTime;
    }

    public void StartTimer()
    {
        ElapsedTime = 0f;
        IsRunning = true;
    }

    public void StopTimer()
    {
        IsRunning = false;
    }
}
