using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;

class Boss : Enemy
{
    public bool isAwaken { get; private set; }
    private System.Random rnd = new System.Random();
    [SerializeField] private GameObject bossbar;
    [SerializeField] private Root rootAttack;
    [SerializeField] private AcidBall acidBall;
    [SerializeField] private PlaySoundsScript soundPlayer;
    [SerializeField] private float attackTimeout;
    [SerializeField]private Animator animator;

    private BossPhase phase1;
    protected override void Start()
    {
        bossbar.SetActive(false);
        soundPlayer = (soundPlayer == null) ? GetComponent<PlaySoundsScript>() : soundPlayer;
        player = FindFirstObjectByType<Player>();

        var phase1Attacks = new List<WeightedAttack>
        {
            new WeightedAttack(RootPlayer, 50),
            new WeightedAttack(AcidAttack, 50)
        };
        phase1 = new BossPhase(attackTimeout, phase1Attacks);

        HP.OnDeath += Die;
    }

    public void StartFight()
    {
        if (isAwaken) return;

        isAwaken = true;
        bossbar.SetActive(true);
        soundPlayer.PlaySound("Roar");
        soundPlayer.PlayLoopedSound("Music");
        ChangeState(phase1);
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
        StartCoroutine(DelayedAcidBallSpawn());
    }

    IEnumerator DelayedAcidBallSpawn()
    {
        yield return new WaitForSeconds(0.2f);

        var pos = (Vector2)transform.position + 2.5f * Vector2.up;
        acidBall.startPosition = pos;
        acidBall.Destination = player.TruePosition;
        Instantiate(acidBall, pos, Quaternion.Euler(0, 0, 0));
    }

    public void RandomAttack(List<WeightedAttack> attackPool)
    {
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