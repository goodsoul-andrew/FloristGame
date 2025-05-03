using UnityEngine;

public class SpawningState : IEnemyState
{
    private float time;
    public void Enter(Enemy enemy)
    {
        time = 0;
        if (enemy is SpawnerEnemy spawner)
        {
            spawner.animator.SetBool("active", true);
        }
    }

    public void Exit(Enemy enemy)
    {
        time = 0;
    }

    public void Update(Enemy enemy)
    {
        if (!(enemy.IsPlayerInChasingRadius() && enemy.CanSeePlayer()))
        {
            if (enemy is SpawnerEnemy spawner)
            {
                spawner.ChangeState(spawner.idleState);
                return;
            }
        }
        if (enemy is SpawnerEnemy spawnerEnemy)
        {
            time += Time.deltaTime;
            if (time >= spawnerEnemy.timeout)
            {
                spawnerEnemy.SpawnEnemies();
                time = 0;
            }
        }
    }
}