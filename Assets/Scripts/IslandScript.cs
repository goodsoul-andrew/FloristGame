using System.Linq;
using UnityEngine;

public class Island : MonoBehaviour
{
    private Player player;
    private int swampLayer;
    private int playerLayer;
    private BoxCollider2D[] swampColliders;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        swampLayer = LayerMask.NameToLayer("Swamp");
        playerLayer = LayerMask.NameToLayer("Player");
        var allSwamp = GameObject.FindGameObjectsWithTag("Swamp");
        swampColliders = allSwamp.Select(el => el.GetComponent<BoxCollider2D>()).ToArray();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var swampCollider in swampColliders)
        {
            Physics2D.IgnoreCollision(collision, swampCollider, true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var swampCollider in swampColliders)
        {
            Physics2D.IgnoreCollision(collision, swampCollider, false);
        }
    }
}
