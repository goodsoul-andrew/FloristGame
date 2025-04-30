using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthCount : MonoBehaviour
{
    private TMP_Text healthText;
    [SerializeField]private GameObject player;
    private Health playerHP;

    void Start()
    {
        healthText = GetComponent<TMP_Text>();
        playerHP = player.GetComponent<Health>();
        playerHP.OnHealthChanged += UpdateHealthDisplay;
    }

    private void UpdateHealthDisplay()
    {
        healthText.text = $"HP: {playerHP.HP} / {playerHP.maxHP}";
    }
}
