using System;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase : IEnemyState
{
    private float timer;
    public float attackTimeout;
    public List<WeightedAttack> attackPool;
    public BossPhase NextPhase;
    public float HpToChangePhase;

    public BossPhase(float attackTimeout, List<WeightedAttack> attackPool)
    {
        this.attackTimeout = attackTimeout;
        this.attackPool = attackPool;
    }

    public BossPhase(float attackTimeout, List<WeightedAttack> attackPool, BossPhase nextPhase, float nextHp)
    {
        this.attackTimeout = attackTimeout;
        this.attackPool = attackPool;
        this.NextPhase = nextPhase;
        this.HpToChangePhase = nextHp;
    }

    public virtual void Enter(Enemy enemy)
    {
        timer = 0;
        var boss = enemy as Boss;
    }

    public void Exit(Enemy enemy)
    {

    }

    public void Update(Enemy enemy)
    {
        var boss = enemy as Boss;

        timer += Time.deltaTime;
        if (timer >= attackTimeout)
        {
            boss.RandomAttack(attackPool);
            timer = 0;
        }
        if (NextPhase != null)
        {
            if (boss.Hp.HP <= HpToChangePhase)
            {
                boss.ChangeState(NextPhase);
            }
        }
    }
}

class BossPhase2 : BossPhase
{
    public BossPhase2(float attackTimeout, List<WeightedAttack> attackPool) : base(attackTimeout, attackPool)
    {

    }

    public override void Enter(Enemy enemy)
    {
        base.Enter(enemy);
        var boss = (Boss)enemy;
        boss.Roar();
        boss.CreateSpawners();
    }
}