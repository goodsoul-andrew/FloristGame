using System.Collections.Generic;
using UnityEngine;

public class CollectFlowersScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public FlowersManager FlowersManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FlowersManager = FindFirstObjectByType<FlowersManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<CollectedFlower>(out var collectedFlower))
        {
            switch (collision.gameObject.tag)
            {
                case "LilyPadCollected":
                    FlowersManager.ChangeNumberOfFlowers(1, collectedFlower.amountOfFlowers);
                    break;
                case "BombFlowerCollected":
                    FlowersManager.ChangeNumberOfFlowers(2, collectedFlower.amountOfFlowers);
                    break;
                case "HealFlowerCollected":
                    FlowersManager.ChangeNumberOfFlowers(3, collectedFlower.amountOfFlowers);
                    break;
                case "WallFlowerCollected":
                    FlowersManager.ChangeNumberOfFlowers(4, collectedFlower.amountOfFlowers);
                    break;
                case "QuestFlower1":
                    //var flower = new Flower("QuestFlower1", collision.gameObject, collision.gameObject.GetComponent<SpriteRenderer>().sprite, 1);
                    //FlowersManager.AddFlower(flower);
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
}
