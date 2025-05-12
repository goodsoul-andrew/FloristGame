using UnityEngine;

public class QuestFlowerbed : MonoBehaviour
{
    [SerializeField] GameObject questPlant;
    public System.Action OnComplete;
    private bool isActive;

    void Start()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log($"{collision.tag} is planted");
        if (! isActive) return;
        if (questPlant.CompareTag(collision.tag))
        {
            OnComplete?.Invoke();
            Debug.Log("Right plant is planted");
        }
    }
}
