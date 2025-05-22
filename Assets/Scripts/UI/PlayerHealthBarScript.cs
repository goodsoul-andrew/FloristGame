using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    [SerializeField]private Health health;

    void Start()
    {
        if (health == null)
        {
            health = FindFirstObjectByType<Player>().GetComponent<Health>();
        }
        healthBar = GetComponent<Image>();
        health.OnHealthChanged += UpdateHealthBar;
    }

    
    private void UpdateHealthBar()
    {
        healthBar.fillAmount = health.HP / health.maxHP;
    }
}