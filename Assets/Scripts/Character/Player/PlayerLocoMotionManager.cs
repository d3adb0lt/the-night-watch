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
        //CaptureMovementInput();
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
        CaptureMovementInput();

        if (!player.canMove)
        {
            return;
        }

        // movement based off of camera perspective and user input
        movementDirection = IsometricCamera.instance.transform.forward * verticalMovement;
        movementDirection += IsometricCamera.instance.transform.right * horizontalMovement;
        movementDirection.Normalize();
        movementDirection.y = 0;

        if (PlayerInputManager.GetInstance().totalMovement > 0.5f)
        {
            // at running speed
            player.characterController.Move(movementDirection * runSpeed * Time.deltaTime);
        }
        else if (PlayerInputManager.GetInstance().totalMovement <= 0.5f)
        {
            // at walking speed (unused unless controller)
            player.characterController.Move(movementDirection * walkSpeed * Time.deltaTime);

        }
    }

    private void HandleRotationMovement()
    {
        if (!player.canRotate)
        {
            return;
        }

        desiredRotationDir = Vector3.zero;
        desiredRotationDir = IsometricCamera.instance.cameraObject.transform.forward * verticalMovement;
        desiredRotationDir += IsometricCamera.instance.cameraObject.transform.right * horizontalMovement;
        desiredRotationDir.Normalize();
        desiredRotationDir.y = 0;

        if (desiredRotationDir == Vector3.zero)
        {
            desiredRotationDir = transform.forward;
        }

        Quaternion newTurnRotation = Quaternion.LookRotation(desiredRotationDir);
        Quaternion desiredRotation = Quaternion.Slerp(transform.rotation, newTurnRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = desiredRotation;
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
            rollDirection = IsometricCamera.instance.cameraObject.transform.forward * verticalMovement;
            rollDirection += IsometricCamera.instance.cameraObject.transform.right * horizontalMovement;

            //rollDirection = 
            rollDirection.y = 0;
            rollDirection.Normalize();

            Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
            player.transform.rotation = playerRotation;

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
