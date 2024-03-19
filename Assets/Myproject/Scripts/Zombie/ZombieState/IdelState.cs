using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FPSZombie.Player;

public class IdelState : StateMachineBehaviour
{
    private float timer;
    private Transform player;
    private NavMeshAgent agent;
    private float chaseRange = 15f;
    private float distance;
    private float walkingTime = 10f;
    private float minIdleTime = 2f; // Optional: Minimum time to stay idle

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = PlayerService.Instance.GetPlayerTransform().transform;
        agent = animator.GetComponent<NavMeshAgent>();
        //distance = Vector3.Distance(animator.transform.position, player.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        distance = Vector3.Distance(animator.transform.position, player.position);

        // Check if minimum idle time has passed before walking
        if (timer > minIdleTime && timer < walkingTime)
        {
            animator.SetBool("Walk", false);
        }
        else if (timer > walkingTime)
        {
            timer = walkingTime; // Reset timer to prevent continuous walking 
            animator.SetBool("Walk", true);
        }



        if (distance < chaseRange)
        {
            animator.SetBool("IsChasing", true);
        }
        else if (distance > chaseRange)
        {
            animator.SetBool("IsChasing", false);
            animator.SetBool("Walk", true); // Optional: Reset walking animation
            timer = 0f; // Restart idle timer
        }
    }
}
