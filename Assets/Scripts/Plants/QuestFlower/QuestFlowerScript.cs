using System.Linq;
using UnityEngine;

public class QuestFlower: Plant
{
    public override bool IsAreaAvailable(Vector2 position)
    {
        var obstacles = new string[] {"Obstacle", "PlayerMinion", "PlantTrap"};
        Collider2D[] colliders = GetCollidersInArea(position);
        var found = false;
        foreach (var collider in colliders)
        {
            if (obstacles.Contains(collider.tag))
            {
                return false;
            }
            if (collider.CompareTag("QuestFlowerbed"))
            {
                var flowerbed = collider.GetComponent<QuestFlowerbed>();
                found = flowerbed.TryActivate(this);
            }
        }
        return found;
    }
}