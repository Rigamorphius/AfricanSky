using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpBehaviour : StateMachineBehaviour
{
    Player player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<Player>();
        //animator.SetBool("Jump", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("Jump", false);
        animator.SetBool("Punch", false);
        player.rigidbody2D.velocity = Vector2.zero;

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Jump", false);
    }
}
