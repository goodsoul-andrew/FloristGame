using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IMoving, IDamageable
{
    public GameObject mainObject;
    [SerializeField] private float speed = 3f;
    [SerializeField] protected float detectionRadius = 10f;
    public DamageDealer damageDealer;
    public float Speed {get; set;}
    public Health HP { get; set; }
    [SerializeField] private Health hp;
    protected Rigidbody2D rb;
    public Collider2D playerCollider {get; private set;}
 
    public Player player;
    protected Collider2D selfCollider;
    private IEnemyState currentState;

    public readonly WanderingState wanderingState = new WanderingState();
    public readonly ChasingState chasingState = new ChasingState();


    void Awake()
    {
        if(mainObject==null)mainObject = this.gameObject;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>();
        selfCollider = GetComponent<Collider2D>();

        HP = (hp==null)? mainObject.GetComponent<Health>(): hp;
        HP.OnDeath += DestroyMyself;
    }


    protected virtual void Start()
    {
        Speed = speed;
        rb = mainObject.GetComponent<Rigidbody2D>();

        damageDealer.Friends.Add("Enemy");

        ChangeState(wanderingState);
    }

    public void ChangeState(IEnemyState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }

    protected virtual void FixedUpdate()
    {
        currentState?.Update(this);
    }

    public bool IsPlayerInChasingRadius()
    {
        if (player is not null)
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
        Vector2 directionToPlayer = (player.TruePosition - (Vector2)selfCollider.transform.position).normalized;
        var r = selfCollider.transform.localScale.x + 0.1f; // selfCollider.radius * 
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
        for (int i = mainObject.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = mainObject.transform.GetChild(i);
            Destroy(child.gameObject);
        }
        Destroy(mainObject.gameObject);
    }
}
