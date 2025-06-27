using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SG
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;
        public PlayerManager player;
        PlayerControls playerControls;

        [Header("Camera Movement Input")]
        [SerializeField] Vector2 cameraInput;
        public float cameraHorizontalInput;
        public float cameraVerticalInput;

        [Header("Movement Input")]
        [SerializeField] Vector2 movementInput;
        public float horizontalInput;
        public float verticalInput;
        public float moveAmount;

        [Header("Player Action Input")]
        [SerializeField] bool dodgeInput = false;


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

            Debug.Log("Player input manager instantiated");

        }

        private void Start()
        {
            //set to false after menu is enabled
            DontDestroyOnLoad(gameObject);

            //when scene changes, this function runs. uncomment when menu is set up
            //SceneManager.activeSceneChanged += OnSceneChange;

            instance.enabled = true;
        }
        //enable this function when menu implemented
        // private void OnSceneChange(Scene oldScene, Scene newScene)
        // {
        //     //if loading into world scene, enable our players controls
        //     if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
        //     {
        //         instance.enabled = true;
        //     }
        //     //other wise at menu, disable player controls
        //     else
        //     {
        //         instance.enabled = false;
        //     }
        // }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
                playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
            }

            playerControls.Enable();
        }

        private void OnDestroy()
        {
            //if we destroy object, unsub from event, uncomment when menu is implemented
            //SceneManager.activeSceneChanged -= OnSceneChange;
        }

        //if we minimize or lower window, stop adjusting inputs
        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls.Disable();
                }
            }
        }

        private void Update()
        {
            HandleAllInputs();
            //Debug.Log("Dodge input is " + dodgeInput);
        }

        private void HandleAllInputs()
        {
            HandlePlayerMovementInput();
            HandleCameraMovementInput();
            HandleDodgeInput();
        }

        // MOVEMENT

        private void HandlePlayerMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            //returns absolute number
            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            //clamping values to 0, 0.5 or 1
            if (moveAmount <= 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5 && moveAmount <= 1)
            {
                moveAmount = 1;
            }

            // we pass 0 on the horiz because we are not locked on, only want non strafing
            // we use the horiz when strafing or locked on

            if (player == null)
            {
                return;
            }
            // if we are not locked on only use move amount
                player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);

            // if we are locked on pass the horizontal movement set
        }

        private void HandleCameraMovementInput()
        { 
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }

        // ACTIONS

        private void HandleDodgeInput()
        {
            //Debug.Log("Dodge input is " + dodgeInput);
            if (dodgeInput)
            {
                dodgeInput = false;

                // do not dodge if menu or ui is open (implement later)
                // perform a dodge
                player.playerLocomotionManager.AttemptToPerformDodge();
            }
        }
    }
}

