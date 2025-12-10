using System.Collections;
using UnityEngine;
using System.Linq;

public class EnemyAI : MonoBehaviour
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    [Header("References")]
    public EnemyTroopSpawner spawner;   // assign EnemyTroopSpawner here

    [Header("Difficulty")]
    public Difficulty difficulty = Difficulty.Medium;

    [Header("Spawn Timing")]
    [Tooltip("Base time (seconds) between enemy spawn decisions.")]
    public float baseDecisionInterval = 2f;

    [Tooltip("Random extra time added/subtracted to make AI less predictable.")]
    public float randomJitter = 0.5f;

    [Header("Global Limits")]
    [Tooltip("Maximum enemy troops allowed at once (0 = no limit).")]
    public int maxActiveTroops = 0;

    [Tooltip("Parent transform used to count active enemy troops (optional).")]
    public Transform enemyTroopParent;

    private Coroutine aiRoutine;

    void Reset()
    {
        // try auto find spawner on same GameObject
        if (spawner == null)
            spawner = GetComponent<EnemyTroopSpawner>();
    }

    void Start()
    {
        if (spawner == null)
        {
            Debug.LogError("EnemyAI: No EnemyTroopSpawner assigned.");
            return;
        }

        aiRoutine = StartCoroutine(AILoop());
    }

    IEnumerator AILoop()
    {
        while (true)
        {
            // optionally respect max troop count
            if (maxActiveTroops > 0 && enemyTroopParent != null)
            {
                int active = enemyTroopParent.GetComponentsInChildren<TroopHealth>()
                                             .Count(t => t.gameObject.activeInHierarchy);
                if (active >= maxActiveTroops)
                {
                    // wait a bit, don't spawn more
                    yield return new WaitForSeconds(1f);
                    continue;
                }
            }

            // Decide what to spawn based on difficulty
            SpawnBasedOnDifficulty();

            // compute next wait
            float interval = GetIntervalForDifficulty();
            float jitter = Random.Range(-randomJitter, randomJitter);
            float wait = Mathf.Max(0.2f, interval + jitter); // never below 0.2
            yield return new WaitForSeconds(wait);
        }
    }

    float GetIntervalForDifficulty()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return baseDecisionInterval * 1.5f;  // slower spawns
            case Difficulty.Medium:
                return baseDecisionInterval;        // normal
            case Difficulty.Hard:
                return baseDecisionInterval * 0.6f; // faster spawns
            default:
                return baseDecisionInterval;
        }
    }

    void SpawnBasedOnDifficulty()
    {
        if (spawner == null) return;

        // weights: [troop1, troop2, troop3]
        float[] weights;

        switch (difficulty)
        {
            case Difficulty.Easy:
                // Weak spam: mostly troop1
                weights = new float[] { 0.7f, 0.2f, 0.1f };
                break;

            case Difficulty.Medium:
                // Mixed army
                weights = new float[] { 0.33f, 0.33f, 0.34f };
                break;

            case Difficulty.Hard:
                // Stronger troops more often
                weights = new float[] { 0.1f, 0.35f, 0.55f };
                break;

            default:
                weights = new float[] { 0.33f, 0.33f, 0.34f };
                break;
        }

        int index = WeightedRandomIndex(weights);

        switch (index)
        {
            case 0: spawner.SpawnTroop1(); break;
            case 1: spawner.SpawnTroop2(); break;
            case 2: spawner.SpawnTroop3(); break;
        }
    }

    int WeightedRandomIndex(float[] weights)
    {
        float total = 0f;
        foreach (var w in weights) total += w;

        float r = Random.Range(0f, total);
        float running = 0f;

        for (int i = 0; i < weights.Length; i++)
        {
            running += weights[i];
            if (r <= running)
                return i;
        }

        return weights.Length - 1;
    }
}


