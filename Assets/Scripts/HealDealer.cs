using System;
using System.Collections.Generic;
using UnityEngine;

public class HealDealer : MonoBehaviour
{
    public float healAmount;
    public HashSet<string> Friends = new HashSet<string>();
    public bool Enabled = true;

    private void Heal(GameObject target)
    {
        if (!Enabled) return;
        if (!Friends.Contains(target.tag)) return;
        if (target.TryGetComponent<Health>(out var health))
        {
            health.Heal(healAmount);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Heal(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Heal(collision.gameObject);
    }
}
