using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyDetector))]
public class TroopAttack : MonoBehaviour
{
    [Header("Firing")]
    [Tooltip("Time between shots (seconds)")]
    public float attackCooldown = 1f;

    [Tooltip("If true, bullets are aimed at the target; otherwise use bulletDirectionX (±1).")]
    public bool fireTowardsEnemy = true;

    [Tooltip("Use 1 for +X firing, -1 for -X firing when not aiming.")]
    public int bulletDirectionX = 1;

    [Header("Bullet / Pool")]
    [Tooltip("Which prefab this troop uses (must be registered in BulletPoolManager.pools)")]
    public GameObject troopBulletPrefab;

    [Tooltip("Where bullets spawn from")]
    public Transform firePoint;

    [Tooltip("Optional per-troop override; if <= 0 prefab value is used")]
    public float bulletSpeedOverride = -1f;
    public float bulletDamageOverride = -1f;

    private EnemyDetector enemyDetector;
    private Coroutine firingCoroutine;

    void Awake()
    {
        enemyDetector = GetComponent<EnemyDetector>();
        if (enemyDetector == null)
            Debug.LogError("TroopAttack requires EnemyDetector on the same GameObject.");
    }

    void Update()
    {
        if (enemyDetector == null) return;

        Transform target = enemyDetector.targetEnemy;

        if (target != null && Vector2.Distance(transform.position, target.position) <= enemyDetector.detectionRange)
        {
            if (firingCoroutine == null)
                firingCoroutine = StartCoroutine(FireWhileInRange());
        }
        else
        {
            if (firingCoroutine != null)
            {
                StopCoroutine(firingCoroutine);
                firingCoroutine = null;
            }
        }
    }

    IEnumerator FireWhileInRange()
    {
        while (true)
        {
            Transform target = enemyDetector.targetEnemy;
            if (target == null) break;

            FireOnce(target);
            yield return new WaitForSeconds(attackCooldown);
        }

        firingCoroutine = null;
    }

    void FireOnce(Transform currentTarget)
    {
        if (firePoint == null)
        {
            Debug.LogWarning($"{name}: FirePoint not assigned.");
            return;
        }

        if (troopBulletPrefab == null)
        {
            Debug.LogWarning($"{name}: troopBulletPrefab not assigned.");
            return;
        }

        if (BulletPoolManager.Instance == null)
        {
            Debug.LogWarning("BulletPoolManager.Instance is null — make sure a manager exists in the scene.");
            return;
        }

        Bullet bullet = BulletPoolManager.Instance.GetBullet(troopBulletPrefab);
        if (bullet == null) return;

        // place and reset bullet
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.gameObject.SetActive(true);

        // optionally override values
        if (bulletSpeedOverride > 0) bullet.speed = bulletSpeedOverride;
        if (bulletDamageOverride > 0) bullet.damage = bulletDamageOverride;

        Vector2 dir;
        if (fireTowardsEnemy && currentTarget != null)
            dir = (currentTarget.position - firePoint.position).normalized;
        else
            dir = new Vector2(Mathf.Sign(bulletDirectionX), 0f);

        bullet.Fire(dir);
    }
}

