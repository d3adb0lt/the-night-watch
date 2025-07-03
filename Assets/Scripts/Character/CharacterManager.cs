using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class CharacterManager : NetworkBehaviour
{
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;
    [HideInInspector] public CharacterNetworkManager characterNetworkManager;

    [Header("Flags")]
    public bool isPerformingAction = false;
    public bool applyRootMotion = false;
    public bool canRotate = true;
    public bool canMove = true;
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);

        characterController = GetComponent<CharacterController>();
        characterNetworkManager = GetComponent<CharacterNetworkManager>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        // if your character moved in your world, send that information over to the connected player,
        // so that your character representation can be changed accordingly on their end
        if (IsOwner)
        {
            characterNetworkManager.networkPosition.Value = transform.position;
            characterNetworkManager.networkRotation.Value = transform.rotation;
        }
        // if the person connected to your game world moves then update their character representation in your world accordingly.
        else
        {
            // update position
            transform.position = Vector3.SmoothDamp
                (transform.position, characterNetworkManager.networkPosition.Value,
                ref characterNetworkManager.networkPositionVelocity, characterNetworkManager.networkPositionSmoothTime);
            // update rotation
            transform.rotation = Quaternion.Slerp
                (transform.rotation, characterNetworkManager.networkRotation.Value,
                characterNetworkManager.networkRotationSmoothTime);
        }
    }

    protected virtual void LateUpdate()
    {
        
    }
}
