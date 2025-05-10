using UnityEngine;

public class Wall : Plant
{
    private Health hp;
    protected override void Awake()
    {
        base.Awake();

        hp = GetComponent<Health>();
        hp.OnDeath += DestroyMyself;
    }

    protected void DestroyMyself()
    {
        Destroy(this.gameObject);
    }
}
