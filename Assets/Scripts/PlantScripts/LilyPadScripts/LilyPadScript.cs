using System;
using UnityEngine;

public class LilyPad : Plant
{
    protected override void Awake()
    {
        base.Awake();
        TutorialScript.FinishTutorial("build");
    }
}
