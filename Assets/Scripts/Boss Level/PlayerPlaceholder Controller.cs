using UnityEngine;

public class PlayerPlaceholderController : MonoBehaviour
{
    // THIS SCRIPT CONTROLS THE MOVEMENT OF THE PLAYER PLACEHOLDER
    // THIS SCRIPT IS FOR TESTING ONLY, SHOULD BE REMOVED WHEN INTEGRATING
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 delta = new Vector3(x, 0, z) * 5f * Time.deltaTime;
        transform.position += delta;
    }
}
