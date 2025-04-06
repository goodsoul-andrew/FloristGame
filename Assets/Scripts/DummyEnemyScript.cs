using UnityEngine;

public class DummyEnemy : MonoBehaviour
{
    public Health health;
    void Start()
    {
        health = GetComponent<Health>();
    }

    void Update()
    {
        if (health.isDead)
        {
            Destroy(this.gameObject);
        }
        this.transform.position += new Vector3(0, 0.01f, 0);
    }
}
