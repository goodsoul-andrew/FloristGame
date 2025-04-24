using System.Collections.Generic;
using UnityEngine;

public class CollectFlowersScript : MonoBehaviour
{
    private readonly List<string> flowerTags = new() { "flower1", "flower2", "flower3" };
    private Rigidbody2D rb;
    public FlowersManager FlowersManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FlowersManager = FindFirstObjectByType<FlowersManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var dist = ((Vector2)rb.transform.position - (Vector2)collision.gameObject.transform.position).magnitude;
        if (dist <= 2 && flowerTags.Contains(collision.gameObject.tag))
        {
            Destroy(collision.gameObject);
            switch (collision.gameObject.tag)
            {
                case "flower1":
                    FlowersManager.ChangeNumberOfFlowers(1);
                    break;
                case "flower2":
                    FlowersManager.ChangeNumberOfFlowers(2);
                    break;
                case "flower3":
                    FlowersManager.ChangeNumberOfFlowers(3);
                    break;
            }
        }
    }
}
