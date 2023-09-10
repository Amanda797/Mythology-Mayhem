using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBlend : StateMachineBehaviour
{
    [SerializeField] string walkingBool;
    [SerializeField] string runningBool;
    [SerializeField] string moveBlend;

    [SerializeField] float walkingLevel;
    [SerializeField] float runningLevel;
    [SerializeField] float idleLevel;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool(walkingBool)) {
            animator.SetFloat(moveBlend, walkingLevel);
        } else if(animator.GetBool(runningBool)) {
            animator.SetFloat(moveBlend, runningLevel);
        } else {
            animator.SetFloat(moveBlend, idleLevel);
        }
    }

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
