using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public bool isSample;
    public float destroyDelay = 10f;

    protected virtual void Start()
    {
        if (! isSample) StartCoroutine(RemoveAfterDelay(destroyDelay));
    }

    private IEnumerator RemoveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    public void DestroyAfterDelay(float delay)
    {
        if (! isSample) StartCoroutine(RemoveAfterDelay(delay));
    }

    protected virtual Collider2D[] GetCollidersInArea(Vector2 position)
    {
        return Physics2D.OverlapCircleAll(position, 0.5f);
    }

    public virtual bool IsAreaAvailable(Vector2 position)
    {
        Collider2D[] colliders = GetCollidersInArea(position);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("PlayerMinion") || collider.gameObject.CompareTag("Obstacle"))
            {
                return false;
            }
        }
        return true;
    }
}
