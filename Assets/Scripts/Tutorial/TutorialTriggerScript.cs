using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private string tutorialName;
    void OnTriggerEnter2D(Collider2D otherCollider) 
    {
        //Debug.Log(otherCollider.gameObject.tag);
        if (otherCollider.gameObject.tag == "Player") 
        {
            FindFirstObjectByType<TutorialManager>().AddTutorialToTheQueue(tutorialName);
        }
    }
}
