using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;        // Panel with Play & Quit buttons
    public GameObject pauseButtonObj;    // Optional: the small Pause button in HUD

    [Header("Settings")]
    public int mainMenuSceneIndex = 0;   // Scene index or use SceneManager.LoadScene("MainMenu")
    //public KeyCode toggleKey = KeyCode.Escape; // key to toggle pause

    bool isPaused = false;

    void Start()
    {
        // Ensure panel hidden at start
        if (pausePanel != null) pausePanel.SetActive(false);
    }

    //void Update()
    //{
    //    // Toggle pause with Escape (or configured key)
    //    if (Input.GetKeyDown(toggleKey))
    //    {
    //        if (isPaused) Resume();
    //        else Pause();
    //    }
    //}

    // Called by UI: open pause menu & stop time
    public void Pause()
    {
        if (isPaused) return;
        isPaused = true;

        // Stop game simulation
        Time.timeScale = 0f;

        // show panel, hide the small pause button if provided
        if (pausePanel != null) pausePanel.SetActive(true);
        if (pauseButtonObj != null) pauseButtonObj.SetActive(false);

        // show cursor for UI interaction
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Called by UI: resume game
    public void Resume()
    {
        if (!isPaused) return;
        isPaused = false;

        // resume simulation
        Time.timeScale = 1f;

        // hide panel, show small pause button
        if (pausePanel != null) pausePanel.SetActive(false);
        if (pauseButtonObj != null) pauseButtonObj.SetActive(true);

        // optional: hide cursor if your game uses it; otherwise leave visible
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Called by UI: quit to main menu (unpause then load)
    public void QuitToMainMenu()
    {
        // restore timescale to normal before scene load
        Time.timeScale = 1f;
        isPaused = false;

        // load main menu (scene index or name)
        SceneManager.LoadScene(mainMenuSceneIndex);
    }

    // Helper to be used by the small Pause button in HUD
    public void OnPauseButtonPressed()
    {
        Pause();
    }
}


