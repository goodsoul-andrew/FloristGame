using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DialigueTrigger : Interaction
{
    [SerializeField] private Dialogue dialogue;

    public override void StartInteraction()
    {
        FindFirstObjectByType<TutorialManager>().AddTutorialToTheQueue("dialogue");
        FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);
    }

    public override void EndInteraction()
    {
        FindFirstObjectByType<DialogueManager>().EndDialogue();
        //Debug.Log("ended");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Player>(out var player))
            player.InteractionEnter(this);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.TryGetComponent<Player>(out var player))
            player.InteractionExit(this);
    }

}

[System.Serializable]
public class Dialogue
{
    public string Name;

    [TextArea(3,10)]
    public string[] Sentences;

}