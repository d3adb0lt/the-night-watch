using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        //taken from input manager
        PlayerManager player;
        public float verticalMovement;
        public float horizontalMovement;
        public float moveAmount;

        private Vector3 moveDirection;
        private Vector3 targetRotationDirection;

        [SerializeField] float walkingSpeed = 2;
        [SerializeField] float runningSpeed = 5;
        [SerializeField] float rotationSpeed = 15;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        public void HandleAllMovement()
        {
            //grounded movement
            HandleGroundedMovement();
            HandleRotation();
            //aerial movement
        }

        private void GetVerticalAndHorizontalInputs()
        {
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.horizontalInput;

            //clamp movement for animations
        }
        private void HandleGroundedMovement()
        {
            GetVerticalAndHorizontalInputs();

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
            //targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection = targetRotationDirection + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
            targetRotationDirection.y = 0;
            targetRotationDirection.Normalize();
            Debug.Log("Active, targetRotation is " + targetRotationDirection);
            
            if (targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
                Debug.Log("Stopped, targetRotation is " + targetRotationDirection);
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            //Debug.Log("New Rotation is " + newRotation);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            //Debug.Log("Target rotation calculated as " + targetRotation);
            transform.rotation = newRotation;
            Debug.Log(" in HandleRotation, transform rotation set to " + transform.rotation);
        }
    }
}
