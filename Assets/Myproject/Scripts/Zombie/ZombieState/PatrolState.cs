using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FPSZombie.Player;
using FPSZombie.Zombie;

public class PatrolState : StateMachineBehaviour
{
    private Transform parantWaypoints;
    private List<Transform> zombieWaypoints;
    private NavMeshAgent agent;
    private float chaseRange = 10f;
    private float attackRange = 3f;
    private Transform player;
    private float distance;
    private int currentWaypointIndex = 0;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        zombieWaypoints = new List<Transform>();
        player = PlayerService.Instance.GetPlayerTransform().transform;
        parantWaypoints = ZombieService.Instance.GetWayPoints();

        foreach (Transform item in parantWaypoints)
            zombieWaypoints.Add(item);

        agent = animator.GetComponent<NavMeshAgent>();
        currentWaypointIndex = Random.Range(0, zombieWaypoints.Count);
        agent.SetDestination(zombieWaypoints[currentWaypointIndex].position);
        agent.speed = Random.Range(2f, 3f);
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = Vector3.Distance(animator.transform.position, player.position);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentWaypointIndex = Random.Range(0, zombieWaypoints.Count);
            agent.SetDestination(zombieWaypoints[currentWaypointIndex].position);
        }
        if (distance < chaseRange)
        {
            agent.speed = 5;
            animator.SetBool("Walk", false);
            animator.SetBool("IsChasing", true);
            agent.SetDestination(player.position); // Chase player
        }
        if (distance < attackRange)
        {
            animator.SetBool("IsChasing", false);
            animator.SetBool("IsAttacking", true);
            agent.SetDestination(animator.transform.position); // Stop movement while attacking
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
