using System;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damageAmount;
    public HashSet<string> Friends = new HashSet<string>();

    private void DealDamage(GameObject target)
    {
        Debug.Log($"Target is {target.name}");
        if (Friends.Contains(target.tag)) return;
        if (target.TryGetComponent<Health>(out var health))
        {
            health.TakeDamage(damageAmount);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        DealDamage(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        DealDamage(collision.gameObject);
    }
}
