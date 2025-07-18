using UnityEngine;
using TMPro;

public class EnemyScript : MonoBehaviour
{
    public float enemyHealth = 50f;
    public TextMeshProUGUI healthTextUI;

    private Animator animator; 
    bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        SetHealthText();
    }
    
    public void TakeDamage(float damage)
    {
        if (isDead) return;
        enemyHealth -= damage;

        if (enemyHealth < 0) enemyHealth = 0;

        animator.SetTrigger("TakeDamage");

        SetHealthText();

        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    void SetHealthText()
    {
        if (healthTextUI != null)
        {
            healthTextUI.text = "Enemy HP: " + enemyHealth.ToString();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");

        isDead = true;
        if (healthTextUI != null)
        {
            healthTextUI.text = "Enemy Defeated!";
        }   
        if (TryGetComponent<EnemyChaseAI>(out var chaseAI))
        {
            chaseAI.enabled = false;
        }
        
        Destroy(gameObject, 4f); // Optional delay
    }
}