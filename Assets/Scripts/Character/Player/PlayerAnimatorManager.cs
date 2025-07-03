using UnityEngine;

public class PlayerAnimatorManager : CharacterAnimatorManager
{
    PlayerManager playerManager;

    protected override void Awake()
    {
        base.Awake();

        playerManager = GetComponent<PlayerManager>();
    }
    // called every frame an animation is playing
    private void OnAnimatorMove()
    {
        if (playerManager.applyRootMotion)
        {
            Vector3 velocity = playerManager.animator.deltaPosition;
            playerManager.characterController.Move(velocity);
            playerManager.transform.rotation *= playerManager.animator.deltaRotation;
        }
    }
}
