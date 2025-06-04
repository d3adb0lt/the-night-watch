using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnerHealth = 100f;

    public TextMeshProUGUI spawnerHealthUI;

    private GameObject currentEnemy;

    void Start()
    {
        SetHealthText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (currentEnemy == null && enemyPrefab != null)
        {
            currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyScript enemyScript = currentEnemy.GetComponent<EnemyScript>();
            if (enemyScript != null && enemyScript.healthTextUI == null && spawnerHealthUI != null)
            {
                enemyScript.healthTextUI = GameObject.Find("EnemyHealthText").GetComponent<TextMeshProUGUI>();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        spawnerHealth -= damage;
        if (spawnerHealth < 0) spawnerHealth = 0;

        SetHealthText();

        if (spawnerHealth <= 0)
        {
            Die();
        }
    }

    void SetHealthText()
    {
        if (spawnerHealthUI != null)
        {
            spawnerHealthUI.text = "Spawner HP: " + spawnerHealth.ToString();
        }
    }

    void Die()
    {
        if (spawnerHealthUI != null)
        {
            spawnerHealthUI.text = "Spawner Destroyed!";
        }
        Destroy(gameObject);
    }
}
