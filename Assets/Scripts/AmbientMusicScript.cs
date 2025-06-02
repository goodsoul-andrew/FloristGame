using UnityEngine;

public class AmbientMusic : MonoBehaviour
{
    private PlaySoundsScript soundPlayer;
    [SerializeField] private bool playOnce;
    void Start()
    {
        soundPlayer = GetComponent<PlaySoundsScript>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playOnce)
            {
                soundPlayer.PlaySound(0);
            }
            else
            {
                soundPlayer.PlayLoopedSound(0);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            soundPlayer.StopSound();
        }
    }
}
