using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FPSZombie.player;
using FPSZombie.Zombie;

public class ChaseState : StateMachineBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private Transform parantWaypoints;
    private List<Transform> zombieWaypoints;
    private float attackRange = 2.5f;
    private float distance;
    private float chaseRange = 25f;
    private int curentWayIndex = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = PlayerService.instance.GetPlayerTransform().transform;
        agent = animator.GetComponent<NavMeshAgent>();
        parantWaypoints = ZombieService.Instance.GetWayPoints();
        zombieWaypoints = new List<Transform>();
        foreach (Transform item in parantWaypoints)
            zombieWaypoints.Add(item);

        agent.speed = Random.Range(6f, 9f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = Vector3.Distance(animator.transform.position, player.position);
        curentWayIndex = Random.Range(0, zombieWaypoints.Count);

        agent.SetDestination(player.position);
        if (distance < attackRange)
        {
            animator.SetBool("IsAttacking", true);
        }
        if (distance > chaseRange)
        {

            animator.SetBool("IsChasing", false);
            animator.SetBool("Walk", true);
            agent.SetDestination(zombieWaypoints[curentWayIndex].position);

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
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
