using UnityEngine;

public class IsoCameraFollow : MonoBehaviour
{
    public Transform player; // reference to the player
    public Vector3 offset = new Vector3(-10, 10, -10); // Isometric offset
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (!player) return;

        // Target position relative to the player
        Vector3 targetPosition = player.position + offset;

        // Smooth camera movement
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
