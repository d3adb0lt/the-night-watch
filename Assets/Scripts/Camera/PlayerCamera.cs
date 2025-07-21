using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    private static PlayerCamera instance;
    public Camera cameraObject;
    public PlayerManager player;
    [SerializeField] Transform cameraPivot;

    // for camera performance
    [Header("Camera Settings")]
    private float cameraSmoothSpeed = 1; // bigger the number the longer it take for the camera to catch up to the target
    [SerializeField] float leftAndRightRotationSpeed = 220;
    [SerializeField] float upAndDownRotationSpeed = 220;
    [SerializeField] float minPivot = -30; // lowest point you can look down
    [SerializeField] float maxPivot = 60; // highest point you can look up
    [SerializeField] float cameraCollisionOffset = 0.2f;
    [SerializeField] LayerMask collideWithLayers;

    [Header("Camera Values")]
    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPos; //used for camera collisions (shift camera object to this position upon collision)
    [SerializeField] float leftAndRightLookAngle;
    [SerializeField] float upAndDownLookAngle;
    private float cameraZPos; // used for camera collision
    private float targetCameraZPos; // used for camera collision

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        cameraZPos = cameraObject.transform.localPosition.z;
    }

    public void HandleAllCameraActions()
    {
        if (player != null)
        {
            // follow player
            FollowTarget();
            // rotate with player
            RotationTarget();
            // collide with objects (dont go through walls)
            HandleCollisions();
        }
    }

    private void FollowTarget()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }

    private void RotationTarget()
    {
        // rotate left and right
        leftAndRightLookAngle += (PlayerInputManager.GetInstance().cameraHInput * leftAndRightRotationSpeed) * Time.deltaTime;
        // rotate up and down
        upAndDownLookAngle -= (PlayerInputManager.GetInstance().cameraVInput * upAndDownRotationSpeed) * Time.deltaTime;
        // if user tries to go beyond limits clamp back to closest value
        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minPivot, maxPivot);

        Vector3 cameraRotation = Vector3.zero;
        Quaternion targetRotation;

        // rotate this gameobject left and right
        cameraRotation.y = leftAndRightLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        transform.rotation = targetRotation;

        // rotate this gameobject up and down
        cameraRotation = Vector3.zero;
        cameraRotation.x = upAndDownLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        cameraPivot.localRotation = targetRotation;
    }

    private void HandleCollisions()
    {
        targetCameraZPos = cameraZPos;
        RaycastHit hit;
        // direction for collision check
        Vector3 direction = cameraObject.transform.position - cameraPivot.position;
        direction.Normalize();

        // we check if there is an object in front of the above Vector3 direction
        if (Physics.SphereCast(cameraPivot.position, cameraCollisionOffset, direction, out hit, Mathf.Abs(targetCameraZPos), collideWithLayers))
        {
            // if yes, do math to find the distance
            float distanceFromHitObject = Vector3.Distance(cameraPivot.position, hit.point);
            // set the new camera position to the difference found between the camera "bubble" and the actual ray hit distance
            targetCameraZPos = -(distanceFromHitObject - cameraCollisionOffset);
        }

        // if the target position is less than the camera bubble then subtract the collision offset to make it snap back into place
        if (Mathf.Abs(targetCameraZPos) < cameraCollisionOffset)
        {
            targetCameraZPos = -cameraCollisionOffset;
        }

        // apply to the final position, lerp does so over time
        cameraObjectPos.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPos, 0.2f);
        cameraObject.transform.localPosition = cameraObjectPos;
    }
    public static PlayerCamera GetInstance()
    {
        return instance;
    }
    public static void SetInstance(PlayerCamera instance)
    {
        PlayerCamera.instance = instance;
    }
}
