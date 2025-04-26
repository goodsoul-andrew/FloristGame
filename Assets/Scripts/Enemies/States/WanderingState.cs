using UnityEngine;

public class WanderingState : IEnemyState
{
    private Vector2 targetPosition;
    public Vector2 Center;
    private float wanderRadius = 3f;

    private float timer;
    public float MoveDuration = 1.5f;

    public void Enter(Enemy enemy)
    {
        Center = enemy.transform.position;
        timer = 0;
        SetNewTarget(enemy);
    }

    public void Update(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (enemy.IsPlayerInChasingRadius() && enemy.CanSeePlayer())
        {
            enemy.ChangeState(enemy.chasingState);
            return;
        }
        if (Vector2.Distance(enemy.transform.position, targetPosition) < 0.1f)
        {
            SetNewTarget(enemy);
        }
        else if (timer > MoveDuration)
        {
            SetNewTarget(enemy);
            timer = 0;
        }
        enemy.MoveTowardsTarget(targetPosition);
    }

    private void SetNewTarget(Enemy enemy)
    {
        targetPosition = Center + Random.insideUnitCircle * wanderRadius;
    }

    public void Exit(Enemy enemy)
    {

    }
}