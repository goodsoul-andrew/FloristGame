using System;
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
        if (collision.gameObject.TryGetComponent<CollectibleFlower>(out var collectedFlower))
        {
            FindFirstObjectByType<TutorialManager>().FinishTutorial("take");
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
                default:
                    if (collectedFlower.PlantObject != null)
                    {
                        //Debug.Log($"collected {collectedFlower.PlantObject.tag}");
                        if (!FlowersManager.TryUpdateFlower(collectedFlower.PlantObject.tag, collectedFlower.amountOfFlowers))
                        {
                            var flower = new Flower(
                                collectedFlower.PlantObject,
                                collectedFlower.PlantObject.GetComponent<SpriteRenderer>().sprite,
                                collectedFlower.amountOfFlowers
                            );
                            FlowersManager.AddFlower(flower);
                        }
                    }
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
}
