using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Plant : MonoBehaviour
{
    public bool IsInfinite;
    public float destroyDelay = 10f;
    

    protected virtual void Awake()
    {
        if (!IsInfinite) StartCoroutine(RemoveAfterDelay(destroyDelay));
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
        var obstacles = new string[] {"Obstacle", "PlayerMinion"};
        Collider2D[] colliders = GetCollidersInArea(position);
        //Debug.Log($"{string.Join(", ", obstacles)}");
        foreach (var collider in colliders)
        {
            //Debug.Log($"{collider.tag} {obstacles.Contains(collider.tag)}");
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
