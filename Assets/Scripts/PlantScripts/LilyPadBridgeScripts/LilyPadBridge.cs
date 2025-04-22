using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LilyPadBridge : Plant
{
    [SerializeField] private GameObject bodyObject;
    private LilyPadBridgeBody body;
    [SerializeField] private GameObject edgeUpObject;
    private LilyPadBridgeEdge edgeUp;
    [SerializeField] private GameObject edgeDownObject;
    private LilyPadBridgeEdge edgeDown;
    [SerializeField] private GameObject edgeLeftObject;
    private LilyPadBridgeEdge edgeLeft;
    [SerializeField] private GameObject edgeRightObject;
    private LilyPadBridgeEdge edgeRight;
    //private static HashSet<Vector2> positions = new HashSet<Vector2>();
    private static Dictionary<Vector2, LilyPadBridge> bridges = new Dictionary<Vector2, LilyPadBridge>();
    private float width = 1.95f;


    protected override void Awake()
    {
        base.Awake();
        body = bodyObject.GetComponent<LilyPadBridgeBody>();
        edgeUp = edgeUpObject.GetComponent<LilyPadBridgeEdge>();
        edgeDown = edgeDownObject.GetComponent<LilyPadBridgeEdge>();
        edgeRight = edgeRightObject.GetComponent<LilyPadBridgeEdge>();
        edgeLeft = edgeLeftObject.GetComponent<LilyPadBridgeEdge>();
    }

    protected void Start()
    {
        //Debug.Log("starting LilyPadBridge");
        //positions.Add(transform.position);
        bridges[transform.position] = this;
        //Debug.Log(positions.Count);
        SetUp();
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

    private void SetUp()
    {
        //Debug.Log("entering SetUp");
        var position = (Vector2)this.transform.position;
        foreach (var pos in bridges.Keys)
        {
            if (pos == position) continue;
            var dist = (pos - position).magnitude;
            if (MathF.Abs(dist - width) <= 0.1)
            {
                var neighbour = bridges[pos];
                var dir = GetDirection(position, pos);
                //Debug.Log($"neighbour at {pos} is {bridges[pos]}, direction is {dir}");
                if (dir.x == 0 && dir.y == 1) // сосед сверху
                {
                    //Debug.Log("neighbour up");
                    this.edgeUp.ChangeAndBlock(false);
                    neighbour.edgeDown.ChangeAndBlock(false);
                }
                else if (dir.x == 0 && dir.y == -1) // сосед снизу
                {
                    //Debug.Log("neighbour down");
                    neighbour.edgeUp.ChangeAndBlock(false);
                    this.edgeDown.ChangeAndBlock(false);
                }
                else if (dir.x == 1 && dir.y == 0) // сосед справа
                {
                    //Debug.Log("neighbour right");
                    this.edgeRight.ChangeAndBlock(false);
                    neighbour.edgeLeft.ChangeAndBlock(false);
                }
                else if (dir.x == -1 && dir.y == 0) // сосед слева
                {
                    //Debug.Log("neighbour left");
                    neighbour.edgeRight.ChangeAndBlock(false);
                    this.edgeLeft.ChangeAndBlock(false);
                }
            }
        }
    }
}