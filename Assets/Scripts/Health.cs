using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHP;
    public bool IsImmortal;
    private float currentHP;
    public System.Action OnHealthChanged;
    public System.Action OnDeath;
    public System.Action OnDamage;
    public System.Action OnHeal;

    public float HP => currentHP;
    public bool IsDead => HP <= 0;

    protected virtual void Start()
    {
        currentHP = maxHP;
        OnHealthChanged?.Invoke();
    }

    public virtual void TakeDamage(float damage)
    {
        if (IsImmortal) return;
        currentHP -= damage;
        //Debug.Log($"Damaged {currentHP + damage} -> {currentHP}");
        if (currentHP <= 0)
        {
            currentHP = 0;
            OnDeath?.Invoke();
        }
        OnHealthChanged?.Invoke();
        OnDamage?.Invoke();
    }

    public virtual void Heal(float amount)
    {
        if (IsImmortal) return;
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        OnHealthChanged?.Invoke();
        OnHeal?.Invoke();
    }

    public virtual void Kill()
    {
        currentHP = 0;
        OnDeath?.Invoke();
    }
}
