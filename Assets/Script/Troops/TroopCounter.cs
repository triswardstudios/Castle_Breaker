using UnityEngine;

public class TroopCounter : MonoBehaviour
{
    public enum TroopSide
    {
        Player,
        Enemy
    }

    public static TroopCounter Instance;

    [Header("Limits")]
    [Tooltip("Maximum number of active troops allowed per side (Player / Enemy).")]
    public int maxTroopsPerSide = 10;

    [Header("Runtime (read-only)")]
    public int playerTroopCount;
    public int enemyTroopCount;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool CanSpawn(TroopSide side)
    {
        switch (side)
        {
            case TroopSide.Player:
                return playerTroopCount < maxTroopsPerSide;
            case TroopSide.Enemy:
                return enemyTroopCount < maxTroopsPerSide;
            default:
                return true;
        }
    }

    public void RegisterTroop(TroopSide side)
    {
        switch (side)
        {
            case TroopSide.Player:
                playerTroopCount++;
                break;
            case TroopSide.Enemy:
                enemyTroopCount++;
                break;
        }
        // Clamp just to be safe
        playerTroopCount = Mathf.Max(0, playerTroopCount);
        enemyTroopCount = Mathf.Max(0, enemyTroopCount);
    }

    public void UnregisterTroop(TroopSide side)
    {
        switch (side)
        {
            case TroopSide.Player:
                playerTroopCount--;
                break;
            case TroopSide.Enemy:
                enemyTroopCount--;
                break;
        }
        playerTroopCount = Mathf.Max(0, playerTroopCount);
        enemyTroopCount = Mathf.Max(0, enemyTroopCount);
    }
}

