using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool IsPaused;
    public Button resumeButton;
    public Button mainMenu;
  
    public string mainMenuSceneName = "MainMenu";
    void Start()
    {
        pauseMenu.SetActive(false);
        // Assuming you have assigned the ResumeButton in the Unity Editor
        resumeButton.onClick.AddListener(ResumeGame);
     
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
  

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName); // Load the main menu scene
    }
}
