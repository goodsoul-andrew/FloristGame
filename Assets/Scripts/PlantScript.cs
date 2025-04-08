using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public bool isSample;
    public float destroyDelay = 10f;

    void Start()
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
}
