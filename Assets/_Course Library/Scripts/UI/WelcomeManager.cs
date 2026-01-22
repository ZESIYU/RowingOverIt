using UnityEngine;

public class WelcomeManager : MonoBehaviour
{
    public GameObject welcomeCanvas;
    public TimerManager timerManager;

    void Start()
    {
        GameState.IsGameStarted = false;
        welcomeCanvas.SetActive(true);
        timerManager.HideTimer();
        timerManager.ResetTimer();
    }
    
    public void StartGame()
    {
        GameState.IsGameStarted = true;
        welcomeCanvas.SetActive(false);
        timerManager.ShowTimer();
        timerManager.StartTimer();
    }
}
