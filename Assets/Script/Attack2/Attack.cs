using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Detection")]
    [Tooltip("How far this unit can detect enemies.")]
    public float detectionRange = 3f;

    [Tooltip("Layer of enemies to detect.")]
    public LayerMask enemyLayer;

    [Header("Attack")]
    [Tooltip("Bullet prefab to spawn when attacking.")]
    public GameObject bulletPrefab;

    [Tooltip("Where the bullet will spawn from.")]
    public Transform firePoint;

    [Tooltip("Time between shots (seconds).")]
    public float fireRate = 0.5f;

    [Tooltip("Fire direction: true = Right, false = Left.")]
    public bool fireRight = true;

    private float nextFireTime = 0f;

    void Update()
    {
        // Look for any enemy in range
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, detectionRange, enemyLayer);

        if (enemy != null)
        {
            // Enemy found in range -> try to fire
            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Fire()
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning("Attack: No bulletPrefab assigned.");
            return;
        }

        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
        Quaternion spawnRot = Quaternion.identity;

        GameObject bulletObj = Instantiate(bulletPrefab, spawnPos, spawnRot);

        if (bulletObj.TryGetComponent<Bullet>(out var bullet))
        {
            // Set direction: right or left
            Vector2 dir = fireRight ? Vector2.right : Vector2.left;
            bullet.SetDirection(dir);
        }
    }

    // Draw detection range in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

