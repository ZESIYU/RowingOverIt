using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject welcomePanel;
    public GameObject tutorialPanel;

    void Start()
    {
        ShowWelcome();
    }

    public void ShowWelcome()
    {
        welcomePanel.SetActive(true);
        tutorialPanel.SetActive(false);
    }

    public void ShowTutorial()
    {
        welcomePanel.SetActive(false);
        tutorialPanel.SetActive(true);
    }
}
