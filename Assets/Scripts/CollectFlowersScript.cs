using System.Collections.Generic;
using UnityEngine;

public class CollectFlowersScript : MonoBehaviour
{
    private readonly List<string> flowerTags = new() { "flower1", "flower2", "flower3" };
    public FlowersManagerScript FlowersManager;

    private void Start()
    {
        FlowersManager = FindFirstObjectByType<FlowersManagerScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (flowerTags.Contains(collision.gameObject.tag))
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
