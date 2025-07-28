using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class EnemyScript : MonoBehaviour
{
    public float maxHealth = 50f;
    public float currentHealth;
    public Slider healthBar;

    private Animator animator;
    bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        // SetHealthText();
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }
    
    public void TakeDamage(float damage)
    {
        if (isDead) return;
        
        animator.SetTrigger("TakeDamage");

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        // Update health bar
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    

    // void SetHealthText()
    // {
    //     if (healthTextUI != null)
    //     {
    //         healthTextUI.text = "Enemy HP: " + enemyHealth.ToString();
    //     }
    // }

    void Die()
    {
        animator.SetTrigger("Die");

        isDead = true; 
        if (TryGetComponent<EnemyChaseAI>(out var chaseAI))
        {
            chaseAI.enabled = false;
        }
        
        Destroy(gameObject, 4f); // Optional delay
    }
}