using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;

public class PlantBomb : Plant
{
    private Animator animator;
    [SerializeField] private DamageDealer damageDealer;
    [SerializeField] private GameObject hitBox;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        var friends = new string[] { "Player", "PlayerMinion", "PlantTrap", "LilyPad", "PlantBomb"};
        damageDealer.Friends.AddRange(friends);
        animator.SetTrigger("StartDetonation");
    }
}
