using UnityEngine;
using UnityEngine.UI;

public class EnergyBarUI : MonoBehaviour
{
    public Slider energySlider;
    public Text energyText;

    void Start()
    {
        if (EnergyManager.Instance != null)
        {
            energySlider.maxValue = EnergyManager.Instance.maxEnergy;
        }
    }

    void Update()
    {
        if (EnergyManager.Instance == null) return;

        int current = EnergyManager.Instance.currentEnergy;
        int max = EnergyManager.Instance.maxEnergy;

        energySlider.value = current;
        energyText.text = $"{current}/{max}";
    }
}
