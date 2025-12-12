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

    [Header("UI")]
    public GameObject difficultyPanel;

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
        quitButton.onClick.RemoveAllListeners();

        // Quit also loads main menu
        quitButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(mainMenuSceneIndex);
        });
    }

    public void PlayGame()
    {
        if (difficultyPanel != null)
        {
            difficultyPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Play: Difficulty panel is not assigned!");
        }
    }

    // Called by Easy / Medium / Hard buttons
    public void PlayEasy()
    {
        SetDifficultyAndStart(0); // 0 = Easy
    }

    public void PlayMedium()
    {
        SetDifficultyAndStart(1); // 1 = Medium
    }

    public void PlayHard()
    {
        SetDifficultyAndStart(2); // 2 = Hard
    }

    void SetDifficultyAndStart(int difficultyIndex)
    {
        // Save difficulty for next scene
        PlayerPrefs.SetInt("Difficulty", difficultyIndex);
        PlayerPrefs.Save();

        // Load your game scene (index 1 like before)
        SceneManager.LoadSceneAsync(1);
    }

}


