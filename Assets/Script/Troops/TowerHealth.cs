using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    public float maxHealth = 200f;
    public bool isPlayerTower = false;   // true for player tower, false for enemy tower

    private float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            OnZeroHealth();
        }
    }

    void OnZeroHealth()
    {
        if (HealthManager.Instance == null)
        {
            Debug.LogWarning("No HealthManager found in scene.");
            return;
        }

        if (isPlayerTower)
        {
            HealthManager.Instance.OnPlayerTowerZero();
        }
        else
        {
            HealthManager.Instance.OnEnemyTowerZero();
        }

        // DO NOT disable the tower object (as you requested)
        // You can play a broken animation or change sprite here instead.
    }
}

