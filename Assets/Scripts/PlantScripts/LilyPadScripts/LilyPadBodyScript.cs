using System;
using UnityEngine;

public class LilyPadBody : MonoBehaviour
{
    private Player player;
    private int waterLayer;
    private int playerLayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        waterLayer = LayerMask.NameToLayer("Water");
        playerLayer = LayerMask.NameToLayer("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Player entered LilyPad");
            Physics2D.IgnoreLayerCollision(waterLayer, playerLayer, true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Player exited LilyPad");
            Physics2D.IgnoreLayerCollision(waterLayer, playerLayer, false);
        }
        if (player.CheckIfOnLilyPad())
        {
            //Debug.Log("Player is on other LilyPad");
            Physics2D.IgnoreLayerCollision(waterLayer, playerLayer, true);
        }
        //Debug.Log(player.CheckIfOnLilyPad());
    }
}