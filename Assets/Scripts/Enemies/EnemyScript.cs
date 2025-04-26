using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IMoving, IDamageable
{
    public DamageDealer damageDealer;
    public float Speed {get; set;}
    [SerializeField] protected float detectionRadius = 10f;
    protected Rigidbody2D rb;
    public Collider2D playerCollider {get; private set;}
    public Health HP { get; set; }

    private Player player;
    protected CircleCollider2D selfCollider;
    private IEnemyState currentState;

    public readonly WanderingState wanderingState = new WanderingState();
    public readonly ChasingState chasingState = new ChasingState();

    protected void Start()
    {
        Speed = 3f;
        HP = GetComponent<Health>();
        damageDealer = GetComponent<DamageDealer>();
        damageDealer.Friends.Add("Enemy");
        rb = GetComponent<Rigidbody2D>();
        HP.OnDeath += DestroyMyself;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>();
        selfCollider = GetComponent<CircleCollider2D>();

        ChangeState(wanderingState);
    }

    public void ChangeState(IEnemyState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }

    void FixedUpdate()
    {
        currentState?.Update(this);
    }

    public bool IsPlayerInChasingRadius()
    {
        if (playerCollider is not null)
        {
            if (Vector2.Distance(transform.position, player.TruePosition) <= detectionRadius && CanSeePlayer())
            {
                return true;
            }
        }
        return false;
    }

    public bool CanSeePlayer()
    {
        var playerPos = playerCollider.transform.position + (Vector3)playerCollider.offset;
        Vector2 directionToPlayer = (player.TruePosition - (Vector2)selfCollider.transform.position).normalized;
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

    public virtual void MoveTowardsTarget(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + Speed * Time.deltaTime * direction);
    }

    protected void DestroyMyself()
    {
        Destroy(gameObject);
    }
}
