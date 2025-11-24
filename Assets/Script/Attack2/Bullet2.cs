using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public float power = 10f;          // damage amount
    public LayerMask enemyLayer;       // who this bullet can damage
    public float lifeTime = 3f;        // auto-disable after some time

    private Vector2 moveDirection = Vector2.right;
    private float disableTime;

    void OnEnable()
    {
        disableTime = Time.time + lifeTime;
    }

    void Update()
    {
        // Move in set direction
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Auto disable after lifeTime
        if (Time.time >= disableTime)
        {
            gameObject.SetActive(false);
        }
    }

    // Called by Attack.cs right after spawning
    public void SetDirection(Vector2 dir)
    {
        if (dir.sqrMagnitude > 0.001f)
            moveDirection = dir.normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if this collider is on the enemy layer
        if ((enemyLayer.value & (1 << other.gameObject.layer)) == 0)
            return; // not in enemy layer, ignore

        // Try troop first
        TroopHealth troopHealth = other.GetComponent<TroopHealth>();
        if (troopHealth != null)
        {
            troopHealth.TakeDamage(power);
            gameObject.SetActive(false);
            return;
        }

        // Try tower
        TowerHealth towerHealth = other.GetComponent<TowerHealth>();
        if (towerHealth != null)
        {
            towerHealth.TakeDamage(power);
            gameObject.SetActive(false);
            return;
        }

        // If it's an enemy layer but no health script, still disable bullet
        gameObject.SetActive(false);
    }
}


