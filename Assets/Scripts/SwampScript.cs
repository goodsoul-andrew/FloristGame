using System;
using System.Collections.Generic;
using UnityEngine;

public class Swamp : MonoBehaviour
{
    private SwampEffect swampEffect;
    private HashSet<GameObject> disabled;

    void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        disabled = new HashSet<GameObject>();
        swampEffect = new SwampEffect();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (! Utils.StandsOnGround(collision.gameObject))
        {
            Enable(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Disable(collision.gameObject);
    }

    public void Enable(GameObject target)
    {
        Debug.Log($"enable swamp for {target}, {disabled.Contains(target)} {Utils.StandsOn(target, "Swamp")}");
        swampEffect.ApplyEffect(target);
        
    }

    public void Disable(GameObject target)
    {
        Debug.Log($"disable swamp for {target}, {disabled.Contains(target)} {Utils.StandsOnGround(target)}");
        swampEffect.RemoveEffect(target);
    }

    public bool IsEnabledFor(GameObject target) => !disabled.Contains(target);
}

public class SwampEffect : StatusEffect
{
    public SwampEffect() : base("Slowness")
    {

    }


    public override void ActivateEffect(GameObject target)
    {
        if (target.TryGetComponent<IMoving>(out var moving))
        {
            moving.Speed = Math.Max(0, moving.Speed / 2);
        }
    }

    public override void CancelEffect(GameObject target)
    {
        if (target.TryGetComponent<IMoving>(out var moving))
        {
            moving.Speed *= 2;
        }
    }
}
