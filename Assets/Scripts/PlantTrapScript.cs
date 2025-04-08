using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTrap: MonoBehaviour
{
    private Animator animator;
    private bool isOpen;
    private Plant plant;
    private DamageDealer damageDealer;

    void Start()
    {
        animator = GetComponent<Animator>();
        damageDealer = GetComponent<DamageDealer>();
        plant = GetComponent<Plant>();
        damageDealer.Friends.Add("Player");
        damageDealer.Friends.Add("PlayerMinion");
        isOpen = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageDealer.Friends.Contains(collision.gameObject.tag)) return;
        if (isOpen)
        {
            isOpen = false;
            animator.SetBool("isOpen", false);
            Debug.Log("Close trap");
            plant.DestroyAfterDelay(0.25f);
        }
    }
}
