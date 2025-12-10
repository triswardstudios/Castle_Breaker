using UnityEngine;
using static TroopCounter;

public class EnemyTroopSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject troop1Prefab;
    public GameObject troop2Prefab;
    public GameObject troop3Prefab;

    public void SpawnTroop1()
    {
        if (TroopCounter.Instance != null && !TroopCounter.Instance.CanSpawn(TroopSide.Enemy))
        {
            Debug.Log("Enemy cannot spawn more troops (limit reached).");
            return;
        }

        Spawn(troop1Prefab);
    }

    public void SpawnTroop2()
    {
        if (TroopCounter.Instance != null && !TroopCounter.Instance.CanSpawn(TroopSide.Enemy))
        {
            Debug.Log("Enemy cannot spawn more troops (limit reached).");
            return;
        }

        Spawn(troop2Prefab);
    }

    public void SpawnTroop3()
    {
        if (TroopCounter.Instance != null && !TroopCounter.Instance.CanSpawn(TroopSide.Enemy))
        {
            Debug.Log("Enemy cannot spawn more troops (limit reached).");
            return;
        }

        Spawn(troop3Prefab);
    }

    void Spawn(GameObject prefab)
    {
        if (prefab == null) return;

        Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;
        Quaternion rot = spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;

        Instantiate(prefab, pos, rot);
    }
}


