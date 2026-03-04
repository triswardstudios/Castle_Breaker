using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance;

    [Header("Energy")]
    public int currentEnergy = 0;
    public int maxEnergy = 1000;

    [Header("Energy Rewards")]
    public int enemyKilledEnergy = 5;
    public int friendlyLostEnergy = 2;
    public int towerDamageEnergy = 3;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    
    // ENERGY FUNCTIONS
    public void AddEnergy(int amount)
    {
        currentEnergy += amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
    }

    public bool SpendEnergy(int amount)
    {
        if (currentEnergy < amount)
            return false;

        currentEnergy -= amount;
        return true;
    }

    
    // EVENT HELPERS
    public void OnEnemyKilled()
    {
        AddEnergy(enemyKilledEnergy);
    }

    public void OnFriendlyLost()
    {
        AddEnergy(friendlyLostEnergy);
    }

    public void OnEnemyTowerDamaged()
    {
        AddEnergy(towerDamageEnergy);
    }
}