using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiControl : MonoBehaviour
{
    [Header("___________TEXT___________")]
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text _highScoreText;
    [Header("___________OBJS___________")]
    [SerializeField] GameObject Death;
    [SerializeField] GameObject PausePanel;

    public static int Score;
    public int _highScore;

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
        Death.SetActive(true);
        Saves.ChangeHighScore(Score);
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
            Score = 0;
            Time.timeScale = 1f;

            _highScore = Saves.SetHighScore();

            if (_highScoreText != null)
            {
                _highScoreText.text = _highScore.ToString();
            }
        }
    }

    void Update()
    {
        if (ScoreText != null)
        {
            ScoreText.text = Score.ToString();
        }
    }
}


