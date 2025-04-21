using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class PlantTrap: Plant
{
    private Animator animator;
    private bool isOpen;
    private DamageDealer damageDealer;
    private readonly string[] friends = new string[] {"Player", "PlayerMinion", "PlantTrap", "LilyPad"};

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        damageDealer = GetComponent<DamageDealer>();
        damageDealer.Friends.AddRange(friends);
        isOpen = true;
        obstacles = new string[] {"Obstacle", "PlayerMinion", "PlantTrap"};
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageDealer.Friends.Contains(collision.gameObject.tag)) return;
        if (isOpen)
        {
            isOpen = false;
            animator.SetBool("isOpen", false);
            //Debug.Log("Close trap");
            DestroyAfterDelay(0.8f);
            TutorialScript.FinishTutorial("fight");
        }
    }
}
