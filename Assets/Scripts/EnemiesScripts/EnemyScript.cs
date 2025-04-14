using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Health health;
    public DamageDealer damageDealer;
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float detectionRadius = 10f;
    protected Rigidbody2D rb;
    protected Collider2D playerCollider;
    protected CircleCollider2D selfCollider;

    protected void Start()
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

    protected void DestroyMyself()
    {
        Destroy(gameObject);
    }
}
