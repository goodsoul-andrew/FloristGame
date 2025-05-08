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

    public void Detonate()
    {
        StartCoroutine(SetActiveAfterDelay(0.01f,true));
        StartCoroutine(SetActiveAfterDelay(0.25f,true));
        DestroyAfterDelay(0.9f);
    }

    private IEnumerator SetActiveAfterDelay(float delay,bool active)
    {
        yield return new WaitForSeconds(delay);
        damageDealer.Enabled = active;
        hitBox.SetActive(active);
    }
}
