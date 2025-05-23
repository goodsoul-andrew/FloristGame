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
    [SerializeField] private GameObject enemy;


    [SerializeField] private PlaySoundsScript soundPlayer;
    private System.Random rnd = new System.Random();

    [SerializeField] private float attackTimeout;
    private BossPhase phase1;
    protected override void Start()
    {
        bossbar.SetActive(false);
        soundPlayer = (soundPlayer == null) ? GetComponent<PlaySoundsScript>() : soundPlayer;
        player = FindFirstObjectByType<Player>();
        phase1 = new BossPhase(attackTimeout, new List<Action> { RootPlayer });

        HP.OnDeath += Die;
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

    public void SpawnEnemies()
    {
        for (var i = 0; i < 2; i++)
        {
            var r = 2f;
            var pos = UnityEngine.Random.insideUnitCircle * r;
            Instantiate(enemy, (Vector2)transform.position + pos, Quaternion.Euler(0, 0, 0));
        }
    }

    public void Attack(List<Action> attackPool)
    {
        var ind = rnd.Next(attackPool.Count);
        attackPool[ind]();
    }

    void Die()
    {
        soundPlayer.StopLoopedSound();
        soundPlayer.PlaySound("Death");
        bossbar.SetActive(false);
        
    }
}