using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

class Boss : Enemy
{
    public bool isAwaken { get; private set; }
    [SerializeField] private GameObject bossbar;
    [SerializeField] private GameObject rootAttack;


    private PlaySoundsScript soundPlayer;
    private System.Random rnd = new System.Random();

    [SerializeField] private float attackTimeout;
    private BossPhase phase1;
    protected override void Start()
    {
        bossbar.SetActive(false);
        soundPlayer = GetComponent<PlaySoundsScript>();
        player = FindFirstObjectByType<Player>();
        phase1 = new BossPhase(attackTimeout, new List<Action> { RootPlayer });
    }

    public void StartFight()
    {
        if (isAwaken) return;

        isAwaken = true;
        bossbar.SetActive(true);
        soundPlayer.PlaySound("Roar");
        soundPlayer.PlayLoopedSound("Music");
        ChangeState(phase1);
    }

    void SpawnRoot(Vector2 position)
    {
        Instantiate(rootAttack, position, Quaternion.Euler(0, 0, 0));
    }

    void RootPlayer()
    {
        SpawnRoot(player.TruePosition);
    }

    public void Attack(List<Action> attackPool)
    {
        var ind = rnd.Next(attackPool.Count);
        attackPool[ind]();
    }
}