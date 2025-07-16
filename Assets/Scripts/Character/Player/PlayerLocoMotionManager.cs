using UnityEngine;

public class PlayerLocoMotionManager : CharacterLocomotionManager
{
    PlayerManager player;

    // taken from the player input manager
    [HideInInspector] public float verticalMovement;
    [HideInInspector] public float horizontalMovement;
    [HideInInspector] public float totalMovement;

    [Header("Movement Settings")]
    private Vector3 movementDirection;
    private Vector3 desiredRotationDir;
    [SerializeField] float walkSpeed = 2;
    [SerializeField] float runSpeed = 5;
    [SerializeField] float rotationSpeed = 12;

    [Header("Dodge")]
    private Vector3 rollDirection;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    protected override void Update()
    {
        base.Update();

        if (player.IsOwner)
        {
            player.characterNetworkManager.verticalMovement.Value = verticalMovement;
            player.characterNetworkManager.horizontalMovement.Value = horizontalMovement;
            player.characterNetworkManager.totalMovement.Value = totalMovement;
        }
        else
        {
            verticalMovement = player.characterNetworkManager.verticalMovement.Value;
            horizontalMovement = player.characterNetworkManager.horizontalMovement.Value;
            totalMovement = player.characterNetworkManager.totalMovement.Value;

            // have to do this since this is not the host, thus it will not be done in the player input manager
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, totalMovement);
        }
    }

    public void HandleAllMovement()
    {
        if (player.isPerformingAction)
        {
            return;
        }
        // ground movement
        HandleGroundMovement();
        HandleRotationMovement();
        // aerial movement
    }

    private void GetMovementValues()
    {
        verticalMovement = PlayerInputManager.GetInstance().vInput;
        horizontalMovement = PlayerInputManager.GetInstance().hInput;
        totalMovement = PlayerInputManager.GetInstance().totalMovement;
        // clamp movements
    }

    private void HandleGroundMovement()
    {
        GetMovementValues();

        if (!player.canMove)
        {
            return;
        }

        // movement based off of camera perspective and user input
        movementDirection = PlayerCamera.GetInstance().transform.forward * verticalMovement;
        movementDirection += PlayerCamera.GetInstance().transform.right * horizontalMovement;
        movementDirection.Normalize();
        movementDirection.y = 0;

        if (PlayerInputManager.GetInstance().totalMovement > 0.5f)
        {
            // at running speed
            player.characterController.Move(movementDirection * runSpeed * Time.deltaTime);
        }
        else if (PlayerInputManager.GetInstance().totalMovement <= 0.5f)
        {
            // at walking speed
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
        desiredRotationDir = PlayerCamera.GetInstance().cameraObject.transform.forward * verticalMovement;
        desiredRotationDir += PlayerCamera.GetInstance().cameraObject.transform.right * horizontalMovement;
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
        if (totalMovement > 0)
        {
            // roll animation
            rollDirection = PlayerCamera.GetInstance().cameraObject.transform.forward * verticalMovement;
            rollDirection += PlayerCamera.GetInstance().cameraObject.transform.right * horizontalMovement;
            rollDirection.y = 0;
            rollDirection.Normalize();

            Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
            player.transform.rotation = playerRotation;

            player.playerAnimatorManager.PlayTargetActionAnimation("RollForward", true, false, false);
        }
        else
        {
            // back-step animation
            player.playerAnimatorManager.PlayTargetActionAnimation("Backstep", true, false, false);
        }
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
        player.playerAnimatorManager.PlayTargetActionAnimation("MeleeAttack" + chainNum, true, true, true);
        player.previousInput = PlayerInputType.Attack1;

    }
}
