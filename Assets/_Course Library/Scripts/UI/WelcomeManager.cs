using UnityEngine;

public class WelcomeManager : MonoBehaviour
{
    public GameObject welcomeCanvas;
    public TimerManager timerManager;
    public HealthUI healthUI;

    void Start()
    {
        GameState.IsGameStarted = false;
        InputGate.InputEnabled = false;
        welcomeCanvas.SetActive(true);
        healthUI.Hide();
        timerManager.HideTimer();
        timerManager.ResetTimer();
    }
    
    public void StartGame()
    {
        GameState.IsGameStarted = true;
        InputGate.InputEnabled = true;
        healthUI.Show();

        Time.timeScale = 1f;
        
        welcomeCanvas.SetActive(false);
        timerManager.StartTimer();
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
