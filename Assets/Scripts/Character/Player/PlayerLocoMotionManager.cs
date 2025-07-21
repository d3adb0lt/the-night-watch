using UnityEngine;

public class PlayerLocoMotionManager : CharacterLocomotionManager
{
    PlayerManager player;
    PlayerWeaponManager playerWeapon;

    // taken from the player input manager
    [HideInInspector] public float verticalMovement;
    [HideInInspector] public float horizontalMovement;
    [HideInInspector] public float totalMovement;

    [Header("Movement Settings")]
    [SerializeField] float walkSpeed = 2;
    [SerializeField] float runSpeed = 5;
    [SerializeField] float rotationSpeed = 12;

    private Vector3 movementDirection;
    private Vector3 desiredRotationDir;

    [Header("Dodge")]
    private Vector3 rollDirection;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();

        playerWeapon = GetComponentInChildren<PlayerWeaponManager>();
    }

    protected override void Update()
    {
        base.Update();
        CaptureMovementInput();
    }

    public void HandleAllMovement()
    {
        if (player.isPerformingAction)
        {
            return;
        }
        // ground movement
        HandleGroundMovement();

        // rotate to face meaningful direction (where the player is facing)
        if (movementDirection.sqrMagnitude > 0.001)
            HandleRotationMovement();
    }

    private void CaptureMovementInput()
    {
        verticalMovement = PlayerInputManager.GetInstance().vInput;
        horizontalMovement = PlayerInputManager.GetInstance().hInput;
        totalMovement = PlayerInputManager.GetInstance().totalMovement;
    }

    private void HandleGroundMovement()
    {
        if (!player.canMove)
        {
            return;
        }

        // movement vector relative to camera orientation
        var camTransform = Camera.main != null ? Camera.main.transform : transform;
        var camForwardOnPlane = Vector3.ProjectOnPlane(camTransform.forward, Vector3.up).normalized;
        var camRightOnPlane = Vector3.ProjectOnPlane(camTransform.right, Vector3.up).normalized;

        movementDirection = camForwardOnPlane * verticalMovement + camRightOnPlane * horizontalMovement;
        movementDirection = movementDirection.sqrMagnitude > 1 ? movementDirection.normalized : movementDirection;

        float speed = (totalMovement > 0.5) ? runSpeed : walkSpeed;
        player.characterController.Move(movementDirection * speed * Time.deltaTime);
    }

    private void HandleRotationMovement()
    {
        if (!player.canRotate)
        {
            return;
        }

        var camTransform = Camera.main != null ? Camera.main.transform : transform;
        desiredRotationDir = Vector3.ProjectOnPlane(camTransform.forward, Vector3.up) * verticalMovement +
                                    Vector3.ProjectOnPlane(camTransform.right,   Vector3.up) * horizontalMovement;

        if (desiredRotationDir.sqrMagnitude < 0.001f)
            return;
        desiredRotationDir.Normalize();

        Quaternion targetRot = Quaternion.LookRotation(desiredRotationDir);
        transform.rotation   = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }

    public void AttemptToPerformDodge()
    {
        if (player.isPerformingAction && !player.actionCancelable)
        {
            // if (PlayerInputBuffer.Instance.GetBufferedInput() != PlayerInputType.Roll)
            // {
            //     PlayerInputBuffer.Instance.BufferInput(PlayerInputType.Roll);
            // }
            return;
        }

        // if moving when dodging perform roll else, perform a backstep
        // roll animation
        var camTransform  = Camera.main != null ? Camera.main.transform : transform;
        rollDirection = Vector3.ProjectOnPlane(camTransform.forward, Vector3.up) * verticalMovement +
                        Vector3.ProjectOnPlane(camTransform.right,   Vector3.up) * horizontalMovement;

        if (rollDirection.sqrMagnitude > 0.001f)
        {
            rollDirection.Normalize();
            player.transform.rotation = Quaternion.LookRotation(rollDirection);
        }

        player.playerAnimatorManager.PlayTargetActionAnimation("RollForward", true, false, false);
    }

    /*
    note to self

    replace buffer with buffer queue for all

    priority right now is to set up a hitbox for the sword and player

    then set up buffer queue

    then implement taking damage and animation for that

    */

    public void AttemptToPerformMeleeAttack(int chainNum)
    {
        if (player.isPerformingAction)
        {
            // if any other buffered input other than attack1, add attack1 to the buffer
            // if (PlayerInputBuffer.Instance.GetBufferedInput() == PlayerInputType.None)
            // {
            //     PlayerInputBuffer.Instance.BufferInput(PlayerInputType.Attack1);
            // }


            //Debug.Log("character cannot perform action");
            return;
        }
        //Debug.Log("melee attack performed");

        playerWeapon.EnableWeaponCollider();
        switch (chainNum)
        {
            case 1:
                playerWeapon.SetWeaponDamage(10);
                break;
            case 2:
                playerWeapon.SetWeaponDamage(20);
                break;
            case 3:
                playerWeapon.SetWeaponDamage(30);
                break;
            default:
                break;
        }        
        player.playerAnimatorManager.PlayTargetActionAnimation("MeleeAttack" + chainNum, true, true, true);
        //player.previousInput = PlayerInputType.Attack1;
    }
}
