using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkBehaviour : StateMachineBehaviour
{

    Transform playerTransform;
    Rigidbody2D rigidbody2D;
    Enemy enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindObjectOfType<Player>().transform;
        rigidbody2D = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.LookAtPlayer();

        Vector2 target = new Vector2(playerTransform.position.x, playerTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rigidbody2D.position, target, enemy.walkSpeed * Time.fixedDeltaTime);
        rigidbody2D.MovePosition(newPosition);

        if(Vector2.Distance(playerTransform.position, rigidbody2D.position) <= enemy.attackRange)
        {
            animator.SetTrigger("Punch");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Punch");
    }
}
