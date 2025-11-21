using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance { get; private set; }

    [System.Serializable]
    public class PoolConfig
    {
        public GameObject prefab;
        public int initialSize = 20;
    }

    [Tooltip("Configure each bullet prefab you will use and an initial size")]
    public PoolConfig[] pools;

    // keyed by prefab reference
    private Dictionary<GameObject, Queue<Bullet>> poolsByPrefab = new ();
    private Transform poolsRoot;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        poolsRoot = new GameObject("BulletPoolsRoot").transform;
        poolsRoot.SetParent(transform, false);

        foreach (var cfg in pools)
        {
            if (cfg.prefab == null) continue;

            var q = new Queue<Bullet>();
            var poolParent = new GameObject(cfg.prefab.name + "_Pool").transform;
            poolParent.SetParent(poolsRoot, false);

            for (int i = 0; i < Mathf.Max(1, cfg.initialSize); i++)
            {
                var obj = Instantiate(cfg.prefab, poolParent);
                obj.SetActive(false);
                var b = obj.GetComponent<Bullet>();
                if (b == null)
                {
                    Debug.LogError($"BulletPoolManager: prefab {cfg.prefab.name} has no Bullet component.");
                    Destroy(obj);
                    continue;
                }
                b.prefabReference = cfg.prefab;
                q.Enqueue(b);
            }

            poolsByPrefab[cfg.prefab] = q;
        }
    }

    // Get an active bullet for the requested prefab
    public Bullet GetBullet(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogWarning("BulletPoolManager.GetBullet called with null prefab.");
            return null;
        }

        if (!poolsByPrefab.TryGetValue(prefab, out var q))
        {
            // create a new queue on demand
            q = new Queue<Bullet>();
            var poolParent = new GameObject(prefab.name + "_Pool").transform;
            poolParent.SetParent(poolsRoot, false);
            poolsByPrefab[prefab] = q;
        }

        Bullet b;
        if (q.Count > 0)
        {
            b = q.Dequeue();
            b.gameObject.SetActive(true);
        }
        else
        {
            // pool exhausted, instantiate extra
            var obj = Instantiate(prefab, poolsRoot);
            b = obj.GetComponent<Bullet>();
            if (b == null)
            {
                Debug.LogError($"BulletPoolManager: prefab {prefab.name} has no Bullet component.");
                Destroy(obj);
                return null;
            }
            b.prefabReference = prefab;
        }

        return b;
    }

    // Called by bullets to return themselves
    public void ReturnBullet(Bullet bullet)
    {
        if (bullet == null) return;

        var prefab = bullet.prefabReference;

        if (prefab != null && poolsByPrefab.TryGetValue(prefab, out var q))
        {
            // Prepare bullet for reuse
            bullet.transform.SetParent(poolsRoot);
            if (!q.Contains(bullet)) q.Enqueue(bullet);
        }
        else
        {
            // If we cannot find matching queue, put into a default queue for that prefab type (create if needed)
            if (prefab == null)
            {
                // no prefab reference — destroy to avoid orphan
                Destroy(bullet.gameObject);
                return;
            }

            var newQ = new Queue<Bullet>();
            var poolParent = new GameObject(prefab.name + "_Pool").transform;
            poolParent.SetParent(poolsRoot, false);
            bullet.transform.SetParent(poolsRoot);
            newQ.Enqueue(bullet);
            poolsByPrefab[prefab] = newQ;
        }
    }
}

