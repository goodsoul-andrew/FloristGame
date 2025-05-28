using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using System.Collections.Generic;

public class SmartEnemyWithAttacks : Enemy
{    
    public bool inAnimation = false;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    new void Start()
    {
        base.Start();

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.speed = Speed;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        //chasingState.ChaseDuration = 5f;
        chasingState.ForgetTarget = false;
    }

    public override void MoveTowardsTarget(Vector2 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, player.TruePosition) <= 2 && !inAnimation && !player.isDead)
        {
            animator.SetTrigger("Attack");
            navMeshAgent.speed = 0;
            inAnimation = true;
        }
    }
    public void UpdateSpeed()
    {
        navMeshAgent.speed = Speed;
    }

}
