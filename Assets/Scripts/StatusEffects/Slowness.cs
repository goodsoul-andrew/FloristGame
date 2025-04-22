using System;
using UnityEngine;

public class Slowness : StatusEffect
{
    private float slowAmount;

    public Slowness(float duration, float slowAmount) : base("Slowness", duration)
    {
        this.slowAmount = slowAmount;
    }

    public Slowness(float slowAmount) : base("Slowness")
    {
        this.slowAmount = slowAmount;
    }

    public override void ActivateEffect(GameObject target)
    {
        if (target.TryGetComponent<IMoving>(out var moving))
        {
            moving.Speed = Math.Max(0, moving.Speed - slowAmount);
        }
    }

    public override void CancelEffect(GameObject target)
    {
        if (target.TryGetComponent<IMoving>(out var moving))
        {
            moving.Speed += slowAmount;
        }
    }
}