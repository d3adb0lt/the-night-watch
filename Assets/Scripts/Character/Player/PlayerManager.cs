using UnityEngine;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerLocoMotionManager playerLocoMotionManager;
    protected override void Awake()
    {
        base.Awake();

        // only player logic after this line

        // can do this bc the two components are apart of the same gameObject
        playerLocoMotionManager = GetComponent<PlayerLocoMotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
    }

    protected override void Update()
    {
        base.Update();

        // if gameobject is not owned by us, do not edit or control it
        // this stops players being able to move themselves and those who joined their server
        // if (!IsOwner)
        // {
        //     return;
        // }

        // all characted movement handled per frame
        playerLocoMotionManager.HandleAllMovement();
    }

    protected override void LateUpdate()
    {
        // if not the local player object do nothing (i.e. dont mess with the camera)
        if (!IsOwner)
        {
            return;
        }

        base.LateUpdate();

        PlayerCamera.GetInstance().HandleAllCameraActions();
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // if this player object is owned by this client
        if (IsOwner)
        {
            PlayerCamera.GetInstance().player = this;
            PlayerInputManager.GetInstance().playerManager = this;
        }
    }
}
