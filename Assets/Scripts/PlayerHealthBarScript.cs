using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    [SerializeField]private GameObject player;
    private Health playerHP;

    void Start()
    {
        healthBar = GetComponent<Image>();
        playerHP = player.GetComponent<Health>();
        playerHP.OnHealthChanged += UpdateHealthBar;
    }

    
    private void UpdateHealthBar()
    {
        healthBar.fillAmount = playerHP.HP / playerHP.maxHP;
    }
}