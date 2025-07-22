using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public float maxBossHealth = 100f;
    public float currBossHealth;

    public Image bossHealthBar;
    private Animator bossAnim;
    private BossController boss;
    [SerializeField] private string endingSceneName = "Ending_Scene"; // exact name in Build Settings
    [SerializeField] private float deathDelay = 2f;
    private bool isDead = false;

    void Start()
    {
        currBossHealth = maxBossHealth;
        bossAnim = GetComponent<Animator>();
        UpdateBossHealthUI();
        boss = GetComponent<BossController>();
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
        if (isDead) return;
        currBossHealth -= damageAmount;
        currBossHealth = Mathf.Clamp(currBossHealth, 0, maxBossHealth);
        if (currBossHealth <= 0)
        {
            isDead = true;
            bossAnim.SetTrigger("Die");
            boss.SetChasing(false);
            boss.DisableLeftFist();
            boss.DisableRightFist();
            StartCoroutine(HandleDeath());
            // end the game
        }
        UpdateBossHealthUI();
    }
     private System.Collections.IEnumerator HandleDeath()
    {
        // wait real-time so even if you pause Time.timeScale it still waits
        yield return new WaitForSecondsRealtime(deathDelay);

        // If you use Netcode, shut it down here before loading
        // if (NetworkManager.Singleton && NetworkManager.Singleton.IsListening)
        //     NetworkManager.Singleton.Shutdown();

        SceneManager.LoadScene(endingSceneName);
        // or: SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    

    // INTEGRATION TO DO - ensure this is the proper way for the boss to take damage from player attack
    // "this" collider is the boss's hitbox, "bullet" collider is from the player (i think)
    // void OnTriggerEnter(Collider bullet)
    // {
    //     if (this.CompareTag("Boss") && bullet.CompareTag("PlayerWeapon"))
    //     {
    //         BossTakeDamage(12.5f);
    //     }
    // }
}
