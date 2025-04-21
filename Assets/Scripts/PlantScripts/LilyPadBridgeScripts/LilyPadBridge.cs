using System;
using UnityEngine;

public class LilyPadBridge : Plant
{
    private LilyPadBridgeBody body;
    private float width = 1.95f;
    
    protected override void Start()
    {
        base.Start();
        foreach (GameObject child in transform)
        {
            if (child.TryGetComponent<LilyPadBridgeBody>(out var bridgeBody))
            {
                body = bridgeBody;
            }
        }
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

    public override GameObject Place(Vector2 position)
    {
        
        var neighbours = Physics2D.OverlapCircleAll(position, width);
        foreach (var neighbour in neighbours)
        {
            if (neighbour.TryGetComponent<LilyPadBridge>(out var bridge))
            {
                var direction = GetDirection(neighbour.transform.position, position);
                position += width * direction;
                break;
            }
        }
        return base.Place(position);
    }
}