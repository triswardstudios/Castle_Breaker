using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Detection")]
    public float detectionRange = 3f;
    public LayerMask enemyLayer;

    [Header("Attack")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;

    [Tooltip("Fire direction: true = Right, false = Left")]
    public bool fireRight = true;

    [Header("Movement Control")]
    public MonoBehaviour movementScript;     // drag your movement script here (OPTION A)
    //public Rigidbody2D rb;                   // for physics movement stopping (OPTION B)

    private float nextFireTime = 0f;
    private bool isAttacking = false;

    void Update()
    {
        // Detect enemy in range
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, detectionRange, enemyLayer);

        if (enemy != null)
        {
            StartAttacking();

            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            StopAttacking();
        }
    }

    void StartAttacking()
    {
        if (isAttacking) return;
        isAttacking = true;

        // OPTION A � disable movement script
        if (movementScript != null)
            movementScript.enabled = false;

        // OPTION B � stop Rigidbody movement
        //if (rb != null)
        //    rb.linearVelocity = Vector2.zero;
    }

    void StopAttacking()
    {
        if (!isAttacking) return;
        isAttacking = false;

        // OPTION A � enable movement script again
        if (movementScript != null)
            movementScript.enabled = true;

        // OPTION B � allow RB / movement script to control movement again
        // Nothing special needed here
    }

    void Fire()
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning("Attack: No bulletPrefab assigned.");
            return;
        }

        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
        GameObject bulletObj = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            Vector2 dir = fireRight ? Vector2.right : Vector2.left;
            bullet.SetDirection(dir);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}


