using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    public string EffectName { get; private set; }
    public float Duration { get; private set; }
    protected HashSet<GameObject> affected = new HashSet<GameObject>();

    public StatusEffect(string effectName, float duration)
    {
        EffectName = effectName;
        Duration = duration;
    }

    public StatusEffect(string effectName)
    {
        EffectName = effectName;
        Duration = float.PositiveInfinity;
    }

    public virtual void ApplyEffect(GameObject target)
    {
        if (! affected.Contains(target))
        {
            affected.Add(target);
            ActivateEffect(target);
        }
    }

    public abstract void ActivateEffect(GameObject target);

    public virtual void RemoveEffect(GameObject target)
    {
        if (affected.Contains(target))
        {
            affected.Remove(target);
            CancelEffect(target);
        }
    }

    public abstract void CancelEffect(GameObject target);
}
