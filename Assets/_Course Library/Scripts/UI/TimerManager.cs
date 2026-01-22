using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject timerCanvas;

    private float elapsedTime = 0f;
    private bool isRunning = false;

    void Start()
    {
        HideTimer();
        ResetTimer();
    }

    void Update()
    {
        if (!isRunning)
            return;

        elapsedTime += Time.deltaTime;
        UpdateUI();
    }

    void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void ShowTimer()
    {
        timerCanvas.SetActive(true);
    }

    public void HideTimer()
    {
        timerCanvas.SetActive(false);
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateUI();
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}