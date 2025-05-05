using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Island : MonoBehaviour
{
    private Player player;
    private Swamp swamp;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GetSwamp();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"{Utils.StandsOnGround(collision.gameObject)}");
        if (Utils.StandsOn(collision.gameObject, "Swamp"))
        {
            swamp.Disable(collision.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (IsTargetOnOtherIsland(collision))
        {
            swamp.Disable(collision.gameObject);
        }
        else if (Utils.StandsOn(collision.gameObject, "Swamp"))
        {
            swamp.Enable(collision.gameObject);
        }
    }

    private void GetSwamp()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.01f);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<Swamp>(out var newSwamp))
            {
                swamp = newSwamp;
                return;
            }
        }
    }

    private bool IsTargetOnOtherIsland(Collider2D target)
    {
        var position = target.transform.position;
        var radius = 0.5f;
        if (target.TryGetComponent<Collider2D>(out var targetCollider))
        {
            position = (Vector2)targetCollider.transform.position + targetCollider.offset;
            if (targetCollider is CircleCollider2D circleCollider)
            {
                radius = circleCollider.radius;
            }
        }
        var colliders = Physics2D.OverlapCircleAll(position, radius);
        foreach (var collider in colliders)
        {
            if (collider != null && Utils.ground.Contains(collider.tag) && collider.transform.position != transform.position)
            {
                //Debug.Log($"Other island is {collider.tag} on {collider.transform.position}, this is {tag} on {transform.position}");
                return true;
            }
        }
        return false;
    }
}
