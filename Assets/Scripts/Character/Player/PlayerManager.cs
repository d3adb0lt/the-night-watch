using UnityEngine;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerLocoMotionManager playerLocoMotionManager;
    protected override void Awake()
    {
        base.Awake();

        // can do this bc the two components are apart of the same gameObject
        playerLocoMotionManager = GetComponent<PlayerLocoMotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();

        PlayerInputManager.GetInstance()?.RegisterPlayer(this);
    }

    protected override void Update()
    {
        base.Update();

        // all characted movement handled per frame
        if (playerLocoMotionManager != null)
            playerLocoMotionManager.HandleAllMovement();
    }
}
