using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwingSFX : StateMachineBehaviour
{
    [SerializeField] AudioRandomizer_SO SwordSwings;
    AudioSource aus;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(aus == null) {
            aus = animator.gameObject.GetComponent<AudioSource>();
        }

        aus.clip = SwordSwings.PlaySiblings();
        aus.Play();
    }
}
