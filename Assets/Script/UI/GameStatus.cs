using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    public static GameStatus Instance;

    [Header("UI")]
    public GameObject statusPanel;   // The whole panel
    public Text statusText;          // Winner / Game Over text
    public Button playButton;        // Play Again / Play button
    public Button quitButton;        // Quit button

    [Header("Scenes")]
    public int mainMenuSceneIndex = 0;   // 0 = Main Menu Scene
    public int gameSceneIndex = 1;       // Game Scene (if you want "Play Again")

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (statusPanel != null)
            statusPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        statusPanel.SetActive(true);
        statusText.text = "Game Over";

        SetupButtons();
    }

    public void ShowWinner()
    {
        statusPanel.SetActive(true);
        statusText.text = "Winner";

        SetupButtons();
    }

    void SetupButtons()
    {
        // Assign button listeners
        playButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();

        // Play Again loads main menu (OR game scene — your choice)
        playButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(mainMenuSceneIndex); // or gameSceneIndex
        });

        // Quit also loads main menu
        quitButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(mainMenuSceneIndex);
        });
    }
}


