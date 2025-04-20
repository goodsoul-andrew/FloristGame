using System;
using UnityEngine;

public class LilyPad : Plant
{
    private GameObject player;
    private int waterLayer;
    private int playerLayer;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        waterLayer = LayerMask.NameToLayer("Water");
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private static Vector2 GetDirection(Vector2 start, Vector2 finish)
    {
        Vector2 direction = finish - start;
        float absX = Mathf.Abs(direction.x); 
        float absY = Mathf.Abs(direction.y); 
        if (absX > absY)
        {
            return direction.x > 0 ? Vector2.right : Vector2.left; 
        }
        else
        {
            return direction.y > 0 ? Vector2.up : Vector2.down; 
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"collision with lily pad");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player entered LilyPad");
            Physics2D.IgnoreLayerCollision(waterLayer, playerLayer, true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player exited LilyPad");
            Physics2D.IgnoreLayerCollision(waterLayer, playerLayer, false);
        }
    }
}
