using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class EnemyScript : MonoBehaviour
{
    // public float enemyHealth = 50f;
    public TextMeshProUGUI healthTextUI;
    public float maxHealth = 50f;
    public float currentHealth;
    public Slider healthBar;

    void Start()
    {
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
        // enemyHealth -= damage;
        // if (enemyHealth < 0) enemyHealth = 0;

        // SetHealthText();

        // if (enemyHealth <= 0)
        // {
        //     Die();
        // }

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        // Update health bar
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    

    // void SetHealthText()
    // {
    //     if (healthTextUI != null)
    //     {
    //         healthTextUI.text = "Enemy HP: " + enemyHealth.ToString();
    //     }
    // }

    // void Die()
    // {
    //     if (healthTextUI != null)
    //     {
    //         healthTextUI.text = "Enemy Defeated!";
    //     }
    //     Destroy(gameObject);
    // }
}
