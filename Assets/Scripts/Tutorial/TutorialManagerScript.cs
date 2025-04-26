using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Tutorial[] tutorials;

    [SerializeField] private Animator animator;
    private Dictionary<string,GameObject> dictOfTutorialsObjects = new();

    private Vector2 screenPosition = new Vector2(-730,-270);
    private Vector2 outOfScreenPosition = new Vector2(-730,-950);

    private HashSet<string> passedTutorials = new();
    private Queue<string> tutorialsQueue = new();
    private bool hasTutorial = false;
    private bool moving = false;
    private string currentTypeOfTutorial ="";

    public void AddTutorialToTheQueue(string typeOfTutorial)
    {
        if(dictOfTutorialsObjects.ContainsKey(typeOfTutorial) && !passedTutorials.Contains(typeOfTutorial))
        {
            passedTutorials.Add(typeOfTutorial);
            tutorialsQueue.Enqueue(typeOfTutorial);
        }
    } 

    public void FinishTutorial(string typeOfTutorial)
    {
        if(hasTutorial && !moving && currentTypeOfTutorial == typeOfTutorial && dictOfTutorialsObjects.TryGetValue(typeOfTutorial,out var tutorial))
        {
            animator.SetBool("IsUp",false);
            StartCoroutine(SetActiveAfterDelay(0.3f,false));
        }
    } 

    void Start()
    {
        foreach(var tutorial in tutorials)
        {
            dictOfTutorialsObjects[tutorial.Name] = tutorial.Object;
        }
    }

    void Update()
    {
        if(!hasTutorial && !moving && tutorialsQueue.Count!=0)
        {
            currentTypeOfTutorial = tutorialsQueue.Dequeue();
            animator.SetBool("IsUp",true);
            dictOfTutorialsObjects[currentTypeOfTutorial].SetActive(true);
            StartCoroutine(SetActiveAfterDelay(0.3f,true));
        }
    }

    private IEnumerator SetActiveAfterDelay(float delay,bool active)
    {
        moving = true;
        yield return new WaitForSeconds(delay);
        dictOfTutorialsObjects[currentTypeOfTutorial].SetActive(active);
        moving = false;
        hasTutorial = active;
    }
}
[System.Serializable]
public class Tutorial
{
    public string Name;

    public GameObject Object;

}