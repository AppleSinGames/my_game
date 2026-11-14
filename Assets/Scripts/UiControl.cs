using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiControl : MonoBehaviour
{
    [Header("___________TEXT___________")]

    [Header("___________OBJS___________")]
    [SerializeField] GameObject Death;
    [SerializeField] GameObject Win;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject PlayerUi;

    public static UiControl Instance;

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ClearHighScore()
    {
        Saves.ClearNum("HighScore");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }

    public void ExitGame()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void DeathScreen()
    {
        Time.timeScale = 0f;
        PlayerUi.SetActive(false);
        Death.SetActive(true);
        Saves.ChangeHighScore(PlayerUiControl.Score);
    }
    public void WinScreen()
    {
        Time.timeScale = 0f;
        Win.SetActive(true);
        PlayerUi.SetActive(false);
        Saves.ChangeHighScore(PlayerUiControl.Score);
    }

    private void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this;
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Time.timeScale = 0f;
        }
        else 
        {
            PausePanel.SetActive(false);
            Death.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}


