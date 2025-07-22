using UnityEngine;
public class IsometricCamera : MonoBehaviour
{
    public static IsometricCamera instance;
    public GameObject cameraObject;

    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow Offset")]
    [SerializeField] private Vector3 offset = new Vector3(-10f, 10f, -10f);

    [Header("Follow Smoothing")]
    [Range(0f, 1f)]
    [SerializeField] private float smoothTime = 0.15f;

    [Header("Isometric Rotation (degrees)")]
    [SerializeField] private float pitch = 35f; // tilt down
    [SerializeField] private float yaw   = 45f; // rotate around Y

    [Header("Projection")]
    [SerializeField] private bool orthographic = false;
    [SerializeField] private float orthographicSize = 6f;

    private Vector3 velocity;      // internal ref for SmoothDamp
    private Camera cam;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        ApplyProjectionSettings();
        if (instance == null)
        {
            instance = this;
        }

    }

    private void LateUpdate()
    {
        if (target == null) return;

        // --- Position ---
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        // --- Rotation ---
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void OnValidate()
    {
        // Sync any Inspector changes at edit‑time
        if (cam == null) cam = GetComponentInChildren<Camera>();
        ApplyProjectionSettings();
    }

    private void ApplyProjectionSettings()
    {
        if (cam == null) return;
        cam.orthographic = orthographic;
        if (orthographic)
            cam.orthographicSize = orthographicSize;
    }
}
