using UnityEngine;

public class TutorialSetterScript : MonoBehaviour
{
    [SerializeField] private GameObject walkTutorial;
    [SerializeField] private GameObject placeTutorial;
    [SerializeField] private GameObject flowerChangingTutorial;
    [SerializeField] private GameObject fightingTutorial;
    [SerializeField] private GameObject buildingTutorial;
    void Start()
    {
        TutorialScript.dictOfTutorialsObjects["walk"] = walkTutorial;
        TutorialScript.dictOfTutorialsObjects["place"] = placeTutorial;
        TutorialScript.dictOfTutorialsObjects["change"] = flowerChangingTutorial;
        TutorialScript.dictOfTutorialsObjects["fight"] = fightingTutorial;
        TutorialScript.dictOfTutorialsObjects["build"] = buildingTutorial;

        TutorialScript.AddTutorialToTheQueue("walk");
        TutorialScript.AddTutorialToTheQueue("place");
        TutorialScript.AddTutorialToTheQueue("change");
    }

    // Update is called once per frame
    void Update()
    {
        TutorialScript.Update();
    }
}
