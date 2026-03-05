using UnityEngine;
using UnityEngine.UI;

public class TroopSpawn : MonoBehaviour
{
    [Header("References")]
    public PointBar pointBar;       // Assign the PointBar script here
    public Transform spawnPoint;    // Where troops will appear

    
    [Header("Troop Buttons")]
    public Button[] troopButtons;

    [Header("Troops Per Age")]
    public TroopAgeSet[] ageTroopSets;

    private int currentAgeIndex = 0;

    void Start()
    {
        //UpdateAgeTroops();
        UpdateButtons();
    }

    void Update()
    {
        if (AgeManager.Instance == null) return;

        int newAgeIndex = AgeManager.Instance.currentAge - 1;

        if (newAgeIndex != currentAgeIndex)
        {
            currentAgeIndex = newAgeIndex;
            UpdateButtons();
        }
    }
    void UpdateButtons()
    {
        for (int i = 0; i < troopButtons.Length; i++)
        {
            int index = i;

            troopButtons[i].onClick.RemoveAllListeners();
            troopButtons[i].onClick.AddListener(() => SpawnTroop(index));
        }
    }

    //void UpdateAgeTroops()
    //{
    //    for (int i = 0; i < troopButtons.Length; i++)
    //    {
    //        int index = i;

    //        troopButtons[i].onClick.RemoveAllListeners();
    //        troopButtons[i].onClick.AddListener(() => SpawnTroop(index));
    //    }
    //}

    void SpawnTroop(int index)
    {
        if (PointBar.Instance == null) return;

        GameObject prefab = ageTroopSets[currentAgeIndex].troopPrefabs[index];
        int cost = ageTroopSets[currentAgeIndex].troopCosts[index];

        if (!PointBar.Instance.SpendPoints(cost))
            return;

        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
}

