using UnityEngine;
using static TroopCounter;

public class TroopHealth : MonoBehaviour
{
    public float maxHealth = 100f;

    [Header("Which side this troop belongs to")]
    public TroopSide side = TroopSide.Player;

    private float currentHealth;

    void OnEnable()
    {
        currentHealth = maxHealth;

        // Count this troop as active
        if (TroopCounter.Instance != null)
        {
            TroopCounter.Instance.RegisterTroop(side);
        }
    }

    void OnDisable()
    {
        // When a troop is disabled (dead / removed), reduce count
        if (TroopCounter.Instance != null)
        {
            TroopCounter.Instance.UnregisterTroop(side);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    void Die()
    {
        // Just disable the troop, OnDisable will update the counter
        gameObject.SetActive(false);
        PointBar.Instance.AddPoints(PointBar.Instance.rewardPerUnitLost);
        if (EnergyManager.Instance != null)
        {
            if (side == TroopSide.Player)
                EnergyManager.Instance.OnFriendlyLost();
            else
                EnergyManager.Instance.OnEnemyKilled();
        }
    }
}


