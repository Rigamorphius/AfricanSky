﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPunchBehaviour : StateMachineBehaviour
{
    Player player;
    HitBox attackHitBox;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<Player>();
        player.rigidbody2D.velocity = Vector2.zero;
        animator.SetBool("Punch", false);
        attackHitBox = player.GetComponentInChildren<HitBox>();
        attackHitBox.hitBoxCollider.enabled = true;

        player.PlaySound();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Punch", false);
        player.rigidbody2D.velocity = Vector2.zero;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Punch");
        attackHitBox.hitBoxCollider.enabled = false;
    }
}
