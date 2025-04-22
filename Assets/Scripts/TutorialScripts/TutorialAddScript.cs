using UnityEngine;

public class TutorialAdderScript : MonoBehaviour
{
    [SerializeField] private string tutorialName;
    void OnTriggerEnter2D(Collider2D otherCollider) 
    {
        Debug.Log(otherCollider.gameObject.tag);
        if (otherCollider.gameObject.tag == "Player") 
        {
            TutorialScript.AddTutorialToTheQueue(tutorialName);
        }
    }
}
