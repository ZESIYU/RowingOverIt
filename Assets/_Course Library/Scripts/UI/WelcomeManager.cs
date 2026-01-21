using UnityEngine;

public class WelcomeManager : MonoBehaviour
{
    public GameObject welcomeCanvas;

    void Start()
    {
        GameState.IsGameStarted = false;
        welcomeCanvas.SetActive(true);
    }
    
    public void StartGame()
    {
        GameState.IsGameStarted = true;
        welcomeCanvas.SetActive(false);
    }
}
