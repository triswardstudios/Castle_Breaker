using UnityEngine;
using UnityEngine.UI;

public class AgeUpgradeUI : MonoBehaviour
{
    public Text ageText;
    public Text costText;
    public Button upgradeButton;

    void Update()
    {
        if (AgeManager.Instance == null)
            return;

        ageText.text = "Age: " + AgeManager.Instance.currentAge;

        if (AgeManager.Instance.currentAge >= AgeManager.Instance.maxAge)
        {
            costText.text = "MAX";
            upgradeButton.interactable = false;
            return;
        }

        int cost = AgeManager.Instance.GetUpgradeCost();
        costText.text = "Upgrade: " + cost;

        if (PointBar.Instance.CurrentPoints >= cost)
        {
            costText.color = Color.green;
            upgradeButton.interactable = true;
        }
        else
        {
            costText.color = Color.red;
            upgradeButton.interactable = false;
        }
    }

    public void UpgradeAge()
    {
        AgeManager.Instance.UpgradeAge();
    }
}
