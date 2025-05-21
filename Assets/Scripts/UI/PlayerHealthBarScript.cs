using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    [SerializeField]private Player player;
    private Health playerHP;

    void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }
        healthBar = GetComponent<Image>();
        playerHP = player.GetComponent<Health>();
        playerHP.OnHealthChanged += UpdateHealthBar;
    }

    
    private void UpdateHealthBar()
    {
        healthBar.fillAmount = playerHP.HP / playerHP.maxHP;
    }
}