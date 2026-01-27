using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinishManager : MonoBehaviour
{
    public GameObject finishCanvas;
    public TextMeshProUGUI timeText;
    public TimerManager timerManager;

    public void OnFinish()
    {
        Debug.Log("FinishManager OnFinish");

        InputGate.InputEnabled = false;
        Time.timeScale = 0f;

        finishCanvas.SetActive(true);

        float time = timerManager.GetCurrentTime();
        timerManager.StopTimer(); // ✅ 很重要，防止最后一帧跳动
        timeText.text = $"Time: {timerManager.GetFormattedTime()}";
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
