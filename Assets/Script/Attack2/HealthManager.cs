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
        Debug.Log("Game Over! Player Tower Destroyed.");
        // Here you can add UI, scene reload, etc.
    }

    public void OnEnemyTowerZero()
    {
        if (gameEnded) return;
        gameEnded = true;
        Debug.Log("Player Wins! Enemy Tower Destroyed.");
        // Here you can add win UI, next level, etc.
    }
}

