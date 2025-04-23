using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DialogueManagerScript : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private Animator animator;

    private Queue<string> sentences = new();

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        animator.SetBool("IsOpen",true);
        nameText.text = dialogue.Name;

        foreach(string sentence in dialogue.Sentences)
            sentences.Enqueue(sentence);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        var sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(var letter in sentence)
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        animator.SetBool("IsOpen",false);
    }
}