using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class CutsceneController : MonoBehaviour
{
    public Animator animator;
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
        FindFirstObjectByType<TutorialManager>().AddTutorialToTheQueue("take");
        player.ResumeGame();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void RestartScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
