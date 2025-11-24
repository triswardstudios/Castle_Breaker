using UnityEngine;

public class TroopHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    void Die()
    {
        // Disable troop game object when health reaches 0
        gameObject.SetActive(false);
    }
}

