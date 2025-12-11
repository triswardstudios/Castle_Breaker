using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PointBar : MonoBehaviour
{
    public static PointBar Instance;

    [Header("UI")]
    public Slider pointSlider;      // assign your UI Slider here

    [Header("Points")]
    public int maxPoints = 100;
    public int startPoints = 50;

    [Header("Animation")]
    [Tooltip("Duration (seconds) to animate the slider when value changes.")]
    public float animateDuration = 0.4f;

    [Tooltip("If true, slider uses whole numbers (integers).")]
    public bool wholeNumbers = true;

    public int CurrentPoints { get; private set; }
    public int rewardPerKill = 5;


    // internal coroutine handle so multiple calls interrupt cleanly
    Coroutine animateCoroutine;

    void Awake()
    {
        // Singleton (optional but convenient)
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (pointSlider == null)
        {
            Debug.LogError("PointBar: pointSlider not assigned in inspector.");
            return;
        }

        pointSlider.wholeNumbers = wholeNumbers;
        pointSlider.maxValue = maxPoints;

        CurrentPoints = Mathf.Clamp(startPoints, 0, maxPoints);
        // initialize slider instantly (no animation on start)
        pointSlider.value = CurrentPoints;
    }

    // Public API --------------------------------------------------------

    public void SetMaxValue(int max)
    {
        maxPoints = max;
        pointSlider.maxValue = maxPoints;

        // clamp current and animate to new value if needed
        CurrentPoints = Mathf.Clamp(CurrentPoints, 0, maxPoints);
        AnimateTo(CurrentPoints);
    }

    public void SetValue(int value)
    {
        value = Mathf.Clamp(value, 0, maxPoints);
        CurrentPoints = value;
        AnimateTo(CurrentPoints);
    }

    public void AddPoints(int amount)
    {
        SetValue(CurrentPoints + amount);
    }

    public bool HasEnoughPoints(int cost)
    {
        return CurrentPoints >= cost;
    }

    public bool SpendPoints(int cost)
    {
        if (!HasEnoughPoints(cost)) return false;
        SetValue(CurrentPoints - cost);
        return true;
    }

    // Animates the slider value to the target (interrupts existing animation)
    void AnimateTo(int target)
    {
        // Stop any ongoing animation
        if (animateCoroutine != null)
        {
            StopCoroutine(animateCoroutine);
            animateCoroutine = null;
        }

        // If duration is effectively zero, set instantly
        if (animateDuration <= 0f)
        {
            pointSlider.value = target;
            return;
        }

        animateCoroutine = StartCoroutine(AnimateSliderCoroutine(pointSlider.value, target, animateDuration));
    }

    IEnumerator AnimateSliderCoroutine(float from, float to, float duration)
    {
        float elapsed = 0f;

        // handle case where from==to
        if (Mathf.Approximately(from, to))
        {
            pointSlider.value = to;
            animateCoroutine = null;
            yield break;
        }

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // smooth easing (smooth step)
            float eased = t * t * (3f - 2f * t); // smootherstep
            float value = Mathf.Lerp(from, to, eased);

            if (wholeNumbers)
                pointSlider.value = Mathf.RoundToInt(value);
            else
                pointSlider.value = value;

            yield return null;
        }

        // ensure exact end value
        if (wholeNumbers)
            pointSlider.value = to;
        else
            pointSlider.value = to;

        animateCoroutine = null;
    }
}



