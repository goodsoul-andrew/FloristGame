using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class PlantTrap: Plant
{
    private Animator animator;
    private bool isOpen;
    private DamageDealer damageDealer;
    private CapsuleCollider2D selfCollider;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        damageDealer = GetComponent<DamageDealer>();
        damageDealer.Friends.Add("Player");
        damageDealer.Friends.Add("PlayerMinion");
        damageDealer.Friends.Add("lilyPad");
        selfCollider = GetComponent<CapsuleCollider2D>();
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
            DestroyAfterDelay(0.8f);
            TutorialScript.FinishTutorial("fight");
        }
    }
}
