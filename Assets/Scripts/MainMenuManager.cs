using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainButtons;
    public GameObject playPanel;
    public GameObject settingsPanel;
    public GameObject exitPanel;

    public void ShowMainMenu()
    {
        mainButtons.SetActive(true);
        playPanel.SetActive(false);
        settingsPanel.SetActive(false);
        exitPanel.SetActive(false);
    }

    public void ShowPlayPanel()
    {
        mainButtons.SetActive(false);
        playPanel.SetActive(true);
        settingsPanel.SetActive(false);
        exitPanel.SetActive(false);
    }

    public void ShowSettingsPanel()
    {
        mainButtons.SetActive(false);
        playPanel.SetActive(false);
        settingsPanel.SetActive(true);
        exitPanel.SetActive(false);
    }

    public void ShowExitPanel()
    {
        mainButtons.SetActive(false);
        playPanel.SetActive(false);
        settingsPanel.SetActive(false);
        exitPanel.SetActive(true);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Classroom");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("DungeonLevel1");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
}