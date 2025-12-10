using UnityEngine;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour
{
    public static GameStatus Instance;

    [Header("UI References")]
    public GameObject statusPanel;   // a panel or empty object that contains the text
    public Text statusText;          // UI Text that will show "Winner" or "Game Over"

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Hide on start
        if (statusPanel != null)
            statusPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        if (statusPanel != null) statusPanel.SetActive(true);
        if (statusText != null) statusText.text = "Game Over";
        Debug.Log("Game Over! Player Tower Destroyed.");
    }

    public void ShowWinner()
    {
        if (statusPanel != null) statusPanel.SetActive(true);
        if (statusText != null) statusText.text = "Winner";
        Debug.Log("Player Wins! Enemy Tower Destroyed.");
    }
}

