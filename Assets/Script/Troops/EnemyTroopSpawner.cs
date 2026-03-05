using UnityEngine;
using static TroopCounter;

[System.Serializable]
public class EnemyTroopAgeSet
{
    public GameObject[] troopPrefabs; // 4 troops per age
}

public class EnemyTroopSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    [Header("Troops Per Age (4 troops each)")]
    public EnemyTroopAgeSet[] ageTroopSets; // Size = 4 (for 4 ages)

    void Spawn(GameObject prefab)
    {
        if (prefab == null) return;

        if (TroopCounter.Instance != null && !TroopCounter.Instance.CanSpawn(TroopSide.Enemy))
        {
            Debug.Log("Enemy cannot spawn more troops (limit reached).");
            return;
        }

        Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;
        Quaternion rot = spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;

        Instantiate(prefab, pos, rot);
    }

    public void SpawnTroop(int troopIndex)
    {
        if (AgeManager.Instance == null) return;

        int ageIndex = AgeManager.Instance.currentAge - 1;

        if (ageIndex >= ageTroopSets.Length) return;
        if (troopIndex >= ageTroopSets[ageIndex].troopPrefabs.Length) return;

        GameObject prefab = ageTroopSets[ageIndex].troopPrefabs[troopIndex];

        Spawn(prefab);
    }
}


