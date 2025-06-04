using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.attachedRigidbody;
        if (rb != null)
        {
            rb.AddForce(transform.forward * 300f);
        }
        Destroy(gameObject);
    }
}
