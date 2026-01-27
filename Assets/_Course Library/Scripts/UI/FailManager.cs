using UnityEngine;
using UnityEngine.SceneManagement;

public class FailManager : MonoBehaviour
{
    public GameObject failCanvas; // 这个就是 FailCanvas

    void Start()
    {
        failCanvas.SetActive(false); // 游戏开始隐藏
    }

    public void ShowFail()
    {
        failCanvas.SetActive(true);
        InputGate.InputEnabled = false; // 阻止玩家操作
        Time.timeScale = 0f;           // 暂停游戏
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // 先恢复时间
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 编辑器退出
#else
        Application.Quit(); // 打包后退出
#endif
    }
}
