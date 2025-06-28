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

    void Update()
    {
        // FOR TESTING ONLY, replace with actual player attack later
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            BossTakeDamage(12.5f);
        }
        
        // FOR TESTING ONLY, replace with boss healing later
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            BossTakeDamage(-8.5f);
        }
    }
}
