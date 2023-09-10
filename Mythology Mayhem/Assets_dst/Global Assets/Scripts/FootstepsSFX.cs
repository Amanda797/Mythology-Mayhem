using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSFX : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.gameObject.GetComponent<PlayerController>().XMovement != 0) {
            animator.gameObject.GetComponent<PlayerController>().FootstepsSFX.Play();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.gameObject.GetComponent<PlayerController>().XMovement == 0) {
            animator.gameObject.GetComponent<PlayerController>().FootstepsSFX.Stop();
        }
    }
}
