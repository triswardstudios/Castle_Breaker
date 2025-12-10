using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    private bool gameEnded = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void OnPlayerTowerZero()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (GameStatus.Instance != null)
            GameStatus.Instance.ShowGameOver();
        else
            Debug.Log("Game Over! (No GameStatus found)");
    }

    public void OnEnemyTowerZero()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (GameStatus.Instance != null)
            GameStatus.Instance.ShowWinner();
        else
            Debug.Log("Player Wins! (No GameStatus found)");
    }
}


