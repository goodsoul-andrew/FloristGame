using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Island : MonoBehaviour
{
    private Player player;
    private int swampLayer;
    private int playerLayer;
    private Swamp swamp;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        swampLayer = LayerMask.NameToLayer("Swamp");
        playerLayer = LayerMask.NameToLayer("Player");
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
        if (Utils.StandsOn(collision.gameObject, "Swamp"))
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
}
