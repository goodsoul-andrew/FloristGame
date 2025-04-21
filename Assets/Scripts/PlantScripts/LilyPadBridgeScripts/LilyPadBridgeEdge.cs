using UnityEngine;

public class LilyPadBridgeEdge : MonoBehaviour
{
    [SerializeField]private BoxCollider2D edgeCollider;

    void Start()
    {
        
    }

    public void ChangeEdge(bool isEnabled)
    {
        edgeCollider.enabled = isEnabled;
        Debug.Log($"edge is now {(edgeCollider.enabled ? "enabled" : "disabled")}");
    }
}