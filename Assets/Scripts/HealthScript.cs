using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHP;
    private float currentHP;
    public System.Action OnHealthChanged;
    public System.Action OnDeath;
    public float HP => currentHP;
    public bool isDead => HP <= 0;

    void Start()
    {
        currentHP = maxHP;
        OnHealthChanged?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        Debug.Log($"Taken {damage} damage. Current HP is {HP}");
        if (currentHP <= 0)
        {
            currentHP = 0;
            OnDeath?.Invoke();
        }
        OnHealthChanged?.Invoke();
    }

    public void Heal(float amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        OnHealthChanged?.Invoke();
    }
}
