using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DialigueTrigger : Interraction
{
    [SerializeField] private Dialogue dialogue;

    public override void StartInterraction()
    {
        FindFirstObjectByType<TutorialManager>().AddTutorialToTheQueue("dialogue");
        FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);
    }

    public override void EndInterraction()
    {
        FindFirstObjectByType<DialogueManager>().EndDialogue();
        Debug.Log("ended");
    }

}

[System.Serializable]
public class Dialogue
{
    public string Name;

    [TextArea(3,10)]
    public string[] Sentences;

}