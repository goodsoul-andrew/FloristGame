using UnityEngine;
using System;

public class SpawnerEnemy : Enemy
{
    public float timeout;
    [SerializeField] private int minCount;
    [SerializeField] private int maxCount;
    [SerializeField] private GameObject enemy;
    private System.Random intRnd;
    public Animator animator;

    public readonly SpawnerIdleState idleState = new SpawnerIdleState();
    public readonly SpawningState spawningState = new SpawningState();

    protected override void Start()
    {
        if (minCount > maxCount) throw new Exception("minCount must be <= maxCount");
        intRnd = new System.Random();
        
        animator = GetComponent<Animator>();
        
        ChangeState(idleState);
    }


    public void SpawnEnemies()
    {
        var cnt = intRnd.Next(minCount, maxCount + 1);
        for (var i = 0; i < cnt; i++)
        {
            var r = ((CircleCollider2D)selfCollider).radius;
            var pos = UnityEngine.Random.insideUnitCircle * r;
            Instantiate(enemy, (Vector2)transform.position + pos, Quaternion.Euler(0, 0, 0));
        }
    }
}
