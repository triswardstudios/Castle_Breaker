using UnityEngine;
using UnityEngine.UI;

public class TowerHealthBarUI : MonoBehaviour
{
    [Header("Tower Reference")]
    public TowerHealth tower;     // assign the tower here

    [Header("UI")]
    public Slider healthSlider;   // the slider that shows HP
    //public Text healthText;       // optional: "150 / 200" display

    void Start()
    {
        if (tower == null)
        {
            Debug.LogError("TowerHealthBarUI: No tower assigned.");
            enabled = false;
            return;
        }

        if (healthSlider != null)
        {
            healthSlider.minValue = 0f;
            healthSlider.maxValue = tower.baseMaxHealth;
            healthSlider.value = tower.CurrentHealth;
        }

        //UpdateText();
    }

    void Update()
    {
        if (tower == null || healthSlider == null)
            return;

        healthSlider.value = tower.CurrentHealth;
        //UpdateText();
    }

    //void UpdateText()
    //{
    //    if (healthText == null || tower == null) return;

    //    healthText.text = $"{Mathf.CeilToInt(tower.CurrentHealth)}/{Mathf.CeilToInt(tower.maxHealth)}";
    //}
}
