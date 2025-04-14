using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SmartEnemy : Enemy
{    
    public NavMeshAgent navMeshAgent;

    new void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        chasingState.ChaseDuration = 5f;
        chasingState.ForgetTarget = false;
    }

    public override void MoveTowardsTarget(Vector2 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
    }
}
