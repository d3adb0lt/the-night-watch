using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int vialAmount = 1;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.AddVial(vialAmount);
                Destroy(gameObject);
            }
        }
    }
}
