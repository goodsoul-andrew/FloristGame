using UnityEngine;

public class DummyEnemy : MonoBehaviour
{
    public Health health;
    public DamageDealer damageDealer;
    void Start()
    {
        health = GetComponent<Health>();
        damageDealer = GetComponent<DamageDealer>();
        health.OnDeath += DestroyMyself;
    }

    private void DestroyMyself()
    {
        Destroy(this.gameObject);
    }
}
