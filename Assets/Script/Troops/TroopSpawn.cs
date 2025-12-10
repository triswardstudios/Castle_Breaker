using UnityEngine;
using UnityEngine.UI;
using static TroopCounter;

public class TroopSpawn : MonoBehaviour
{
    [Header("References")]
    public PointBar pointBar;       // Assign the PointBar script here
    public Transform spawnPoint;    // Where troops will appear

    [Header("Buttons")]
    public Button button1;
    public Button button2;
    public Button button3;

    [Header("Costs")]
    public int cost1 = 15;
    public int cost2 = 25;
    public int cost3 = 45;

    [Header("Troop Prefabs")]
    public GameObject troop1Prefab;
    public GameObject troop2Prefab;
    public GameObject troop3Prefab;

    void Start()
    {
        // Hook up button clicks (or you can assign in Inspector)
        if (button1 != null) button1.onClick.AddListener(SpawnTroop1);
        if (button2 != null) button2.onClick.AddListener(SpawnTroop2);
        if (button3 != null) button3.onClick.AddListener(SpawnTroop3);
    }

    void Update()
    {
        if (pointBar == null) return;

        int points = pointBar.CurrentPoints;

        // Enable / disable buttons based on current points
        if (button1 != null) button1.interactable = points >= cost1;
        if (button2 != null) button2.interactable = points >= cost2;
        if (button3 != null) button3.interactable = points >= cost3;
    }

    public void SpawnTroop1()
    {
        if (TroopCounter.Instance != null && !TroopCounter.Instance.CanSpawn(TroopSide.Player))
        {
            Debug.Log("Player cannot spawn more troops (limit reached).");
            return;
        }
        TrySpawn(troop1Prefab, cost1);
    }

    public void SpawnTroop2()
    {
        if (TroopCounter.Instance != null && !TroopCounter.Instance.CanSpawn(TroopSide.Player))
        {
            Debug.Log("Player cannot spawn more troops (limit reached).");
            return;
        }
        TrySpawn(troop2Prefab, cost2);
    }

    public void SpawnTroop3()
    {
        if (TroopCounter.Instance != null && !TroopCounter.Instance.CanSpawn(TroopSide.Player))
        {
            Debug.Log("Player cannot spawn more troops (limit reached).");
            return;
        }
        TrySpawn(troop3Prefab, cost3);
    }

    void TrySpawn(GameObject prefab, int cost)
    {
       //Troop1
        if (prefab == null || spawnPoint == null || pointBar == null)
            return;

        if (!pointBar.SpendPoints(cost))
            return;
       
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        ////Troop2
        //if (troop2Prefab == null || spawnPoint == null || pointBar == null)
        //    return;

        //if (!pointBar.SpendPoints(cost2))
        //    return;

        //Instantiate(troop2Prefab, spawnPoint.position, spawnPoint.rotation);
       
        ////Troop3
        //if (troop3Prefab == null || spawnPoint == null || pointBar == null)
        //    return;

        //if (!pointBar.SpendPoints(cost3))
        //    return;

        //Instantiate(troop3Prefab, spawnPoint.position, spawnPoint.rotation);
    }
    
}

