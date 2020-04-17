using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkBehaviour : StateMachineBehaviour
{

    Transform playerTransform;
    Rigidbody2D rigidbody2D;
    Enemy enemy;
    EnemyAI enemyAI;
    EnemyAI.PatrolStates patrolStates;

    public float distanceTravelled;
    public Vector2 lastPosition;
   
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindObjectOfType<Player>().transform;
        rigidbody2D = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
        enemyAI = animator.GetComponent<EnemyAI>();
        patrolStates = enemyAI.patrolState;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        enemy.LookAtPlayer();
        CheckStates();

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


    void CheckStates()
    {
        Vector2 target = new Vector2(playerTransform.position.x, playerTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rigidbody2D.position, target, enemy.walkSpeed * Time.fixedDeltaTime);
        switch (patrolStates)
        {
            case EnemyAI.PatrolStates.None:
                rigidbody2D.MovePosition(newPosition);
                break;
            case EnemyAI.PatrolStates.Horizontal:
                MoveHorizontally();
                break;
            case EnemyAI.PatrolStates.Vertical:
                
                break;
            case EnemyAI.PatrolStates.Random:
                break;
            default:
                break;
        }
    }

    void MoveHorizontally()
    {
        if(distanceTravelled >= 1)
            rigidbody2D.velocity = new Vector2(Mathf.Lerp(0.5f, -0.5f, 1f), 0);
        if (rigidbody2D.velocity.x <= -0.5)
            rigidbody2D.velocity = new Vector2(Mathf.Lerp(-0.5f, 0.5f, 1f), 0);

        distanceTravelled += Vector2.Distance(enemy.transform.position, lastPosition);
        lastPosition = enemy.transform.position;

    }
}
