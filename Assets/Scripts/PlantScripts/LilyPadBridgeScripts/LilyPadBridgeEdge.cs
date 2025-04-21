using UnityEngine;

public class LilyPadBridgeEdge : MonoBehaviour
{
    [SerializeField]private BoxCollider2D edgeCollider;
    private bool block;

    void Start()
    {
        edgeCollider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water") && ! block)
        {
            Debug.Log("edge is on water");
            edgeCollider.enabled = true;
        }
        /*else if (collision.CompareTag("LilyPad"))
        {
            edgeCollider.enabled = false;
            isAlwaysDisabled = true;
        }*/
    }

    public void Change(bool isEnabled)
    {
        if (! block)
        {
            edgeCollider.enabled = isEnabled;
            //Debug.Log($"edge is now {(edgeCollider.enabled ? "enabled" : "disabled")}");
        }
    }

    public void ChangeAndBlock(bool isEnabled)
    {
        if (! block)
        {
            edgeCollider.enabled = isEnabled;
            block = true;
            //Debug.Log($"edge is now {(edgeCollider.enabled ? "enabled" : "disabled")}");
        }
    }
}