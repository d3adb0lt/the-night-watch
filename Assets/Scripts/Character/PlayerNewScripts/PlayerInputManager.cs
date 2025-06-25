using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SG
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;
        PlayerControls playerControls;

        [Header("Movement Input")]
        [SerializeField] Vector2 movementInput;
        public float horizontalInput;
        public float verticalInput;
        public float moveAmount;

        [Header("Camera Movement Input")]
        [SerializeField] Vector2 cameraInput;
        public float cameraHorizontalInput;
        public float cameraVerticalInput;


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
            HandlePlayerMovementInput();
            HandelCameraMovementInput();

        }

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
        }

        private void HandelCameraMovementInput()
        { 
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }
    }
}

