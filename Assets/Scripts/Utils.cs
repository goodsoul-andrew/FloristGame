using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Utils
{
    private static string[] ground = new string[] { "Ground", "LilyPad", "LilyPadBridge" };
    private static string[] notWalkable = new string[] { "Swamp" };

    public static bool StandsOnGround(GameObject target) => StandsOn(target, ground);

    public static bool StandsOn (GameObject target, IEnumerable<string> tags)
    {
        var position = target.transform.position;
        if (target.TryGetComponent<Collider2D>(out var targetCollider))
        {
            position = (Vector2)targetCollider.transform.position + targetCollider.offset;
        }
        var colliders = Physics2D.OverlapCircleAll(position, 0.5f);
        foreach (var collider in colliders)
        {
            if (collider != null && tags.Contains(collider.tag))
            {
                //Debug.Log($"{collider.transform.position}");
                return true;
            }
        }
        return false;
    }

    public static bool StandsOn(GameObject target, string tag)
    {
        var position = target.transform.position;
        if (target.TryGetComponent<Collider2D>(out var targetCollider))
        {
            position = (Vector2)targetCollider.transform.position + targetCollider.offset;
        }
        var colliders = Physics2D.OverlapCircleAll(position, 0.5f);
        foreach (var collider in colliders)
        {
            //Debug.Log($"{collider.tag} {collider}");
            if (collider != null && collider.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }
}