using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public float speed = 10f;
    public float lifeTime = 6f;

    private Vector3 direction;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthManagerScript playerHealthManagerScript = other.GetComponent<PlayerHealthManagerScript>();
            if (playerHealthManagerScript != null)
            {
                playerHealthManagerScript.PlayerChangeHealth(-damage);
            }
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
