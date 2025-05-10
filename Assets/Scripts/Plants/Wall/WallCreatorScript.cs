using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WallCreator : Plant
{
    [SerializeField] private int length;
    [SerializeField] private float distanceBetweenSegments;
    [SerializeField] private Plant wallSegment;
    private Vector2 playerPosition;
    protected override void Awake()
    {
        base.Awake();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        var direction = (playerPosition - (Vector2)transform.position).normalized;
        var wallDirection = new Vector2(-direction.y, direction.x);

        wallSegment.TryPlace((Vector2)transform.position);
        for(var i = 1; i < length; i++)
        {
            StartCoroutine(PlantAfterDelay(i/5f,(Vector2)transform.position + wallDirection * i * distanceBetweenSegments));
            StartCoroutine(PlantAfterDelay(i/5f,(Vector2)transform.position - wallDirection * i * distanceBetweenSegments));
        }

        DestroyAfterDelay(length/5f);
    }

    private IEnumerator PlantAfterDelay(float delay, Vector2 vector)
    {
        yield return new WaitForSeconds(delay);
        wallSegment.TryPlace(vector);
    }
}
