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
        // check enemy layer hit
        if ((enemyLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            // Award points
            if (PointBar.Instance != null)
            {
                PointBar.Instance.AddPoints(PointBar.Instance.rewardPerKill);
                PointBar.Instance.AddPoints(PointBar.Instance.onTowerDamaged);
            }

            // damage the troop / tower if needed
            TroopHealth troopHealth = other.GetComponent<TroopHealth>();
            if (troopHealth != null)
            {
                troopHealth.TakeDamage(power);
            }

            TowerHealth towerHealth = other.GetComponent<TowerHealth>();
            if (towerHealth != null)
            {
                towerHealth.TakeDamage(power);
            }

            // disable bullet after hit
            gameObject.SetActive(false);
        }
    }
    
}


