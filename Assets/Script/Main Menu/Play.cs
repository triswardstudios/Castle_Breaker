using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    [Header("UI")]
    public GameObject difficultyPanel;   // assign in Inspector

    // Called by the main "Play" button
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

    public void ExitGame()
    {
        Application.Quit();
    }
}

