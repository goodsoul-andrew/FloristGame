using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swamp : MonoBehaviour
{
    private SwampEffect swampEffect;
    private HashSet<GameObject> disabled;

    void Awake()
    {

    }

    void Start()
    {
        disabled = new HashSet<GameObject>();
        swampEffect = new SwampEffect(this, 2f, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Utils.StandsOnGround(collision.gameObject))
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
    public readonly float damageTimeout;
    public readonly float damageAmount;
    private Coroutine damageCoroutine;
    private Swamp swamp;

    public SwampEffect(Swamp sw, float dmgTimeout, float dmgAmount) : base("Slowness")
    {
        swamp = sw;
        damageTimeout = dmgTimeout;
        damageAmount = dmgAmount;
    }

    public override void ActivateEffect(GameObject target)
    {
        if (target.TryGetComponent<IMoving>(out var moving))
        {
            moving.Speed = Math.Max(0, moving.Speed / 2);
        }
        damageCoroutine = target.GetComponent<MonoBehaviour>().StartCoroutine(DealDamage(target));
        Debug.Log($"Swamp effect activated for {target}");
    }

    public override void CancelEffect(GameObject target)
    {
        if (target.TryGetComponent<IMoving>(out var moving))
        {
            moving.Speed *= 2;
        }
        if (damageCoroutine != null)
        {
            target.GetComponent<MonoBehaviour>().StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
        Debug.Log($"Swamp effect cancelled for {target}");
    }

    IEnumerator DealDamage(GameObject target)
    {
        while (affected.Contains(target))
        {
            if (target.TryGetComponent<Health>(out var health))
            {
                Debug.Log($"Swamp Effect damaged {target}");
                health.TakeDamage(damageAmount);
            }
            yield return new WaitForSeconds(damageTimeout);
        }
    }
}
