using UnityEngine;

class BossFlower : MonoBehaviour
{
    private Player player;
    [SerializeField] private CutsceneController cutscene;

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        cutscene = FindFirstObjectByType<CutsceneController>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        player.isDead = true;
        player.isPaused = true;
        player.Hp.IsImmortal = true;
        cutscene.animator.SetTrigger("Win");
    }
}