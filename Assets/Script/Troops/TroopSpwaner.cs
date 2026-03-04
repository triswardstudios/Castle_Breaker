using UnityEngine;
using UnityEngine.UI;

public class TroopSpawn : MonoBehaviour
{
    [Header("References")]
    public PointBar pointBar;       // Assign the PointBar script here
    public Transform spawnPoint;    // Where troops will appear

    [Header("Troop Buttons")]
    public Button[] troopButtons;

    [Header("Troop Sets Per Age")]
    [SerializeField] private GameObject[][] troopPrefabsPerAge;
    [SerializeField] private int[][] troopCostsPerAge;

    private int currentAgeIndex = 0;

    void Start()
    {
        UpdateAgeTroops();
    }

    void Update()
    {
        if (AgeManager.Instance == null) return;

        int newAgeIndex = AgeManager.Instance.currentAge - 1;

        if (newAgeIndex != currentAgeIndex)
        {
            currentAgeIndex = newAgeIndex;
            UpdateAgeTroops();
        }
    }

    void UpdateAgeTroops()
    {
        for (int i = 0; i < troopButtons.Length; i++)
        {
            int index = i;

            troopButtons[i].onClick.RemoveAllListeners();
            troopButtons[i].onClick.AddListener(() => SpawnTroop(index));
        }
    }

    void SpawnTroop(int index)
    {
        if (AgeManager.Instance == null) return;
        if (PointBar.Instance == null) return;

        GameObject prefab = troopPrefabsPerAge[currentAgeIndex][index];
        int cost = troopCostsPerAge[currentAgeIndex][index];

        if (!PointBar.Instance.SpendPoints(cost))
            return;

        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
}
