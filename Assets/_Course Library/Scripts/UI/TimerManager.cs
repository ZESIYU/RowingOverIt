using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private float elapsedTime = 0f;
    private bool isRunning = false;

    void Update()
    {
        if (!isRunning)
            return;

        elapsedTime += Time.deltaTime;
        UpdateTimerUI();
    }

    // ======================
    // 控制
    // ======================

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
        UpdateTimerUI();
    }

    // ======================
    // UI
    // ======================

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        float seconds = elapsedTime % 60f;

        timerText.text = $"{minutes:00}:{seconds:00.00}";
    }

    public void ShowTimer()
    {
        timerText.gameObject.SetActive(true);
    }

    public void HideTimer()
    {
        timerText.gameObject.SetActive(false);
    }

    // ======================
    // 给外部用
    // ======================

    public float GetCurrentTime()
    {
        return elapsedTime;
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        float seconds = elapsedTime % 60f;
        return $"{minutes:00}:{seconds:00.00}";
    }
}