using UnityEngine;

public class AgeManager : MonoBehaviour
{
    public static AgeManager Instance;

    [Header("Age System")]
    public int currentAge = 1;
    public int maxAge = 4;

    [Header("Upgrade Costs (Nano Chips)")]
    public int[] ageUpgradeCosts;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool CanUpgradeAge()
    {
        if (currentAge >= maxAge)
            return false;

        int cost = GetUpgradeCost();

        if (PointBar.Instance == null)
            return false;

        return PointBar.Instance.HasEnoughPoints(cost);
    }

    public void UpgradeAge()
    {
        if (!CanUpgradeAge())
            return;

        int cost = GetUpgradeCost();

        if (PointBar.Instance.SpendPoints(cost))
        {
            currentAge++;

            UpgradeAllTowers();
        }
    }

    public int GetUpgradeCost()
    {
        if (currentAge - 1 < ageUpgradeCosts.Length)
            return ageUpgradeCosts[currentAge - 1];

        return 0;
    }
    void UpgradeAllTowers()
    {
        TowerHealth[] towers = FindObjectsByType<TowerHealth>(FindObjectsSortMode.None);

        foreach (TowerHealth tower in towers)
        {
            tower.UpgradeHealth(currentAge);
        }
    }
}
