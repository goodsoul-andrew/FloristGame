using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SmartEnemy : Enemy
{    private NavMeshAgent navMeshAgent;

    new void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    void FixedUpdate()
    {
        if (playerCollider is not null)
        {
            if (Vector2.Distance(transform.position, playerCollider.transform.position) <= detectionRadius)
            {
                navMeshAgent.SetDestination(playerCollider.transform.position);
            }
        }
    }
}
