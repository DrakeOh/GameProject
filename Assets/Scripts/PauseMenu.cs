using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool IsPaused;
    public Button resumeButton;
    public Button mainMenu;
    public Button PlayAgain;
    public string gameSceneName = "GameScene";
    public string mainMenuSceneName = "MainMenu";
    public string mainMenuSceneGamneOver = "PlayAgain";
    public GameObject GameOverUI;

    void Start()
    {
        pauseMenu.SetActive(false);
        // Assuming you have assigned the ResumeButton in the Unity Editor
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenu.onClick.AddListener(StartGame);
        PlayAgain.onClick.AddListener(Restart);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void gameOver()
    {
        GameOverUI.SetActive(true);
        mainMenu.onClick.AddListener(StartGame);

    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName); // Load the game scene
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName); // Load the main menu scene
    }
    public void Restart()
    {
        SceneManager.LoadScene(mainMenuSceneGamneOver); // Load the main menu scene
    }
}
