using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    private static PlayerInputManager instance;
    public PlayerManager playerManager;
    public PlayerInputBuffer playerInputBuffer;
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
    [SerializeField] bool attackInput = false;
    [SerializeField] int chainNum = 0;
    [SerializeField] PlayerInputType bufferedInput = PlayerInputType.None;

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

        userControls.UserMovement.Movement.performed += i => 
            movement = i.ReadValue<Vector2>();

        userControls.UserCamera.Movement.performed += i => 
            cameraInput = i.ReadValue<Vector2>();

        userControls.UserActions.Dodge.performed += i =>
        {
            if (playerManager.isPerformingAction)
            {
                PlayerInputBuffer.Instance.BufferInput(PlayerInputType.Roll);
            }
            else
            {
                dodgeInput = true;
            }
        };

        userControls.UserActions.MeleeAttack.performed += i =>
        {
            if (playerManager.isPerformingAction)
            {
                if (chainNum == 0)
                {
                    PlayerInputBuffer.Instance.BufferInput(PlayerInputType.Attack1);
                    Debug.Log("a1 queued");
                } 
                else if (chainNum == 1)
                {
                    PlayerInputBuffer.Instance.BufferInput(PlayerInputType.Attack2);
                    Debug.Log("a2 queued");
                } 
                else if (chainNum == 2)
                {
                    PlayerInputBuffer.Instance.BufferInput(PlayerInputType.Attack3);
                    Debug.Log("a3 queued");
                }
                else if (chainNum == 3)
                {
                    PlayerInputBuffer.Instance.BufferInput(PlayerInputType.Attack1);
                    Debug.Log("loop back, a1 queued");
                }


            }
            else
            {
                attackInput = true;
                chainNum = 1;
            }
        };
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
        ReadInputBuffer();
        HandleMoveInput();
        HandleCameraMoveInput();
        HandleDodgeInput();
        HandleMeleeAttackInput();

        // methods for testing
        TestDeathAndDamageAnimPlayer();
    }

    //TO - DO make input queue

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

    // attacking
    private void HandleMeleeAttackInput()
    {
        if (attackInput)
        {
            attackInput = false;

            //attack
            playerManager.playerLocoMotionManager.AttemptToPerformMeleeAttack(chainNum);
        }
    }

    // testing purposes, handle damage animation
    private void TestDeathAndDamageAnimPlayer()
    {
        // testing damage anim
        if (Input.GetKeyDown(KeyCode.O))
        {
            playerManager.playerAnimatorManager.PlayTargetActionAnimation("TakeDamage", true, true, false);
        }

        // testing death anim
        else if (Input.GetKeyDown(KeyCode.P))
        {
            playerManager.playerAnimatorManager.PlayTargetActionAnimation("Death", true, false, false);
        }
    }

    // private void HandleInputBuffer()
    // {
    //     bufferedInput = PlayerInputBuffer.Instance.GetBufferedInput();
    //     switch (bufferedInput)
    //     {
    //         case PlayerInputType.Attack1:
    //             playerManager.playerLocoMotionManager.AttemptToPerformMeleeAttack();
    //             break;

    //         // case PlayerInputType.Attack2:
    //         //     playerManager.playerLocoMotionManager.AttemptToPerformMeleeAttack(2);
    //         //     break;

    //         // case PlayerInputType.Attack3:
    //         //     Debug.Log("Third chain attack performed");
    //         //     playerManager.playerLocoMotionManager.AttemptToPerformMeleeAttack(3);
    //         //     break;

    //         case PlayerInputType.Roll:
    //             playerManager.playerLocoMotionManager.AttemptToPerformDodge();
    //             break;

    //         default:
    //             break;
    //     }
    // }

    private void ReadInputBuffer()
    {
        if (!playerManager.isPerformingAction)
        {
            bufferedInput = PlayerInputBuffer.Instance.GetBufferedInput();
            switch (bufferedInput)
            {
                case PlayerInputType.Attack1:
                    attackInput = true;
                    chainNum = 1;
                    break;
                case PlayerInputType.Attack2:
                    attackInput = true;
                    chainNum = 2;
                    break;
                case PlayerInputType.Attack3:
                    attackInput = true;
                    chainNum = 3;
                    break;
                case PlayerInputType.Roll:
                    dodgeInput = true;
                    chainNum = 0;
                    break;
                default:
                    break;
            }
            PlayerInputBuffer.Instance.ConsumeBufferedInput();
        }
    }
}

