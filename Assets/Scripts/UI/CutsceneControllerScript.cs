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
        animator.SetTrigger("StartCutscene");
        StartCoroutine(StartEndDelay(21));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("EndCutscene");
            StartCoroutine(StartEndDelay(0));
        }
    }

    public IEnumerator StartEndDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        FindFirstObjectByType<TutorialManager>().AddTutorialToTheQueue("walk");
        FindFirstObjectByType<TutorialManager>().AddTutorialToTheQueue("place");
        FindFirstObjectByType<TutorialManager>().AddTutorialToTheQueue("change");
        player.ResumeGame();
        Destroy(this);
    }
}
