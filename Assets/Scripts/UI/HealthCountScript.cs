using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthCount : MonoBehaviour
{
    private TMP_Text healthText;
    [SerializeField]private Player player;
    private Health playerHP;

    void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }
        healthText = GetComponent<TMP_Text>();
        playerHP = player.GetComponent<Health>();
        playerHP.OnHealthChanged += UpdateHealthDisplay;
    }

    private void UpdateHealthDisplay()
    {
        healthText.text = $"HP: {playerHP.HP} / {playerHP.maxHP}";
    }
}
