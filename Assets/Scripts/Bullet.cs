using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.attachedRigidbody;
        if (rb != null)
        {
            rb.AddForce(transform.forward * 300f);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        else if (collision.gameObject.CompareTag("Spawner"))
        {
            EnemySpawner enemySpawner = collision.gameObject.GetComponent<EnemySpawner>();
            if (enemySpawner != null)
            {
                enemySpawner.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
