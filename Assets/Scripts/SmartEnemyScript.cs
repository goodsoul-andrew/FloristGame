using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SmartEnemy : MonoBehaviour
{
    public Health health;
    public DamageDealer damageDealer;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float detectionRadius = 10f;
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private CircleCollider2D selfCollider;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        health = GetComponent<Health>();
        damageDealer = GetComponent<DamageDealer>();
        rb = GetComponent<Rigidbody2D>();
        health.OnDeath += DestroyMyself;
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>();
        selfCollider = GetComponent<CircleCollider2D>();
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
                //MoveTowardsPlayer();
            }
        }
    }

    private bool CanSeePlayer()
    {
        var playerPos = playerCollider.transform.position + (Vector3)playerCollider.offset;
        Vector2 directionToPlayer = (playerPos - selfCollider.transform.position).normalized;
        var r = selfCollider.radius * selfCollider.transform.localScale.x + 0.1f;
        var hits = Physics2D.RaycastAll(selfCollider.transform.position, directionToPlayer, detectionRadius - r);
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
            else if (hit.collider.CompareTag("Obstacle"))
            {
                return false;
            }
        }
        return false;
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
        rb.MovePosition(rb.position + moveSpeed * Time.deltaTime * direction);
    }

    private void DestroyMyself()
    {
        Destroy(gameObject);
    }
}
