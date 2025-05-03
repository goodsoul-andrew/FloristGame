using UnityEngine;

public class ChasingState : IEnemyState
{
    private float timer;
    public float ChaseDuration = float.PositiveInfinity;
    public bool ForgetTarget = true;

    public void Enter(Enemy enemy)
    {
        timer = 0;
    }

    public void Exit(Enemy enemy)
    {
        
    }

    public void Update(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (!(enemy.IsPlayerInChasingRadius() && enemy.CanSeePlayer()))
        {
            if (ForgetTarget || timer >= ChaseDuration)
            {
                enemy.ChangeState(enemy.wanderingState);
                return;
            }
        }
        enemy.MoveTowardsTarget(enemy.player.TruePosition);
    }
}