using UnityEngine;
using UnityEngine.UI;

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

    void SpawnTroop1()
    {
        TrySpawn(troop1Prefab, cost1);
    }

    void SpawnTroop2()
    {
        TrySpawn(troop2Prefab, cost2);
    }

    void SpawnTroop3()
    {
        TrySpawn(troop3Prefab, cost3);
    }

    void TrySpawn(GameObject prefab, int cost)
    {
        if (prefab == null || spawnPoint == null || pointBar == null)
            return;

        // Spend points only if enough
        if (pointBar.SpendPoints(cost))
        {
            Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}

