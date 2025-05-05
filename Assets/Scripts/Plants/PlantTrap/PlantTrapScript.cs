using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;

public class PlantTrap : Plant
{
    private Animator animator;
    private bool isOpen;
    private DamageDealer damageDealer;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        damageDealer = GetComponent<DamageDealer>();
        var friends = new string[] { "Player", "PlayerMinion", "PlantTrap", "LilyPad" };
        damageDealer.Friends.AddRange(friends);
        isOpen = true;
        //obstacles = new string[] { "Obstacle", "PlayerMinion", "PlantTrap" };
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (! damageDealer.Friends.Contains(collision.tag))
        {
            //Debug.Log($"{collision.tag} {string.Join(", ", damageDealer.Friends)} {damageDealer.Friends.Contains(collision.tag)}");
            if (isOpen)
            {
                isOpen = false;
                animator.SetBool("isOpen", false);
                //Debug.Log("Close trap");
                DestroyAfterDelay(0.8f);
                FindFirstObjectByType<TutorialManager>().FinishTutorial("fight");
            }
        }
    }

    public override bool IsAreaAvailable(Vector2 position)
    {
        var obstacles = new string[] {"Obstacle", "PlayerMinion", "PlantTrap"};
        Collider2D[] colliders = GetCollidersInArea(position);
        //Debug.Log($"{string.Join(", ", obstacles)}");
        foreach (var collider in colliders)
        {
            //Debug.Log($"{collider.tag} {obstacles.Contains(collider.tag)}");
            if (obstacles.Contains(collider.tag))
            {
                return false;
            }
        }
        return true;
    }
}
