using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int vialAmount = 1;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddVial(vialAmount);
                Destroy(gameObject);
            }
        }
    }
}
