using UnityEngine;

public static class TutorialScript
{
    public static GameObject walkTutorial;
    public static GameObject placeTutorial;
    public static GameObject flowerChangingTutorial;
    public static GameObject fightingTutorial;
    public static GameObject buildingTutorial;

    public static void StartTutorial()
    {
        var transform = walkTutorial.GetComponent<RectTransform>();
        Debug.Log(transform.anchoredPosition);
    } 
}
