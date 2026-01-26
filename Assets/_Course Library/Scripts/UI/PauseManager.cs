using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    // public TimerManager timerManager;
    public PauseMenuPositioner positioner;

    private InputAction menuAction;
    private bool isPaused = false;

    void Awake()
    {
        // 监听 XR Controller 的 Menu Button
        menuAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>/menuButton"
        );
    }

    void OnEnable()
    {
        menuAction.Enable();
        menuAction.performed += OnMenuPressed;
    }

    void OnDisable()
    {
        menuAction.performed -= OnMenuPressed;
        menuAction.Disable();
    }

    void OnMenuPressed(InputAction.CallbackContext context)
    {
        if (!GameState.IsGameStarted)
            return;

        if (!isPaused)
        {
            PauseGame();
        }
    }

    void Start()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    void PauseGame()
    {
        isPaused = true;
        InputGate.InputEnabled = false;
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        // timerManager.StopTimer();
        positioner.PlaceInFrontOfPlayer();
    }

    public void ResumeGame()
    {
        isPaused = false;
        InputGate.InputEnabled = true;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        // timerManager.StartTimer();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;   // 非常重要：先恢复时间
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}