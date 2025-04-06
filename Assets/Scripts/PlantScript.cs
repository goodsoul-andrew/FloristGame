using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public bool isSample;
    public float destroyDelay = 10f;

    void Start()
    {
        if (! isSample) StartCoroutine(RemoveTrapAfterDelay(destroyDelay));
    }

    private IEnumerator RemoveTrapAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
