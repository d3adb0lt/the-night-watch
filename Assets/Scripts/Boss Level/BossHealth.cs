using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float maxBossHealth = 100f;
    public float currBossHealth;

    public Image bossHealthBar;

    void Start()
    {
        currBossHealth = maxBossHealth;
        UpdateBossHealthUI();
    }

    public void UpdateBossHealthUI()
    {
        if (bossHealthBar != null)
        {
            bossHealthBar.fillAmount = currBossHealth / maxBossHealth;
        }
    }

    // can also pass a negative value here for boss healing
    public void BossTakeDamage(float damageAmount)
    {
        currBossHealth -= damageAmount;
        currBossHealth = Mathf.Clamp(currBossHealth, 0, maxBossHealth);
        UpdateBossHealthUI();
    }

    // INTEGRATION TO DO - ensure this is the proper way for the boss to take damage from player attack
    // "this" collider is the boss's hitbox, "bullet" collider is from the player (i think)
    void OnTriggerEnter(Collider bullet)
    {
        if (this.CompareTag("Boss") && bullet.CompareTag("PlayerWeapon"))
        {
            BossTakeDamage(12.5f);
        }
    }
}
