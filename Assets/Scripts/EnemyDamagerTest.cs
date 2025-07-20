//please delete this file. it's just a test file

using UnityEngine;

public class EnemyDamagerTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Find any enemy in the scene and damage it
            EnemyScript enemy = FindObjectOfType<EnemyScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(10f);
                Debug.Log("Damaged enemy! Health remaining: " + enemy.currentHealth);
            }
        }
    }
}