using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHP = 100;
    private float currentHP;
    public float HP => currentHP;
    public bool isDead => HP <= 0;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        Debug.Log($"Taken {damage} damage. Current HP is {HP}");
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    private void Die()
    {
        Debug.Log("Died");
    }
}
