using System;
using UnityEngine;

public class LilyPad : Plant
{
    protected override void Start()
    {
        base.Start();
        TutorialScript.FinishTutorial("build");
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
}
