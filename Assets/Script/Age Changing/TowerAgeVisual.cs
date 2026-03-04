using UnityEngine;

public class TowerAgeVisual : MonoBehaviour
{
    [Header("Tower Age Sprites (in order)")]
    public GameObject[] ageSprites; // 0 = age1, 1 = age2, etc

    private int currentAgeShown = -1;

    void Start()
    {
        UpdateTowerVisual();
    }

    void Update()
    {
        if (AgeManager.Instance == null) return;

        // Only update when age changes
        if (currentAgeShown != AgeManager.Instance.currentAge)
        {
            UpdateTowerVisual();
        }
    }

    void UpdateTowerVisual()
    {
        int age = AgeManager.Instance.currentAge;

        currentAgeShown = age;

        for (int i = 0; i < ageSprites.Length; i++)
        {
            ageSprites[i].SetActive(i == age - 1);
        }
    }
}