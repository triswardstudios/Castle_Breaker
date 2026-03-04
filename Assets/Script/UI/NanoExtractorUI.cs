using UnityEngine;
using UnityEngine.UI;

public class NanoExtractorUI : MonoBehaviour
{
    public NanoExtractor extractor;

    [Header("UI References")]
    public Text levelText;
    public Text outputText;
    public Text costText;
    public Button upgradeButton;

    void Update()
    {
        if (extractor == null || EnergyManager.Instance == null)
            return;

        // Update level display
        levelText.text = "Level: " + extractor.level;

        // Update nano output display
        outputText.text = "Nano/sec: " + extractor.nanoPerSecond;

        if (extractor.level >= extractor.maxLevel)
        {
            costText.text = "MAX";
            upgradeButton.interactable = false;
            return;
        }

        int cost = extractor.GetUpgradeCost();
        costText.text = "Upgrade: " + cost;

        // Change colour depending on energy
        if (EnergyManager.Instance.currentEnergy >= cost)
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

    public void OnUpgradePressed()
    {
        extractor.UpgradeExtractor();
    }
}