using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamageDealer : MonoBehaviour
{
    public float damageAmount;
    public float Timeout = 1f;
    public HashSet<string> friends = new HashSet<string>();
    private Collider2D selfCollider;
    private bool _enabled = true;
    public bool Enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            selfCollider.enabled = _enabled;
        }
    }

    private List<Collider2D> collidersInTrigger = new List<Collider2D>();
    private Coroutine damageCoroutine;

    void Start()
    {
        selfCollider = GetComponent<Collider2D>();
    }

    private void DealDamage(Collider2D collider)
    {
        if (friends.Contains(collider.tag)) return;
        if (collider.TryGetComponent<Health>(out var health))
        {
            health.TakeDamage(damageAmount);
        }
    }

    private IEnumerator DamageOverTime()
    {
        while (Enabled)
        {
            foreach (var collider in collidersInTrigger)
            {
                DealDamage(collider);
            }
            yield return new WaitForSeconds(Timeout);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Enabled)
        {
            collidersInTrigger.Add(collision);
            damageCoroutine ??= StartCoroutine(DamageOverTime());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (Enabled)
        {
            collidersInTrigger.Remove(collision);
            if (collidersInTrigger.Count == 0 && damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }
}
