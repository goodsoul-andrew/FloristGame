using System.Collections;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float effectDuration = 0.1f;
    private Coroutine currentEffectCoroutine;
    private AudioSource audioSource;
    public AudioClip HurtSound;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnEnable()
    {
        Health healthComponent = GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.OnDamage += HandleDamage;
        }
    }
 
    private void OnDisable()
    {
        Health healthComponent = GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.OnDamage -= HandleDamage;
        }
    }

    private void HandleDamage()
    {
        if (currentEffectCoroutine != null)
        {
            StopCoroutine(currentEffectCoroutine);
        }
        currentEffectCoroutine = StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        audioSource.PlayOneShot(HurtSound);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(effectDuration);
        spriteRenderer.color = originalColor;
    }
}
