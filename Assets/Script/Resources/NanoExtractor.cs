using UnityEngine;

public class NanoExtractor : MonoBehaviour
{
    [Header("Production")]
    public float nanoPerSecond = 5f;

    [Header("Level System")]
    public int level = 1;
    public int maxLevel = 3;

    [Tooltip("Upgrade cost for each level (index 0 = level 1->2 cost)")]
    public int[] upgradeCosts;

    [Tooltip("Nano increase % per level (index 0 = level 1->2 increase %)")]
    public float[] nanoIncreasePercent;

    private float timer = 0f;

    void Update()
    {
        GenerateNano();
    }

    void GenerateNano()
    {
        if (PointBar.Instance == null) return;

        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            timer = 0f;

            PointBar.Instance.AddPoints(Mathf.RoundToInt(nanoPerSecond));
        }
    }

    // UPGRADE FUNCTION
    public void UpgradeExtractor()
    {
        if (level >= maxLevel) return;

        int cost = GetUpgradeCost();

        if (EnergyManager.Instance == null) return;

        if (EnergyManager.Instance.SpendEnergy(cost))
        {
            ApplyUpgrade();
        }
    }

    void ApplyUpgrade()
    {
        // Apply percentage increase
        float percent = nanoIncreasePercent[level - 1] / 100f;
        nanoPerSecond += nanoPerSecond * percent;

        level++;
    }

    public int GetUpgradeCost()
    {
        if (level - 1 < upgradeCosts.Length)
            return upgradeCosts[level - 1];

        return 0;
    }
}