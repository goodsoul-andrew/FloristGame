using Unity.VisualScripting;
using UnityEngine;

public class AcidBall : MonoBehaviour
{
    public Vector2 Destination;
    public float MoveSpeed = 5f;
    [SerializeField]private GameObject acidPuddle;

    public Vector2 startPosition;

    void Start()
    {
        //startPosition = transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, Destination) < 0.1f)
        {
            Instantiate(acidPuddle, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(this.gameObject);
            return;
        }
        Vector2 direction = (Destination - (Vector2)transform.position).normalized;
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }
}