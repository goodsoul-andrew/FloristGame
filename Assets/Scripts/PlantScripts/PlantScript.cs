using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public bool IsInfinite;
    public float destroyDelay = 10f;
    protected string[] obstacles = new string[] {"Obstacle", "PlayerMinion"};

    protected virtual void Awake()
    {
        if (! IsInfinite) StartCoroutine(RemoveAfterDelay(destroyDelay));
    }

    private IEnumerator RemoveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    public void DestroyAfterDelay(float delay)
    {
        if (! IsInfinite) StartCoroutine(RemoveAfterDelay(delay));
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
            if (obstacles.Contains(collider.tag))
            {
                return false;
            }
        }
        return true;
    }

    public virtual GameObject Place(Vector2 position)
    {
        return Instantiate(this.gameObject, position, Quaternion.Euler(0, 0, 0));
    }

    public virtual bool TryPlace(Vector2 position)
    {
        if (IsAreaAvailable(position))
        {
            Instantiate(this.gameObject, position, Quaternion.Euler(0, 0, 0));
            return true;
        }
        return false;
    }
}
