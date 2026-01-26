using UnityEngine;

public class WelcomeManager : MonoBehaviour
{
    public GameObject welcomeCanvas;
    // public TimerManager timerManager;

    void Start()
    {
        GameState.IsGameStarted = false;
        InputGate.InputEnabled = false;
        welcomeCanvas.SetActive(true);
        // timerManager.HideTimer();
        // timerManager.ResetTimer();
        if (RaceTimer.Instance != null)
        {
            RaceTimer.Instance.StopTimer();
        }
    }
    
    public void StartGame()
    {
        GameState.IsGameStarted = true;
        InputGate.InputEnabled = true;
        welcomeCanvas.SetActive(false);
        // timerManager.ShowTimer();
        // timerManager.StartTimer();
        if (RaceTimer.Instance != null)
        {
            RaceTimer.Instance.StartTimer();
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();

        // 下面这段只在 Editor 里有用，不影响 Build
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
