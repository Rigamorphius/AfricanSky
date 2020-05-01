using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AIWalkBehaviour : StateMachineBehaviour
{
    Enemy_AI enemyAI;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        enemyAI = animator.GetComponentInParent<Enemy_AI>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        enemyAI.rb2D.MovePosition(enemyAI.target.position);
    }
}
