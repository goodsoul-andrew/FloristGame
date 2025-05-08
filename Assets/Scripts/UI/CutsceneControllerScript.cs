using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class CutsceneController : MonoBehaviour
{
    private Animator animator;
    private Player player;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindFirstObjectByType<Player>();
        player.ResumeGame();
        player.isPaused = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("EndCutscene");
        }
    }

    public void EndCutscene()
    {
        FindFirstObjectByType<TutorialManager>().AddTutorialToTheQueue("walk");
        FindFirstObjectByType<TutorialManager>().AddTutorialToTheQueue("place");
        FindFirstObjectByType<TutorialManager>().AddTutorialToTheQueue("change");
        player.ResumeGame();
        Destroy(this);
    }
}
