using UnityEngine;

public class SpawnerIdleState : IEnemyState
{
    public void Enter(Enemy enemy)
    {
        if (enemy is SpawnerEnemy spawner)
        {
            spawner.animator.SetBool("active", false);
        }
    }

    public void Exit(Enemy enemy)
    {
        
    }

    public void Update(Enemy enemy)
    {
        Debug.Log($"Update SpawnerIdleState {enemy.IsPlayerInChasingRadius()} {enemy.CanSeePlayer()}");
        if (enemy.IsPlayerInChasingRadius() && enemy.CanSeePlayer())
        {
            Debug.Log(enemy);
            if (enemy is SpawnerEnemy spawner)
            {
                spawner.ChangeState(spawner.spawningState);
            }
            return;
        }

    }
    
}