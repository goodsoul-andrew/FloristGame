using System.Collections;
using UnityEngine;

public class HurtEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Health HP;
    [SerializeField] private AudioSource audioSource;
    private Color originalColor;
    public float effectDuration = 0.1f;
    public float shakeStrength = 0.1f;
    private Coroutine currentEffectCoroutine;
    public AudioClip HurtSound;
    private void Awake()
    {
        if (HP == null)
        {
            HP = GetComponent<Health>();
        }
        if (HP != null)
        {
            HP.OnDamage += HandleDamage;
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        originalColor = spriteRenderer.color;
    }

    private void HandleDamage()
    {
        if (currentEffectCoroutine != null)
        {
            StopCoroutine(currentEffectCoroutine);
        }
        StartCoroutine(FlashRed());
        StartCoroutine(Shake());
    }

    private IEnumerator FlashRed()
    {
        audioSource.PlayOneShot(HurtSound);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(effectDuration);
        spriteRenderer.color = originalColor;
    }
    
    private IEnumerator Shake()
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < effectDuration)
        {
            float xOffset = Random.Range(-shakeStrength, shakeStrength);
            float yOffset = Random.Range(-shakeStrength, shakeStrength);
            transform.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
