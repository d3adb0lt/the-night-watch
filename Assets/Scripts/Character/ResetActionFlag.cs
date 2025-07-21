using UnityEngine;

public class ResetActionFlag : StateMachineBehaviour
{
    CharacterManager characterManager;
    PlayerWeaponManager playerWeapon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (characterManager == null)
        {
            characterManager = animator.GetComponent<CharacterManager>();
            playerWeapon = animator.GetComponentInChildren<PlayerWeaponManager>();
        }

        // this runs when the action is completed
        characterManager.isPerformingAction = false;
        characterManager.applyRootMotion = false;
        characterManager.canRotate = true;
        characterManager.canMove = true;
        characterManager.actionCancelable = false;
        characterManager.isChaining = false;
        characterManager.chainNum = 0;
        characterManager.previousInput = PlayerInputType.None;

        playerWeapon.DisableWeaponCollider();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
