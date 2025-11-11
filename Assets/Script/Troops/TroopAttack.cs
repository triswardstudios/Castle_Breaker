using UnityEngine;

public class TroopAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [Tooltip("Time between attacks in seconds.")]
    public float attackcooldown = 1.0f;

    [Tooltip("Attack range (same as detection range).")]
    public float attackRange = 2f;

    [Tooltip("Damage per attack.")]
    public float damage = 10f;

    private EnemyDetector enemyDetector;
    private float lastAttackTime;

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyDetector = GetComponent<EnemyDetector>();
        if (enemyDetector != null)
            attackRange = enemyDetector.detectionRange;//Sync ranges

    }

    //Update is called once per frame
    void Update()
    {
        if (enemyDetector == null || enemyDetector.targetEnemy == null)
            return;
        float distance = Vector2.Distance(transform.position, enemyDetector.targetEnemy.position);
        if (distance <= attackRange && Time.time >= lastAttackTime + attackcooldown)
        {
            Attack(enemyDetector.targetEnemy);
            lastAttackTime = Time.time;
        } 
    }
    void Attack(Transform enemy)
    {
        Debug.Log($"{name} attacks {enemy.name} for {damage} damage!");
        // Example: If enemy has a health script
        // enemy.GetComponent<EnemyHealth>()?.TakeDamage(damage);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
