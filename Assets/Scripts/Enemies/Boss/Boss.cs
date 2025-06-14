using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class Boss : Enemy
{
    public bool isAwaken { get; private set; }
    private System.Random rnd = new System.Random();
    [SerializeField] private GameObject bossbar;
    [SerializeField] private Root rootAttack;
    [SerializeField] private Ball acidBall;
    [SerializeField] private Ball spawnerBall;
    [SerializeField]private Ball enemyBall;
    [SerializeField] private CollectibleFlower drop;
    [SerializeField] public PlaySoundsScript soundPlayer;
    [SerializeField] private float attackTimeout;
    [SerializeField] private Animator animator;

    private BossPhase phase1;
    private BossPhase2 phase2;
    protected override void Start()
    {
        bossbar.SetActive(false);
        Hp.IsImmortal = true;
        soundPlayer = (soundPlayer == null) ? GetComponent<PlaySoundsScript>() : soundPlayer;
        player = FindFirstObjectByType<Player>();

        var phase2Attacks = new List<WeightedAttack>
        {
            new WeightedAttack(RootPlayer, 35),
            new WeightedAttack(AcidAttack, 40),
            new WeightedAttack(() => RandomRootField(40, 10f), 10),
            new WeightedAttack(EnemyAttack, 10),
            new WeightedAttack(SpawnerAttack, 5)
        };
        phase2 = new BossPhase2(attackTimeout * 0.8f, phase2Attacks);

        var phase1Attacks = new List<WeightedAttack>
        {
            new WeightedAttack(RootPlayer, 40),
            new WeightedAttack(AcidAttack, 40),
            new WeightedAttack(EnemyAttack, 19),
            new WeightedAttack(SpawnerAttack, 1)
        };
        phase1 = new BossPhase(attackTimeout, phase1Attacks, phase2, Hp.maxHP / 2);

        Hp.OnDeath += Die;
    }

    void Update()
    {
        if (!isAwaken) return;
        if (player.TruePosition.x > transform.position.x)
        {
            animator.SetBool("LooksRight", true);
        }
        else
        {
            animator.SetBool("LooksRight", false);
        }
    }

    public void StartFight()
    {
        if (isAwaken) return;

        isAwaken = true;
        bossbar.SetActive(true);
        Roar();
        soundPlayer.PlayLoopedSound("Music");
        Hp.IsImmortal = false;
        ChangeState(phase1);
    }

    public void Roar()
    {
        soundPlayer.PlaySound("Roar");
    }

    void SpawnRoot(Vector2 position)
    {
        Instantiate(rootAttack, position, Quaternion.Euler(0, 0, 0));
    }

    void RootPlayer()
    {
        SpawnRoot(player.TruePosition);
    }

    void AcidAttack()
    {
        animator.SetTrigger("AcidAttack");
        StartCoroutine(DelayedAcidBallSpawn(acidBall));
    }
    void SpawnerAttack()
    {
        animator.SetTrigger("AcidAttack");
        StartCoroutine(DelayedAcidBallSpawn(spawnerBall));
    }

    void EnemyAttack()
    {
        animator.SetTrigger("AcidAttack");
        StartCoroutine(DelayedAcidBallSpawn(enemyBall));
    }

    void RandomRootField(int count, float radius)
    {
        for (var i = 0; i < count; i++)
        {
            var pos = (Vector2)selfCollider.transform.position + radius * UnityEngine.Random.insideUnitCircle;
            SpawnRoot(pos);
        }
    }

    IEnumerator DelayedAcidBallSpawn(Ball ball)
    {
        yield return new WaitForSeconds(0.2f);

        var pos = (Vector2)transform.position + 2.5f * Vector2.up;
        ball.startPosition = pos;
        ball.Destination = player.TruePosition;
        Instantiate(ball, pos, Quaternion.Euler(0, 0, 0));
    }

    public void CreateSpawners()
    {
        var ball = spawnerBall;
        var pos = (Vector2)transform.position + 5f * Vector2.right;
        ball.startPosition = pos;
        ball.Destination = pos;
        Instantiate(ball, pos, Quaternion.Euler(0, 0, 0));
        pos = (Vector2)transform.position + 5f * Vector2.left;
        ball.startPosition = pos;
        ball.Destination = pos;
        Instantiate(ball, pos, Quaternion.Euler(0, 0, 0));
    }

    public void RandomAttack(List<WeightedAttack> attackPool)
    {
        Debug.Log($"Auddio source enabled : {soundPlayer.audioSource.enabled}");
        var totalWeight = attackPool.Select(el => el.Weight).Sum();
        var ind = rnd.Next(totalWeight);
        var currentWeight = 0;
        foreach (var weightedAttack in attackPool)
        {
            currentWeight += weightedAttack.Weight;
            if (ind < currentWeight)
            {
                weightedAttack.Attack?.Invoke();
                return;
            }
        }
    }

    void Die()
    {
        soundPlayer.StopLoopedSound();
        soundPlayer.PlaySound("Death");
        bossbar.SetActive(false);
        Instantiate(drop, Hp.transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(animator.gameObject);
        Destroy(Hp.gameObject);
        Destroy(gameObject);
    }
}

[Serializable]
public class WeightedAttack
{
    public Action Attack;
    [Range(0, 100)]
    public int Weight = 50;

    public WeightedAttack(Action atk, int w)
    {
        Attack = atk;
        Weight = w;
    }
}