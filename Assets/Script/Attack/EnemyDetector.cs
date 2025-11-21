using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [Header("Detection Settings")]
    [Tooltip("Layer mask to detect enemies.")]
    public LayerMask enemyLayerMask;

    [Tooltip ("Detection range for enemies.")]
    public float detectionRange = 2f;

    [HideInInspector] public Transform targetEnemy;

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyLayerMask);

        if (hits.Length > 0)
        {
            //Pick the closest enemy
            float closestDistance = Mathf.Infinity;
            Transform closestEnemy = null;
            foreach (var hit in hits)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hit.transform;
                }
            }
            targetEnemy = closestEnemy;
        }
        else
        {
            targetEnemy = null;
        }
    }

    // Optional: visualize detection range in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
