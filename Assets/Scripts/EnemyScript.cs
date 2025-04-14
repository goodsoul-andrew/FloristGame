using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Health health;
    public DamageDealer damageDealer;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float detectionRadius = 10f;
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private CircleCollider2D selfCollider;

    void Start()
    {
        health = GetComponent<Health>();
        damageDealer = GetComponent<DamageDealer>();
        rb = GetComponent<Rigidbody2D>();
        health.OnDeath += DestroyMyself;
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>();
        selfCollider = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        if (playerCollider is not null)
        {
            if (Vector2.Distance(transform.position, playerCollider.transform.position) <= detectionRadius && CanSeePlayer())
            {
                MoveTowardsPlayer();
            }
        }
    }

    private bool CanSeePlayer()
    {
        var playerPos = playerCollider.transform.position + (Vector3)playerCollider.offset;
        var r = selfCollider.radius * selfCollider.transform.localScale.x + 0.1f;
        Vector2 directionToPlayer = (playerPos - selfCollider.transform.position).normalized;
        Vector2 rayPos = (Vector2)selfCollider.transform.position + selfCollider.offset + directionToPlayer * r;
        RaycastHit2D hit = Physics2D.Raycast(rayPos, directionToPlayer, detectionRadius - r);
        Debug.DrawRay(rayPos, directionToPlayer, Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true; 
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
