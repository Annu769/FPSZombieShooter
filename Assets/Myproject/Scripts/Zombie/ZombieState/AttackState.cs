using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FPSZombie.player;

public class AttackState : StateMachineBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private float chaseRange = 10f;
    private float distance;
    private float attcakDistance = 2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = PlayerService.instance.GetPlayerTransform().transform;
        // distance = Vector3.Distance(animator.transform.position, player.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = Vector3.Distance(animator.transform.position, player.position);

        // Check for attack animation event to transition out
        if (distance > attcakDistance)
        {
            animator.SetBool("IsAttacking", false);
        }

        if (distance > chaseRange)
        {
            animator.SetBool("IsChasing", true);
            agent.SetDestination(player.position); // Chase player if out of range
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Stop movement when exiting attack state (optional)
        agent.SetDestination(agent.transform.position);
    }

    public void OnAttackFinished(Animator animator) // This function will be called by the animation event
    {
        animator.SetBool("IsAttacking", false);
    }
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
