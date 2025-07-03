using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    private static PlayerInputManager instance;
    public PlayerManager playerManager;
    UserControls userControls;

    [Header("Player Movement Input")]
    [SerializeField] Vector2 movement;
    public float vInput;
    public float hInput;
    public float totalMovement;

    [Header("Camera Movement Input")]
    [SerializeField] Vector2 cameraInput;
    public float cameraVInput;
    public float cameraHInput;

    [Header("Player Action Input")]
    [SerializeField] bool dodgeInput = false;

    private void Awake()
    {
        // can only ever be one input manager, if more than one, Destroy immediately
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

        // when scene changes, this runs once
        SceneManager.activeSceneChanged += OnSceneChange;

        instance.enabled = false;
    }
    public static PlayerInputManager GetInstance()
    {
        return instance;
    }
    public static void SetInstance(PlayerInputManager instance)
    {
        PlayerInputManager.instance = instance;
    }

    private void OnSceneChange(Scene currScene, Scene comingScene)
    {
        // loading into world scene? enable player inputs
        if (comingScene.buildIndex == WorldSaveGameManager.GetInstance().GetWorldSceneIndex())
        {
            // input manager enabled
            instance.enabled = true;
        }
        // have to be in a "non-playable area", i.e. main menu
        // player cannot move during menu scene
        else
        {
            instance.enabled = false;
        }
    }

    // called when this script is active
    private void OnEnable()
    {
        if (userControls == null)
        {
            userControls = new UserControls();

            // takes the values from the performed user control and adds them to the Vector2 var movement
            userControls.UserMovement.Movement.performed += i => movement = i.ReadValue<Vector2>();

            userControls.UserCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();

            userControls.UserActions.Dodge.performed += i => dodgeInput = true;
        }
        userControls.Enable();
    }

    // called whenever the game is minimized or maximized
    // game maximized controls are enabled, game minimized controls disabled
    private void OnApplicationFocus(bool focus)
    {
        if (enabled)
        {
            if (focus)
            {
                userControls.Enable();
            }
            else
            {
                userControls.Disable();
            }
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from event, to prevent memory leak
        SceneManager.activeSceneChanged -= OnSceneChange;

    }

    private void Update()
    {
        HandleAllInputs();
    }

    private void HandleAllInputs()
    {
        HandleMoveInput();
        HandleCameraMoveInput();
        HandleDodgeInput();
    }

    // movement
    private void HandleMoveInput()
    {
        vInput = movement.y;
        hInput = movement.x;

        // total movement should be positive
        totalMovement = Mathf.Clamp01(Mathf.Abs(vInput) + Mathf.Abs(hInput));

        // clamps the values to .5 for walking and 1 for running
        if (totalMovement <= 0.5 && totalMovement > 0)
        {
            totalMovement = 0.5f;
        }
        else if (totalMovement > 0.5 && totalMovement <= 1)
        {
            totalMovement = 1;
        }

        if (playerManager == null)
        {
            return;
        }

        playerManager.playerAnimatorManager.UpdateAnimatorMovementParameters(0, totalMovement);
    }

    private void HandleCameraMoveInput()
    {
        cameraVInput = cameraInput.y;
        cameraHInput = cameraInput.x;
    }

    // action
    private void HandleDodgeInput()
    {
        if (dodgeInput)
        {
            dodgeInput = false;
            // perform dodge

            playerManager.playerLocoMotionManager.AttemptToPerformDodge();
        }
    }
}

