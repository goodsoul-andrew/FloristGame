using UnityEngine;

public class LilyPadEdge : MonoBehaviour
{
    [SerializeField]private BoxCollider2D edgeCollider;
    [SerializeField]private BoxCollider2D triggerCollider;
    private bool isAlwaysDisabled = false;

    void Start()
    {
        edgeCollider = GetComponent<BoxCollider2D>();
        edgeCollider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water") && ! isAlwaysDisabled)
        {
            //Debug.Log("edge is on water");
            edgeCollider.enabled = true;
        }
        else if (collision.CompareTag("LilyPad"))
        {
            edgeCollider.enabled = false;
            isAlwaysDisabled = true;
        }
    }

    public void ChangeEdge(bool isEnabled)
    {
        edgeCollider.enabled = isEnabled;
    }
}