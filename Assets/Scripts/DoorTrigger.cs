using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Door door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null && inventory.hasKey)
            {
                door.OpenDoor();
            }
            else
            {
                Debug.Log("You need a key to open this door!");
            }
        }
    }
}
