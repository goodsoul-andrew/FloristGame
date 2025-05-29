using UnityEngine;


class BossAwaker : MonoBehaviour
{
    [SerializeField] private Boss boss;
    void OnTriggerEnter2D(Collider2D collision)
    {
        boss.StartFight();
    }
}