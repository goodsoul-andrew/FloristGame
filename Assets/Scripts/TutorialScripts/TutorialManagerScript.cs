using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TutorialManagerScript : MonoBehaviour
{
    [SerializeField] private Tutorial[] tutorials;
    private Dictionary<string,GameObject> dictOfTutorialsObjects = new();

    private bool movingUp = false;
    private bool movingDown = false;

    private Vector2 screenPosition = new Vector2(-600,-270);
    private Vector2 outOfScreenPosition = new Vector2(-600,-950);

    private HashSet<string> passedTutorials = new();
    private Queue<string> tutorialsQueue = new();
    private bool hasTutorial = false;
    private string currentTypeOfTutorial ="";
    private RectTransform rectTransform;

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
        if(hasTutorial && !movingUp && currentTypeOfTutorial == typeOfTutorial && dictOfTutorialsObjects.TryGetValue(typeOfTutorial,out var tutorial))
        {
            rectTransform = tutorial.GetComponent<RectTransform>();
            movingDown = true;
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
        if(!hasTutorial && tutorialsQueue.Count!=0)
        {
            var typeOfTutorial = tutorialsQueue.Dequeue();
            var tutorial = dictOfTutorialsObjects[typeOfTutorial];
            rectTransform = tutorial.GetComponent<RectTransform>();
            movingUp = true;
            currentTypeOfTutorial = typeOfTutorial;
            hasTutorial = true;
        }

        if(movingUp)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, screenPosition, Time.deltaTime * 3);
            if(Vector2.Distance(rectTransform.anchoredPosition, screenPosition) < 16f)
            {
                movingUp = false;
            } 
        }
        if(movingDown)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, outOfScreenPosition, Time.deltaTime * 3);
            if(Vector2.Distance(rectTransform.anchoredPosition, outOfScreenPosition) < 16f) 
            {
                hasTutorial = false;
                movingDown = false;
            }
        }
    }
}
[System.Serializable]
public class Tutorial
{
    public string Name;

    public GameObject Object;

}