using UnityEngine;
using UnityEngine.UI;

public class PlayerTroopCounterUI : MonoBehaviour
{
    [Header("UI")]
    public Text counterText;   // UI Text (7/10)

    void Update()
    {
        if (TroopCounter.Instance == null || counterText == null)
            return;

        int current = TroopCounter.Instance.playerTroopCount;
        int max = TroopCounter.Instance.maxTroopsPerSide;

        counterText.text = $"{current}/{max}";
    }
}

