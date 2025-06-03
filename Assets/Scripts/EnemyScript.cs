using UnityEngine;
using TMPro;

public class EnemyScript : MonoBehaviour
{
    public float enemyHealth = 50f;
    public TextMeshProUGUI healthTextUI;

    void Start()
    {
        SetHealthText();
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        if (enemyHealth < 0) enemyHealth = 0;

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
        if (healthTextUI != null)
        {
            healthTextUI.text = "Enemy Defeated!";
        }
        Destroy(gameObject);
    }
}
