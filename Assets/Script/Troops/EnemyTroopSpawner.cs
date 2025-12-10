using UnityEngine;

public class EnemyTroopSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public Transform spawnPoint;      // where enemy troops appear

    [Header("Troop Prefabs")]
    public GameObject troop1Prefab;   // short range / cheap
    public GameObject troop2Prefab;   // mid
    public GameObject troop3Prefab;   // long range / strong

    public void SpawnTroop1()
    {
        Spawn(troop1Prefab);
    }

    public void SpawnTroop2()
    {
        Spawn(troop2Prefab);
    }

    public void SpawnTroop3()
    {
        Spawn(troop3Prefab);
    }

    private void Spawn(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogWarning("EnemyTroopSpawner: prefab is not assigned.");
            return;
        }

        Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;
        Quaternion rot = spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;

        Instantiate(prefab, pos, rot);
    }
}

