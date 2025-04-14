using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHP;
    private float currentHP;
    public System.Action OnHealthChanged;
    public System.Action OnDeath;

    public float HP => currentHP;
    public bool IsDead => HP <= 0;

    protected virtual void Start()
    {
        currentHP = maxHP;
        OnHealthChanged?.Invoke();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            OnDeath?.Invoke();
        }
        OnHealthChanged?.Invoke();
    }

    public virtual void Heal(float amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        OnHealthChanged?.Invoke();
    }
}
