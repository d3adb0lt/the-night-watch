using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform openPosition; 
    private bool isOpen = false;

    public void OpenDoor()
    {
        if (isOpen) return;

        transform.position = openPosition.position;
        isOpen = true;
        Debug.Log("Door opened!");
    }
}
