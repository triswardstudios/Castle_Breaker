using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 8f;
    public float damage = 10f;
    public float lifeTime = 3f;
    public bool useGravity = false;

    [HideInInspector] public GameObject prefabReference; // important: set by pool manager
    Rigidbody2D rb;
    float disableTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        disableTime = Time.time + lifeTime;
        if (!useGravity && rb != null) rb.gravityScale = 0f;
    }

    void Update()
    {
        if (Time.time >= disableTime)
            ReturnToPool();
    }

    // Called to launch the bullet
    public void Fire(Vector2 direction)
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = direction.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Try to damage an EnemyHealth (optional)
        var health = other.GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
            ReturnToPool();
            return;
        }

        // Ignore collisions with certain tags/layers if needed here.
        // Default behaviour: return to pool on any hit.
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        if (rb != null) rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);

        // Return to the manager
        if (BulletPoolManager.Instance != null)
            BulletPoolManager.Instance.ReturnBullet(this);
        else
            Destroy(gameObject); // fallback
    }
}
