using UnityEngine;

public class AcidBall : MonoBehaviour
{
    public Vector2 Destination;
    public float MoveSpeed = 5f;

    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, Destination) < 0.1f)
        {
            //Debug.Log("Цель достигнута!");
            return;
        }
        Vector2 direction = (Destination - (Vector2)transform.position).normalized;
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }
}