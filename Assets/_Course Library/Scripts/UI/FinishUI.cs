using TMPro;
using UnityEngine;

public class FinishUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void OnEnable()
    {
        float t = RaceTimer.Instance.ElapsedTime;
        timeText.text = $"Time: {FormatTime(t)}";
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        float seconds = time % 60f;
        return $"{minutes:00}:{seconds:00.00}";
    }
}
