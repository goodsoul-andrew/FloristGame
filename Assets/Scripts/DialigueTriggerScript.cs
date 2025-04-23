using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DialigueTriggerScript : Interraction
{
    [SerializeField] private Dialogue dialogue;

    public override void Interract()
    {
        FindFirstObjectByType<DialogueManagerScript>().StartDialogue(dialogue);
    }

}

[System.Serializable]
public class Dialogue
{
    public string Name;

    [TextArea(3,10)]
    public string[] Sentences;

}