using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Arc_Bullet : MonoBehaviour
{
    [Header("Mortar Movement")]
    public float speed = 8f;        // horizontal speed
    public float arcHeight = 12f;   // how high the shell goes
    public float gravity = 9.8f;

    [Header("Explosion")]
    public float explosionRadius = 3f;
    public float power = 25f;       // damage per target
    public LayerMask enemyLayer;

    [Header("Life")]
    public float lifeTime = 5f;

    private Vector2 moveDirection = Vector2.right;
    private Vector2 velocity;
    private float disableTime;
    private bool hasExploded = false;

    void OnEnable()
    {
        disableTime = Time.time + lifeTime;
        hasExploded = false;

        // initial velocity → forward + upward
        velocity = new Vector2(
            moveDirection.x * speed,
            arcHeight
        );
    }

    void Update()
    {
        // gravity
        velocity.y -= gravity * Time.deltaTime;

        // move shell
        transform.position += (Vector3)(velocity * Time.deltaTime);

        // safety disable
        if (Time.time >= disableTime)
        {
            Debug.Log("Arc_Bullet: Auto-disable after lifeTime.");
            Explode();
        }
    }

    public void SetDirection(Vector2 dir)
    {
        if (dir.sqrMagnitude > 0.001f)
            moveDirection = dir.normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // explode on hitting ground, troop, or tower
        if (!hasExploded)
        {
            Debug.Log("Arc_Bullet: Hit " + other.name);
            Explode();
        }
    }

    void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        //  AOE damage
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            explosionRadius,
            enemyLayer
        );

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<TroopHealth>(out TroopHealth troop))
            {
                troop.TakeDamage(power);
            }

            if (hit.TryGetComponent<TowerHealth>(out TowerHealth tower))
            {
                tower.TakeDamage(power);
            }
        }

        // reward points ONCE per explosion
        if (PointBar.Instance != null)
        {
            PointBar.Instance.AddPoints(PointBar.Instance.rewardPerKill);
            PointBar.Instance.AddPoints(PointBar.Instance.onTowerDamaged);
        }

        // disable shell
        gameObject.SetActive(false);
    }

    // Visualize explosion radius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}



