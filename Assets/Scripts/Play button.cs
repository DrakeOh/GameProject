using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public string gameSceneName = "GameScene"; // Name of your game scene

    void Start()
    {
        // Assuming your button is attached to this GameObject
        Button button = GetComponent<Button>();
        // Add a listener to call the StartGame function when the button is clicked
        button.onClick.AddListener(StartGame);
    }

    // This function is called when the "Play" button is clicked
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName); // Load the game scene
                Time.timeScale = 1f;

    }
}
