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
        TutorialScript.FinishTutorial("build");
    }
}
