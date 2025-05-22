using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthCount : MonoBehaviour
{
    private TMP_Text healthText;
    [SerializeField]private Health health;

    void Start()
    {
        if (health == null)
        {
            health = FindFirstObjectByType<Player>().GetComponent<Health>();
        }
        healthText = GetComponent<TMP_Text>();
        health.OnHealthChanged += UpdateHealthDisplay;
    }

    private void UpdateHealthDisplay()
    {
        healthText.text = $"HP: {health.HP} / {health.maxHP}";
    }
}
