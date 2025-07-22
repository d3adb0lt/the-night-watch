using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;

    [Header("Flags")]
    public bool isPerformingAction = false;
    public bool applyRootMotion = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool actionCancelable = false;
    public bool isChaining = false;
    public bool isInvulnerable = false;

    [Header("Misc values")]
    public int chainNum = 0;
    public PlayerInputType previousInput = PlayerInputType.None;

    protected virtual void Awake()
    {
        //DontDestroyOnLoad(this);

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
    }

    protected virtual void LateUpdate()
    {     
    }

    public void EnableInvulnerability()
    {
        this.isInvulnerable = true;
    }

    public void DisableInvulnerability()
    {
        this.isInvulnerable = false;
    }
}
