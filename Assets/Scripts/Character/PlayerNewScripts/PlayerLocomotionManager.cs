using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        //taken from input manager
        PlayerManager player;
        [HideInInspector] public float verticalMovement;
        [HideInInspector] public float horizontalMovement;
        [HideInInspector] public float moveAmount;

        [Header("Movement Settings")]
        private Vector3 moveDirection;
        private Vector3 targetRotationDirection;

        [SerializeField] float walkingSpeed = 2;
        [SerializeField] float runningSpeed = 5;
        [SerializeField] float rotationSpeed = 15;

        [Header("Dodge")]
        private Vector3 rollDirection;


        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        protected override void Update()
        {
            
            // NETCODE PORTION, UNCOMMENT LATER
            // if (player.IsOwner)
            // {
            //     player.characterNetworkManager.verticalMovement.Value = verticalMovement;
            //     player.characterNetworkManager.horizontalMovement.Value = horizontalMovement;
            //     player.characterNetworkManager.moveAmount.Value = moveAmount;
            // }
            // else
            // {
            //     verticalMovement = player.characterNetworkManager.verticalMovement.Value;
            //     horizontalMovement = player.characterNetworkManager.horizontalMovement.Value;
            //     moveAmount = player.characterNetworkManager.moveAmount.Value;

            //     // if not locked on, pass move amount
            //     player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);

            //     // if locked on, pass vert and horiz
            // }
            
        }

        public void HandleAllMovement()
        {

            //grounded movement
            HandleGroundedMovement();
            HandleRotation();
            //aerial movement
        }

        private void GetMovementValues()
        {
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.horizontalInput;
            moveAmount = PlayerInputManager.instance.moveAmount;

            //clamp movement for animations
        }
        private void HandleGroundedMovement()
        {
            GetMovementValues();

            if (!player.canMove)
                return;
            //our move direction is based on our cameras facing persepctive and movement inputs
            moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
            moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
            moveDirection.y = 0;
            moveDirection.Normalize();


            if (PlayerInputManager.instance.moveAmount > 0.5f)
            {
                // move at a running speed
                player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);

            }
            else if (PlayerInputManager.instance.moveAmount <= 0.5f)
            {
                // move at a walking speed
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            }

            //Debug.Log("In HandleGroundedMovement(), transform.rotation is " + transform.rotation);
        }

        private void HandleRotation()
        {
            if (!player.canRotate)
                return;
            targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection = targetRotationDirection + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
            targetRotationDirection.y = 0;
            targetRotationDirection.Normalize();
            //Debug.Log("Active, targetRotation is " + targetRotationDirection);
            
            if (targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
                //Debug.Log("Stopped, targetRotation is " + targetRotationDirection);
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            //Debug.Log("New Rotation is " + newRotation);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            //Debug.Log("Target rotation calculated as " + targetRotation);
            transform.rotation = targetRotation;
            //Debug.Log(" in HandleRotation, transform rotation set to " + transform.rotation);
        }

        public void AttemptToPerformDodge()
        {
            // if (player.isPerformingAction)
            // {
            //     Debug.Log("Player is performing action!");
            //     return;
            // }
            // if we are moving, we dodge
            if (moveAmount > 0)
            {
                rollDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
                rollDirection += PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
                rollDirection.y = 0;
                rollDirection.Normalize();

                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                player.transform.rotation = playerRotation;

                // change string arg to name of currently used roll animation
                player.playerAnimatorManager.PlayTargetActionAnimation("RollForward", true, true);
                Debug.Log("Dodged!");
                // perform a roll anim
            }
            // if stationary, we backstep
            else
            {
                // perform a backstep anim
                player.playerAnimatorManager.PlayTargetActionAnimation("Backstep", true, true);
            }
        }
    }
}
