using UnityEngine;
using Unity.Netcode;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager characterManager;

    float vertical;
    float horizontal;
    protected virtual void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
    }
    public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
    {
        characterManager.animator.SetFloat("Horizontal", horizontalValue, 0.1f, Time.deltaTime);
        characterManager.animator.SetFloat("Vertical", verticalValue, 0.1f, Time.deltaTime);
    }

    public virtual void PlayTargetActionAnimation(string targetAnimation,
        bool isPerformingAction, bool actionCancelable, bool isChaining, bool applyRootMotion = true,
        bool canRotate = false, bool canMove = false)
    {
        characterManager.applyRootMotion = applyRootMotion;
        characterManager.animator.CrossFade(targetAnimation, 0.2f);

        // used to stop character from performing new actions in the midst of one
        characterManager.isPerformingAction = isPerformingAction;
        characterManager.actionCancelable = actionCancelable;
        characterManager.isChaining = isChaining;
        characterManager.canRotate = canRotate;
        characterManager.canMove = canMove;



        // tell host that an animation was played, and that every client should see that animation too
        characterManager.characterNetworkManager.NotifyServerOfActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
    }
}
