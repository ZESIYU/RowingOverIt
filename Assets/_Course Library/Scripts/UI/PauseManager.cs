using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public TimerManager timerManager;
    public PauseMenuPositioner positioner;
    public GameObject pausePanel;
    public GameObject respawnPanel;

    Rigidbody playerRB;

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
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    void PauseGame()
    {
        isPaused = true;
        InputGate.InputEnabled = false;
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        timerManager.StopTimer();
        positioner.PlaceInFrontOfPlayer();
        pausePanel.SetActive(true);
        respawnPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;
        InputGate.InputEnabled = true;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        timerManager.StartTimer();
        pausePanel.SetActive(false);
        respawnPanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;   // 非常重要：先恢复时间
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenRespawnMenu()
    {
        pausePanel.SetActive(false);
        respawnPanel.SetActive(true);
    }

    public void BackToPause()
    {
        pausePanel.SetActive(true);
        respawnPanel.SetActive(false);
    }

    public void TeleportToCheckpoint(int checkpointIndex)
    {
        Time.timeScale = 1f;

        CheckpointManager.Instance.TeleportToCheckpoint(
            checkpointIndex,
            playerRB
        );

        pausePanel.SetActive(false);
        respawnPanel.SetActive(false);

        isPaused = false;
        InputGate.InputEnabled = true;
    }
}