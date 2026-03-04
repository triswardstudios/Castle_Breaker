using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    [Header("Base Health")]
    public float baseMaxHealth = 1000f;

    [Header("Age Health Increase %")]
    public float[] ageHealthIncreasePercent = { 25f, 35f, 50f };

    public bool isPlayerTower = false;   // true for player tower, false for enemy tower

    private float currentHealth;
    private float maxHealth;

    public float CurrentHealth => currentHealth;

    void Start()
    {
        maxHealth = baseMaxHealth;
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (!isPlayerTower && EnergyManager.Instance != null)
        {
            EnergyManager.Instance.OnEnemyTowerDamaged();
        }
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
    }
    public void UpgradeHealth(int newAge)
    {
        if (newAge <= 1) return;

        int index = newAge - 2;

        if (index >= ageHealthIncreasePercent.Length) return;

        float percent = ageHealthIncreasePercent[index] / 100f;

        float oldMaxHealth = maxHealth;
        float totalDamageTaken = oldMaxHealth - currentHealth;

        maxHealth = oldMaxHealth + (oldMaxHealth * percent);

        currentHealth = maxHealth - totalDamageTaken;

        if (currentHealth < 0)
            currentHealth = 0;
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}

