using UnityEngine;

public class TutorialTriggerScript : MonoBehaviour
{
    [SerializeField] private string tutorialName;
    void OnTriggerEnter2D(Collider2D otherCollider) 
    {
        Debug.Log(otherCollider.gameObject.tag);
        if (otherCollider.gameObject.tag == "Player") 
        {
            FindFirstObjectByType<TutorialManagerScript>().AddTutorialToTheQueue(tutorialName);
        }
    }
}
