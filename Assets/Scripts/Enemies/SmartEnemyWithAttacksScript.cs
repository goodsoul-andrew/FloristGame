using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using System.Collections.Generic;

public class SmartEnemyWithAttacks : Enemy
{    
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject hitBox;
    private bool inAnimation = false;
    private bool hasEnemyInArea = false;

    new void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.speed = Speed;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        //chasingState.ChaseDuration = 5f;
        chasingState.ForgetTarget = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Player>(out var player)) hasEnemyInArea = true;
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.TryGetComponent<Player>(out var player)) hasEnemyInArea = false;
    }

    public override void MoveTowardsTarget(Vector2 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
    }

    private IEnumerator SetActiveAfterDelay(float delay,bool active)
    {
        yield return new WaitForSeconds(delay);
        hitBox.SetActive(active);
    }

    private IEnumerator EndAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        inAnimation = false;
        Debug.Log("ended");
        animator.Play("Idle");
        navMeshAgent.speed = Speed;
    }

    private void Update()
    {
        if(hasEnemyInArea && !inAnimation)
        {
            navMeshAgent.speed = 0;
            inAnimation = true;
            animator.Play("EnemyHit");
            StartCoroutine(SetActiveAfterDelay(0.43f,true));
            StartCoroutine(SetActiveAfterDelay(0.55f,false));
            StartCoroutine(EndAnimationAfterDelay(1f));
        }
    }

}
