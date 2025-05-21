using System;
using UnityEngine;

public class LilyPadBridgeBody : MonoBehaviour
{
    private Player player;
    private int swampLayer;
    private int playerLayer;
    public float Width {get; private set;}
    

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        swampLayer = LayerMask.NameToLayer("Swamp");
        playerLayer = LayerMask.NameToLayer("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Player entered LilyPad");
            Physics2D.IgnoreLayerCollision(swampLayer, playerLayer, true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Player exited LilyPad");
            Physics2D.IgnoreLayerCollision(swampLayer, playerLayer, false);
        }
        if (Utils.StandsOnGround(player.gameObject))
        {
            //Debug.Log("Player is on other LilyPad");
            Physics2D.IgnoreLayerCollision(swampLayer, playerLayer, true);
        }
        //Debug.Log($"player is on LilyPad: {player.CheckIfOnLilyPad()}");
    }
}
