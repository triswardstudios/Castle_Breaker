using UnityEngine;
using UnityEngine.UI;

public class PointBar : MonoBehaviour
{
    [Header("Point Bar Settings")]
    public Slider pointSlider;      // Assign your UI Slider here
    public int maxPoints = 100;     // Max value of bar
    public int startPoints = 50;    // Default starting points

    public int CurrentPoints { get; private set; }

    void Start()
    {
        // Initialize values
        pointSlider.maxValue = maxPoints;
        CurrentPoints = Mathf.Clamp(startPoints, 0, maxPoints);
        pointSlider.value = CurrentPoints;
    }

    public void AddPoints(int amount)
    {
        CurrentPoints = Mathf.Clamp(CurrentPoints + amount, 0, maxPoints);
        pointSlider.value = CurrentPoints;
    }

    public bool HasEnoughPoints(int cost)
    {
        return CurrentPoints >= cost;
    }

    public bool SpendPoints(int cost)
    {
        if (!HasEnoughPoints(cost))
            return false;

        CurrentPoints -= cost;
        pointSlider.value = CurrentPoints;
        return true;
    }
}


