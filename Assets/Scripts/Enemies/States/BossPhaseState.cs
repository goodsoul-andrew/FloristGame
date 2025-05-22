using System;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase : IEnemyState
{
    private float timer;
    public float attackTimeout;
    public List<Action> attackPool;

    public BossPhase(float attackTimeout, List<Action> attackPool)
    {
        this.attackTimeout = attackTimeout;
        this.attackPool = attackPool;
    }

    public void Enter(Enemy enemy)
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
            boss.Attack(attackPool);
            timer = 0;
        }
    }
}