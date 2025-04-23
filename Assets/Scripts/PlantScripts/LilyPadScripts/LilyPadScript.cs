using System;
using System.Collections.Generic;
using UnityEngine;

public class LilyPad : Plant
{
    private static Dictionary<Vector2, LilyPad> bridges = new Dictionary<Vector2, LilyPad>();
    private float width = 1.95f;
    protected override void Awake()
    {
        base.Awake();
        bridges[transform.position] = this;
    }

    private static Vector2 GetDirection(Vector2 start, Vector2 finish)
    {
        Vector2 direction = finish - start;
        //Debug.Log($"{start} -> {finish}, {direction}");
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

    public override bool TryPlace(Vector2 position)
    {
        position.x = MathF.Round(position.x, 2);
        position.y = MathF.Round(position.y, 2);
        if (bridges.Count == 0)
        {
            if (base.IsAreaAvailable(position))
            {
                base.Place(position);
                return true;
            }
            return false;
        }
        else
        {
            var minDist = float.MaxValue;
            var bestPos = Vector2.zero;
            foreach (var pos in bridges.Keys)
            {
                var dist = (pos - position).magnitude;
                var diff = MathF.Abs(dist - width);
                if (dist < minDist)
                {
                    minDist = dist;
                    bestPos = pos;
                }
            }
            if (minDist <= width + 0.3f)
            {
                var direction = GetDirection(bestPos, position);
                position = bestPos + direction * width;
            }
            position.x = MathF.Round(position.x, 2);
            position.y = MathF.Round(position.y, 2);
            if (base.IsAreaAvailable(position) && !bridges.ContainsKey(position))
            {
                base.Place(position);
                return true;
            }
            return false;
        }
    }
}
