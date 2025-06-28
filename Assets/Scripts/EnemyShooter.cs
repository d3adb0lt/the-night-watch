using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject player;
    public Transform shootPoint;
    public float shootInterval = 2f;

    private float shootTimer;

    private void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    private void Shoot()
    {
        if (projectilePrefab == null || shootPoint == null)
        {
            Debug.LogWarning("ProjectilePrefab or ShootPoint not assigned");
            return;
        }
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Vector3 direction = (FindObjectOfType<PlayerHealth>().transform.position - shootPoint.position).normalized;
        projectile.GetComponent<Projectile>().SetDirection(direction);
    }
}
