using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class TutorialScript
{
    public static Dictionary<string,GameObject> dictOfTutorialsObjects = new();

    private static bool movingUp = false;
    private static bool movingDown = false;

    private static Vector2 screenPosition = new Vector2(-600,-270);
    private static Vector2 outOfScreenPosition = new Vector2(-600,-950);

    private static HashSet<string> passedTutorials = new();
    private static Queue<string> tutorialsQueue = new();
    private static bool hasTutorial = false;
    private static string currentTypeOfTutorial ="";
    private static RectTransform transform;

    public static void AddTutorialToTheQueue(string typeOfTutorial)
    {
        if(dictOfTutorialsObjects.ContainsKey(typeOfTutorial) && !passedTutorials.Contains(typeOfTutorial))
        {
            passedTutorials.Add(typeOfTutorial);
            tutorialsQueue.Enqueue(typeOfTutorial);
        }
    } 

    public static void FinishTutorial(string typeOfTutorial)
    {
        if(hasTutorial && !movingUp && currentTypeOfTutorial == typeOfTutorial && dictOfTutorialsObjects.TryGetValue(typeOfTutorial,out var tutorial))
        {
            transform = tutorial.GetComponent<RectTransform>();
            movingDown = true;
        }
    } 


    public static void Update()
    {
        if(!hasTutorial && tutorialsQueue.Count!=0)
        {
            var typeOfTutorial = tutorialsQueue.Dequeue();
            var tutorial = dictOfTutorialsObjects[typeOfTutorial];
            transform = tutorial.GetComponent<RectTransform>();
            movingUp = true;
            currentTypeOfTutorial = typeOfTutorial;
            hasTutorial = true;
        }

        if(movingUp)
        {
            transform.anchoredPosition = Vector2.Lerp(transform.anchoredPosition, screenPosition, Time.deltaTime * 3);
            if(Vector2.Distance(transform.anchoredPosition, screenPosition) < 16f)
            {
                movingUp = false;
            } 
        }
        if(movingDown)
        {
            transform.anchoredPosition = Vector2.Lerp(transform.anchoredPosition, outOfScreenPosition, Time.deltaTime * 3);
            if(Vector2.Distance(transform.anchoredPosition, outOfScreenPosition) < 16f) 
            {
                hasTutorial = false;
                movingDown = false;
            }
        }
    }
}
